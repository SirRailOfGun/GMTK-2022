using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public GameObject diePrefab;
    static public int RollDice(int numDice)
    {
        //Uncomment this at your own risk!
        // Debug.Log("Rolling " + numDice + " Dice"); //Debug code

        int result = 0;
        bool negVal = false;
        if (numDice < 0)
        {
            negVal = true;
            numDice = Mathf.Abs(numDice);
        }
        for (int i = 0; i < numDice; i++)
        {
            result += Random.Range(1, 6);
            //GameObject currentDie = Instantiate(diePrefab);
            //currentDie.GetComponent<DiceRender>().diceValue = result;
            //currentDie.GetComponent<DiceRender>().renderPos = new Vector3(0,3,0);
        }

        // Debug.Log("Result is " + result); //Debug code

        if (negVal)
        {
            return result * -1;
        }
        else
        {
            return result;
        }
    }
}
