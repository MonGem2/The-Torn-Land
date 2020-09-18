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
    public bool Usable = true;
    public bool Hiden = false;
    // Vector3 Pos;
    public void Start()
    {
        // ico.enabled = false;
        //GetComponent<MeshRenderer>().enabled = false;
        // Pos = transform.position;
        if (Usable)
        {
            Hide();
        }
        ico = GetComponent<Image>();
        if (player != null)
        {
            player.OnSkillRemoved += OnSkillRemove;
        }

    }
    public void Hide() {
        if (!Hiden)
        { transform.localPosition = transform.localPosition + new Vector3(0, 0, -99); Hiden = true; }
    }
    public void Show() {
        if (Hiden)
        { transform.localPosition = transform.localPosition + new Vector3(0, 0, 99); Hiden = false; }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        player.OnSkillRemoved += OnSkillRemove;
    }
    public void OnSkillRemove(object value)
    {
        if (skill == null)
        {
            return;
        }
        if ((int)value == skill.ID)
        {
            Debug.Log("SkillButton:remove");
            Remove();
        }
    }
    // Start is called before the first frame update
    public void Set(Skill Skill, bool usable=true)
    {
        Usable = usable;
        //ico.enabled = true;
       // GetComponent<MeshRenderer>().enabled = true;
        Remove();
        if (Usable)
        {
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        skill = Skill;
        skill.onSkillUse += OnUse;
        skill.OnCooldownEnded += OnCooldownEnded;
        if (Usable)
        {
            Show();
        }

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
        Debug.Log("Removing skill");
        if (Usable)
        {
            Hide();
        }
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
        if (ico == null)
        {
            ico = GetComponent<Image>();
        }
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
        ico.fillAmount = 1;
        Debug.LogWarning("HEEEYYY");

    }
    public void Update()
    {
        if (Cooldown)
        {
           // Debug.Log(ico.fillAmount);
            ico.fillAmount += (1 / skill.Cooldown*Time.deltaTime);
        }
    }
    public OnChangeParameterTrigger OnClick;
    public OnChangeParameterTrigger OnRightClick;
    public void OnButtonClick()
    {
        Debug.Log("Pizdec");
        if (OnClick != null)
        {
            OnClick(this);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pezd");
        if (Usable)
        {
            if (stopOthers != null)
            {
                stopOthers(this);
            }
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
                if (OnRightClick != null)
                {
                    OnRightClick(this);
                }
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
        else
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (OnRightClick != null)
                {
                    OnRightClick(this);
                }
            }
        }
    }
    Color color;
    public void OnGreen()
    {
        if (ico.color != Color.green)
        {
            color = ico.color;
            ico.color = Color.green;
        }
    }
    public void OffGreen()
    {
        Debug.Log("blat "+(color==Color.green));
        ico.color = color;
    }
}
