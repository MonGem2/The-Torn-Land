using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;
public delegate void OnChangeParameterTrigger(float value);

public class Creature : MonoBehaviour
{
    // Races race { get; set; }

    #region Parameters
    public Loader loader;
    public Inventory inventory;
    #region MaxHP
    protected float _maxHp;
    public OnChangeParameterTrigger MaxHPChangeTrigger;
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
            if (MaxHPChangeTrigger != null)
            {
                MaxHPChangeTrigger(value);
            }
            _maxHp = value;
        }

    }
    #endregion
    #region MaxMP
    public OnChangeParameterTrigger MaxMPChangeTrigger;
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
            if (MaxMPChangeTrigger != null)
            {
                MaxMPChangeTrigger(value);
            }
            _maxMp = value;
        }

    }
    #endregion
    #region MaxST
    public OnChangeParameterTrigger MaxSTChangeTrigger;
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
            if (MaxSTChangeTrigger != null)
            { MaxSTChangeTrigger(value); }
            _maxSt = value;
        }

    }
    #endregion
    #region MaxSP
    public OnChangeParameterTrigger MaxSPChangeTrigger;
    protected float _maxSp;
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
            if (MaxSPChangeTrigger != null) { MaxSPChangeTrigger(value); }
            _maxSp = value;
        }

    }
    #endregion
    #region HP
    public OnChangeParameterTrigger HPChangeTrigger;
    protected float hP = 100;
    public virtual float HP { get => hP; set {
            if (HPChangeTrigger != null)
            { HPChangeTrigger(value); }
            
            if (value > MaxHP)
            {
                hP = MaxHP;
            }
            else if (value < 0)
            {
                hP = 0;
                Death();
            }
            else
            {
                hP = value;
            }
        } }
    #endregion
    #region MP
    public OnChangeParameterTrigger MPChangeTrigger;
    protected float mP = 100;
    public virtual float MP { get => mP; set {
            if (MPChangeTrigger != null) { MPChangeTrigger(value); }
            if (value > MaxMP)
            {
                mP = MaxMP;
            }
            else if (value<0)
            {
                mP = 0;
            }
            else
            {
                mP = value;
            }
        
        } }
    #endregion
    #region ST
    public OnChangeParameterTrigger STChangeTrigger;
    protected float sT = 100;
    public virtual float ST { get => sT; set {
            if (STChangeTrigger != null) { STChangeTrigger(value); }
            sT = value;
            if (value > MaxST)
            {
                sT = MaxST;
            }
            else if (value < 0)
            {
                sT = 0;                
            }
            else
            {
                sT = value;
            }
        } }
    #endregion
    #region SP
    public OnChangeParameterTrigger SPChangeTrigger;
    protected float sP = 100;
    public virtual float SP { get => sP; set { if (SPChangeTrigger != null) { SPChangeTrigger(value); }
            if (value > MaxSP)
            {
                sP = MaxSP;
            }
            else if (value < 0)
            {
                sP = 0;
                
            }
            else
            {
                sP = value;
            }

        } }
    #endregion
    #region SumBaseDamage
    protected float _sumBaseDamage;
    public OnChangeParameterTrigger SumBaseDamageChangeTrigger;
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
            if (SumBaseDamageChangeTrigger != null)
            { SumBaseDamageChangeTrigger(value); }
            _sumBaseDamage = value;
        }

    }
    #endregion
    #region RegSpeedHP
    public OnChangeParameterTrigger RegSpeedHPChangeTrigger;
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
            if (RegSpeedHPChangeTrigger != null)
            {
                RegSpeedHPChangeTrigger(value);
            }
            _regSpeedHP = value;
        }

    }
    #endregion
    #region RegSpeedMP
    public OnChangeParameterTrigger RegSpeedMPChangeTrigger;
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
            if (RegSpeedMPChangeTrigger!=null)
            {
                RegSpeedMPChangeTrigger(value);
            }
            _regSpeedMP = value;
        }

    }
    #endregion
    #region RegSpeedST
    public OnChangeParameterTrigger RegSpeedSTChangeTrigger;
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
            if (RegSpeedSTChangeTrigger != null)
            {
                RegSpeedSTChangeTrigger(value);
            }
            _regSpeedST = value;
        }

    }
    #endregion
    #region RegSpeedSP
    public OnChangeParameterTrigger RegSpeedSPChangeTrigger;
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
            if (RegSpeedSPChangeTrigger != null)
            {
                RegSpeedSPChangeTrigger(value);
            }
            _regSpeedSP = value;
        }

    }
    #endregion
    #region MagResist
    public OnChangeParameterTrigger MagResistChangeTrigger;
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
            if (MagResistChangeTrigger != null)
            {
                MagResistChangeTrigger(value);
            }
            _magResist = value;
        }

    }
    #endregion
    #region PhysResist
    public OnChangeParameterTrigger PhyResistChangeTrigger;
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
            if (PhyResistChangeTrigger != null)
            {
                PhyResistChangeTrigger(value);
            }
            _phyResist = value;
        }

    }
    #endregion
    #region SoulResist
    public OnChangeParameterTrigger SoulResistChangeTrigger;
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
            if (SoulResistChangeTrigger != null)
            {
                SoulResistChangeTrigger(value);
            }
            _soulResist = value;
        }

    }
    #endregion
    #region States
    protected List<State> _states = new List<State>();
    public List<State> States { get=>_states; set { UnityEngine.Debug.Log("hheeyy"); _states = value; } }
    #endregion
    #region Skills
    protected List<Skill> _skills=new List<Skill>();
    public List<Skill> Skills { get=>_skills; set {  _skills = value; } }
    #endregion
    #region Speed
    protected float _speed=1f;
    public float Speed { get=> _speed; set=>_speed=value; }
    #endregion
    public bool MoveLock = false;
    public bool AtackLock = false;
    public bool CanAttack = true;
    #endregion
    protected void Start()
    {
        StartCoroutine(Regeneration(5));
    }
    #region Intake
    public virtual void Damage(BulletData bulletData, bool MaxEffectSet = true)
    {
      //  Debug.Log("y1");
        foreach (var item in States)
        {
            if (item.type == StateType.InfinityPower)
            {
                if (item.Params[0] == 1) { return; }
            }
        }
     //   Debug.Log("y2");
        float Damage = bulletData.ManaDamage / MagResist + bulletData.PhysicDamage / PhysResist + bulletData.SoulDamage / SoulResist;
        HP -= Damage;
   //     Debug.Log("y3");
    //    Debug.Log("My hp is:"+HP);
        if (HP < 0)
        {
       //     Debug.Log("y4");
            Death();

        }
        if (HP > MaxHP)
        {
         //   Debug.Log("y5");
            HP = MaxHP;
            if (MaxEffectSet)
            {
                AddEffect(0);
            }
        }
       // Debug.Log("y6");
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
    #endregion
    #region SkillsCode
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

        if (!CanAttack || AtackLock)
        {
            yield break;
        }
        if (!skill.CanBeUsed || skill.locked)
        {
            yield break;
        }
        if (MP < skill.MPIntake || SP < skill.SPIntake || ST < skill.STIntake)
        {
            yield break;
        }
       // Debug.Log("x2");
        skill.CanBeUsed = false;
        if (skill.onSkillUse != null)
        {
            skill.onSkillUse(skill);
        }
        StartCoroutine(SkillCooldownReset(skill.Cooldown, skill));
        
        IntakeMP(skill.MPIntake);
        IntakeST(skill.STIntake);
        IntakeSP(skill.SPIntake);
        AddEffects(skill.EffectsIds);
     //   Debug.Log("x3");
        CanAttack = false;
        Vector2 vector = targetPos - (Vector2)transform.position;
        foreach (var item in skill.Bullets)
        {
            yield return new WaitForSeconds(item.ShootPeriod);
            Bullet bullet = Instantiate(loader.BulletsPerhubs[item.PerhubID]).GetComponent<Bullet>();
            bullet.data = item.Clone();
            //Debug.Log("My damage:  " + item.ManaDamage + "  " + item.SoulDamage + "  " + item.PhysicDamage);
            bullet.data.ManaDamage *= SumBaseDamage;
            bullet.data.SoulDamage *= SumBaseDamage;
            bullet.data.PhysicDamage *= SumBaseDamage;
            //Debug.Log("My damage:  "+item.ManaDamage+"  "+item.SoulDamage+"  "+item.PhysicDamage);
            if (!item.SelfAttack)
            {
        //        Debug.Log("123456789");
                bullet.User = transform;
            }
          //  Debug.LogWarning("qweryu:   "+item.Binded);
            if (item.Binded)
            {

                bullet.Shoot(transform.position, vector.normalized, transform);
            }
            else
            {
            //    Debug.Log("-qweww8790-----------");
                bullet.Shoot(transform.position, targetPos);
            }

        }
        CanAttack = true;
  //      Debug.Log("x4");

    }
    private protected IEnumerator SkillCooldownReset(float Cooldown, Skill skill)
    {
        yield return new WaitForSeconds(Cooldown);
        skill.CanBeUsed = true;
        if (skill.OnCooldownEnded != null)
        {
            skill.OnCooldownEnded(skill);
        }
    }
    #endregion
    #region StatesCode

    public virtual void AddEffects(List<int> EffectIds)
    {
        Debug.LogWarning("//TODO:AddEffect");
    }
    public virtual void AddEffect(int EffectId)
    {
        Debug.LogWarning("//TODO:AddEffect");
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
    #endregion
    #region Regeneration
    public EventHandler RegenerationTriggered;
    protected virtual IEnumerator Regeneration(float Timeout)
    {

        while (true)
        {
            //Debug.Log(HP+"      "+MaxHP);
            yield return new WaitForSeconds(Timeout);
            if (RegenerationTriggered != null)
            {
                RegenerationTriggered(Timeout, null);
            }
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
    #endregion
    protected virtual void Death()
    {
        Debug.Log("12345678");
        Destroy(gameObject);
    }
    #region GetCharacter
    public float GetMaxHP()
    {
        return _maxHp;
    }
    public float GetMaxMP()
    {
        return _maxMp;
    }
    public float GetMaxSP()
    {
        return _maxSp;
    }
    public float GetMaxST()
    {
        return _maxSt;
    }
    public float GetMaxSumBaseDamage()
    {
        return _sumBaseDamage;
    }
    public float GetHPRegSpeed()
    {
        return _regSpeedHP;
    }
    public float GetSPRegSpeed()
    {
        return _regSpeedSP;
    }
    public float GetSTRegSpeed()
    {
        return _regSpeedST;
    }
    public float GetMPRegSpeed()
    {
        return _regSpeedMP;
    }
    public float GetMagReist()
    {
        return _magResist;
    }
    public float GetPhyResist()
    {
        return _phyResist;
    }
    public float GetSoulResist()
    {
        return _soulResist;
    }
    #endregion
    //public float CraftTalent;
    //public float PhysicalFightTalent;
    //public float ManaFightTalent;
    //public float GodlessTalent;


}
