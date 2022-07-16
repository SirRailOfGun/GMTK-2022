using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentInfo : MonoBehaviour
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


    public void GenerateEquipment(int level)
    {
        level = Mathf.Max(level, 1);

        bool isWeapon = Random.value > .5f;

        if (isWeapon)
        {
            GenerateGear(level);
        }
        else
        {
            GenerateWeapon(level);
        }
    }

    public void GenerateGear(int level)
    {
        int remaining = level;
        while (remaining > 0)
        {
            // Local Rolls
            int statRoll = Mathf.FloorToInt(Random.value * 8);
            int statValue = 1 + Mathf.FloorToInt(Random.value * (remaining - 1));
            switch (statRoll)
            {
                case 0: // cycling
                    GlobalCycling += statValue;
                    remaining -= statValue;
                    break;
                case 1: // toHit
                    GlobalToHit += statValue;
                    remaining -= statValue;
                    break;
                case 2: // parry
                    GlobalParry += statValue;
                    remaining -= statValue;
                    break;
                case 3: // damage
                    GlobalDamage += statValue;
                    remaining -= statValue;
                    break;
                case 4: // armor
                    GlobalArmor += statValue;
                    remaining -= statValue;
                    break;
                case 5: // hitLoc
                    GlobalHitLoc += statValue;
                    remaining -= statValue;
                    break;
                case 6: // dr
                    GlobalDR += statValue;
                    remaining -= statValue;
                    break;
                case 7: // hd
                    if (Random.value < .1f)
                    {
                        GlobalHD += statValue / 4 + 1;
                        remaining -= statValue;
                    }
                    else
                    {
                        itemHD += statValue;
                        remaining -= statValue;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void GenerateWeapon(int level)
    {
        isWeapon = true;

        int globalstat = (int)Mathf.Min(new int[] {
            Mathf.FloorToInt(Random.value * level),
            Mathf.FloorToInt(Random.value * level),
            Mathf.FloorToInt(Random.value * level) });
        int remaining = level - globalstat;
        while (remaining > 0)
        {
            // Local Rolls
            int statRoll = Mathf.FloorToInt(Random.value * 6);
            int statValue = 1 + Mathf.FloorToInt(Random.value * (remaining - 1));
            remaining -= statValue;
            switch (statRoll)
            {
                case 0: // cycling
                    WeaponCycling += statValue;
                    remaining -= statValue;
                    break;
                case 1: // toHit
                    WeaponToHit += statValue;
                    remaining -= statValue;
                    break;
                case 2: // parry
                    WeaponParry += statValue;
                    remaining -= statValue;
                    break;
                case 3: // damage
                    WeaponDamage += statValue;
                    remaining -= statValue;
                    break;
                case 4: // armor
                    WeaponArmor += statValue;
                    remaining -= statValue;
                    break;
                case 5: // hitLoc
                    WeaponHitLoc += statValue;
                    remaining -= statValue;
                    break;
                default:
                    break;
            }
        }

        remaining = globalstat;
        while (remaining > 0)
        {
            // Local Rolls
            int statRoll = Mathf.FloorToInt(Random.value * 8);
            int statValue = 1 + Mathf.FloorToInt(Random.value * (remaining - 1));
            switch (statRoll)
            {
                case 0: // cycling
                    GlobalCycling += statValue;
                    remaining -= statValue;
                    break;
                case 1: // toHit
                    GlobalToHit += statValue;
                    remaining -= statValue;
                    break;
                case 2: // parry
                    GlobalParry += statValue;
                    remaining -= statValue;
                    break;
                case 3: // damage
                    GlobalDamage += statValue;
                    remaining -= statValue;
                    break;
                case 4: // armor
                    GlobalArmor += statValue;
                    remaining -= statValue;
                    break;
                case 5: // hitLoc
                    GlobalHitLoc += statValue;
                    remaining -= statValue;
                    break;
                case 6: // dr
                    GlobalDR += statValue;
                    remaining -= statValue;
                    break;
                case 7: // hd
                    if (Random.value < .1f)
                    {
                        GlobalHD += statValue / 4 + 1;
                        remaining -= statValue;
                    }
                    else
                    {
                        itemHD += statValue;
                        remaining -= statValue;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
