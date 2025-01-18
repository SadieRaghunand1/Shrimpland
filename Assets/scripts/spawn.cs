using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject wrench;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnW());

    }

    IEnumerator spawnW()
    { 
        yield return new WaitForSeconds(2);
        Instantiate(wrench);
    }

}
