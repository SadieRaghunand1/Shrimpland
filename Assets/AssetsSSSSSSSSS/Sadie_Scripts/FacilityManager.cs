using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour
{
    [System.Serializable]
    public class FacilityData
    {
        public string facilityName;
        public string[] breakableThings;
        public bool[] statuses;
    }

    public FacilityData[] facilityDatas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
