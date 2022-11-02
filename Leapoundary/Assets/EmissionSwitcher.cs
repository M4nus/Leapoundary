using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionSwitcher : MonoBehaviour
{
    public Settings settings;

    void Start()
    {
        if(settings.GetGraphicalOption() == 0)
        {
            GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
