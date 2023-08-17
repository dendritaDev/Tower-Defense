using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //Para detectar interacción con la UI de la pantalla

public abstract class TradeCupcakeTower : MonoBehaviour, IPointerClickHandler
{
    
    protected static SugarMeter sugarMeter;//moneda del juego
    protected static Tower currentActiveTower;
    void Start()
    {
        if(sugarMeter == null)
        {
            sugarMeter = FindObjectOfType<SugarMeter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setActiveTower(Tower newCupcakeTower)
    {
        currentActiveTower = newCupcakeTower;
    }

    //funcion que se tiene que implementar en las subclases y cada una tendra su implementación
    public abstract void OnPointerClick(PointerEventData eventData);


}
