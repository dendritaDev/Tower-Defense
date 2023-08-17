using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Tooltip("Vida máxima que tendrá el jugador")]
    public int maxHealth = 100;

    //Referencia a la healt bar filling de la UI que hemos hecho
    private Image fillingImage;
    
    //Vida actual del jugador
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        fillingImage = GetComponent<Image>();

        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Aplica daño al jugador y devuelve el estado de Game Over
    /// </summary>
    /// <param name="damage">daño que se le aplica al jugador</param>
    /// <returns>si se tiene que triggear el game over si el jugador esta muerto</returns>
    public bool ApplyDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth > 0)
        {
            UpdateHealthBar();
            return false;
        }

        currentHealth = 0;
        UpdateHealthBar();
        return true;
    }


    void UpdateHealthBar()
    {
        //Calcular el % de la vida que me queda

        float percentage = currentHealth * 1.0f / maxHealth;
        //Modificamos el filling image del relleno de la imagen de vida
        fillingImage.fillAmount = percentage;
    }
}
