using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public GameObject selectedObject;
    public List<Vector3> allLocations;
    public List<Vector3> validLocations;
    public float validOffset = 0.25f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        validLocations = allLocations;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.SendMessage("Grab", mousePosition + offset);
            //selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.SendMessage("LetGo");
            selectedObject = null;
        }
    }
    
    public Vector3 TakePos(Vector3 position, Vector3 oldPosition)
    {
        for (int i = 0; i < validLocations.Count; i++)
        {
            if (Vector3.Distance(position, validLocations[i]) < validOffset)
            {
                if (oldPosition != new Vector3(-9.5f, -9.5f, 0))
                {
                    validLocations.Add(oldPosition);
                }
                oldPosition = validLocations[i];
                if (i > 0)
                {
                    validLocations.RemoveAt(i);
                }
            }
        }
        return oldPosition;
    }
}