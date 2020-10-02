using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableLaserBox : MonoBehaviour
{
    // Start is called before the first frame update
    public LaserBox laser;
    public LaserBox laser2;
    void Start()
    {
        laser.GetComponent<LaserBox>();
        laser2.GetComponent<LaserBox>();
    }

    void OnTriggerEnter2D()
    {
        laser.Destroy();
        laser2.Destroy();
    }
}
