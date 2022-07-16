using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    static public int RollDice(int numDice)
    {
        int result = 0;
        bool negVal = false;
        if (numDice < 0)
        {
            negVal = true;
            numDice = Mathf.Abs(numDice);
        }
        for (int i = 0; i < numDice; i++)
        {
            result += UnityEngine.Random.Range(1, 6);
        }

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
