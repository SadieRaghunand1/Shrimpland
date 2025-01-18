using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fallingPiece : MonoBehaviour
{
    public GameObject losePic;
    public int lose = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lose += 1;
        Destroy(gameObject);
        Instantiate(losePic);
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lose == 1)
        {
            Instantiate(losePic);
        }

    }
}
