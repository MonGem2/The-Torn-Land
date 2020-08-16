using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Loader loader;
    public BulletData data;
    Vector2 Direction=new Vector2();
    Vector2 PlayerPosition;
    public Animation animation;
    float DAngle;
    public void Shoot(Vector2 playerPos, Vector2 targetPos)
    {
        
        PlayerPosition = playerPos;
        DAngle = UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle);
        Vector2 Dir = (targetPos- playerPos).normalized;
        double ResAngle = (double)((float)Math.Acos(Math.Abs(Dir.x)) * 180 / (float)Math.PI);
        Debug.Log($"Target angle:{Math.Acos(Math.Abs(Dir.x)) * 180 / (float)Math.PI}");
        if (Dir.x > 0)
        {
            
            if (Dir.y < 0)
            {
                ResAngle =360-ResAngle ;  
            }
        
        }
        else
        {
            if (Dir.y > 0)
            {
                ResAngle = 180-ResAngle;
            }
            else
            {
                ResAngle += 180;
            }
        
        }
        ResAngle += DAngle + data.AdditionalAngle;
        Debug.Log($"Res angle:{ResAngle}");
        Direction.x = (float)Math.Cos(ResAngle * Math.PI / 180);
        Direction.y = (float)Math.Sin(ResAngle * Math.PI / 180);
        if (data.type == BulletType.Stab)
        {
            transform.position=playerPos +Direction * data.Distance;
            if (Direction.y > 0)
            {
                gameObject.transform.Rotate(0, 0, (float)Math.Acos(Direction.x) * 180 / (float)Math.PI + data.AdditionalAngle);// + UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle));
            }
            else
            {
                gameObject.transform.Rotate(0, 0, -((float)Math.Acos(Direction.x) * 180 / (float)Math.PI + data.AdditionalAngle));// + UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle));
            }
            //DeleteOnTime(data.FlyTime);
        }
        if (data.type == BulletType.Swing)
        {
            LineRenderer lnr = gameObject.GetComponent<LineRenderer>();
            transform.position = playerPos;
            transform.Rotate(0, 0, (float)ResAngle-DAngle-data.DeltaAngle);
            //transform.Rotate(0, 0, (float)Math.Acos(Direction.x) * 180 / (float)Math.PI + data.AdditionalAngle +DAngle);// + UnityEngine.Random.Range(-data.DeltaAngle, data.DeltaAngle));

            //DeleteOnTime(data.FlyTime);
        }
        if (data.type == BulletType.Ray)
        {
            LineRenderer lnr = gameObject.GetComponent<LineRenderer>();
            //Debug.Log($"My count of points:{lnr.positionCount}");
            if (data.Through)
            {
                lnr.SetPositions(new Vector3[] { playerPos + Direction * data.Distance, playerPos + Direction * data.Range });
            }
            else
            {
                var hit = Physics2D.Raycast(playerPos + Direction * data.Distance, Direction, data.Range);
                if (hit.transform == null)
                {
                    lnr.SetPositions(new Vector3[] { playerPos + Direction * data.Distance, playerPos + Direction * data.Range });
                }
                else
                {
                    lnr.SetPositions(new Vector3[] { playerPos + Direction * data.Distance, hit.point });
                }
            }
            //Debug.Log($"My count of points(the same in the end):{lnr.positionCount}");
            StartCoroutine(RayAttck(data.AttackTimeout));
            //Debug.Log($"Player pos:{playerPos}, result pos:{Direction}, End lisne:{targetPos}");
            //DeleteOnTime(data.FlyTime);
        }
        if (data.type == BulletType.Bullet)
        {
            //Debug.Log("GGWP");
            transform.position= playerPos + Direction * data.Distance;
            //Debug.Log(ResAngle);
            gameObject.transform.Rotate(0, 0, (float)ResAngle);
            
            //Debug.Log($"Direction y: {Direction.y}, Direction angle:{Math.Acos(Direction.y)*180 / Math.PI}, additional angle:{data.AdditionalAngle}");
            //Debug.Log($"Result:{(float)Math.Acos(Direction.y)*180/(float)Math.PI + data.AdditionalAngle}");
            //DeleteOnTime(data.FlyTime);
        }
        if(data.type==BulletType.Area)
        {
            //Debug.Log("Holl");
            if (Vector2.Distance(playerPos, targetPos) < data.Range)
            {
                transform.position = playerPos+ Direction*Vector2.Distance(targetPos, playerPos);
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            }
            else
            {
                transform.position = playerPos + Direction * data.Range;
                transform.position=new Vector3(transform.position.x, transform.position.y, 1);
            }
            
            //DeleteOnTime(data.FlyTime);
        }
        StartCoroutine(DeleteOnTime(data.FlyTime));
    }

    IEnumerator RayAttck(float WaitForSecond)
    {
        if (!data.Through)
        {
            while (true)
            {
                yield return new WaitForSeconds(WaitForSecond);
                var hit = Physics2D.Raycast(PlayerPosition + Direction * data.Distance, Direction, data.Range);
                // Debug.LogWarning(hit.transform);
                if (hit.transform == null)
                {
                    continue;
                }
                foreach (var item in data.DontAttack)
                {
                    if (item == hit.transform.gameObject.tag)
                    {
                        DestroyTrigger(false);
                        yield break;
                    }
                }
                if (hit.transform.gameObject.tag == "enemy")
                {
                    hit.transform.gameObject.GetComponent<Creature>().Damage(data);
                }
            }
        }
        while (true)
        {
            yield return new WaitForSeconds(WaitForSecond);
            var hit = Physics2D.RaycastAll(PlayerPosition + Direction * data.Distance, Direction, data.Range);
            // Debug.LogWarning(hit.transform);
            if (hit.Length == 0)
            {
                continue;
            }
            foreach (var item in data.DontAttack)
            {
                foreach (var item1 in hit)
                {
                    if (item == item1.transform.gameObject.tag)
                    {
                        DestroyTrigger(false);
                        yield break;
                    }
                }
                
            }
            foreach (var item in hit)
            {
                if (item.transform.gameObject.tag == "enemy")
                {
                    item.transform.gameObject.GetComponent<Creature>().Damage(data);
                }
            }
            
        }
    }

    IEnumerator DeleteOnTime(float time)
    { 
        //Debug.Log("i'l delete this shit");
        yield return new  WaitForSeconds(time);
        //Debug.Log("i'm deleting this shit");
        DestroyTrigger(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var item in data.DontAttack)
        {
            if (item == collision.tag)
            {
                DestroyTrigger(false);
                return;
            }
        }
        //Debug.Log("HIIIII!");
        if (collision.tag == "detector")
        {
            //Debug.Log("HIIIII232435!");
            return;
        }
        if (collision.tag == "bullet")
        {
            return;
        }
        // Debug.Log("i'm finaly here");

        //Physics.IgnoreCollision(collision.collider, collider);
        if (collision.tag == "enemy")
        {
            //data.PhysicDamage / data.AttackTimeout * Time.deltaTime;
            if (data.type == BulletType.Area)
            {
                //data.PhysicDamage / data.AttackTimeout * Time.deltaTime;
                collision.GetComponent<Creature>().Damage(data);
            }
            else
            {
                collision.GetComponent<Creature>().Damage(data);
            }
        }
        if (!data.Through&&collision.GetComponent<Bullet>()==null)
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
        if (data.type == BulletType.Swing)
        {
            transform.Rotate(new Vector3(0, 0, data.DeltaAngle * 2 / data.FlyTime * Time.deltaTime));
            // if (Direction.y > 0)
            // {
            //     
            // }
            // else
            // {
            //     transform.Rotate(new Vector3(0, 0, + data.DeltaAngle * 2 / data.FlyTime * Time.deltaTime));
            // }

            // Debug.Log(data.DeltaAngle * 2 / data.FlyTime );
        }
    }
}
public class BulletData {
    public float ShootPeriod;
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
    internal float AttackTimeout;
}
public enum BulletType { 
    Stab=0,
    Swing,
    Bullet,
    Ray,
    Area
}