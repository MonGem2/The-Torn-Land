using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Text text;
    public float xp;
    void Start()
    {
        
    }
    public bool CheckXP(float XP)
    {
        if (XP < xp)
        {
            return false;   
        }
        xp -= XP;
        return true;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
