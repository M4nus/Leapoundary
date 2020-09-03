﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testSDK : MonoBehaviour
{
    public Settings settings;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "SDK LVL: " + settings.GetSDKLevel();
    }
}
