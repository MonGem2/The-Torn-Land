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
    public Text lvlText;
    bool LvlUp=false;
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
        player.OnLevelChange += (x) => {
            lvlText.text = "Lvl:" + player.Lvl;
        };
    }
    public void LevelUpBegin()
    {
        LvlUp = true;
    }
    public void LevelUpEnd()
    {
        LvlUp = false;
    }
    public void UpdateUI()
    {
        if (LvlUp)
        {
            Debug.LogError("qwertyu");
            slider.value = xp;
           //slider.gameObject.
            text.text = xp + "/" + player.MaxXP;
        }
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
    public bool XPGet(float count)
    {
        if (LvlUp)
        {
            if (xp < count)
            {
                return false;
            }
            xp -= count;
            UpdateUI();
            return true;
        }
        return false;
    }
    public bool XPReturn(float count)
    {
        if (LvlUp)
        {
            if (xp+count > player.XP)
            {
                return false;
            }
            xp += count;
            UpdateUI();
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
