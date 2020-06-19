using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            StaticData.MapData.Add(new List<WorldMapCell>());

            for (int k = 0; k < 100; k++)
            {
                WorldMapCell gg = new WorldMapCell();
                gg.Message = i.ToString() + "HI" + k.ToString();
                StaticData.MapData[i].Add(gg);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
