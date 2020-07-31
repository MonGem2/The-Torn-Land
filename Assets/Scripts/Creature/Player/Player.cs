﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public StatsBarScript StatsBar;

    public override float HP {
        get { return hP;  }
        set {
            hP = value;
            StatsBar.SetHp(hP);
        }
    }
    public override float MP
    {
        get { return mP; }
        set
        {
            mP = value;
            StatsBar.SetMp(mP);
        }
    }
    public override float ST
    {
        get {
            
            return sT; }
        set
        {
            Debug.Log("1:"+value);
            sT = value;
            Debug.Log(sT);
            StatsBar.SetSt(sT);
        }
    }
    public override float SP
    {
        get { return sP; }
        set
        {
            sP = value;
            StatsBar.SetSp(sP);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.MaxHP = 200;
        this.MaxMP = 200;
        this.MaxSP = 200;
        this.MaxST = 200;
        this.HP = 10;
        this.MP = 10;
        this.ST = 10;
        this.SP = 10;
        this.RegSpeedHP = 3;
        this.RegSpeedMP = 3;
        this.RegSpeedSP = 3;
        this.RegSpeedST = 3;
        this.SumBaseDamage = 5;
        this.MagResist = 1;
        this.PhysResist = 1;
        this.SoulResist = 1;
        StatsBar.SetMaxParams(this.MaxHP, this.MaxMP, MaxST, 100, 100, MaxMP, MaxSP);
        loader.LoadSkills();
        StartCoroutine(Regeneration(1));
        //Debug.LogWarning(loader.Skils.Count);
        Skills = new List<Skill>();
        Skills.Add(loader.Skills[0]);
        ActiveSkill = loader.Skills[0];
    }
    void  Death()
    { }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(this.UseSkill(ActiveSkill, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
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
}
