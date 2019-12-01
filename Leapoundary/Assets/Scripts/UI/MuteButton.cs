using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Image sprite;
    public Sprite noMuted;
    public Sprite muted;

    private void Start()
    {
        sprite = GetComponent<Image>();
    }

    public void ToggleVolume()
    {
        if(AudioListener.volume != 0)
        {
            AudioListener.volume = 0f;
            sprite.sprite = muted;
        }
        else
        {
            AudioListener.volume = 1f;
            sprite.sprite = noMuted;
        }
    }
}
