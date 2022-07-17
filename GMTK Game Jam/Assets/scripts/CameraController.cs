using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int gameState = 1; // 0 is combat, 1 is inventory, 2 is loot

    public void NewGameState(int state)
    {
        gameState = state; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case 0:
                transform.position = new Vector3(0,5,-10);
                break;
            case 1:
                transform.position = new Vector3(0,-5,-10);
                break;
            case 2:
                transform.position = new Vector3(11,-5,-10);
                break;
        }
    }
}
