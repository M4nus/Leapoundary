using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        if(PlayerSettings.instance.gameType == GameType.Classic)
            text.text = "HighScore: " + PlayerPrefs.GetInt("HighScoreClassic", 0);
        if(PlayerSettings.instance.gameType == GameType.Ninja)
            text.text = "HighScore: " + PlayerPrefs.GetInt("HighScoreRainbow", 0);
    }

    void OnEnable()
    {
        if(PlayerSettings.instance.gameType == GameType.Classic)
            if(PlayerSettings.instance.leaps > PlayerPrefs.GetInt("HighScoreClassic", 0))
            {
                PlayerPrefs.SetInt("HighScoreClassic", PlayerSettings.instance.leaps);
                text.text = "HighScoreClassic: " + PlayerSettings.instance.leaps; 
            }
        if(PlayerSettings.instance.gameType == GameType.Ninja)
            if(PlayerSettings.instance.leaps > PlayerPrefs.GetInt("HighScoreRainbow", 0))
            {
                PlayerPrefs.SetInt("HighScoreRainbow", PlayerSettings.instance.leaps);
                text.text = "HighScoreRainbow: " + PlayerSettings.instance.leaps;
            }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScoreRainbow");
        PlayerPrefs.DeleteKey("HighScoreClassic");
    }
}
