using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTwitter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Application.OpenURL("https://twitter.com/M4nusPotax");
            PlayerSettings.instance.ResetBall();
        }
    }

    public void LoadRatePage()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
