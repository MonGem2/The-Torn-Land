using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    public EntityMovement EntityMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        EntityMovement.DetectSomething(collision.transform);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        EntityMovement.DetectSomething(collision.transform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
