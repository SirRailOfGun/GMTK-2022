using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public EquipmentAndStats player;
    public GameObject selectedObject;
    public GameObject blankItem;
    public GameObject defaultItem;
    public List<Vector3> allLocations;
    public List<Vector3> validLocations;
    public float validOffset = 0.25f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        for(float x = 1f; x < 10.5f; x++)
        {
            for(float y = -.5f; y > -10; y--)
            {
                allLocations.Add(new Vector3(x, y, 0f));
            }
        }
        foreach(var location in allLocations)
        {
            validLocations.Add(location);
        }
        GameObject starterItem = Instantiate(defaultItem);
        starterItem.transform.position = new Vector3(-2f, -7.5f, 0f);
        starterItem.GetComponent<DraggableSprite>().lastPos = starterItem.transform.position;
        TakePos(starterItem.transform.position, new Vector3(-9.5f, -9.5f, 0), starterItem);
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
    
    public Vector3 TakePos(Vector3 position, Vector3 oldPosition, GameObject item)
    {
        for (int i = 0; i < validLocations.Count; i++)
        {
            if (Vector3.Distance(position, validLocations[i]) < validOffset)
            {
                int index = allLocations.IndexOf(validLocations[i]);

                //Debug.Log("new position : " + validLocations[i]); //debug code
                //Debug.Log("new position index : " + index);   //debug code

                if (index > 0 && index < 25)
                {
                    player.equip(item, index - 1);
                }
                index = allLocations.IndexOf(oldPosition);

                //Debug.Log("old position : " + oldPosition);   //debug code
                //Debug.Log("old position index : " + index);   //debug code
                //if (index >= 0 && index < allLocations.Count)   //debug code
                //{   //debug code
                //    Debug.Log(allLocations[index]); //debug code
                //}   //debug code

                if (index > 0 && index < 25)
                {
                    player.equip(Instantiate(blankItem), index - 1);
                }
                bool savelocation = false;
                foreach(var location in allLocations)
                {
                    if(oldPosition == location)
                    {
                        savelocation = true;
                        break;
                    }
                }
                if ((oldPosition != new Vector3(-9.5f, -9.5f, 0)) && savelocation)
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