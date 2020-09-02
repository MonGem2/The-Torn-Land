using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
public delegate void BehaviourChanged(int value);
public class AIForm : Creature
{
    public List<Item> Loot;
    public LootSkript LootPerhub;
    public List<Transform> Targets;
    public Transform ActiveTarget;
    public List<string> TargetTags;
    public EntityMovement movement;
    public float ChangeTargetTimeout;
    public float Exp;
    
    public SkillParameterType ManaSkillFilter=SkillParameterType.Large;
    public SkillParameterType StaminaSkillFilter = SkillParameterType.Large;
    public SkillParameterType SoulSkillFilter = SkillParameterType.Large;


    public GeneralSkillType behaviour= GeneralSkillType.TargetAttack;

    #region EntityBehaviour
    public SkillBehaviourType SituationSkill;
    public BehaviourChanged CurrentEntityTypeChanged;
    protected SkillBehaviourType _currentEntityType;
    public SkillBehaviourType CurrentEntityType { get {
            return _currentEntityType;
        }
        set {
            if (value < MinBehaviour)
            {
                _currentEntityType = MinBehaviour;
            }
            else if (value > StartEntityType)
            {
                _currentEntityType = StartEntityType;
            }
            else
            {
                _currentEntityType = value;

            }
            if (CurrentEntityTypeChanged != null)
            {
                CurrentEntityTypeChanged((int)_currentEntityType);
            }
            if(SituationSkill>CurrentEntityType)
            {
                SituationSkill = CurrentEntityType;
            }
            if (_currentEntityType == SkillBehaviourType.RunAway)
            {
                movement.behavior = Behavior.RunAway;
            }
            if (_currentEntityType == SkillBehaviourType.RunAway&&behaviour< GeneralSkillType.Move)
            {

                behaviour = GeneralSkillType.Move;
            }
            if (_currentEntityType == SkillBehaviourType.DefendAttack && behaviour < GeneralSkillType.Defend)
            {
                behaviour = GeneralSkillType.Defend;
            }
            if (_currentEntityType == SkillBehaviourType.FuriousAttack && behaviour < GeneralSkillType.Control)
            {
                behaviour = GeneralSkillType.Control;
            }
            if (_currentEntityType == SkillBehaviourType.DeefDefend&&behaviour<GeneralSkillType.Buff)           
            {
                behaviour = GeneralSkillType.Buff;
            }            
        } }
    public SkillBehaviourType StartEntityType;
    public SkillBehaviourType MinBehaviour;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        behaviour = GeneralSkillType.TargetAttack;
        StartEntityType = SkillBehaviourType.Attack;
        CurrentEntityType = StartEntityType;
        SituationSkill = CurrentEntityType;
        MinBehaviour = SkillBehaviourType.RunAway;
        this.MPChangeTrigger += (x) => {
            if (x / this.MaxMP >= 0.8)
            {
                ManaSkillFilter = SkillParameterType.Large;
                return;
            }
            if (x / this.MaxMP >= 0.4)
            {
                ManaSkillFilter = SkillParameterType.Avarage;
                return;
            }
            if (x / this.MaxMP >= 0.1)
            {
                ManaSkillFilter = SkillParameterType.Litle;
                return;
            }
            ManaSkillFilter = SkillParameterType.None;

        };
        this.STChangeTrigger += (x) => {
            if (x / this.MaxST >= 0.8)
            {
                StaminaSkillFilter = SkillParameterType.Large;
                return;
            }
            if (x / this.MaxST >= 0.4)
            {
                StaminaSkillFilter = SkillParameterType.Avarage;
                return;
            }
            if (x / this.MaxST >= 0.1)
            {
                StaminaSkillFilter = SkillParameterType.Litle;
                return;
            }
            StaminaSkillFilter = SkillParameterType.None;

        };
        this.SPChangeTrigger += (x) => {
            if (x / this.MaxSP >= 0.8)
            {
                SoulSkillFilter = SkillParameterType.Large;
                return;
            }
            if (x / this.MaxSP >= 0.4)
            {
                SoulSkillFilter = SkillParameterType.Avarage;
                return;
            }
            if (x / this.MaxSP >= 0.1)
            {
                SoulSkillFilter = SkillParameterType.Litle;
                return;
            }
            SoulSkillFilter = SkillParameterType.None;

        };
        this.HPChangeTrigger += (x) =>
        {
            if (x / MaxHP <= 0.5)
            {
                if (behaviour < GeneralSkillType.Heal)
                {
                    behaviour = GeneralSkillType.Heal;
                }
            }
            if (x / MaxHP >= 0.9)
            {
                if (behaviour == GeneralSkillType.Heal)
                {
                    behaviour = GeneralSkillType.TargetAttack;
                }
                if (StartEntityType == SkillBehaviourType.FuriousAttack&&SituationSkill== SkillBehaviourType.Attack)
                {
                    SituationSkill = SkillBehaviourType.FuriousAttack;
                    return;
                }

            }
            if (x / MaxHP >= 0.5)
            {
                if (StartEntityType > SkillBehaviourType.Attack)
                {
                    SituationSkill = SkillBehaviourType.Attack;
                    return;
                }
            }
            if (x / MaxHP >= 0.3)
            {
                if (StartEntityType > SkillBehaviourType.DefendAttack)
                {
                    SituationSkill = SkillBehaviourType.DefendAttack;
                    return;
                }
            }
            if (x / MaxHP >= 0.1)
            {
                if (StartEntityType > SkillBehaviourType.DefendAttack)
                {
                    SituationSkill = SkillBehaviourType.DeefDefend;
                    return;
                }
            }

            SituationSkill = SkillBehaviourType.RunAway;


        };
        this.MaxHP = 200;
        this.MaxMP = 200;
        this.MaxSP = 200;
        this.MaxST = 200;
        //StatsBar.SetMaxParams(this.MaxHP, 100, MaxST, 100, 100, MaxMP, MaxSP);
        this.HP = MaxHP;
        this.MP = MaxMP;
        this.ST = MaxST;
        this.SP = MaxSP;
        this.RegSpeedHP = 3;
        this.RegSpeedMP = 3;
        this.RegSpeedSP = 3;
        this.RegSpeedST = 3;
        this.SumBaseDamage = 5;
        this.MagResist = 1;
        this.PhysResist = 1;
        this.SoulResist = 1;
        this.Speed = 1;

