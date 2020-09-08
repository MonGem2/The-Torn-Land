 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Creature
{
    public StatsBarScript StatsBar;
    public SkillBarScript SkillBar;
    public Messanger messanger;
    public OnChangeParameterTrigger OnActiveSkillChanged;
    Skill _activeSkill;
    public Skill ActiveSkill { get { return _activeSkill; } set { _activeSkill = value;if (OnActiveSkillChanged != null) { OnActiveSkillChanged(value); } } }
    public Skill OnClickSkill;
    public EventSystem eventSystem;
    public GameObject GUI;
    public GameObject DeadScreen;
    public PlayerMovement movement;
    #region MaxHungry
    protected float _maxHungry;
    public OnChangeParameterTrigger MaxHungryChangeTrigger;
    public virtual float MaxHungry
    {
        get
        {

            return _maxHungry;
        }
        set
        {
            if (MaxHungryChangeTrigger != null)
            {
                MaxHungryChangeTrigger(value);
            }
            _maxHungry = value;
        }

    }
    #endregion
    #region Hungry
    protected float _hungry;
    public OnChangeParameterTrigger HungryChangeTrigger;
    public virtual float Hungry
    {
        get
        {

            return _hungry;
        }
        set
        {
            if (HungryChangeTrigger != null)
            {
                HungryChangeTrigger(value);
            }
            if (value > MaxHungry)
            {
                _hungry = MaxHungry;
            }
            else if (value<0)
            {
                _hungry = 0;
            }
            else
            {
                _hungry = value;
            }

        }

    }
    #endregion
    #region HungryReg
    public OnChangeParameterTrigger RegSpeedHChangeTrigger;
    protected float _regSpeedH;
    public virtual float RegSpeedH
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)PlayerStatsChangeLD.HungryRegSpeed];
                }
            }
            return _regSpeedH * Aditional;
        }
        set
        {
            if (RegSpeedHChangeTrigger != null)
            {
                RegSpeedHChangeTrigger(value);
            }
            _regSpeedH = value;
        }

    }

    #endregion
    #region MaxThirst
    protected float _maxThirst;
    public OnChangeParameterTrigger MaxThirstChangeTrigger;
    public virtual float MaxThirst
    {
        get
        {

            return _maxThirst;
        }
        set
        {
            if (MaxThirstChangeTrigger != null)
            {
                MaxThirstChangeTrigger(value);
            }
            _maxThirst = value;
        }

    }
    #endregion
    #region Thirst
    protected float _thirst;
    public OnChangeParameterTrigger ThirstChangeTrigger;
    public virtual float Thirst
    {
        get
        {

            return _thirst;
        }
        set
        {
            if (ThirstChangeTrigger != null)
            {
                ThirstChangeTrigger(value);
            }
            if (value > MaxThirst)
            {
                _thirst = MaxThirst;
            }
            else if (value < 0)
            {
                _thirst = 0;
            }
            else
            {
                _thirst = value;
            }
        }

    }
    #endregion
    #region ThirstReg
    public OnChangeParameterTrigger RegSpeedTChangeTrigger;
    protected float _regSpeedT;
    public virtual float RegSpeedT
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.PlayerParameterAdder)
                {
                    Aditional += item.Params[(int)PlayerStatsChangeLD.ThirstRegSpeed];
                }
            }
            return _regSpeedT * Aditional;
        }
        set
        {
            if (RegSpeedTChangeTrigger != null)
            {
                RegSpeedTChangeTrigger(value);
            }
            _regSpeedT = value;
        }

    }

    #endregion
    #region MaxCorruption
    protected float _maxCorruption;
    public OnChangeParameterTrigger MaxCorruptionChangeTrigger;
    public virtual float MaxCorruption
    {
        get
        {

            return _maxCorruption;
        }
        set
        {
            if (MaxCorruptionChangeTrigger != null)
            {
                MaxCorruptionChangeTrigger(value);
            }
            _maxCorruption = value;
        }

    }
    #endregion
    #region Corruption
    protected float _corruption;
    public OnChangeParameterTrigger CorruptionChangeTrigger;
    public virtual float Corruption
    {
        get
        {

            return _corruption;
        }
        set
        {
            if (CorruptionChangeTrigger != null)
            {
                CorruptionChangeTrigger(value);
            }
            if (value > MaxCorruption)
            {
                _corruption = MaxCorruption;
            }
            else if (value < 0)
            {
                _corruption = 0;
            }
            else
            {
                _corruption = value;
            }
        }

    }
    #endregion
    #region CorruptionReg
    public OnChangeParameterTrigger RegSpeedCPChangeTrigger;
    protected float _regSpeedCP;
    public virtual float RegSpeedCP
    {
        get
        {
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.playerStatsChange)
                {
                    Aditional += item.Params[(int)PlayerStatsChangeLD.CorruptionRegSpeed];
                }
            }
            return _regSpeedCP * Aditional;
        }
        set
        {
            if (RegSpeedCPChangeTrigger != null)
            {
                RegSpeedCPChangeTrigger(value);
            }
            _regSpeedCP = value;
        }

    }

    #endregion
    #region XP
    protected float _xP;
    public OnChangeParameterTrigger XPChangeTrigger;
    public virtual float XP
    {
        get
        {

            return _xP;
        }
        set
        {
            if (XPChangeTrigger != null)
            {
                XPChangeTrigger(value);
            }
            int Aditional = 1;
            foreach (var item in States)
            {
                if (item.type == StateType.ParameterChanger)
                {
                    Aditional += item.Params[(int)PlayerStatsChangeLD.XPBonus];
                }
            }
            if (value > MaxXP)
            {
                Lvl++;
                //_xP = MaxXP;
            }
            else
            {
                if (value-_xP>0)
                {
                    value += (value - _xP)*Aditional;
                }
                _xP = value;
            }
        }

    }
    #endregion
    #region MaxXP
    protected float _maxXP;
    public OnChangeParameterTrigger MaxXPChangeTrigger;
    public virtual float MaxXP
    {
        get
        {

            return _maxXP;
        }
        set
        {
            if (MaxXPChangeTrigger != null)
            {
                MaxXPChangeTrigger(value);
            }
            _maxXP = value;
        }

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //this.MaxHP = 200;
        //this.MaxMP = 200;
        //this.MaxSP = 200;
        //this.MaxST = 200;
        //this.MaxHungry = 100;
        //this.MaxThirst = 100;
        //this.MaxCorruption = 100;
        ////StatsBar.SetMaxParams(this.MaxHP, 100, MaxST, 100, 100, MaxMP, MaxSP);
        //this.HP = MaxHP;
        //this.MP = MaxMP;
        //this.ST = MaxST;
        //this.SP = MaxSP;
        //this.Thirst = this.MaxThirst;
        //this.Hungry = this.MaxHungry;
        //this.Corruption = this.MaxCorruption;
        //this.RegSpeedHP = 3;
        //this.RegSpeedMP = 3;
        //this.RegSpeedSP = 3;
        //this.RegSpeedST = 3;
        //this.RegSpeedH = -1;
        //this.RegSpeedT = -1;
        //this.RegSpeedCP = -1;
        //this.SumBaseDamage = 5;
        //this.MagResist = 1;
        //this.PhysResist = 1;
        //this.SoulResist = 1;
        //this.Speed = 3;
        if (!loader.LoadPlayer(this))
        {
            loader.CreatePlayer(this);
        }        
        StartCoroutine(Regeneration(1));
       // StartCoroutine(Save());
        //Debug.LogWarning(loader.Skils.Count);
       // Skills = new List<Skill>();
       // Skills.Add(loader.Skills[0].Clone());
       // Skills.Add(loader.Skills[1].Clone());
       // Skills.Add(loader.Skills[2].Clone());
        ActiveSkill = Skills[2];
        OnClickSkill = ActiveSkill;
        SkillBar.SetSkillOnButton(Skills[0], 0);
        SkillBar.SetSkillOnButton(Skills[1], 1);
        this.RegenerationTriggered += RegTrigger;
        this.OnSkillAdded += (x) =>
        {
            if (SkillBar.CheckSpace())
            {
                SkillBar.SetSkillOnButton((Skill)x);
            }
        };
    }
    public void PlayerStateAdded(State state)
    {
        if (state.type == StateType.PlayerParameterAdder)
        {
            Debug.Log("Creature:ParameterAdder triggered");
            this.Corruption += state.Params[(int)PlayerParameterAdderLD.Corruption];
            this.Hungry += state.Params[(int)PlayerParameterAdderLD.Hungry];
            this.Thirst += state.Params[(int)PlayerParameterAdderLD.Threat];
            this.XP += state.Params[(int)PlayerParameterAdderLD.XP];
            return;
        }
    }
    public IEnumerator Save()
    {
        yield return new WaitForSeconds(10);
        loader.SavePlayer(this);
    
    }
    void RegTrigger(object obj, object eventArgs)
    {
        float Timeout = (float)obj;
        Corruption += RegSpeedCP*Timeout;
        Hungry += RegSpeedH * Timeout;
        Thirst += RegSpeedT * Timeout;

    }
    public bool SetActiveSkill(Skill skill)
    {
       
        if (ActiveSkill == skill)
        {
            Debug.Log("JJ");
            ActiveSkill = OnClickSkill;
            return true;
        }

        ActiveSkill = skill;
        if (ActiveSkill == null)
        {
            ActiveSkill = OnClickSkill;
            return false;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool CanShoot=true;
            // Debug.Log(EventSystem.current.lastSelectedGameObject.tag);
            if (EventSystem.current.IsPointerOverGameObject())
            {
                var pd = new PointerEventData(eventSystem) { position = Input.mousePosition };
                var results = new List<RaycastResult>();
                eventSystem.RaycastAll(pd, results);
                foreach (var result in results)
                {
                    if (result.gameObject.tag == "UI" || result.gameObject.tag == "Untagged")
                    {
                       //Debug.Log(result.gameObject.tag);
                        CanShoot = false;
                    }
                }
            }
            if (!AttackLock && CanShoot && ActiveSkill.CanBeUsed && CanAttack && !ActiveSkill.locked)
            {
                Skill temp = ActiveSkill;
                if (ActiveSkill != OnClickSkill)
                {
                    ActiveSkill = OnClickSkill;
                }
                StartCoroutine(this.UseSkill(temp, Camera.main.ScreenToWorldPoint(Input.mousePosition)));

            }
            else if (!CanShoot)
            { 
            
            }
            else if (AttackLock)
            {
                messanger.SetMessage("Attack locked");
            }
            else if (ActiveSkill.locked)
            {
                messanger.SetMessage("Skill locked");
            }
            //GameObject bullet = Instantiate(loader.BulletsPerhubs[0]);
            //Bullet bl = bullet.GetComponent<Bullet>();
            //BulletData data = new BulletData();
            //data.AdditionalAngle = 0;
            //data.DeltaAngle = 15;
            //data.Distance = new Vector2(0.5f, 0.5f);
            //data.DontAttack = new List<string>();
            //data.DontAttack.Add("player");
            //data.EffectsIDs = new List<int>();
            //data.FlyTime = 5;
            //data.ManaDamage = 0;
            //data.PerhubID = 0;
            //data.PhysicDamage = 5;
            //data.Range = 10;
            //data.SoulDamage = 0;
            //data.Through = false;
            //data.type = BulletType.Bullet;
            //bl.data = data;
            //bl.loader = loader;
            //
            //bl.Shoot(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));


            //GameObject bullet = Instantiate(loader.BulletsPerhubs[1]);
            //Bullet bl = bullet.GetComponent<Bullet>();
            //BulletData data = new BulletData();
            //data.AdditionalAngle = 0;
            //data.DeltaAngle = 15;
            //data.Distance = new Vector2(0.5f, 0.5f);
            //data.DontAttack = new List<string>();
            //data.DontAttack.Add("player");
            //data.EffectsIDs = new List<int>();
            //data.FlyTime = 0. 2f;
            //data.ManaDamage = 0;
            //data.PerhubID = 0;
            //data.PhysicDamage = 5;
            //data.Range = 10;
            //data.SoulDamage = 0;
            //data.Through = false;
            //data.type = BulletType.Ray;
            //data.AttackTimeout = 0.05f;
            //bl.data = data;
            //bl.loader = loader;
            //bl.Shoot(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //{ 
            //    GameObject bullet = Instantiate(loader.BulletsPerhubs[2]);
            //    Bullet bl = bullet.GetComponent<Bullet>();
            //    BulletData data = new BulletData();
            //    data.AdditionalAngle = 0;
            //    data.DeltaAngle = 15;
            //    data.Distance = new Vector2(0.5f, 0.5f);
            //    data.DontAttack = new List<string>();
            //    //data.DontAttack.Add("player");
            //    data.EffectsIDs = new List<int>();
            //    data.FlyTime = 200;
            //    data.ManaDamage = 0;
            //    data.PerhubID = 0;
            //    data.PhysicDamage = 5;
            //    data.Range = 2;
            //    data.SoulDamage = 0;
            //    data.Through = true;
            //    data.type = BulletType.Area;
            //    data.AttackTimeout = 0.05f;
            //    bl.data = data;
            //    bl.loader = loader;
            //    bl.Shoot(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //}
            //{
            //    GameObject bullet = Instantiate(loader.BulletsPerhubs[3]);
            //    Bullet bl = bullet.GetComponent<Bullet>();
            //    BulletData data = new BulletData();
            //    data.AdditionalAngle = 0;
            //    data.DeltaAngle = 60;
            //    data.Distance = new Vector2(0.5f, 0.5f);
            //    data.DontAttack = new List<string>();
            //    //data.DontAttack.Add("player");
            //    data.EffectsIDs = new List<int>();
            //    data.FlyTime = 0.3f;
            //    data.ManaDamage = 0;
            //    data.PerhubID = 0;
            //    data.PhysicDamage = 5;
            //    data.Range = 2;
            //    data.SoulDamage = 0;
            //    data.Through = true;
            //    data.type = BulletType.Swing;
            //    data.AttackTimeout = 0.1f;
            //    bl.data = data;
            //    bl.loader = loader;
            //    bl.Shoot(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //}
        }
    }
    protected override void Death()
    {
        Time.timeScale = 0;
        GUI.active = false;
        DeadScreen.active = true;
    }
}
