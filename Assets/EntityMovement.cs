using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class EntityMovement : MonoBehaviour
{
    public Vector2 ActivePoint;
   // int my_point;
   // public List<Vector2> PathBack; 

    public bool Sleep;
    public float ScnRange=15;
    public float Distance;
    public Behavior behavior;
    //bool PointSeted = true;    
    public AIForm entity;
    public GameObject gm;
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
        Debug.Log(Sleep);
        if (entity.ActiveTarget == null&&!Sleep)
        {
            Debug.LogWarning("zxcvb");
            entity.ChangeTarget();           
        }
        if (entity.ActiveTarget != null && !Sleep && Vector2.Distance(entity.ActiveTarget.position, transform.position) > Distance)
        {
            if (Vector2.Distance(ActivePoint, transform.position) < entity.Speed*Time.deltaTime)
            {
                //ActivePoint = null;
                entity.ChangeTarget();
                //Debug.Log("HEEEEEEYYYYYY");
            
            }
            else
            {
                if (CheckMovement)
                {
                    Vector2 vec = (ActivePoint - (Vector2)transform.position).normalized;
                    if (vec.x >= 0 && vec.y >= 0) { transform.Translate(new Vector2(1, 1).normalized*entity.Speed * Time.deltaTime); }
                    if (vec.x > 0 && vec.y < 0) { transform.Translate(new Vector2(1, -1).normalized * entity.Speed * Time.deltaTime); }
                    if (vec.x < 0 && vec.y > 0) { transform.Translate(new Vector2(-1, 1).normalized * entity.Speed * Time.deltaTime); }
                    if (vec.x <= 0 && vec.y <= 0) { transform.Translate(new Vector2(-1, -1).normalized * entity.Speed * Time.deltaTime); }
                    CheckMovement = false;
                }
                else
                {
                    transform.Translate((ActivePoint - (Vector2)transform.position).normalized * entity.Speed * Time.deltaTime);
                }
                
            }
        }
        if (entity.ActiveTarget != null && !Sleep && Vector2.Distance(entity.ActiveTarget.position, transform.position) < Distance)
        {
            // (PathBack.Last() - (Vector2)transform.position).normalized
            transform.Translate((-ActivePoint + (Vector2)transform.position).normalized * entity.Speed * Time.deltaTime);
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Sleep)
        {
            if (entity.ActiveTarget != null)
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
                    gm.transform.position = ActivePos;
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
        Debug.Log("1");
        foreach (var item in entity.TargetTags)
        {
            if (transform.tag == item)
            {
                Debug.Log("2");
                if (!entity.Targets.Contains(transform))
                {
                    AddTarget(transform);
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
        Debug.Log("5");
        Debug.Log("000000000000000");
        Sleep = false;
        behavior = Behavior.MoveToTarget;
        entity.Targets.Add(transform);
        Debug.Log("6");
        if (entity.ActiveTarget == null)
        {
            entity.ActiveTarget = transform;
        }
        Debug.Log("7");
    }
}
public enum Behavior { 
    MoveToTarget,
    Return,
    RunAway,
    Defender,
    Stay
}