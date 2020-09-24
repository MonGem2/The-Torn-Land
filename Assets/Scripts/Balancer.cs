using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Balancer
{
    // Start is called before the first frame update
    public static float GetParameterPrice(float parameter, float lvl, float startprice=10,float baseMultiplier=1 ,float parameterMultiplier=10,float lvlMutiplier=50 )
    {
        return startprice*(baseMultiplier+parameter%parameterMultiplier)+lvl%lvlMutiplier;
    }
}
