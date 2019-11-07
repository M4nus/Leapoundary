using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaps : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    void Update()
    {
        text.text = "" + PlayerSettings.instance.leaps;
    }
}
