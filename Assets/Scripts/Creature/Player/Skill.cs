using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public int ID;
    public List<BulletData> Bullets;
    public List<int> EffectsIds;
    public float Cooldown;
    public bool CanBeUsed = true;
    public float MPIntake;
    public float SPIntake;
    public float STIntake;
    public bool locked;
    public SkillType skillType;
    public Skill Clone()
    {
        Skill skill = new Skill();
        skill.Bullets = Bullets;
        //skill.CanBeUsed = true;
        skill.Cooldown = Cooldown;
        skill.EffectsIds = EffectsIds;
        skill.ID = ID;
        skill.MPIntake = MPIntake;
        skill.SPIntake = SPIntake;
        skill.STIntake = STIntake;       
        return skill;
    }
}
public enum SkillType {
    ZoneAttack,
    TargetAttack,
    Move,
    Defend,
    MassDefend,
    Control
}