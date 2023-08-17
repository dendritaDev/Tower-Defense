using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SugarMeter : MonoBehaviour
{
    private int sugarScore = 50;

    private TextMeshProUGUI sugarMeter;
    // Start is called before the first frame update
    void Start()
    {
        sugarMeter = GetComponent<TextMeshProUGUI>();

        UpdateSugarMeter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSugar(int sugar)
    {
        sugarScore += sugar;

        if (sugarScore < 0) sugarScore = 0;

        UpdateSugarMeter();
    }

    public int getSugarScore()
    {
        return sugarScore;
    }

    void UpdateSugarMeter()
    {
        sugarMeter.text = getSugarScore().ToString();
    }

}

