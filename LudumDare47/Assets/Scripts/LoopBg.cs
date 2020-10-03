using System.Collections;
using UnityEngine;

public class LoopBg : MonoBehaviour
{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height + 1000, mainCamera.transform.position.z));
        foreach(GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void loadChildObjects(GameObject obj)
    {
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }


    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfobjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;
            if(transform.position.y + screenBounds.y > lastChild.transform.position.y + halfobjectHeight)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfobjectHeight * 2, lastChild.transform.position.z);
            }
            else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfobjectHeight)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y - halfobjectHeight * 2, firstChild.transform.position.z);
            }

        }
    }

    private void LateUpdate()
    {
        foreach(GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }

}
