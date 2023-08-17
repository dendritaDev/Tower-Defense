using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Cuanto daño recibirá el enemigo")]
    public float damage;
    [Tooltip("Velocidad a la que se mueve el proyectil")]
    public float speed = 1f;
    [Tooltip("Direccion en la que apunta el proyectil")]
    public Vector3 direction;
    [Tooltip("Tiempo de vida del proyectil en segundos")]
    public float lifeDuration = 10f;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //Normalizar la dirección del proyectil -> magnitud = 1

        direction = direction.normalized;

        //rotamos el proyectil
        float angleRadians = Mathf.Atan2(-direction.y, direction.x);
        float angleDegree = angleRadians * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleDegree, Vector3.forward);

        Destroy(gameObject, lifeDuration);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //x = v(cantidad:speed y direccion:direction) * t
        //transform.position += speed * direction * Time.fixedDeltaTime;

        _rigidbody.MovePosition(transform.position + (speed * direction) * Time.fixedDeltaTime);


    }
}
