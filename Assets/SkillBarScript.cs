using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillBarScript : MonoBehaviour
{
    public List<SkillButton> buttons;
    void Start()
    {
            
    }
    public void SetSkillOnButton(int Skill, int Button)
    {
        if (Button < 0 || Button >= buttons.Count)
        {
            return;
        }
        buttons[Button].Set( Skill);
    }
    public void Use(int skill, float Cooldown)
    {
        foreach (var item in buttons)
        {
            if (item.skill == skill)
            {
                item.SetCooldown(Cooldown);                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
         
    }
}
