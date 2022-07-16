using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentAndStats : MonoBehaviour
{
    public EquipSlot[] equipment = new EquipSlot[24];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateHealth()
    {
        int GlobalHitDice = 0;
        foreach(EquipSlot slot in equipment)
        {
            GlobalHitDice += slot.equippedItem.GlobalHD;
        }
        for (int i = 0; i < equipment.Length; i++)
        {
            equipment[i].health = DiceRoller.RollDice(GlobalHitDice + equipment[i].BonusHD + equipment[i].equippedItem.itemHD);
        }
    }

    [System.Serializable]
    public struct EquipSlot
    {
        public string Name;
        public EquipmentInfo equippedItem;
        public int health;
        public int BonusHD;
    }

    [System.Serializable]
    public struct EquipmentInfo
    {
        public string Name;
        public bool isWeapon;       //is this item supposed to be in the weapon list

        //Weapon stats are only applied if the item is the active weapon, ItemDR is applied if it is equipped to the hit location

        public int WeaponCycling;   //how many dice to roll to determine steps to move through the weapon list
        public int GlobalCycling;
        public int WeaponToHit;     //how many dice to roll to beat parry
        public int GlobalToHit;
        public int WeaponParry;     //how many dice to roll to parry an attack
        public int GlobalParry;
        public int WeaponDamage;    //how many damage dice to roll
        public int GlobalDamage;
        public int WeaponArmor;     //how many dice to roll to armor an attack
        public int GlobalArmor;
        public int WeaponHitLoc;    //how many dice to roll to determine hit loc
        public int GlobalHitLoc;
        public int ItemDR;          //how many dice to roll to determine damage reduction
        public int GlobalDR;

        public int itemHD;          //adds health to equip slot
        public int GlobalHD;        //adds health to all slots

        public int luck;            //number of enemy items to steal
    }
}
