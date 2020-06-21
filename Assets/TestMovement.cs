using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{

    public float speed = 5;
    public GameObject ActiveMapCell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.S))
            gameObject.transform.Translate(0, -speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.A))
            gameObject.transform.Translate(-speed * Time.deltaTime, 0,  0);
        if (Input.GetKey(KeyCode.D))
            gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        Vector3 point = gameObject.transform.position - ActiveMapCell.transform.position;
        
        if (point.x < 0||point.x>50||point.y<0||point.y>50)
        {
            //Go to another canvas
            ActiveMapCell = gameObject.transform.parent.GetComponent<WorldState>().GetCell(gameObject.transform.position);
            Debug.Log("i go out from canvas");
            return;
        }
        if (point.x < 10)
        {
            Debug.Log("loading -1 0");
            gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( -1, 0, ActiveMapCell, gameObject.transform.position);
        }
        if (point.x >40)
        {
            gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( +1, 0, ActiveMapCell, gameObject.transform.position);
            Debug.Log("loading +1 0");
            //load cell x:+1;y:0
        }
        if (point.y < 10)
        {
            gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( 0, +1, ActiveMapCell, gameObject.transform.position);
            Debug.Log("loading 0 +1");
            //load cell x:0;y:+1
            if (point.x < 10)
            {
                gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( -1, +1, ActiveMapCell, gameObject.transform.position);
                Debug.Log("loading -1 +1");
                //load cell x:-1;y:+1
            }
            if (point.x > 40)
            {
                gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( +1, +1, ActiveMapCell, gameObject.transform.position);
                Debug.Log("loading +1 +1");
                //load cell x:+1;y:+1
            }
        }
        if (point.y > 40)
        {
            gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( 0, -1, ActiveMapCell, gameObject.transform.position);
            Debug.Log("loading 0 -1");
            //load cell x:0;y:-1
            if (point.x < 10)
            {
                gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( -1, -1, ActiveMapCell, gameObject.transform.position);
                Debug.Log("loading -1 -1");
                //load cell x:-1;y:-1
            }
            if (point.x > 40)
            {
                gameObject.transform.parent.GetComponent<WorldState>().CreateWorldMapCell( -1, -1, ActiveMapCell, gameObject.transform.position);
                Debug.Log("loading -1 -1");
                //load cell x:+1;y:-1
            }

        }

    }
}
