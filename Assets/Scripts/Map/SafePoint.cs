using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    // Start is called before the first frame update
    public DetectorScript detector;
    public GameObject Button;
    public Loader loader;
    
    void Start()
    {
        //Debug.LogError("????");
        detector.ColisionEnter += (x) => {
          //  Debug.LogError("1????");
            if (((Transform)x).tag=="Player")
            {
                Button.SetActive(true);
            }
        };
        detector.ColisionExit += (x) => {
           // Debug.LogError("2????");
            if (((Transform)x).tag == "Player")
            {
                Button.SetActive(false);
            }
        };
    }
    public void Safe()
    {
        loader.SavePlayer();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
