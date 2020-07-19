using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Loader loader;
    public BulletData data;
    public void Shot(Vector2 playerPos, Vector2 targetPos)
    {
        if (data.type == BulletType.Vawe)
        {
            transform.position=playerPos +(targetPos - playerPos).normalized * 2;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public class BulletData {
    public int PerhubID;
    public float PhysicDamage = 0;
    public float ManaDamage = 0;
    public float SoulDamage = 0;
    public List<int> EffectsIDs;
    public BulletType type;
    public float FlyTime = 0;
}
public enum BulletType { 
    Vawe=0,
    Bullet,
    Ray,
    Area
}