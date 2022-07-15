using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRender : MonoBehaviour
{
    SpriteRenderer rend;
    public List<Sprite> DiceSprites = new List<Sprite>();
    public int diceValue; // dice shows this + 1
    public Vector2 renderPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = DiceSprites[diceValue];
        transform.position = renderPos;
    }
}
