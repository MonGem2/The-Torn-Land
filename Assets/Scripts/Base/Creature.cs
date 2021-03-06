﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;
public delegate void OnChangeParameterTrigger(object value);

public class Creature : MyObject
{
    // Races race { get; set; }

    #region Parameters
    public Loader loader;
    public Inventory inventory;
    public EventSystem eventSystem;
    #region MaxHP
    protected float _maxHp;
    public OnChangeParameterTrigger MaxHPChangeTrigger;
    public virtual float MaxHP
    {
        get
        {
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.MaxHP];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _maxHp * Aditional;
        }
        set
        {

            _maxHp = value;
            if (MaxHPChangeTrigger != null)
            {
                MaxHPChangeTrigger(value);
            }
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
            float AditionalMP = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    AditionalMP += item.Params[(int)ParameterChangerLD.MaxMP];
                }
            }
            if (AditionalMP <= 0.0001)
            {
                AditionalMP = 0.0001f;
            }
            if (AditionalMP > 5)
            {
                AditionalMP = 5;
            }
            return _maxMp * AditionalMP;
        }
        set
        {

            _maxMp = value;
            if (MaxMPChangeTrigger != null)
            {
                MaxMPChangeTrigger(value);
            }
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.MaxST];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _maxSt * Aditional;
        }
        set
        {
            _maxSt = value;
            if (MaxSTChangeTrigger != null)
            { MaxSTChangeTrigger(value); }
           
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.MaxSP];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _maxSp * Aditional;
        }
        set
        {
            _maxSp = value;
            if (MaxSPChangeTrigger != null) { MaxSPChangeTrigger(value); }
            
        }

    }
    #endregion
    #region HP
    public OnChangeParameterTrigger HPChangeTrigger;
    protected float hP = 100;
    public virtual float HP { get => hP; set {
            if (InfinityHP)
            {
                return;
            }


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
            if (HPChangeTrigger != null)
            { HPChangeTrigger(value); }
        } }
    #endregion
    #region MP
    public OnChangeParameterTrigger MPChangeTrigger;
    protected float mP = 100;
    public virtual float MP { get => mP; set {
            
            if (InfinityMP) {return; }
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
            if (MPChangeTrigger != null) { MPChangeTrigger(value); }
        } }
    #endregion
    #region ST
    public OnChangeParameterTrigger STChangeTrigger;
    protected float sT = 100;
    public virtual float ST { get => sT; set {

            if (InfinityST) { return; }
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
            if (STChangeTrigger != null) { STChangeTrigger(value); }
        } }
    #endregion
    #region SP
    public OnChangeParameterTrigger SPChangeTrigger;
    protected float sP = 100;
    public virtual float SP { get => sP; set { 
            if (InfinitySP) { return; }
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
            if (SPChangeTrigger != null) { SPChangeTrigger(value); }
        } }
    #endregion
    #region SumBaseDamage
    protected float _sumBaseDamage;
    public OnChangeParameterTrigger SumBaseDamageChangeTrigger;
    public virtual float SumBaseDamage
    {
        get
        {
            if (OneShot)
            {
                return 9999999;
            }
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.SumBaseDamage];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _sumBaseDamage * Aditional;
        }
        set
        {
            _sumBaseDamage = value;
            if (SumBaseDamageChangeTrigger != null)
            { SumBaseDamageChangeTrigger(value); }

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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.RegSpeedHP];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _regSpeedHP * Aditional;
        }
        set
        {
            _regSpeedHP = value;
            if (RegSpeedHPChangeTrigger != null)
            {
                RegSpeedHPChangeTrigger(value);
            }
         
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.RegSpeedMP];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _regSpeedMP * Aditional;
        }
        set
        {
            _regSpeedMP = value;
            if (RegSpeedMPChangeTrigger!=null)
            {
                RegSpeedMPChangeTrigger(value);
            }
        
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.RegSpeedST];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _regSpeedST * Aditional;
        }
        set
        {
            _regSpeedST = value;
            if (RegSpeedSTChangeTrigger != null)
            {
                RegSpeedSTChangeTrigger(value);
            }
           
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.RegSpeedSP];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _regSpeedSP * Aditional;
        }
        set
        {
            _regSpeedSP = value;
            if (RegSpeedSPChangeTrigger != null)
            {
                RegSpeedSPChangeTrigger(value);
            }
         
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.MagResist];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _magResist * Aditional;
        }
        set
        {
            _magResist = value;
            if (MagResistChangeTrigger != null)
            {
                MagResistChangeTrigger(value);
            }
          
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.PhyResisst];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _phyResist * Aditional;
        }
        set
        {
            _phyResist = value;
            if (PhyResistChangeTrigger != null)
            {
                PhyResistChangeTrigger(value);
            }
           
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
            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.SoulResist];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _soulResist * Aditional;
        }
        set
        {
            _soulResist = value;
            if (SoulResistChangeTrigger != null)
            {
                SoulResistChangeTrigger(value);
            }
           
        }

    }
    #endregion
    #region States
    protected List<State> _states = new List<State>();
    public List<State> States { get=>_states; set {  _states = value; } }
    #endregion
    #region Skills
    protected List<Skill> _skills=new List<Skill>();
    public List<Skill> Skills { get=>_skills; set {  _skills = value; } }
    #endregion
    #region Speed
    protected float _speed=1f;
    public OnChangeParameterTrigger OnSpeedChanged;
    public float Speed { get {

            float Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)ParameterChangerLD.Speed];
                }
            }
            if (Aditional <= 0.0001)
            {
                Aditional = 0.0001f;
            }
            if (Aditional > 5)
            {
                Aditional = 5;
            }
            return _speed*Aditional;
                
          } set
        {
            _speed = value;
            if (OnSpeedChanged != null)
            {
                OnSpeedChanged(value);
            }
          
        } }
    #endregion
    #region Level
    protected int _lvl = 1;
    public OnChangeParameterTrigger OnLevelChange;
    public int Lvl { get => _lvl; set {
            _lvl = value;
            Debug.LogError("WTF");
            if (OnLevelChange!=null)
            {
                OnLevelChange(value);
            }           
            } }
    #endregion
    public bool MoveLock = false;
    public bool AttackLock = false;
    public bool CanAttack = true;
    public bool InfinityHP;
    public bool InfinityMP;
    public bool InfinityST;
    public bool InfinitySP;
    public bool OneShot;
    #endregion

    #region Intake
    public override void Damage(BulletData bulletData, bool MaxEffectSet = true)
    {
      //  ////Debug.Log("y1");
        foreach (var item in States)
        {
            if (item.type == StateType.InfinityPower)
            {
                if (item.Params[0] == 1) { return; }
            }
        }
        float Damage = bulletData.ManaDamage / MagResist + bulletData.PhysicDamage / PhysResist + bulletData.SoulDamage / SoulResist;
        if (bulletData.type == BulletType.Area||bulletData.type== BulletType.Swing)
        {
            Damage *= Time.deltaTime / bulletData.AttackTimeout;
        }
     //   ////Debug.Log("y2");
        
        HP -= Damage;
   //     ////Debug.Log("y3");
    //    ////Debug.Log("My hp is:"+HP);
        if (HP < 0)
        {
       //     //Debug.Log("y4");
            Death();

        }
        if (HP > MaxHP)
        {
         //   //Debug.Log("y5");
            HP = MaxHP;
            if (MaxEffectSet)
            {
                //AddEffect(0);
            }
        }
       // //Debug.Log("y6");
        ////Debug.LogWarning("//TODO:IntakeDamage");
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

            //AddEffect(0); return false;
        }
        if (MP > MaxMP)
        {
            MP = MaxMP;
            if (MaxEffectSet)
            {
                //AddEffect(0);
            }
            return false;
        }
        return true;
        //Debug.LogWarning("//TODO:IntakeMP");
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

            //AddEffect(0); return false;
        }
        if (ST > MaxST)
        {
            ST = MaxST;
            if (MaxEffectSet)
            {
                //AddEffect(0);
            }
            return false;
        }
        return true;
        //Debug.LogWarning("//TODO:IntakeST");
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

            //AddEffect(0); return false;
        }
        if (SP > MaxSP)
        {
            SP = MaxSP;
            if (MaxEffectSet)
            {
                //AddEffect(0);
            }
            return false;
        }
        return true;
        //Debug.LogWarning("//TODO:IntakeSP");
    }
    #endregion
    #region SkillsCode
    public OnChangeParameterTrigger OnSkillAdded;
    public OnChangeParameterTrigger OnSkillRemoved;
    public void AddSkill(Skill Skill)
    {
        foreach (var item in Skills)
        {
            if (item.ID == Skill.ID)
            {
                return;
            }
        }

        Skills.Add(Skill);
        if (OnSkillAdded != null)
        {
            OnSkillAdded(Skill);
        }
    }
    public void RemoveSkill(int SkillID)
    {
    
        Skills.Remove(Skills.Find(x => x.ID == SkillID));
        if (OnSkillRemoved != null)
        {
            OnSkillRemoved(SkillID);
        }
    }
    private protected IEnumerator UseSkill(Skill skill, Vector2 targetPos)
    {

        if (!CanAttack || AttackLock)
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
       // //Debug.Log("x2");
        skill.CanBeUsed = false;

        StartCoroutine(SkillCooldownReset(skill.Cooldown, skill));
        
        IntakeMP(skill.MPIntake);
        IntakeST(skill.STIntake);
        IntakeSP(skill.SPIntake);
        AddEffects(skill.EffectsIds);
     //   //Debug.Log("x3");
        CanAttack = false;
        Vector2 vector = targetPos - (Vector2)transform.position;
        if (skill.onSkillUse != null)
        {
            skill.onSkillUse(skill);
        }
        foreach (var item in skill.Bullets)
        {
            yield return new WaitForSeconds(item.ShootPeriod);
            Bullet bullet = Instantiate(loader.BulletsPerhubs[item.PerhubID]).GetComponent<Bullet>();
            bullet.data = item.Clone();
            ////Debug.Log("My damage:  " + item.ManaDamage + "  " + item.SoulDamage + "  " + item.PhysicDamage);
            bullet.data.ManaDamage *= SumBaseDamage;
            bullet.data.SoulDamage *= SumBaseDamage;
            bullet.data.PhysicDamage *= SumBaseDamage;
            ////Debug.Log("My damage:  "+item.ManaDamage+"  "+item.SoulDamage+"  "+item.PhysicDamage);
            if (!item.SelfAttack)
            {
        //        //Debug.Log("123456789");
                bullet.User = transform;
            }
          //  //Debug.LogWarning("qweryu:   "+item.Binded);
            if (item.Binded)
            {

                bullet.Shoot(transform.position, vector.normalized, transform);
            }
            else
            {
            //    //Debug.Log("-qweww8790-----------");
                bullet.Shoot(transform.position, targetPos);
            }

        }
        CanAttack = true;
  //      //Debug.Log("x4");

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
    protected bool PointerOnObject(string Tags)
    {
        bool CanShoot = true;
        // //Debug.Log(EventSystem.current.lastSelectedGameObject.tag);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            var pd = new PointerEventData(eventSystem) { position = Input.mousePosition };
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(pd, results);
            foreach (var result in results)
            {
                if (Tags.Contains(result.gameObject.tag))
                {
                    ////Debug.Log(result.gameObject.tag);
                    return true;
                }
            }
        }
        return false;
    }
    protected bool PointerOnObject(string Tags, Vector3 point)
    {
        //Debug.Log("PointerOnObject 1");
        bool CanShoot = true;
        // //Debug.Log(EventSystem.current.lastSelectedGameObject.tag);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("PointerOnObject 2");
            //Debug.Log(point);
            //Debug.Log(Camera.main.WorldToScreenPoint(point));
            //Debug.Log(Camera.main.ScreenToWorldPoint(point));
            //Debug.Log("Pointer cursor "+ (Input.mousePosition));
            //Debug.Log("Pointer cursor " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            var pd = new PointerEventData(eventSystem) { position =Camera.main.WorldToScreenPoint(point) };
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(pd, results);
            //Debug.Log("PointerOnObject 3  "+results.Count);
            foreach (var result in results)
            {
                //Debug.Log("PointerOnObject  "+result.gameObject.tag);
                if (Tags.Contains(result.gameObject.tag))
                {
                    ////Debug.Log(result.gameObject.tag);
                    return true;
                }
            }
        }
        return false;
    }
    public OnChangeParameterTrigger TeleporteInObject;
    public OnChangeParameterTrigger OnEffectMove;
    #region StatesCode
    public virtual IEnumerator Move(State state)
    {


        //Debug.Log("Creature:Move 1");
        if (state.Params[(int)MoveLD.type] == (int)MoveTypeLD.Teleportation&&!MoveLock)
        {
            //Debug.Log("Creature:Move 6");
            bool MoveLocked = !MoveLock;
            bool AttackLocked = !AttackLock;
            MoveLock = true;
            AttackLock = true;
            Vector3 endPos=new Vector3(); //
            if (state.Params[(int)MoveLD.directionType] == (int)MoveDirectionTypeLD.staticPoint)
            {
                endPos = new Vector3(state.Params[(int)MoveLD.Direction_x], state.Params[(int)MoveLD.Direction_y], transform.position.z);
            }
            
            if (state.Params[(int)MoveLD.directionType] == (int)MoveDirectionTypeLD.InDirection)
            {
                endPos = transform.position + (new Vector3(state.Params[(int)MoveLD.Direction_x], state.Params[(int)MoveLD.Direction_y])).normalized * state.Params[(int)MoveLD.distance_damagemodifier];
            }
            if (state.Params[(int)MoveLD.directionType] == (int)MoveDirectionTypeLD.InDirectionOfMouse)
            {
                
                if (PointerOnObject("undestruct"))
                {
                    if (TeleporteInObject != null)
                    {
                        TeleporteInObject(endPos);
                    }
                    if (MoveLocked)
                    {
                        MoveLock = false;
                    }
                    if (AttackLocked)
                    {
                        AttackLock = false;
                    }
                    yield break;
                }
                endPos = transform.position + (Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized +(new Vector3(state.Params[(int)MoveLD.Direction_x], state.Params[(int)MoveLD.Direction_y])).normalized).normalized * state.Params[(int)MoveLD.distance_damagemodifier];

            }
            if (state.Params[(int)MoveLD.directionType] == (int)MoveDirectionTypeLD.to)
            {
                endPos = transform.position + new Vector3(state.Params[(int)MoveLD.Direction_x], state.Params[(int)MoveLD.Direction_y]);
            }
            if (state.Params[(int)MoveLD.directionType] == (int)MoveDirectionTypeLD.toMouse)
            {
                endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPos.z = -1;
            }
            //Debug.Log("087654321");
            if ( PointerOnObject("undestruct", endPos))
            {
                if (TeleporteInObject!=null)
                {
                    TeleporteInObject(endPos);
                }
                if (MoveLocked)
                {
                    MoveLock = false;
                }
                if (AttackLocked)
                {
                    AttackLock = false;
                }
                yield break;
            }


            SpaceVortex spaceVortex1 = Instantiate(Resources.Load<GameObject>("SpaceVortexPerhub")).GetComponent<SpaceVortex>();
            //Debug.Log(spaceVortex1);
            spaceVortex1.Set(this, state.Params[(int)MoveLD.time], state.Params[(int)MoveLD.distance_damagemodifier], transform.position);

            SpaceVortex spaceVortex2 = Instantiate(Resources.Load<GameObject>("SpaceVortexPerhub")).GetComponent<SpaceVortex>();
            //Debug.Log(spaceVortex2);
            spaceVortex2.Set(this, state.Params[(int)MoveLD.time], state.Params[(int)MoveLD.distance_damagemodifier], endPos);

            if (OnEffectMove != null)
            {
                OnEffectMove(endPos);
            }
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(state.Params[(int)MoveLD.time]);



            transform.position = endPos;
            if (MoveLocked)
            {
                MoveLock = false;
            }
            if (AttackLocked)
            {
                AttackLock = false;
            }
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        //if (state.Params[(int)MoveLD.type] == (int)MoveTypeLD.PhysicPower)
        //{
        //    GetComponent<Rigidbody>().AddForce(-transform.forward * 5, ForceMode.Force);
        //
        //}


    }
    public virtual void AddEffects(List<State> Effects)
    {
        foreach (var item in Effects)
        {
            //Debug.LogError("Pexda "+item.ID);
            StartCoroutine(StateAdder(item));
        }
    }
    public virtual void AddEffect(State Effect)
    {
        StartCoroutine(StateAdder(Effect));
        
    }
    public OnChangeParameterTrigger OnStateAdded;
    public OnChangeParameterTrigger OnStateEnded;
    public IEnumerator StateAdder(State state)
    {
        
//        ////Debug.Log(state.type);
        foreach (var item in States)
        {
            if (item.ID == state.ID)
            {
                yield break;
            }
        }
//        ////Debug.Log("Creature:State adder:spell:"+state.ID);
        if (state.type == StateType.ParameterAdder)
        {
            ////Debug.Log("Creature:ParameterAdder triggered");
            this.HP += state.Params[(int)ParameterAdderLD.HP];
            this.MP += state.Params[(int)ParameterAdderLD.MP];
            this.SP += state.Params[(int)ParameterAdderLD.SP];            
            this.ST += state.Params[(int)ParameterAdderLD.ST];
            yield break;
        }
        if (state.type == StateType.Move)
        {
            ////Debug.Log("Creature:Move triggered");
            StartCoroutine(Move(state));
            yield break;
        }

        if (OnStateAdded != null)
        {
            OnStateAdded(state);
        }
        if (state.type == StateType.PlayerParameterAdder)
        {
            yield break;
        }
        States.Add(state);
        if (state.type == StateType.SkillHider)
        {
            SkillHiderOn(state);
            if (state.Duration == -1)
            {
                yield break;
            }   
            yield return new WaitForSeconds(state.Duration);
            if (OnStateEnded != null)
            {
                OnStateEnded(state);
            }
            SkillHiderOff(state);
            States.Remove(state);
            yield break;
        }
        if (state.type == StateType.SkillAdder)
        {
            SkillAdderOn(state);
            if (state.Duration == -1)
            {
                yield break;
            }
            yield return new WaitForSeconds(state.Duration);
            if (OnStateEnded != null)
            {
                OnStateEnded(state);
            }
            SkillAdderOff(state);
            States.Remove(state);
            yield break;
        }
        if (state.type == StateType.DazerMovement)
        {
            ////Debug.Log("Dazer start");
            MoveLock = true;
            if (state.Duration == -1)
            {
                yield break;
            }
            yield return new WaitForSeconds(state.Duration);
            if (OnStateEnded != null)
            {
                OnStateEnded(state);
            }
            MoveLock = false;
            ////Debug.Log("Dazer end");
            States.Remove(state);
            yield break;
        }
        if (state.type == StateType.DazerAttack)
        {
            ////Debug.Log("Dazer attack start");
            AttackLock = true;
            if (state.Duration == -1)
            {
                yield break;
            }
            ////Debug.LogError(state.Duration);
            yield return new WaitForSeconds(state.Duration);
            if (OnStateEnded != null)
            {
                OnStateEnded(state);
            }
            AttackLock = false;
            ////Debug.Log("Dazer attack end");
            States.Remove(state);
            yield break;
        }
        if (state.type == StateType.InfinityPower)
        {
            ////Debug.Log("infinity power start");
            InfinityPowerON(state);
            if (state.Duration == -1)
            {
                yield break;
            }
            yield return new WaitForSeconds(state.Duration);
            if (OnStateEnded != null)
            {
                OnStateEnded(state);
            }

            InfinityPowerOFF(state);
            ////Debug.Log("infinity power end");
            States.Remove(state);
            yield break;
        }
        if (state.type == StateType.ParameterChanger)
        {
            if (MaxHPChangeTrigger != null)
            {
                MaxHPChangeTrigger(MaxHP);
            }
            if (MaxMPChangeTrigger != null)
            {
                MaxMPChangeTrigger(MaxMP);
            }
            if (MaxSTChangeTrigger != null)
            {
                MaxSTChangeTrigger(MaxST);
            }
            if (MaxSPChangeTrigger != null)
            {
                MaxSPChangeTrigger(MaxSP);
            }
            if (this.SumBaseDamageChangeTrigger != null)
            {
                SumBaseDamageChangeTrigger(SumBaseDamage);
            }
            if (RegSpeedMPChangeTrigger != null)
            {
                RegSpeedMPChangeTrigger(RegSpeedMP);
            }
            if (RegSpeedSTChangeTrigger != null)
            {
                RegSpeedSTChangeTrigger(RegSpeedST);
            }
            if (RegSpeedSPChangeTrigger != null)
            {
                RegSpeedSPChangeTrigger(RegSpeedSP);
            }
            if (MagResistChangeTrigger != null)
            {
                MagResistChangeTrigger(MagResist);
            }
            if (PhyResistChangeTrigger != null)
            {
                PhyResistChangeTrigger(PhysResist);
            }
            if (SoulResistChangeTrigger != null)
            {
                SoulResistChangeTrigger(SoulResist);
            }
            if (OnSpeedChanged != null)
            {
                OnSpeedChanged(Speed);
            }
        }
        if (state.Duration == -1)
        {
            yield break;
        }
        ////Debug.LogError(state.Duration+"  "+state.type);
        yield return new WaitForSeconds(state.Duration);
        States.Remove(state);
        if (state.type == StateType.ParameterChanger)
        {
            if (MaxHPChangeTrigger != null)
            {
                MaxHPChangeTrigger(MaxHP);
            }
            if (MaxMPChangeTrigger != null)
            {
                MaxMPChangeTrigger(MaxMP);
            }
            if (MaxSTChangeTrigger != null)
            {
                MaxSTChangeTrigger(MaxST);
            }
            if (MaxSPChangeTrigger != null)
            {
                MaxSPChangeTrigger(MaxSP);
            }
            if (this.SumBaseDamageChangeTrigger != null)
            {
                SumBaseDamageChangeTrigger(SumBaseDamage);
            }
            if (RegSpeedMPChangeTrigger != null)
            {
                RegSpeedMPChangeTrigger(RegSpeedMP);
            }
            if (RegSpeedSTChangeTrigger != null)
            {
                RegSpeedSTChangeTrigger(RegSpeedST);
            }
            if (RegSpeedSPChangeTrigger != null)
            {
                RegSpeedSPChangeTrigger(RegSpeedSP);
            }
            if (MagResistChangeTrigger != null)
            {
                MagResistChangeTrigger(MagResist);
            }
            if (PhyResistChangeTrigger != null)
            {
                PhyResistChangeTrigger(PhysResist);
            }
            if (SoulResistChangeTrigger != null)
            {
                SoulResistChangeTrigger(SoulResist);
            }
            if (OnSpeedChanged != null)
            {
                OnSpeedChanged(Speed);
            }
        }
        if (OnStateEnded != null)
        {
            OnStateEnded(state);
        }
        ////Debug.LogError("pizdec Ended");

    }
    protected void InfinityPowerON(State state)
    {
        if (state.Params[(int)InfinityPowerLD.InfinityHP] == 1)
        {
            InfinityHP = true;
        }
        if (state.Params[(int)InfinityPowerLD.InfinityMP] == 1)
        {
            InfinityMP = true;
        }
        if (state.Params[(int)InfinityPowerLD.InfinitySP] == 1)
        {
            InfinitySP = true;
        }
        if (state.Params[(int)InfinityPowerLD.InfinityST] == 1)
        {
            InfinityST = true;
        }
        if (state.Params[(int)InfinityPowerLD.OneShot] == 1)
        {
            OneShot = true;
        }
    }
    protected void InfinityPowerOFF(State state)
    {
        if (state.Params[(int)InfinityPowerLD.InfinityHP] == 1)
        {
            InfinityHP = false;
        }
        if (state.Params[(int)InfinityPowerLD.InfinityMP] == 1)
        {
            InfinityMP = false;
        }
        if (state.Params[(int)InfinityPowerLD.InfinitySP] == 1)
        {
            InfinitySP = false;
        }
        if (state.Params[(int)InfinityPowerLD.InfinityST] == 1)
        {
            InfinityST = false;
        }
        if (state.Params[(int)InfinityPowerLD.OneShot] == 1)
        {
            OneShot = false;
        }
    }
    protected void CycleState()
    { 
        
    }
    protected void SkillAdderOn(State state)
    {
        foreach (var item in state.Params)
        {
            ////Debug.Log(item);
            ////Debug.Log(loader.Skills[(int)item]);
            AddSkill(loader.Skills[(int)item].Clone());

        }

    }
    protected void SkillAdderOff(State state)
    {
        foreach (var item in state.Params)
        {
            RemoveSkill((int)item);

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
            //////Debug.Log(HP+"      "+MaxHP);
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
                    Magreg = item.Params[(int)RegenerationStopLD.Magreg] ==1;
                    HPReg = item.Params[(int)RegenerationStopLD.HPReg] == 1;
                    STReg = item.Params[(int)RegenerationStopLD.STReg] == 1;
                    SPReg = item.Params[(int)RegenerationStopLD.SPReg] == 1;

                }
            }
            if (!Magreg && MP != MaxMP)
            {
                IntakeMP(-RegSpeedMP * Timeout, false);
            }
            if (!HPReg && HP != MaxHP)
            {
                
                HP += RegSpeedHP * Timeout*3 / (MagResist + SoulResist + PhysResist);
                if (HP > MaxHP)
                { HP = MaxHP; }
            }
            if (!STReg && ST != MaxST)
            {
                IntakeST(-RegSpeedST * Timeout, false);
            }
            if (!SPReg && SP != MaxSP)
            {
                IntakeSP(-RegSpeedSP * Timeout, false);
            }
        }
    }
    #endregion
    protected virtual void Death()
    {
        ////Debug.Log("12345678");
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
    public float GetSpeed()
    {
        return _speed;
    }
    #endregion
    //public float CraftTalent;
    //public float PhysicalFightTalent;
    //public float ManaFightTalent;
    //public float GodlessTalent;


}
