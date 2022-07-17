using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{
    public List<Sprite> itemSprite;
    public List<Sprite> weaponSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetRandomItemSprite() {
        return itemSprite[Mathf.FloorToInt(Random.value * itemSprite.Count)];
    }

    public Sprite GetRandomWeaponSprite() {
        return weaponSprite[Mathf.FloorToInt(Random.value * weaponSprite.Count)];
    }
}
