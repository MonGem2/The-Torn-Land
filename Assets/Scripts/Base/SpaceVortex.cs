using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceVortex : MyObject
{
    Creature creature;
    float dmgPL;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Set(Creature entity, float Duration, float DamageModifier, Vector3 position)
    {
        gameObject.tag = entity.tag;
        gameObject.transform.position = position;
        
        creature = entity;
        dmgPL = DamageModifier;
        StartCoroutine(Remove(Duration));
        transform.SetParent(creature.transform);
    }
    IEnumerator Remove(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
    public override void Damage(BulletData bulletData, bool MaxEffectSet = true)
    {
        bulletData.ManaDamage *= dmgPL;
        creature.Damage(bulletData, MaxEffectSet);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
