using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackAShrimp : MonoBehaviour
{
    bool isBroken = true;
    // Start is called before the first frame update
    void Start()
    {
    
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
    if (isBroken == false) {
        Debug.Log("Boioioioioing");
        }
    else if (isBroken == true) {
        Debug.Log("Shrimp machine broke");
        }  

    }

}
