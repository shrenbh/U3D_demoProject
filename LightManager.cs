using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Lampstandard[] lights;
    public GameObject againButton;
    
    
    private void Start()
    {
        Time.timeScale = 1; // 初始时游戏时间为正常值
    }

    private void Update()
    {
        if (AllLightsAreBright())
        {
            againButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    bool AllLightsAreBright()
    {
        foreach (var light in lights)
        {
            if (!light.isBright)
            {
                return false;
            }
        }
        return true;
    }
}
