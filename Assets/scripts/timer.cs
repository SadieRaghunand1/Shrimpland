using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float time;
    public float elapTime;
    public GameObject lose;
    public int time1 = 0;
    public int time2 = 0;


    public AppleBobbingManager rollerCoasterManager;

    // Update is called once per frame
    void Update()
    {

        if (time1 == 0)
        {
            if (time <= 10)
            {
                elapTime += Time.deltaTime;
                timerText.text = elapTime.ToString("F2");
            }
        }




        if (time1 == 0)
        {
            if (elapTime >= 10.00)
            {
                time1 += 1;
                //Instantiate(lose);
                rollerCoasterManager.ResetGame();
            }
        }

    }
}
