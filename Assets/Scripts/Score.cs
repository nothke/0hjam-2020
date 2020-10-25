using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    void Start()
    {

    }

    public TMPro.TextMeshProUGUI text;

    float surviveTime = 60;
    bool ended;

    void Update()
    {
        if (!ended)
        {
            text.text = "Infected: " + GuyMovement.totalInfected + " Total: " + GuyMovement.totalPpl;
            text.text += "\nTime until 2021: " + Mathf.FloorToInt(surviveTime);

            surviveTime -= Time.deltaTime;
        }

        if (surviveTime < 0 && !ended)
        {
            End();
            ended = true;
        }
    }

    void End()
    {
        Time.timeScale = 0.01f;
        text.text = GuyMovement.totalInfected < GuyMovement.totalPpl ?
            "SUCCESS! Not all of your customers got infected! You survived 2020!" :
            "GAMEOVER! Everyone got infected! You didn't survive 2020";
    }
}
