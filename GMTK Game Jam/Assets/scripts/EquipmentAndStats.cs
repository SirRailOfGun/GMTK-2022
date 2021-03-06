using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentAndStats : MonoBehaviour
{
    public string characterName = "";
    public int currentWeapon;
    public EquipSlot[] equipment = new EquipSlot[24];
    public HPBarManager[] healthBars = new HPBarManager[24];
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
        healthBars = GetComponentsInChildren<HPBarManager>();
        InstantiateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            healthBars[i].currentValue = equipment[i].health;
        }
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
            int maxHP = DiceRoller.RollDice(GlobalHitDice + equipment[i].BonusHD + equipment[i].equippedItem.GetComponent<EquipmentInfo>().itemHD);
            equipment[i].health = maxHP;
            healthBars[i].maxValue = maxHP;
        }
    }

    public int GetCyclingDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalCycling;
        }
        if(currentWeapon >= 0 && currentWeapon < equipment.Length) {
            count += equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().WeaponCycling;
        }
        return count;
    }

    public int GetToHitDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalToHit;
        }
        if (currentWeapon >= 0 && currentWeapon < equipment.Length) {
            count += equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().WeaponToHit;
        }
        return count;
    }

    public int GetParryDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalParry;
        }
        if (currentWeapon >= 0 && currentWeapon < equipment.Length) {
            count += equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().WeaponParry;
        }
        return count;
    }

    public int GetDamageDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalDamage;
        }
        if (currentWeapon >= 0 && currentWeapon < equipment.Length) {
            count += equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().WeaponDamage;
        }
        return count;
    }

    public int GetArmorDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalArmor;
        }
        if (currentWeapon >= 0 && currentWeapon < equipment.Length) {
            count += equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().WeaponArmor;
        }
        return count;
    }

    public int GetHitLocDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalHitLoc;
        }
        if (currentWeapon >= 0 && currentWeapon < equipment.Length) {
            count += equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().WeaponHitLoc;
        }
        return count;
    }

    public int GetGlobalDRDice() {
        int count = 0;
        foreach (EquipSlot slot in equipment) {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().GlobalDR;
        }
        return count;
    }
    public int GetLuckDice()
    {
        int count = 0;
        foreach (EquipSlot slot in equipment)
        {
            count += slot.equippedItem.GetComponent<EquipmentInfo>().luck;
        }
        return count;
    }

    public int GetSlotDRDice(int slot) {
        int count = GetGlobalDRDice();
        slot = slot % equipment.Length;
        if (slot >= 0 && slot < equipment.Length) {
            count += equipment[slot].equippedItem.GetComponent<EquipmentInfo>().ItemDR;
        }
        return count;
    }

    public void AdvanceWeapon(int count) {
        int weapons = GetWeaponCount();
        if(weapons == 0) {
            return;
        }
        count = count % weapons;
        if(!equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().isWeapon) {
            count = Mathf.Max(1,count);
        }
        while(count > 0) {
            currentWeapon = (currentWeapon + 1) % equipment.Length;
            if (equipment[currentWeapon].equippedItem.GetComponent<EquipmentInfo>().isWeapon) {
                count--;
            }
        }
    }

    public void TakeDamage(int damage, int slot) {
        float damageMod = 1;
        int overCrit = slot / equipment.Length;
        if (overCrit > 1)
        {
            damageMod += (overCrit * 0.2f);
        }
        slot = slot % equipment.Length;
        if (slot >= 0 && slot < equipment.Length) {
            equipment[slot].health -= (int)(damage * damageMod);
            if(equipment[slot].health <= 0) {
                Destroy(gameObject);
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
