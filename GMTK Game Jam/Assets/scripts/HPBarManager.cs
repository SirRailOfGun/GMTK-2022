using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPBarManager : MonoBehaviour
{
    public float maxValue;
    public float currentValue;

    //set in editor
    public GameObject barObject;
    public GameObject valueReadout;
    public TextMeshPro value;
    public float maxScale;
    public float minScale;
    public float yScale;

    // Start is called before the first frame update
    void Start()
    {
        value = valueReadout.GetComponent<TextMeshPro>();
        barObject.transform.localScale = new Vector3(maxScale * Mathf.Min((1 - minScale) * currentValue / maxValue + minScale, 1), yScale, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        barObject.transform.localScale = new Vector3(maxScale * Mathf.Min((1 - minScale) * currentValue / maxValue + minScale, 1), yScale, 0.1f);
        value.text = Mathf.Floor(currentValue).ToString();
    }
}
