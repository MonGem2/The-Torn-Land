using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    public Player player;
    public Skill skill;
    bool Cooldown = false;
    bool TurnOn = false;
    // Start is called before the first frame update
    public void Set(Skill Skill)
    {
        Remove();
        gameObject.GetComponent<Image>().color = Color.yellow;
        skill = Skill;
        Cooldown = false;
        skill.onSkillUse += OnUse;
        skill.OnCooldownEnded = OnCooldownEnded;
        
    }
    public void Remove()
    {
        gameObject.GetComponent<Image>().color = Color.white;
        if (skill != null)
        {
            if (skill.onSkillUse != null)
            {
                skill.onSkillUse -= OnUse;
            }
            if (skill.OnCooldownEnded != null)
            {
                skill.OnCooldownEnded -= OnCooldownEnded;
            }
        }
        skill = null;
        Cooldown = false;
        
    }
    public void OnUse(Skill skill)
    {   
        gameObject.GetComponent<Image>().color = Color.blue;
        Cooldown = true;
    }
    public void OnCooldownEnded(Skill skill)
    {
        if (TurnOn)
        {
            gameObject.GetComponent<Image>().color = Color.black;
            if (!player.SetActiveSkill(skill))
            { 
                Remove();
            }
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        Cooldown = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (skill != null && !Cooldown)
        {
            if (!player.SetActiveSkill(skill))
            {
                skill = null;
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.black;
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
            TurnOn = !TurnOn;
    }
    public void Deactivate()
    {
        
        Cooldown = true;
    }
    public void Activate()
    {
        Cooldown = false;       
    }
    public void SetCooldown(float time)
    {
        gameObject.GetComponent<Image>().color = Color.red;
        
    }
    IEnumerator CooldownReset(float time)
    {
        gameObject.GetComponent<Image>().color = Color.red;
        Cooldown = true;
        yield return new WaitForSeconds(time);
        Cooldown = false;
        
        gameObject.GetComponent<Image>().color = Color.black;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
