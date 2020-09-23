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
    public Player player;
    void Start()
    {
        slider.maxValue = player.MaxXP;
        slider.value = player.XP;
        xp = player.XP;
        text.text = player.XP + "/" + player.MaxXP;
        player.XPChangeTrigger += (x) => {
            slider.value = player.XP;
            xp = player.XP;
            text.text = player.XP + "/" + player.MaxXP;
        };
        player.MaxXPChangeTrigger += (x) =>
        {
            slider.maxValue = player.MaxXP;                        
            text.text = player.XP + "/" + player.MaxXP;
        };
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
