using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject prefabEnemy;
    public GameObject prefabItem;
    public GameObject activeEnemy;

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}

    public void GenerateEnemy(int level)
    {
        activeEnemy = Instantiate(prefabEnemy);
        EquipmentAndStats enemyItems = activeEnemy.GetComponent<EquipmentAndStats>();
        int itemPoints = level ^ 2;
        int itemSlot = 0;
        while (itemPoints > 0)
        {
            GameObject newItem = Instantiate(prefabItem);
            if (itemPoints < 6 || itemSlot == 23)
            {
                newItem.GetComponent<EquipmentInfo>().GenerateWeapon(itemPoints);
                enemyItems.equip(newItem,itemSlot);
                itemPoints = 0;
            }
            else
            {
                int diceToRoll = itemPoints / 6;
                int newItemValue = DiceRoller.RollDice(diceToRoll);
                newItem.GetComponent<EquipmentInfo>().GenerateEquipment(newItemValue);
                enemyItems.equip(newItem, itemSlot);
                itemPoints -= newItemValue;
            }
            itemSlot++;
        }
    }
}
