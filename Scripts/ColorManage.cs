using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManage
{
    public static Color SetColor(int para)
    {
        switch (para)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.yellow;
            case 2:
                return Color.green;
            default:
                return Color.blue;
        }
    }
}
