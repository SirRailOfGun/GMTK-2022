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
        item.GenerateEquipment(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(regenerateItem) {
            regenerateItem = false;
            item.GenerateEquipment(itemLevel);
        }   
    }
}
