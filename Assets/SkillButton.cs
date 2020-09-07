using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public delegate void CallbackHendler(object obj);
public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    public Player player;
    public Skill skill;
    public CallbackHendler stopOthers;
    //bool Active = false;
    bool Cooldown = false;
    public bool TurnOn = false;
    public Image ico;
    Vector3 Pos;
    public void Start()
    {
        // ico.enabled = false;
        //GetComponent<MeshRenderer>().enabled = false;
        Pos = transform.position;
        transform.position = new Vector3(0, 0, -11);
        ico = GetComponent<Image>();
    }
    // Start is called before the first frame update
    public void Set(Skill Skill)
    {
        //ico.enabled = true;
       // GetComponent<MeshRenderer>().enabled = true;
        Remove();
        gameObject.GetComponent<Image>().color = Color.yellow;
        skill = Skill;
        skill.onSkillUse += OnUse;
        skill.OnCooldownEnded = OnCooldownEnded;
        transform.position = Pos;
        //ico.sprite = Resources.Load<Sprite>("CircleBorder");
        if (skill.spriteN == -1)
        {
            Debug.Log("Pesda" + skill.ico);
            ico.sprite = Resources.Load<Sprite>(skill.ico);
        }
        else
        {
            Debug.Log("Heey"+skill.ico);
            ico.sprite = (Sprite)Resources.LoadAll(skill.ico)[skill.spriteN];
        }
    }
    public void Stop()
    {
        ico.color = Color.white;
        TurnOn = false;
    }
    public void Remove()
    {
        transform.position = new Vector3(0, 0, -11);
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
       // Active = false;
        TurnOn = false;
        ico.sprite = Resources.Load<Sprite>("CircleBorder");
        //ico.enabled = false;
    }
    public void OnUse(Skill skill)
    {

        Cooldown = true;
        ico.fillAmount = 0;
        if (TurnOn)
        {
            //  Active = true;
            Debug.Log(player.ActiveSkill.ID==skill.ID);
            player.SetActiveSkill(skill);
        }
        else
        {
         //   Active = false;
            ico.color = Color.white;
        }
 
    }    
    public void OnCooldownEnded(Skill skill)
    {
        Cooldown = false;
        ico.fillOrigin = 360;

    }
    public void Update()
    {
        if (Cooldown)
        {
           // Debug.Log(ico.fillAmount);
            ico.fillAmount += (1 / skill.Cooldown*Time.deltaTime);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        stopOthers(this);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (player.ActiveSkill == skill)
            {
                player.SetActiveSkill(skill);
                ico.color = Color.white;
                TurnOn = false;
            }
            else
            {
                 player.SetActiveSkill(skill);
                 ico.color = Color.black;
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (player.ActiveSkill == skill)
            {
                if (TurnOn)
                {
                    TurnOn = false;
                    //Active = false;
                    player.SetActiveSkill(skill);
                    ico.color = Color.white;
                }
                else
                {
                    TurnOn = true;
                    ico.color = Color.red;
                }
            }
            else
            {

                TurnOn = true;
                player.SetActiveSkill(skill);
                ico.color = Color.red;
        
            }
        }
    }

}
