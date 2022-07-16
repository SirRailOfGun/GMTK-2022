using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EquipmentAndStats;

public class EquipmentGenerator : MonoBehaviour
{
    public bool regenerateItem = true;
    public int itemLevel = 1;

    public EquipmentInfo item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(regenerateItem) {
            regenerateItem = false;
            item = GenerateEquipment(itemLevel);
        }   
    }

    public EquipmentInfo GenerateEquipment(int level) {
        level = Mathf.Max(level, 1);

        bool isWeapon = Random.value > .5f;

        if(isWeapon) {
            return GenerateGear(level);
        }
        else {
            return GenerateWeapon(level);
        }
    }

    public EquipmentInfo GenerateGear(int level) {
        EquipmentInfo equipment = new EquipmentInfo();

        int remaining = level;
        while (remaining > 0) {
            // Local Rolls
            int statRoll = Mathf.FloorToInt(Random.value * 8);
            int statValue = 1 + Mathf.FloorToInt(Random.value * (remaining - 1));
            switch (statRoll) {
                case 0: // cycling
                    equipment.GlobalCycling += statValue;
                    remaining -= statValue;
                    break;
                case 1: // toHit
                    equipment.GlobalToHit += statValue;
                    remaining -= statValue;
                    break;
                case 2: // parry
                    equipment.GlobalParry += statValue;
                    remaining -= statValue;
                    break;
                case 3: // damage
                    equipment.GlobalDamage += statValue;
                    remaining -= statValue;
                    break;
                case 4: // armor
                    equipment.GlobalArmor += statValue;
                    remaining -= statValue;
                    break;
                case 5: // hitLoc
                    equipment.GlobalHitLoc += statValue;
                    remaining -= statValue;
                    break;
                case 6: // dr
                    equipment.GlobalDR += statValue;
                    remaining -= statValue;
                    break;
                case 7: // hd
                    if(Random.value < .1f) {
                        equipment.GlobalHD += statValue / 4 + 1;
                        remaining -= statValue;
                    }
                    else {
                        equipment.itemHD += statValue;
                        remaining -= statValue;
                    }
                    break;
                default:
                    break;
            }
        }


        return equipment;
    }

    public EquipmentInfo GenerateWeapon(int level) {
        EquipmentInfo equipment = new EquipmentInfo();
        equipment.isWeapon = true;

        int globalstat = (int)Mathf.Min(new int[] { 
            Mathf.FloorToInt(Random.value * level),
            Mathf.FloorToInt(Random.value * level),
            Mathf.FloorToInt(Random.value * level) });
        int remaining = level - globalstat;
        while (remaining > 0) {
            // Local Rolls
            int statRoll = Mathf.FloorToInt(Random.value * 6);
            int statValue = 1 + Mathf.FloorToInt(Random.value * (remaining - 1));
            remaining -= statValue;
            switch (statRoll) {
                case 0: // cycling
                    equipment.WeaponCycling += statValue;
                    remaining -= statValue;
                    break;
                case 1: // toHit
                    equipment.WeaponToHit += statValue;
                    remaining -= statValue;
                    break;
                case 2: // parry
                    equipment.WeaponParry += statValue;
                    remaining -= statValue;
                    break;
                case 3: // damage
                    equipment.WeaponDamage += statValue;
                    remaining -= statValue;
                    break;
                case 4: // armor
                    equipment.WeaponArmor += statValue;
                    remaining -= statValue;
                    break;
                case 5: // hitLoc
                    equipment.WeaponHitLoc += statValue;
                    remaining -= statValue;
                    break;
                default:
                    break;
            }
        }

        remaining = globalstat;
        while (remaining > 0) {
            // Local Rolls
            int statRoll = Mathf.FloorToInt(Random.value * 8);
            int statValue = 1 + Mathf.FloorToInt(Random.value * (remaining - 1));
            switch (statRoll) {
                case 0: // cycling
                    equipment.GlobalCycling += statValue;
                    remaining -= statValue;
                    break;
                case 1: // toHit
                    equipment.GlobalToHit += statValue;
                    remaining -= statValue;
                    break;
                case 2: // parry
                    equipment.GlobalParry += statValue;
                    remaining -= statValue;
                    break;
                case 3: // damage
                    equipment.GlobalDamage += statValue;
                    remaining -= statValue;
                    break;
                case 4: // armor
                    equipment.GlobalArmor += statValue;
                    remaining -= statValue;
                    break;
                case 5: // hitLoc
                    equipment.GlobalHitLoc += statValue;
                    remaining -= statValue;
                    break;
                case 6: // dr
                    equipment.GlobalDR += statValue;
                    remaining -= statValue;
                    break;
                case 7: // hd
                    if (Random.value < .1f) {
                        equipment.GlobalHD += statValue / 4 + 1;
                        remaining -= statValue;
                    }
                    else {
                        equipment.itemHD += statValue;
                        remaining -= statValue;
                    }
                    break;
                default:
                    break;
            }
        }

        return equipment;
    }
}
