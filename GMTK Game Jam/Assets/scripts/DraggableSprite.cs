using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    //public List<Vector3> validLocations;
    public DragController controller;
    public Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("BG");
        controller = (DragController)go.GetComponent(typeof(DragController));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Grab(Vector3 mousePos)
    {
        transform.position = mousePos;
    }

    void LetGo()
    {
        lastPos = controller.TakePos(transform.position, lastPos, gameObject);
        transform.position = lastPos;
    }

    void CleanUpTrash()
    {
        if (lastPos == new Vector3(-9.5f,-9.5f,0) || lastPos.x > 10.5f)
        {
            Destroy(gameObject);
            //remove this object
        }
    }
}
