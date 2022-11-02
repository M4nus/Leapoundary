using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotivationalText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public List<string> motivational = new List<string>();
    public List<string> gratulations = new List<string>();

    void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();

        if(PlayerSettings.instance.gameType == GameType.Classic)
        {
            if(PlayerSettings.instance.leaps >= PlayerPrefs.GetInt("HighScoreClassic", 0))
                text.text = gratulations[Random.Range(0, gratulations.Count)];
            else
                text.text = motivational[Random.Range(0, motivational.Count)];
        }
        if(PlayerSettings.instance.gameType == GameType.Ninja)
        {
            if(PlayerSettings.instance.leaps >= PlayerPrefs.GetInt("HighScoreRainbow", 0))
                text.text = gratulations[Random.Range(0, gratulations.Count)];
            else
                text.text = motivational[Random.Range(0, motivational.Count)];
        }
    }
}