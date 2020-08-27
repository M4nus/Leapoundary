using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretColor : MonoBehaviour
{
    public Settings settings;

    void Start()
    {
        if(settings.GetGraphicalOption() == 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
            GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = Color.white;
        }
    }
}
