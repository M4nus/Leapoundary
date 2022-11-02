using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleGraphicalOption : MonoBehaviour
{
    public Settings settings;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if(settings.GetGraphicalOption() == 0)
                settings.SetGraphicalOption(1);
            else
                settings.SetGraphicalOption(0);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
