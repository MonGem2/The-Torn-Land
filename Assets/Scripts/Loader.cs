using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Loader
{
    public static bool IsMapGenered=false;
    public static void LoadMap()
    {
        for (int i = 0; i < 100; i++)
        {
            StaticData.MapData.Add(new List<WorldMapCell>());

            for (int k = 0; k < 100; k++)
            {
                WorldMapCell gg = new WorldMapCell(k, i);
                gg.Message = i.ToString() + "HI" + k.ToString();
                StaticData.MapData[i].Add(gg);
                
            }
        }
        IsMapGenered = true;
    }
}
