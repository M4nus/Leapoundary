using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UpgradeSettings : MonoBehaviour
{
    public EnemySpawner spawner;
    public Volume volume;
    public ColorAdjustments color;
    public Vignette vignette;
    public ParticleContainer pc;
    public ColorCurves curves;

    private void Start()
    {
        pc = GetComponent<ParticleContainer>();
        spawner = GetComponent<EnemySpawner>();
        volume = GameObject.Find("Post-process Volume").GetComponent<Volume>();
        volume.profile.TryGet(out color);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out curves);
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

    public void SpawnTriangle(int amount)
    {
        for(int i=0; i<amount; i++)
            spawner.SpawnTriangle();
    }

    public void SpawnStander(int amount)
    {
        for(int i = 0; i < amount; i++)
            spawner.SpawnStander();
    }

    public void SpeedSpawnerStander()
    {
        if(PlayerSettings.instance.standerSpawnTime > 5)
            PlayerSettings.instance.standerSpawnTime -= 1f;
    }

    public void SpeedSpawnerTriangle()
    {
        if(PlayerSettings.instance.triangleSpawnTime > 5)
            PlayerSettings.instance.triangleSpawnTime -= 1f;
    }

    #endregion

    #region Positive
    public void GiveHeart(int amount)
    {
        PlayerSettings.instance.lives += amount;

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
            Instantiate(pc.enemyDeath, enemy.transform.position, Quaternion.identity);
            enemy.SetActive(false);
        }
    }

    public void SlowSpawnerStander()
    {
        if(PlayerSettings.instance.standerSpawnTime < 20)
            PlayerSettings.instance.standerSpawnTime += 1f;
    }

    public void SlowSpawnerTriangle()
    {
        if(PlayerSettings.instance.triangleSpawnTime < 20)
            PlayerSettings.instance.triangleSpawnTime += 1f;
    }

    #endregion

    #region Neutral

    public void HueIncrement()
    {
        color.hueShift.value += 30;
    }

    // Hue shift
    public void HueShift()
    {
        if(!curves.active)
            curves.active = true;
        else
            curves.active = false;
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
