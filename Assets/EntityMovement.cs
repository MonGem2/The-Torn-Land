using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class EntityMovement : MonoBehaviour
{
    public Vector2 ActivePoint;
    public bool IsChangePoint=false;
    // int my_point;
    // public List<Vector2> PathBack; 
    public Rigidbody2D rigidbody;
    public bool Sleep;
    public float ScnRange=15;
    public float Distance;
    public Behavior behavior=Behavior.None;
    //bool PointSeted = true;    
    public AIForm entity;

   // public GameObject gm;
    bool CheckMovement = false;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "undestruct")
        {
            //collision.transform.position = transform.position;
            //Debug.Log(collision.transform.tag);
            CheckMovement = true;
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

        // Debug.Log(Sleep);
        if (entity.ActiveTarget == null && !Sleep)
        {
            //   Debug.LogWarning("zxcvb");
            entity.ChangeTarget();
        }
        if (entity.MoveLock)
        {
            return;
        }
        if (behavior == Behavior.MoveToTarget)
        {
            Debug.LogWarning("#1");
            if (entity.ActiveTarget != null && !Sleep && Vector2.Distance(entity.ActiveTarget.position, transform.position) > Distance+0.1)
            {
                Debug.LogWarning("#2");
                if (Vector2.Distance(ActivePoint, transform.position) < entity.Speed * Time.deltaTime)
                {
                //ActivePoint = null;
                    entity.ChangeTarget();
                //Debug.Log("HEEEEEEYYYYYY");
                }
                else
                {
                    Debug.LogWarning("#3");
                    if (CheckMovement)
                    {
                        Vector2 vec = (ActivePoint - (Vector2)transform.position).normalized;
                        if (vec.x >= 0 && vec.y >= 0) { transform.Translate(new Vector2(1, 1).normalized * 5 * Time.deltaTime);  }
                        if (vec.x > 0 && vec.y < 0) { transform.Translate(new Vector2(1, -1).normalized * 5 * Time.deltaTime);  }
                        if (vec.x < 0 && vec.y > 0) { transform.Translate(new Vector2(-1, 1).normalized * 5 * Time.deltaTime);  }
                        if (vec.x <= 0 && vec.y <= 0) { transform.Translate(new Vector2(-1, -1).normalized * 5 * Time.deltaTime);  }
                        CheckMovement = false;
                    }
                    else
                    {
                        Debug.LogWarning("#4");
                        transform.Translate((ActivePoint - (Vector2)transform.position).normalized * entity.Speed * Time.deltaTime);
                    //transform.Translate((ActivePoint - (Vector2)transform.position).normalized * entity.Speed * Time.deltaTime);
                    }

                }
            }
            if (entity.ActiveTarget != null && !Sleep && Vector2.Distance(entity.ActiveTarget.position, transform.position) < Distance-0.1)
            {
                Debug.LogWarning("#5");
                // (PathBack.Last() - (Vector2)transform.position).normalized
                transform.Translate( (-ActivePoint + (Vector2)transform.position).normalized * entity.Speed * Time.deltaTime);
            }
        }

        if (behavior == Behavior.RunAway)
        {
            if (!Sleep && entity.ActiveTarget == null)
            {
                entity.ChangeTarget();
            }
            if (IsChangePoint)
            {
                int x = 0;
                while (!ChangePoint()) { x++;if (x > 25) { break; } }
                if (x > 25)
                {
                    behavior = Behavior.MoveToTarget;
                    entity.NoWay();
                    return;
                }
                
                IsChangePoint = false;
            }
            if (!Sleep && Vector2.Distance(ActivePoint, transform.position) < 0.1)
            {
                IsChangePoint = true;
            }
            if (!Sleep && Vector2.Distance(ActivePoint, transform.position) > 0.1)
            {
                transform.Translate((ActivePoint - (Vector2)transform.position).normalized * entity.Speed * Time.deltaTime);
            }
        }

    }

    bool ChangePoint()
    {
        //UnityEngine.Random.
        Debug.Log("Hi gays");
        Vector2 Dir= new Vector2(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5))+ (Vector2)(transform.position - entity.ActiveTarget.position).normalized;
        Dir.Normalize();
        float MaxRange = 4;
        if (!Physics2D.RaycastAll(transform.position, Dir, 5).Any(x =>
        {
            if (entity.Targets.Contains(x.transform))
            {
                return true;
            }
            if (x.transform.tag == "undestruct")
            {
                if (MaxRange > Vector2.Distance(x.point, transform.position))
                {
                    MaxRange = Vector2.Distance(x.point, transform.position);

                    Debug.Log("HMMMM    "+MaxRange+"        "+x.point);
                }


                return true;
            }
            return false;
        }
        ))
        {
            if (MaxRange > 1)
            {
                float range = UnityEngine.Random.Range(1, MaxRange );
                ActivePoint =(Vector2)transform.position+ Dir * range;
                Debug.Log("and finaly i'm here with point: " +range);
                Debug.Log("and finaly i'm here with point: " + ActivePoint);
                Debug.Log("and finaly i'm here with point: " + transform.position);
               // gameObject.active = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        { return false; }
        //Debug.Log(Physics2D.Raycast(.transform.transform.tag);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Sleep)
        {
            if (entity.ActiveTarget != null&&behavior== Behavior.MoveToTarget)
            {

                //Debug.Log(ActiveTarget.position);
                Vector2 myPosition = GetCenter(transform);
                Vector2 ActivePos = GetCenter(entity.ActiveTarget);
                RaycastHit2D[] hits = Physics2D.RaycastAll(myPosition,(ActivePos- myPosition).normalized,Vector2.Distance(ActivePos,myPosition));
                //Debug.Log("Start");
                //int Behaviour;
                bool IsPathFree = true;
                //string str = "";
                foreach (var item in hits)
                {
                  //  str += item.transform.tag+"  ";
                    //Debug.Log(item.transform.position);
                    if (item.transform == entity.ActiveTarget)
                    {
                        break;
                    }
                    
                    if ((item.transform.tag == "undestruct" || item.transform.tag == "item"||item.transform.tag == "bullet"))
                    {
                        IsPathFree = false;
                    }
                    if (item.transform.tag == "bullet")
                    {
                        entity.Danger();      
                    }
                }
                //if (!IsPathFree && PointSeted)
                //{
                //    PointSeted = false;
                //    PathBack.Add(ActivePoint);
                //}
                //Debug.Log(IsPathFree+"\nData:"+ Vector2.Distance(ActiveTarget.position, transform.position));
                if (IsPathFree)
                {
                    ActivePoint = ActivePos;
                    //gm.transform.position = ActivePos;
                    //PointSeted = true;
                }

            }
            
        }
        
    }
    public Vector2 GetCenter(Transform transform)
    {
        return (Vector2)transform.position + transform.gameObject.GetComponent<Collider2D>().offset ;
    }
    public void DetectSomething(Transform transform)
    {
        //Debug.Log("1");
        if (transform.GetComponent<MyObject>() == null) {
            return;
        }
        Debug.Log("EntityMovement:Go in");
        foreach (var item in entity.TargetTags)
        {
            if (transform.tag == item)
            {
                Debug.Log("EntityMovement:step 2 "+transform.tag);
                //Debug.Log("2");
                if (behavior == Behavior.RunAway)
                {
                    ChangePoint();
                }
                if (!entity.Targets.Contains(transform))
                {
                    Debug.Log("EntityMovement:adding target");
                    AddTarget(transform);
                    entity.NewTarget(transform.GetComponent<MyObject>());
                }
                else if (entity.ActiveTarget == null)
                {
                    entity.ActiveTarget = transform;
                }
            }
        }
        
    }
    public void AddTarget(Transform transform)
    {
      // Debug.Log("5");
        //Debug.Log("000000000000000");
        Sleep = false;
        if (behavior == Behavior.None)
        {
            behavior = Behavior.MoveToTarget;
        }
        entity.Targets.Add(transform);
       // Debug.Log("6");
        if (entity.ActiveTarget == null)
        {
            entity.ActiveTarget = transform;
        }
        //Debug.Log("7");
    }
}
public enum Behavior { 
    None,
    MoveToTarget,
    RunAway
}