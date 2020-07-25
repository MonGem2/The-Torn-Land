using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public override void AddEffects(List<int> EffectIds)
    { 
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Skills = new List<Skill>();
        Skill skill = new Skill();
        skill.Cooldown = 0;
        skill.EffectsIds = new List<int>();
        skill.MPIntake = 10;
        skill.STIntake = 10;
        skill.SPIntake = 10;
        skill.Bullets = new List<BulletData>();
        {
            {
                BulletData data = new BulletData();
                data.AdditionalAngle = 30;
                data.DeltaAngle = 0;
                data.Distance = new Vector2(0.5f, 0.5f);
                data.DontAttack = new List<string>();
                //data.DontAttack.Add("player");
                data.EffectsIDs = new List<int>();
                data.FlyTime = 200;
                data.ManaDamage = 0;
                data.PerhubID = 2;
                data.PhysicDamage = 5;
                data.Range = 2;
                data.SoulDamage = 0;
                data.Through = true;
                data.type = BulletType.Area;
                data.AttackTimeout = 0.05f;
                skill.Bullets.Add(data);
            }
            {
                BulletData data = new BulletData();
                data.AdditionalAngle = -30;
                data.DeltaAngle = 0;
                data.Distance = new Vector2(0.5f, 0.5f);
                data.DontAttack = new List<string>();
                //data.DontAttack.Add("player");
                data.EffectsIDs = new List<int>();
                data.FlyTime = 200;
                data.ManaDamage = 0;
                data.PerhubID = 2;
                data.PhysicDamage = 5;
                data.Range = 2;
                data.SoulDamage = 0;
                data.Through = true;
                data.type = BulletType.Area;
                data.AttackTimeout = 0.05f;
                skill.Bullets.Add(data);
            }
        }
        Skills.Add(skill);
        ActiveSkill = skill;
    }
    public override void Damage(BulletData bulletData)
    { }
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
