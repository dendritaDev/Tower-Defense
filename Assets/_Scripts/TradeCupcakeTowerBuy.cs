using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowerBuy : TradeCupcakeTower
{
    [Tooltip("Variable para identificar el prefab de la torreta que debemos instanciar con este botón")]
    public GameObject cupcakeTowerPrefab;


    public override void OnPointerClick(PointerEventData eventData)
    {
        //Cogemos el precio de lo que vale la torreta que vamos a comprar
        int price = cupcakeTowerPrefab.GetComponent<Tower>().initialCost;

        //Si tenemos suficiente dinero la compramos y la instanciamos 
        if(price <= sugarMeter.getSugarScore())
        {
            sugarMeter.AddSugar(-price);

            GameObject newTower = Instantiate(cupcakeTowerPrefab);

            currentActiveTower = newTower.GetComponent<Tower>();
        }
    }
}

