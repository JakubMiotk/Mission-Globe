using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryBackground : MonoBehaviour
{
    public SpriteRenderer parent;
    private Color parentColor;
    private Color thisColor;
    void Update()
    {
        parentColor = parent.color;
        thisColor = gameObject.GetComponent<SpriteRenderer>().color;
        parentColor.a = thisColor.a;
        gameObject.GetComponent<SpriteRenderer>().color = parentColor;
    }
}
