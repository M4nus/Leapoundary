using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    public Settings settings;
    // Start is called before the first frame update
    void Start()
    {
        Volume volume = GetComponent<Volume>();

        if(settings.GetGraphicalOption() == 0)
        {
            volume.enabled = false;
        }
        else if(settings.GetGraphicalOption() == 1)
        {
            volume.enabled = true;
        }
    }
}
