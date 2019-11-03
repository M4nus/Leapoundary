using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    void Update()
    {
        RotateDirection();
    }

    private void RotateDirection()
    {
        PlayerSettings.instance.GetCrosshairDirection(gameObject);
    }
}
