using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tongs : MonoBehaviour
{
//sets player movespeed
    public float mouthSpeed = 1f;
    private Vector2 mousePosition;



// Update is called once per frame
void Update()

{   //gets position
        if (Input.GetMouseButton(0))
        { 
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, mouthSpeed);
        }
}

}
