using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Variables de torre")]
    [Tooltip("Distancia máxima a la que puede disparar la torreta")]
    public float rangeRadius;
    [Tooltip("Tiempo de recarga antes de poder disparar otra vez")]
    public float reloadTime;
    [Tooltip("Prefab del proyectil que va a disparar mi torreta")]
    public GameObject projectilePrefab;
    [Tooltip("Tiempo que ha pasado desde la última vez que la torre disparó")]
    private float timeSinceLastShot;

    [Header("Niveles de torreta")]
    [Tooltip("Nivel actual de la torreta")]
    private int upgradeLevel;

    public int UpgradeLevel
    {
        get { return upgradeLevel; }

        set { upgradeLevel = value; }
    }

    [Tooltip("Sprites de los diferentes niveles de la torreta")]
    public Sprite[] upgradeSprites;
    [Tooltip("Variable para saber si una torreta se puede seguir mejorando")]
    public bool isUpgradable = true;

    [Header("Niveles de torreta")]
    [Tooltip("Nivel actual de la torreta")]
    private int _upgradeLevel;

    [Header("Economia de la torreta")]
    [Tooltip("Precio de compra de la torreta")]
    public int initialCost;

    [Tooltip("Precio de venta de la torreta")]
    public int sellCost;

    [Tooltip("Precio de mejora de la torreta")]
    public int upgradeCost;

    [Tooltip("Precio de incremento de venta de la torreta")]
    public int sellIncrementCost;

    [Tooltip("Precio de incremento de mejora de la torreta")]
    public int upgradeIncrementCost;

    private SpriteRenderer _spriteRenderer;

    [Tooltip("Game objects de los projectiles")]
    public GameObject[] projectilePrefabs;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastShot >= reloadTime)
        {


            //Esto mete en un array todos los colliders que entren en un circuclo centrado en mi posicion y con radio rangeRadius
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeRadius);

            if (hitColliders.Length != 0)
            {

                float minDistance = int.MaxValue;
                int index = -1;

                //comprobamos si de todos los colliders que hemos chocado hay algún enemigo
                for (int i = 0; i < hitColliders.Length; i++)
                {

                    if (hitColliders[i].tag == "Enemy")
                    {
                        float distance = Vector2.Distance(hitColliders[i].transform.position, this.transform.position);

                        //Nos quedamos con el enemigo más cercano
                        if (distance < minDistance)
                        {
                            index = i;
                            minDistance = distance;
                        }
                    }

                }

                if (index < 0)
                {
                    return;
                }
                //De haber un enemigo al que disparar isntanciamos un proyectil y le damos la dirección que calculamos con la posicion de torre y enemigo
                Transform target = hitColliders[index].transform;
                Vector2 direction = (target.position - this.transform.position).normalized;

                GameObject projectile = GameObject.Instantiate(projectilePrefab, this.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Projectile>().direction = direction;


                timeSinceLastShot = 0;
            }
        }

        timeSinceLastShot += Time.deltaTime;
    }

    public void UpgradeTower()
    {
        if (!isUpgradable)
        {
            return;
        }

        this.UpgradeLevel++;

        if (this.upgradeLevel == upgradeSprites.Length)
        {
            isUpgradable = false;
        }

        rangeRadius += 1f;
        reloadTime -= 0.5f;

        sellCost += sellIncrementCost;
        upgradeCost += upgradeIncrementCost;


        _spriteRenderer.sprite = upgradeSprites[upgradeLevel];
        projectilePrefab = projectilePrefabs[upgradeLevel];
    }

    //Cuando haga el raton click en una torreta
    private void OnMouseDown()
    {
        //cuando haga click la torreta se convierte en la currentTower
        TradeCupcakeTower.setActiveTower(this);
        
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
    }
}
