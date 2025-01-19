using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPiece : MonoBehaviour
{
    public GameObject losePic;
    public int lose = 0;
    public int rotate = 0;
    public float speed = 0;
    public float speed2 = 0;
    public float rotateSpeed = 0;
    public Rigidbody2D rb;

    public AppleBobbingManager rollerCoasterManager;


    private void Start()
    {
        rollerCoasterManager = FindObjectOfType<AppleBobbingManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 16)
        {
            rollerCoasterManager.countFallen++;
            //lose += 1;
            Destroy(gameObject);
            //Instantiate(losePic);
        }


    }

    private void OnMouseDown()
    {
        Debug.Log("Wrench clicked");
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        /*if (lose == 1)
        {
            Instantiate(losePic);
        }*/

        if (rotate == 0)
        {
            rotateSpeed = speed + speed2;
            speed2 += 1;
            rb.MoveRotation(rotateSpeed);
        }
        if (rotate == 1)
        {
            rotateSpeed = speed + speed2;
            speed2 -= 1;
            rb.MoveRotation(rotateSpeed);
        }


    }
}
