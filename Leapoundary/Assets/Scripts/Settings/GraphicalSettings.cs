using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphicalSettings : MonoBehaviour
{
    public Settings gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(gameSettings);

        if(gameSettings.GetGraphicalOption() == 0)
        {
        }
    }

    
}
