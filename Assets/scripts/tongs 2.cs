using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tongs2 : MonoBehaviour
{
    //sets player movespeed
    public float moveSpeed = 5f;

    //sets the rigid body for colliding
    public Rigidbody2D rb;

    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()

    {   //gets position
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //gets mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        //makes character move at movement speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //gets where character is looking at
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        //rotates character
        rb.rotation = angle;
    }
}
