using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackAShrimp : MonoBehaviour
{
    bool isBroken = false;
    [SerializeField] private Animator animator;

    GameObject shrimp;
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
            animator.SetTrigger("Appear");
        }
    else if (isBroken == true) {
        Debug.Log("Shrimp machine broke");

        }  

    }

}
