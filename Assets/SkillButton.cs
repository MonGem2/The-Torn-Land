using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Player player;
    public int skill=-1;
    bool Cooldown = false;
    Coroutine CooldownReset_;
    // Start is called before the first frame update
    public void Set(int Skill)
    {
        Remove();
        gameObject.GetComponent<Image>().color = Color.yellow;
        skill = Skill;
        Cooldown = false;
        CooldownReset_ = null;
    }
    public void Remove()
    {
        gameObject.GetComponent<Image>().color = Color.white;
        skill = -1;
        Cooldown = false;
        CooldownReset_ = null;
    }
    public void OnClick() {
        
        if (skill != -1&&!Cooldown)
        {

            Debug.Log("Call a method");
            if (!player.SetActiveSkillByID(skill))
            {
                skill = -1;
            }
            else {
                gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }
    public void Deactivate()
    {
        if (CooldownReset_ != null)
        {
            StopCoroutine(CooldownReset_);
        }
        Cooldown = true;
    }
    public void Activate()
    {
        Cooldown = false;       
    }
    public void SetCooldown(float time)
    {
        gameObject.GetComponent<Image>().color = Color.red;
        CooldownReset_ = StartCoroutine(CooldownReset(time));
    }
    IEnumerator CooldownReset(float time)
    {
        gameObject.GetComponent<Image>().color = Color.red;
        Cooldown = true;
        yield return new WaitForSeconds(time);
        Cooldown = false;
        CooldownReset_ = null;
        gameObject.GetComponent<Image>().color = Color.black;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
