using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WayPoint firstWayPoint;

    [Header("Pantallas de GameOver/Win")]
    public GameObject winningScreen;
    public GameObject losingScreen;

    //referencia a la barra de vida del castillo de azucar
    private HealthBar playerHealth;

    //Variable para saber cuantos pandas nos quedan por derrotar y ganar el nivel
    private int numberOfPandasToDefeat;

    private Transform spawnPoint;

    //bool para saber si el raton está en una zona donde poder poner torretas
    private bool _isPointerOnAllowedArea = false;

    [Header("Panda y Oleadas")]
    public GameObject pandaPrefab;
    public int numberOfWaves;
    public int numberOfPandasPerWave;

    //SugarMeter para ñadirle puntos cuando matemos un panda.
    private static SugarMeter sugarMeter;

    private void Start()
    {

        if (sugarMeter == null)
        {
            sugarMeter = FindObjectOfType<SugarMeter>();
        }


        //Referencia barra vida jguador
        playerHealth = FindObjectOfType<HealthBar>();

        //Recuperamos el objeto Spawning Point
        spawnPoint = GameObject.Find("Spawning Point").transform;

        StartCoroutine(WaveSpawner());

    }


    //Getter de _isPointerOnAllowedArea
    public bool isPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }

    //Si el raton esta dentro de una zona donde poner torretas es true
    private void OnMouseEnter()
    {
        _isPointerOnAllowedArea = true;
    }
    
    //Si el raton esta dentro de una zona donde poner torretas es false
    private void OnMouseExit()
    {
        _isPointerOnAllowedArea = false;
    }

    private void Update()
    {
        //God Mode
        //Conseguir mucho dinero para poder probarlo todo
        if(Input.GetKeyDown(KeyCode.F1))
        {
            sugarMeter.AddSugar(999950);
        }

        //"Ganar Instantaneo"
        if (Input.GetKeyDown(KeyCode.F2))
        {
            
            if(winningScreen.activeInHierarchy)
            {
                winningScreen.SetActive(false);
            }
            else
            {
                winningScreen.SetActive(true);
            }
            
        }

        //"Perder Instantaneo"
        if (Input.GetKeyDown(KeyCode.F3))
        {
            
            if (losingScreen.activeInHierarchy)
            {
                losingScreen.SetActive(false);
            }
            else
            {
                losingScreen.SetActive(true);
            }
            
        }
    }



    // -- GAME OVER --

    /// <summary>
    /// Se llamará cuando se cumplan las condiciones de gameOver o bienporque el jugador gane derrotando todas las oleadas o porque el castillo de azucar se queda sin vida
    /// </summary>
    /// <param name="playerHasWon"></param>
    private void GameOver(bool playerHasWon)
    { 
        if(playerHasWon)
        {
            winningScreen.SetActive(true);
        }
        else
        {
            losingScreen.SetActive(true);
        }

        //Congelamos el tiempo para que se pare el videojuego detrás de las escenas
        Time.timeScale = 0;
    }

    //Funcion que llamamos cada vez que matamos un Panda
    public void OneMorePandaInHell()
    {
        numberOfPandasToDefeat--;
        sugarMeter.AddSugar(5);
    }


    /// <summary>
    /// Funcion que daña la vida del jugador cuando el panda llega a la tarta. Monitoriza además si todavia queda vida, y si se nos agota, llama al game over con el parámetro playerHasWon a false
    /// </summary>
    /// <param name="damage"></param>
    public void BiteTheCake(int damage)
    {
        
        bool isCakeAllEaten = playerHealth.ApplyDamage(damage);

        if(isCakeAllEaten)
        {
            GameOver(false);
        }

        OneMorePandaInHell();
    }

    /// <summary>
    /// Corutina que creará oleadas de enemigos
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveSpawner()
    {
        for (int i=0; i<numberOfPandasPerWave; i++)
        {
            yield return PandaSpawner();
            numberOfPandasPerWave +=3;
        }

        //Cuando hayemos derrotado todas las waves, habremos ganado
        GameOver(false);
    }

    /// <summary>
    /// Corutina que crea los pandas de una oleada simple y espera hasta que no quede ninguno
    /// </summary>
    /// <returns></returns>
    private IEnumerator PandaSpawner()
    {
        numberOfPandasToDefeat = numberOfPandasPerWave;

        for(int i=0; i<numberOfPandasPerWave; i++)
        {
            Instantiate(pandaPrefab, spawnPoint.position, Quaternion.identity);

            //ponemos a descansar la corutina unos segundos segun el numero de pandas que se tengan que instanciar
            float ratio = (i * 1.0f) / (numberOfPandasPerWave - 1);

            float timeToWait = Mathf.Lerp(5f, 7f, ratio) + Random.Range(0f, 2f);


            yield return new WaitForSeconds(timeToWait);
        }

        //cuando todos hayan sido spawneados esperar a que todos hayan sido derrotados o el player haya muerto
        yield return new WaitUntil(() => numberOfPandasToDefeat <= 0);
    }


}
