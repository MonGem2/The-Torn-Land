using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    // Races race { get; set; }
    public Loader loader;
    protected float _maxHp;
    
    public virtual float MaxHP
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[0];
                }
            }
            return _maxHp * Aditional;
        }
        set
        {

            _maxHp = value;
        }

    }
    protected float _maxMp;
    public virtual float MaxMP
    {
        get
        {
            int AditionalHP = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    AditionalHP += item.Params[1];
                }
            }
            return _maxMp * AditionalHP;
        }
        set
        {
            _maxMp = value;
        }

    }
    protected float _maxSt;
    public virtual float MaxST
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[2];
                }
            }
            return _maxSt * Aditional;
        }
        set
        {
            _maxSt = value;
        }

    }
    protected  float _maxSp;
    public virtual float MaxSP
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[3];
                }
            }
            return _maxSp * Aditional;
        }
        set
        {
            _maxSp = value;
        }

    }

    protected float hP = 100;
    public virtual float HP { get => hP; set => hP = value; }
    protected float mP = 100;
    public virtual float MP { get => mP; set => mP = value; }
    protected float sT = 100;
    public virtual float ST { get => sT; set => sT = value; }
    protected float sP = 100;
    public virtual float SP { get => sP; set => sP = value; }
    protected float _sumBaseDamage;
    public virtual float SumBaseDamage
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[4];
                }
            }
            return _sumBaseDamage * Aditional;
        }
        set
        {
            _sumBaseDamage = value;
        }

    }
    protected float _regSpeedHP;
    public virtual float RegSpeedHP
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[5];
                }
            }
            return _regSpeedHP * Aditional;
        }
        set
        {
            _regSpeedHP = value;
        }

    }
    protected float _regSpeedMP;
    public virtual float RegSpeedMP
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[6];
                }
            }
            return _regSpeedMP * Aditional;
        }
        set
        {
            _regSpeedMP = value;
        }

    }
    protected float _regSpeedST;
    public virtual float RegSpeedST
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[7];
                }
            }
            return _regSpeedST * Aditional;
        }
        set
        {
            _regSpeedST = value;
        }

    }
    protected float _regSpeedSP;
    public virtual float RegSpeedSP
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[8];
                }
            }
            return _regSpeedSP * Aditional;
        }
        set
        {
            _regSpeedSP = value;
        }

    }
    protected float _magResist;
    public virtual float MagResist
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[9];
                }
            }
            return _magResist * Aditional;
        }
        set
        {
            _magResist = value;
        }

    }
    protected float _phyResist;
    public virtual float PhysResist
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[10];
                }
            }
            return _phyResist * Aditional;
        }
        set
        {
            _phyResist = value;
        }

    }
    protected float _soulResist;
    public virtual float SoulResist
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[11];
                }
            }
            return _soulResist * Aditional;
        }
        set
        {
            _soulResist = value;
        }

    }
    public bool MoveLock = false;
    public bool AtackLock = false;
    public List<State> States = new List<State>();
    public List<Skill> Skills = new List<Skill>();
    public float Speed { get; set; } = 1f;

    protected void Start()
    {
        StartCoroutine(Regeneration(5));
    }
    public virtual void AddEffects(List<int> EffectIds)
    {
        Debug.LogWarning("//TODO:AddEffect");
    }
    public virtual void AddEffect(int EffectId)
    {
        Debug.LogWarning("//TODO:AddEffect");
    }
    public virtual void Damage(BulletData bulletData, bool MaxEffectSet = true)
    {
        
        foreach (var item in States)
        {
            if (item.type == StateType.InfinityPower)
            {
                if (item.Params[0] == 1) { return; }
            }
        }

        float Damage = bulletData.ManaDamage / MagResist + bulletData.PhysicDamage / PhysResist + bulletData.SoulDamage / SoulResist;
        HP -= Damage;
        Debug.Log("My hp is:"+HP);
        if (HP < 0)
        {
            Death();

        }
        if (HP > MaxHP)
        {
            HP = MaxHP;
            if (MaxEffectSet)
            {
                AddEffect(0);
            }
        }
        //Debug.LogWarning("//TODO:IntakeDamage");
    }

    public bool IntakeMP(float Count, bool MaxEffectSet = true)
    {
        foreach (var item in States)
        {
            if (item.type == StateType.InfinityPower)
            {
                if (item.Params[1] == 1) { return false; }
            }
        }
        MP -= Count / MagResist;
        if (MP < 0)
        {
            MP = 0;

            AddEffect(0); return false;
        }
        if (MP > MaxMP)
        {
            MP = MaxMP;
            if (MaxEffectSet)
            {
                AddEffect(0);
            }
            return false;
        }
        return true;
        Debug.LogWarning("//TODO:IntakeMP");
    }
    public bool IntakeST(float Count, bool MaxEffectSet = true)
    {
        foreach (var item in States)
        {
            if (item.type == StateType.InfinityPower)
            {
                if (item.Params[2] == 1) { return false; }
            }
        }
        ST -= Count / PhysResist;
        if (ST < 0)
        {
            ST = 0;

            AddEffect(0); return false;
        }
        if (ST > MaxST)
        {
            ST = MaxST;
            if (MaxEffectSet)
            {
                AddEffect(0);
            }
            return false;
        }
        return true;
        Debug.LogWarning("//TODO:IntakeST");
    }
    public bool IntakeSP(float Count, bool MaxEffectSet = true)
    {
        foreach (var item in States)
        {
            if (item.type == StateType.InfinityPower)
            {
                if (item.Params[3] == 1) { return false; }
            }
        }
        SP -= Count / SoulResist;
        if (SP < 0)
        {
            SP = 0;

            AddEffect(0); return false;
        }
        if (SP > MaxSP)
        {
            SP = MaxSP;
            if (MaxEffectSet)
            {
                AddEffect(0);
            }
            return false;
        }
        return true;
        Debug.LogWarning("//TODO:IntakeSP");
    }
    protected void AddSkill(int SkillID)
    {
        Skills.Add(loader.Skills[SkillID].Clone());
    }
    protected void RemoveSkill(int SkillID)
    {
        Skills.Remove(Skills.Find(x => x.ID == SkillID));
    }
    private protected IEnumerator UseSkill(Skill skill, Vector2 targetPos)
    {
        if (!skill.CanBeUsed || skill.locked)
        {
            yield break;
        }
        if (MP < skill.MPIntake||SP<skill.SPIntake||ST<skill.STIntake)
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
    protected IEnumerator StateAdder(State state)
    {
        States.Add(state);
        if (state.type == StateType.SkillHider)
        {
            SkillHiderOn(state);
            yield return new WaitForSeconds(state.Duration);
            SkillHiderOff(state);
            yield break;
        }
        if (state.type == StateType.SkillAdder)
        {
            SkillAdderOn(state);
            yield return new WaitForSeconds(state.Duration);
            SkillAdderOff(state);
            yield break;
        }
        if (state.type == StateType.DazerMovement)
        {
            MoveLock = true;
            yield return new WaitForSeconds(state.Duration);
            MoveLock = false;
            yield break;
        }
        yield return new WaitForSeconds(state.Duration);
        States.Remove(state);
    }
    protected void SkillAdderOn(State state)
    {
        foreach (var item in state.Params)
        {
            AddSkill(item);

        }

    }
    protected void SkillAdderOff(State state)
    {
        foreach (var item in state.Params)
        {
            RemoveSkill(item);

        }

    }
    protected void SkillHiderOn(State state)
    {
        if (state.Params.Count == 0)
        {
            foreach (var item in Skills)
            {
                item.locked = true;
            }
        }
        foreach (var item in Skills)
        {
            if (state.Params.Contains(item.ID))
            {
                item.locked = true;
            }
        }
    }
    protected void SkillHiderOff(State state)
    {
        if (state.Params.Count == 0)
        {
            foreach (var item in Skills)
            {
                item.locked = false;
            }
        }
        foreach (var item in Skills)
        {
            if (state.Params.Contains(item.ID))
            {
                item.locked = false;
            }
        }
    }
    protected IEnumerator Regeneration(float Timeout)
    {

        while (true)
        {
            //Debug.Log(HP+"      "+MaxHP);
            yield return new WaitForSeconds(Timeout);
            bool Magreg = true, HPReg = true, STReg = true, SPReg = true;
            foreach (var item in States)
            {
                if (item.type == StateType.RegenerationStop)
                {
                    Magreg = item.Params[0] == 1 ? false : true;
                    HPReg = item.Params[1] == 1 ? false : true;
                    STReg = item.Params[2] == 1 ? false : true;
                    SPReg = item.Params[3] == 1 ? false : true;

                }
            }
            if (Magreg && MP != MaxMP)
            {
                IntakeMP(-RegSpeedMP * Timeout, false);
            }
            if (HPReg && HP != MaxHP)
            {
                
                HP += RegSpeedHP * Timeout*3 / (MagResist + SoulResist + PhysResist);
                if (HP > MaxHP)
                { HP = MaxHP; }
            }
            if (STReg && ST != MaxST)
            {
                IntakeST(-RegSpeedST * Timeout, false);
            }
            if (SPReg && SP != MaxSP)
            {
                IntakeSP(-RegSpeedSP * Timeout, false);
            }
        }
    }
    protected void Death()
    {
        Destroy(gameObject);
    }
    //public float CraftTalent;
    //public float PhysicalFightTalent;
    //public float ManaFightTalent;
    //public float GodlessTalent;


}
