using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public Door door;
    void Start()
    {
        door.GetComponent<Door>();
    }

    void OnTriggerEnter2D()
    {
        door.Destroy();
    }
}
