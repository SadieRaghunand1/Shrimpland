using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float time;
    private float elapTime;



    // Update is called once per frame
    void Update()
    {
        elapTime += Time.deltaTime;
        timerText.text = elapTime.ToString("F2");

        if (elapTime >= 10)
        {
            Time.timeScale = 0;
        }
        if (time >= 10)
        {
            Time.timeScale = 0;
        }
    }
}
