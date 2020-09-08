using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSkillUse(Skill skill);
public delegate void OnSkillCooldownEnded(Skill skill);
public class Skill
{
    public OnSkillUse onSkillUse;
    public OnSkillCooldownEnded OnCooldownEnded;
    public int ID;
    public List<BulletData> Bullets;
    public List<State> EffectsIds;
    public float Cooldown;
    public bool CanBeUsed = true;
    public float MPIntake;
    public float SPIntake;
    public float STIntake;
    public bool locked;
    public float Range;
    public string ico;
    public int spriteN=-1;
    public GeneralSkillType skillType;
    public SkillParameterType MPParameter;
    public SkillParameterType SPParameter;
    public SkillParameterType STParameter;
    public SkillBehaviourType SkillBehaviourType;
    public Skill Clone()
    {
        Skill skill = new Skill();
        skill.Bullets = new List<BulletData>();
        foreach (var item in Bullets)
        {
            skill.Bullets.Add(item.Clone());
        }
         
        //skill.CanBeUsed = true;
        skill.Cooldown = Cooldown;
        skill.EffectsIds = EffectsIds;
        skill.ID = ID;
        skill.MPIntake = MPIntake;
        skill.SPIntake = SPIntake;
        skill.STIntake = STIntake;
        skill.skillType = skillType;
        skill.MPParameter = MPParameter;
        skill.SPParameter = SPParameter;
        skill.STParameter = STParameter;
        skill.SkillBehaviourType = SkillBehaviourType;
        skill.ico = ico;
        skill.spriteN = spriteN;
        return skill;
    }
}
public enum GeneralSkillType {
  
    TargetAttack=0,
    Heal,
    Defend,
    ZoneAttack,
    Buff,    
    Control,
    Move,
    RegParams,

}
public enum SkillParameterType { 
    None=0,
    Litle,
    Avarage,
    Large
}
public enum SkillBehaviourType{
    None=0,
    RunAway,    
    DeefDefend,
    DefendAttack,
    Attack,
    FuriousAttack
}