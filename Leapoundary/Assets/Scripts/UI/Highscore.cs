using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        text.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    void OnEnable()
    {
        if(PlayerSettings.instance.leaps > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", PlayerSettings.instance.leaps);
            text.text = "HighScore: " + PlayerSettings.instance.leaps; 
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
