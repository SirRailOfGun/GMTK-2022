using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    public List<Vector3> validLocations;
    public Vector3 lastPos;
    public float validOffset = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        foreach(Vector3 validLoc in validLocations)
        {
            if (Vector3.Distance(transform.position, validLoc) < validOffset)
            {
                lastPos = validLoc;
            }
        }
        transform.position = lastPos;
    }
}
