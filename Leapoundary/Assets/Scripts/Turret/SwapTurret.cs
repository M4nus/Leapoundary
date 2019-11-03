using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTurret : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlayerSettings.instance.MoveTurrets();
            PlayerSettings.instance.ResetBall();
        }
    }
}
