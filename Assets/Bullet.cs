using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Loader loader;
    public BulletData data;
    Vector2 Direction;
    public Animation animation;
    public void Shoot(Vector2 playerPos, Vector2 targetPos)
    {
        Direction = (targetPos - playerPos).normalized;
        if (data.type == BulletType.Stab)
        {
            transform.position=playerPos +Direction * data.Distance;
            gameObject.transform.Rotate(0, 0, (float)Math.Acos(Direction.y) + data.AdditionalAngle + UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle));
            //DeleteOnTime(data.FlyTime);
        }
        if (data.type == BulletType.Swing)
        {
            transform.position = playerPos + Direction * data.Distance;
            //DeleteOnTime(data.FlyTime);
        }
        if (data.type == BulletType.Ray)
        {
            LineRenderer lnr = gameObject.GetComponent<LineRenderer>();
            lnr.SetPositions(new Vector3[] { playerPos + Direction * data.Distance, Direction * data.Range });
            //DeleteOnTime(data.FlyTime);
        }
        if (data.type == BulletType.Bullet)
        {
            //Debug.Log("GGWP");
            transform.position= playerPos + Direction * data.Distance;
            if (Direction.y > 0)
            {
                gameObject.transform.Rotate(0, 0, (float)Math.Acos(Direction.x) * 180 / (float)Math.PI + data.AdditionalAngle);// + UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle));
            }
            else {
                gameObject.transform.Rotate(0, 0, -((float)Math.Acos(Direction.x) * 180 / (float)Math.PI + data.AdditionalAngle));// + UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle));
            }
            //Debug.Log($"Direction y: {Direction.y}, Direction angle:{Math.Acos(Direction.y)*180 / Math.PI}, additional angle:{data.AdditionalAngle}");
            //Debug.Log($"Result:{(float)Math.Acos(Direction.y)*180/(float)Math.PI + data.AdditionalAngle}");
            //DeleteOnTime(data.FlyTime);
        }
        if(data.type==BulletType.Area)
        {
            transform.position = playerPos + Direction * data.Range;
            //DeleteOnTime(data.FlyTime);
        }
        StartCoroutine(DeleteOnTime(data.FlyTime));
    }
    IEnumerator DeleteOnTime(float time)
    {
        //Debug.Log("i'l delete this shit");
        yield return new  WaitForSeconds(time);
        //Debug.Log("i'm deleting this shit");
        DestroyTrigger(false);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("i'm here");
        foreach (var item in data.DontAttack)
        {
            if (item == other.gameObject.tag)
            {
                DestroyTrigger(false);
                return;
            }
        }
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<Enemy>().Damage();
        }
        if (!data.Through)
        {
            DestroyTrigger(true);
        }


    }
    void DestroyTrigger(bool DoAnimation) 
    {
        if (DoAnimation)
        { 
            //ToDo
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (data.type == BulletType.Bullet)
        {
            transform.position = (Vector2)transform.position + Direction * data.Range / data.FlyTime * Time.deltaTime;
            
        }
    }
}
public class BulletData {
    public List<string> DontAttack;
    public int PerhubID;
    public float PhysicDamage = 0;
    public float ManaDamage = 0;
    public float SoulDamage = 0;
    public List<int> EffectsIDs;
    public BulletType type;
    public float FlyTime = 0;
    public float Range = 1;
    public Vector2 Distance;
    public bool Through;
    public float AdditionalAngle;
    public float DeltaAngle;
}
public enum BulletType { 
    Stab=0,
    Swing,
    Bullet,
    Ray,
    Area
}