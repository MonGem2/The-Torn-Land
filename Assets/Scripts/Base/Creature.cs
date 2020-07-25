using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    // Races race { get; set; }
    public Loader loader;
    public float MaxHP { get; set; }
    public float MaxMP { get; set; }
    public float MaxST { get; set; }
    public float MaxSP { get; set; }

    public float HP { get; set; }
    public float MP { get; set; }
    public float ST { get; set; }
    public float SP { get; set; }

    public float SumBaseDamage { get; set; }

    //public float XPBonus;
    public float HPBonus { get; set; }
    public float STBonus { get; set; }
    public float MPBonus { get; set; }
    public float SPBonus { get; set; }

    public float RegSpeedHP { get; set; }
    public float RegSpeedMP { get; set; }
    public float RegSpeedST { get; set; }
    public float RegSpeedSP { get; set; }

    public float MagResist { get; set; }
    public float PhysResist { get; set; }
    public float PsyhResist { get; set; }
    public float SoulResist { get; set; }
    public List<Skill> Skills;
    public Skill ActiveSkill;
    public virtual void AddEffects(List<int> EffectIds)
    {
        Debug.LogWarning("//TODO:AddEffect");
    }
    public virtual void AddEffect(int EffectId)
    {
        Debug.LogWarning("//TODO:AddEffect");
    }
    public virtual void Damage(BulletData bulletData)
    {
        Debug.LogWarning("//TODO:IntakeDamage");
    }
    public void IntakeMP(float Count)
    {
        Debug.LogWarning("//TODO:IntakeMP");
    }
    public void IntakeST(float Count)
    {
        Debug.LogWarning("//TODO:IntakeST");
    }
    public void IntakeSP(float Count)
    {
        Debug.LogWarning("//TODO:IntakeSP");
    }
    private protected IEnumerator UseSkill(Skill skill, Vector2 targetPos)
    {
        if (!skill.CanBeUsed)
        {
            yield break;
        }
        skill.CanBeUsed = false;
        StartCoroutine(SkillCooldownReset(skill.Cooldown, skill));
        IntakeMP(skill.MPIntake);
        IntakeST(skill.STIntake);
        IntakeSP(skill.SPIntake);
        AddEffects(skill.EffectsIds);
        foreach (var item in skill.Bullets)
        {
            yield return new WaitForSeconds(item.ShootPeriod);
            Bullet bullet = Instantiate(loader.BulletsPerhubs[item.PerhubID]).GetComponent<Bullet>();
            bullet.data = item;
            bullet.Shoot(transform.position, targetPos);

            
        }
    }
    private protected IEnumerator SkillCooldownReset(float Cooldown, Skill skill)
    {
        yield return new WaitForSeconds(Cooldown);
        skill.CanBeUsed = true;
    }
    //public float CraftTalent;
    //public float PhysicalFightTalent;
    //public float ManaFightTalent;
    //public float GodlessTalent;


}
