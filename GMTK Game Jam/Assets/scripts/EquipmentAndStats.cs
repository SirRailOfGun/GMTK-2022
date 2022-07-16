using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentAndStats : MonoBehaviour
{
    public int currentWeapon;
    public EquipSlot[] equipment = new EquipSlot[24];
    public GameObject blankItem;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            if (!(equipment[i].equippedItem))
            {
                equip(Instantiate(blankItem), i);
                //int defaultTier = 0;
                //equipment[i].equippedItem.SendMessage("GenerateEquipment", defaultTier);
            }
        }
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
            GlobalHitDice += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalHD;
        }
        for (int i = 0; i < equipment.Length; i++)
        {
            equipment[i].health = DiceRoller.RollDice(GlobalHitDice + equipment[i].BonusHD + equipment[i].equippedItem.GetComponent<EquipmentInfo>().itemHD);
        }
    }

    public int GetCyclingDice() {
        return 1;
    }

    public void AdvanceWeapon(int count) {
        int weapons = GetWeaponCount();
        if(weapons == 0) {
            return;
        }
        count = count % weapons;
        while(count > 0) {
            currentWeapon = (currentWeapon + 1) % equipment.Length;
            if (equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().isWeapon) {
                count--;
            }
        }
    }
    
    public int GetWeaponCount() {
        int count = 0;
        for (int i = 0; i < equipment.Length; i++) {
            if (equipment[i].equippedItem.GetComponent<EquipmentInfo>().isWeapon) {
                count++;
            }
        }
        return count;
    }

    public void equip(GameObject item, int equipSlot)
    {
        if (equipment[equipSlot].equippedItem)
        {
            if (equipment[equipSlot].equippedItem.name == "")
            {
                Destroy(equipment[equipSlot].equippedItem);
            }
        }
        equipment[equipSlot].equippedItem = item;
    }

    [System.Serializable]
    public struct EquipSlot
    {
        public string Name;
        public GameObject equippedItem;
        public int health;
        public int BonusHD;
    }
}
