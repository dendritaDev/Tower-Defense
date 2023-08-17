using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TradeCupcakeTowerSell : TradeCupcakeTower
{
    [Tooltip("Variable para identificar el prefab de la torreta que debemos instanciar con este botón")]
    public GameObject cupcakeTowerPrefab;

   
    public override void OnPointerClick(PointerEventData eventData)
    {
        //si no hay torreta seleccionada no hacemos nada
        if (currentActiveTower == null)
        {
            return;
        }
        //Si llego aqui es que tengo una torreta seleccionada

        //Consulto el precio de venta de la torreta
        int sellingPrice = currentActiveTower.sellCost;

        //Sumamos ese dinero al medidor de azucar del usuario
        sugarMeter.AddSugar(sellingPrice);

        //Destruimos el cupcake actual ya que acabamos de venderlo
        currentActiveTower.DestroyTower();


    }
}
