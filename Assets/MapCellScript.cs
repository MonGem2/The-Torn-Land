using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCellScript : MonoBehaviour
{
    public float X;
    public float Y;
    public Material material;
    public float SomeParameter;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(X, Y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
