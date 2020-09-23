using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    public OnChangeParameterTrigger ColisionEnter;
    public OnChangeParameterTrigger ColisionExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(ColisionEnter!=null)
        {
            ColisionEnter(collision.transform);

        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (ColisionEnter != null)
        {
            ColisionEnter(collision.transform);

        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (ColisionExit != null)
        {
            ColisionExit(collision.transform);

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {

        if (ColisionExit != null)
        {
            ColisionExit(collision.transform);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
