using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButton : MonoBehaviour
{
    public int newState = 0;
    public bool canBeUsed = false;
    public bool ignoreAbove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool inRange = true;
        if (mousePosition.x < transform.position.x - transform.localScale.x)
        {
            inRange = false;
        }
        if (mousePosition.x > transform.position.x + transform.localScale.x)
        {
            inRange = false;
        }
        if (mousePosition.y < transform.position.y - transform.localScale.y)
        {
            inRange = false;
        }
        if (mousePosition.y > transform.position.y + transform.localScale.y)
        {
            inRange = false;
        }
        if (Input.GetMouseButtonDown(0) && inRange && (canBeUsed || ignoreAbove))
        {
            gameObject.transform.parent.BroadcastMessage("newGameState", newState);
            if(newState == 0)
            {
                gameObject.transform.parent.BroadcastMessage("cleanUpTrash");
            }
            canBeUsed = false;
        }
    }

    void useNow()
    {
        canBeUsed = true;
    }
}
