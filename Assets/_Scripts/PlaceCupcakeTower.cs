using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCupcakeTower : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        float z = 7.0f; //Como la camara esta en z = -10, la z de la torres son en -3, asi que le sumaremos estos 7

        //La posicion de la torre sera donde este el raton
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));

        //Ahora tenemos que comprobar si donde haga click hay un sitio para poner una torre
        if(Input.GetMouseButtonDown(0) && gameManager.isPointerOnAllowedArea())
        {
            GetComponent<Tower>().enabled = true;
            //Le añadimos un collider encima para que ya no podamos volver a seleccionar este sitio pra poner otra torreta
            gameObject.AddComponent<BoxCollider2D>();

            Destroy(this); //Destruimos este script pq no hace falta
        }

    }
}
