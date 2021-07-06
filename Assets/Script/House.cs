using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    public Image imageRadar;

    // Start is called before the first frame update
    void Start()
    {
        Radar.RegisterRadarObject(this.gameObject, imageRadar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
