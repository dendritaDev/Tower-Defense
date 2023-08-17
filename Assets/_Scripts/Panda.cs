using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour
{
    [Tooltip("Vida del panda")]
    public float health;
    [Tooltip("Velocidad del panda")]
    public float speed;
    [Tooltip("Daño que hacemos a la tarta cuando el panda llega a ella")]
    public int cakeEatenPerBite;

    private Animator _animator;

    private Rigidbody2D _rigidbody;

    //esto es asignarle un numero a cada trigger, pq identificarlos con el numero es mucho más rapido
    //que hacerlo con strings, esto se hace para mejorar performance el hash seria el numero de identificador
    private int animatorDieTriggerHash = Animator.StringToHash("DieTrigger");
    private int animatorEatTriggerHash = Animator.StringToHash("EatTrigger");
    private int animatorHitTriggerHash = Animator.StringToHash("HitTrigger");

    //Variable compartida por todos los pandas con la información necesaria de waypoints
    private static GameManager gameManager;

    //waypoint actual al que se dirige el panda
    private WayPoint currentWayPoint;

    //umbral a partir del cual consideramos que ya hemos alcanzado el waypoint
    private const float wayPointThreshold = 0.001f;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        currentWayPoint = gameManager.firstWayPoint;

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentWayPoint == null)
        {
            if(!isDead)
            {
                Eat();
                isDead = true;
            }

            return;
        }

        float distance = Vector2.Distance(this.transform.position, currentWayPoint.GetPosition());

        if (distance <= wayPointThreshold)
        {
            currentWayPoint = currentWayPoint.GetNextWayPoint();
        }
        else
        {
            MoveTowards(currentWayPoint.GetPosition());
        }
    }

    private void MoveTowards(Vector3 destination)
    {
        float step = speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, destination, step));

    }

    private void Hit(float damage)
    {
        if(!isDead)
        {
            health -= damage;

            if (health > 0)
            {
                _animator.SetTrigger(animatorHitTriggerHash);
            }
            else
            {
                _animator.SetTrigger(animatorDieTriggerHash);
                gameManager.OneMorePandaInHell();
                isDead = true;
                speed = -1;
                Invoke("DestroyObject", 0.8f); // espera 1 segundo antes de destruir el objeto
            }
        }

    }

    private void Eat()
    {
        _animator.SetTrigger(animatorEatTriggerHash);
        gameManager.BiteTheCake(cakeEatenPerBite);
        Invoke("DestroyObject", 2f); // espera 1 segundo antes de destruir el objeto
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Projectile"))
        {
            Hit(collision.GetComponent<Projectile>().damage);
        }
    }
}
