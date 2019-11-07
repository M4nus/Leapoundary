using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public int heartID;

    private void Update()
    {
        CheckLives();
    }

    void CheckLives()
    {
        if(heartID > PlayerSettings.instance.lives)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