        ChangeTargetTimeout = 10;
        loader.LoadSkills();
        Skills = new List<Skill>();
        Skills.Add(loader.Skills[0]);
        Skills.Add(loader.Skills[1]);
        Skills.Add(loader.Skills[2]);
        Skills.Add(loader.Skills[3]);
        StartCoroutine(Regeneration(1));
        StartCoroutine(TargetChanger());
        //Debug.LogWarning(loader.Skils.Count);
    }
    IEnumerator TargetChanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(ChangeTargetTimeout);
            if (!movement.Sleep)
            {
                ChangeTarget();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ActiveTarget != null)
        {
            if (CanAttack)
            {
                //UnityEngine.Debug.Log("HEEEEEYYYuyyy");
                Skill skill = SelectSkill();
                if (skill != null)
                {
                    StartCoroutine(UseSkill(skill,movement.GetCenter(ActiveTarget)));
                }
            }
            
        }
    }
    public Skill SelectSkill()
    {
        List<Skill> skills1=new List<Skill>();
        List<Skill> skills2;
        
        
        for (int i = 0; i < Skills.Count; i++)
        {
            if (SkillCanBeUsed(Skills[i]))
            {
                skills1.Add(Skills[i]);
            }
        }
        Debug.Log("Skills can be used:");
        OutSkillArray(skills1);
        if (skills1.Count == 0)
        {
            return null;
        }

        //behaviour skill select
        skills2 = skills1.Where(x => x.skillType <= behaviour).OrderBy(x => x.skillType).ToList();
        Debug.Log("Skills selected by behaviour:");
        OutSkillArray(skills2);
        if (skills2.Count == 0)
        {
            return null;   
        }
        if (skills2.Count == 1)
        {
            return skills2[0];
        }

        //Parameter skill select
        skills1 = skills2.Where(x => x.MPParameter <= ManaSkillFilter && x.STParameter <= StaminaSkillFilter && x.SPParameter <= SoulSkillFilter).ToList();
        Debug.Log("Skills selected by parameters:");
        OutSkillArray(skills1);
        if (skills1.Count == 0)
        {
            // if (behaviour < GeneralSkillType.RegParams)
            // {
            //     behaviour = GeneralSkillType.RegParams;
            // }
            //Skip parameter skill select
            skills1 = skills2;
        }
        if (skills1.Count == 1)
        {
            return skills1[0];
        }
        
        //Situattion skill select
        skills2 = skills1.Where(x => x.SkillBehaviourType >= SituationSkill).ToList();
        Debug.Log("Skills selected by sityation param:");
        OutSkillArray(skills2);
        if (skills2.Count == 0)
        {
            return skills1[Random.Range(0, skills1.Count - 1)];
        }
        else
        {
            return skills2[Random.Range(0, skills2.Count-1)];
        }
        //
        return null;
    }
    void OutSkillArray(List<Skill> skills)
    {
        string str="";
        foreach (var item in skills)
        {
            str += item.ID + "  " + item.skillType + "  " + item.SkillBehaviourType + ";";
        }
        Debug.Log(str);
    }
    bool SkillCanBeUsed(Skill skill)
    {
        if (skill.MPIntake <= MP && skill.SPIntake <= SP && skill.STIntake <= ST&&skill.CanBeUsed&&!skill.locked&&Vector2.Distance(ActiveTarget.position, transform.position)<=skill.Range)
        {
            return true;
        }
        return false;
    }
    public void Danger()
    { 
        if(CurrentEntityType!= SkillBehaviourType.FuriousAttack&&CurrentEntityType< SkillBehaviourType.DefendAttack)
        {
            if (behaviour < GeneralSkillType.Defend)
            {
                behaviour = GeneralSkillType.Defend;
            }
            CurrentEntityType = SkillBehaviourType.DefendAttack;
        }
        
    }
    public void ChangeTarget()
    {
        //Debug.Log("i'm here");
        float Lenght = 10000000;
        for (int i = 0; i < Targets.Count; i++)
        {
            var item = Targets[i];        
            if (item == null)
            {
                Targets.RemoveAt(i);
                i--;
                continue;
            }
            float Distance = Vector2.Distance(transform.position, item.position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(movement.GetCenter(transform), movement.GetCenter(item)-movement.GetCenter(transform), Distance);
            if (hits.Any(x => x.transform.tag == "undestruct"))
            {
                continue;
            }
            if (Distance < Lenght)
            {
                ActiveTarget = item;
                Lenght = Distance;
            }
        }
        if (ActiveTarget == null)
        {
            movement.Sleep = true;
        }
    }
    public void NoWay()
    {
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            if (behaviour >= GeneralSkillType.Control)
            {
                behaviour = GeneralSkillType.Buff;
            }
            SituationSkill = SkillBehaviourType.FuriousAttack;
        }
        else
        {
            
            SituationSkill = SkillBehaviourType.DeefDefend;
        }
    }
    public void NewTarget(Creature target)
    {

        float Chance = UnityEngine.Random.Range(0, 100);
        Chance += (int)StartEntityType * 3;
        Debug.Log("My chance  " + Chance);
        if (Targets.Count == 3)
        {
            Debug.Log("HHHEEEEYYY_______12345678");
            if (Chance == 115)
            {
                CurrentEntityType = SkillBehaviourType.FuriousAttack;
            }
            else if (Chance >= 110)
            {
                CurrentEntityType = SkillBehaviourType.RunAway;
            }
            else
            {
                CurrentEntityType--;
            }
        }
        if (Targets.Count == 5)
        {
            if (Chance >= 110)
            {
                CurrentEntityType = SkillBehaviourType.FuriousAttack;
            }
            else if (Chance >= 90)
            {
                CurrentEntityType = SkillBehaviourType.RunAway;
            }
            else
            {
                CurrentEntityType--;
            }
        }
        if (Targets.Count == 10)
        {

            if (Chance >= 50)
            {
                CurrentEntityType = SkillBehaviourType.RunAway;
            }
            else if (Chance >= 25)
            {
                CurrentEntityType = SkillBehaviourType.FuriousAttack;
            }
            else {
                CurrentEntityType = SkillBehaviourType.DeefDefend;
            }

        }
        if (CurrentEntityType == SkillBehaviourType.FuriousAttack)
        {
            Debug.Log("Heyy");
            return;
        }
        float Power = CheckEnemy(target);
        Debug.Log("Enemy power:  "+Power);
        if (Power > 500)
        {
            if (Chance > 110)
            {
                CurrentEntityType = SkillBehaviourType.DeefDefend;
            }
            else {
                CurrentEntityType = SkillBehaviourType.RunAway;
            }
            return;
        }
        if (Power > 250)
        {
            if (Chance > 80)
            {
                CurrentEntityType = SkillBehaviourType.DeefDefend;
            }
            else if (Chance > 10)
            {
                CurrentEntityType = SkillBehaviourType.RunAway;
            }
            else
            {
                CurrentEntityType--;
            }
            return;
        }
        if (Power > 100)
        {
            return;
        }
        if (Power < 100)
        {
            SituationSkill++;
            CurrentEntityType++;
            return;
        }
    }
    float CheckEnemy(Creature target)
    {
        //Debug.Log("HP divine: "+  target.HP/ HP );
        //Debug.Log("Damage divine: " +  target.SumBaseDamage/ SumBaseDamage );
        //Debug.Log("MP divine: " +  target.MP/ MP  *  target.MagResist/ MagResist );
        //Debug.Log("ST divine: " +  target.ST/ ST  *  target.PhysResist/ PhysResist );
        //Debug.Log("SP divine: " +  target.SP/ SP  *  target.SoulResist/ SoulResist );
        //Debug.Log("Speed divine: " +  target.Speed/ Speed );
        //Debug.Log("Skills divine: " +  target.Skills.Count);
        //Debug.Log("Skills divine: " + (float)Skills.Count/target.Skills.Count);
        return (target.SumBaseDamage/SumBaseDamage+target.HP/HP+target.MP/MP*target.MagResist/MagResist+target.ST/ST*target.PhysResist/PhysResist+target.SP/SP*target.SoulResist/SoulResist+target.Speed/Speed+(float)target.Skills.Count/Skills.Count)*100/7;



    }

    protected override void Death()
    {
        LootSkript loot = Instantiate(LootPerhub);
        loot.transform.position = transform.position;
        Destroy(gameObject);
    }
}
