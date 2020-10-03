using UnityEngine;

public class LoopBg : MonoBehaviour
{

    public GameObject spacePreFab;


    Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject.FindGameObjectWithTag("space");
        Instantiate(spacePreFab, new Vector2(0,0), Quaternion.identity);
        Destroy(other.gameObject);
    }
}