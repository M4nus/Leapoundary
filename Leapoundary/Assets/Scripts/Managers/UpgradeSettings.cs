using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSettings : MonoBehaviour
{
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
    
    public void AddEnemyTypeAmount(string tag, int amount)
    {
        if(tag == "Triangle")
            PlayerSettings.instance.triangleLimit += amount;
        if(tag == "Stander")
            PlayerSettings.instance.standerLimit += amount;
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
            Debug.Log("Enemies: " + enemy);
            Destroy(enemy);
        }
    }

    #endregion

    #region Neutral

    // Change hue
    // Slow time
    // Speed Time
    // Postprocess effects

    #endregion
}
