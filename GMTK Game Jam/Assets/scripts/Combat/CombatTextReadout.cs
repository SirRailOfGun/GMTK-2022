using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatTextReadout : MonoBehaviour
{
    public int gameState = 1; // 0 is combat, 1 is inventory, 2 is loot
    public GameObject selectedObject;
    public int whenToUse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObject)
        {
            EquipmentInfo equipStats = selectedObject.GetComponent<EquipmentInfo>();
            string readoutText = "";
            readoutText += equipStats.Name;
            readoutText += "\n_________\nWhile Active\n----------------\n";
            readoutText += "Weapon Progression\n" + equipStats.WeaponCycling + "d6\n";
            readoutText += "Accuracy\n" + equipStats.WeaponToHit + "d6\n";
            readoutText += "Parry\n" + equipStats.WeaponParry + "d6\n";
            readoutText += "Damage\n" + equipStats.WeaponDamage + "d6\n";
            readoutText += "Critical\n" + equipStats.WeaponHitLoc + "d6\n";

            readoutText += "\nBonuses to equipped location\n----------------\n";
            readoutText += "Extra Hit Dice\n" + equipStats.itemHD + "d6\n";
            readoutText += "D R\n" + equipStats.ItemDR + "d6\n";

            readoutText += "\nGlobal Bonuses\n----------------\n";
            readoutText += "Weapon Progression\n" + equipStats.GlobalCycling + "d6\n";
            readoutText += "Accuracy\n" + equipStats.GlobalToHit + "d6\n";
            readoutText += "Parry\n" + equipStats.GlobalParry + "d6\n";
            readoutText += "Damage\n" + equipStats.GlobalDamage + "d6\n";
            readoutText += "Critical\n" + equipStats.GlobalHitLoc + "d6\n";
            readoutText += "Extra Hit Dice\n" + equipStats.GlobalHD + "d6\n";
            readoutText += "D R\n" + equipStats.GlobalDR + "d6\n";

            gameObject.GetComponent<TextMeshPro>().SetText(readoutText);
        }
        else
        {
            gameObject.GetComponent<TextMeshPro>().SetText("");
        }
    }
}
