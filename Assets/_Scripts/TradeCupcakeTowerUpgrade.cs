using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowerUpgrade : TradeCupcakeTower
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

        //Comprobamos que se pueda subir de nivel la torreta y que dispongamos de dinero

        int upgradePrice = currentActiveTower.upgradeCost;

        if(currentActiveTower.isUpgradable && upgradePrice < sugarMeter.getSugarScore())
        {
            //Mejoramos torre y quitamos el coste del dinero que tengamos
            sugarMeter.AddSugar(-upgradePrice);
            currentActiveTower.UpgradeTower();
        }    
    }
}
