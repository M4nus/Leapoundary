using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretColor : MonoBehaviour
{
    public Settings settings;
    public Color color = Color.white;

    void Start()
    {
        if(settings.GetGraphicalOption() == 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = color;
            GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = color;
        }
    }
}
