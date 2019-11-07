using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaps : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    void Update()
    {
        if(PlayerSettings.instance.ballState == BallState.Death)
            text.text = "Leaps: " + PlayerSettings.instance.leaps;
        else
            text.text = "" + PlayerSettings.instance.leaps;
    }
}
