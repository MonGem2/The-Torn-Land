using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillBarScript : MonoBehaviour
{
    public List<SkillButton> buttons;
    void Start()
    {
        foreach (var item in buttons)
        {
            item.stopOthers += StopOthers;
        }
    }
    public void SetSkillOnButton(Skill Skill, int Button=-1)
    {
        if (Button == -1)
        {
            foreach (var item in buttons)
            {
                if (item.skill == null)
                {
                    item.Set(Skill);
                    return;
                }
            }
        }
        if (Button < 0 || Button >= buttons.Count)
        {
            return;
        }
        buttons[Button].Set( Skill);
    }
    public bool CheckSpace()
    {
        foreach (var item in buttons)
        {
            if (item.skill == null)
            {
                return true;
            }
        }
        return false;
    }
    public void StopOthers(object obj)
    {
        foreach (var item in buttons)
        {
            if (item != obj)
            {
                item.Stop();
            }
        }
    }
    public void ShowAll()
    {
        foreach (var item in buttons)
        {
            item.Show();
        }
    }
    public void HideAll()
    {
        foreach (var item in buttons)
        {
            if (item.skill == null)
            {
                item.Hide();
            }
        }
    }
    public void OnAllGreen()
    {
        foreach (var item in buttons)
        {
            item.OnGreen();
        }
    }
    public void OffGreen()
    {
        foreach (var item in buttons)
        {
            item.OffGreen();
        }
    }
    public void SetUsableFalse()
    {
        foreach (var item in buttons)
        {
            item.Usable = false;
        }
    }
    public void SetUsableTrue()
    {
        foreach (var item in buttons)
        {
            item.Usable = true ;
        }
    }
    //  public void Use(Skill skill)
    //  {
    //      foreach (var item in buttons)
    //      {
    //          if (item.skill == skill.ID)
    //          {
    //              item.SetCooldown(skill);
    //          }
    //      }
    //  }
    // Update is called once per frame
    void Update()
    {
         
    }
}
