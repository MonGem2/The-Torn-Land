using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIForm : Creature
{
    public List<Transform> Targets;
    public Transform ActiveTarget;
    public List<string> TargetTags;
    public EntityMovement movement;
    public float ChangeTargetTimeout;
    // Start is called before the first frame update
    void Start()
    {
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
        
    }
    public void Danger()
    { 
        
    }
    public void ChangeTarget()
    {
        Debug.Log("i'm here");
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
}
public enum AttackBehavior { 
    Defend,
    No
}
