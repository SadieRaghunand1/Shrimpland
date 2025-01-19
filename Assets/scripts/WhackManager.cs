using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WhackManager : MonoBehaviour
{
    public int spotsFixed;
    public int priceToFix;
    int numberOfBrokenSpots;
    bool isBroken = true;
    public GameObject[] ListOfHoles;
    public GameObject Button;
    public TMP_Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        // int risk = Random.Range(0, 10);

        if (isBroken == true)
        {
            Button.SetActive(false);
            numberOfBrokenSpots = Random.Range(1, 4);
            spotsFixed = 0;

            for (int i = 0; i < numberOfBrokenSpots; i++)
            {
                int x = Random.Range(0, 8);
                //This nested if statement is a cheeky fix for if the game rolls the same well twice,
                //it will make the for loop run an extra time
                if (ListOfHoles[x].GetComponent<WhackAShrimp>().isWellBroken == true) {
                    i--;
                }
                ListOfHoles[x].GetComponent<WhackAShrimp>().isWellBroken = true;
                ListOfHoles[x].GetComponent<WhackAShrimp>().amIFixed = false;
                Debug.Log("Hole #" + x + " is broken");
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinCondition()
    {
        if (spotsFixed == numberOfBrokenSpots)
        {
            Debug.Log("You have fixed the ride");
            priceToFix = numberOfBrokenSpots * 10;
            buttonText.text = "Repair - $" + priceToFix + ",000";
            Button.SetActive(true);
        }
    }


}
