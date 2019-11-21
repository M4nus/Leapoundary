using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UpgradeSettings : MonoBehaviour
{
    public Volume volume;
    public ColorAdjustments color;
    public Vignette vignette;

    private void Start()
    {
        volume = GameObject.Find("Post-process Volume").GetComponent<Volume>();
        volume.profile.TryGet(out color);
        volume.profile.TryGet(out vignette);
    }

    #region Negative
    public void RemoveHeart(int amount)
    {
        for(int i=0; i<amount; i++)
            PlayerSettings.instance.HurtBall();
    }

    public void DecreaseBallSpeed(float amount)
    {
        PlayerSettings.instance.ballSpeed -= amount;

        if(PlayerSettings.instance.ballSpeed < 300)
            PlayerSettings.instance.ballSpeed = 300;
    }
    
    public void AddTriangleAmount(int amount)
    {
        PlayerSettings.instance.triangleLimit += amount;
    }

    public void AddStanderAmount(int amount)
    {
        PlayerSettings.instance.standerLimit += amount;
    }

    #endregion

    #region Positive
    public void GiveHeart(int amount)
    {
        PlayerSettings.instance.lives += amount;
        Debug.Log("GiveHeart: " + PlayerSettings.instance.lives);

        if(PlayerSettings.instance.lives > 5)
           PlayerSettings.instance.lives = 5;
    }

    public void IncreaseBallSpeed(float amount)
    {
        PlayerSettings.instance.ballSpeed += amount;

        if(PlayerSettings.instance.ballSpeed > 1000)
            PlayerSettings.instance.ballSpeed = 1000;
    }

    public void ClearEnemyType(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject enemy in enemies)
        {
            Debug.Log("Enemies: " + enemy);
            Destroy(enemy);
        }
    }

    #endregion

    #region Neutral

    // Hue shift
    public void HueShift()
    {
        if(!color.hueShift.overrideState)
            color.hueShift.overrideState = true;
        else
            color.hueShift.overrideState = false;
    }

    // Saturation
    public void Saturation()
    {
        if(!color.saturation.overrideState)
            color.saturation.overrideState = true;
        else
            color.saturation.overrideState = false;
    }

    // Contrast
    public void Contrast()
    {
        if(!color.contrast.overrideState)
            color.contrast.overrideState = true;
        else
            color.contrast.overrideState = false;
    }

    // Colorful
    public void Colorful()
    {
        if(!vignette.active)
            vignette.active = true;
        else
            vignette.active = false;
    }

    #endregion
}
