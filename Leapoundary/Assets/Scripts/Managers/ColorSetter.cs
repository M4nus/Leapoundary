using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    public enum ColorType
    {
        Player,
        Enemies,
        Enviro,
        Other
    }

    [SerializeField]
    private ColorType colorType;
    private Color localColor;

    // Start is called before the first frame update
    void Start()
    {
        if(colorType == ColorType.Player)
            localColor = ColorManager.instance.player;
        else if(colorType == ColorType.Enemies)
            localColor = ColorManager.instance.enemies;
        else if(colorType == ColorType.Enviro)
            localColor = ColorManager.instance.enviro;
        else if(colorType == ColorType.Other)
            localColor = ColorManager.instance.other;
        

        if(GetComponent<SpriteGlow.SpriteGlowEffect>() != null)
            GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = localColor;
        else
        {
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            localColor *= 2f;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", localColor);
        }
    }
}
