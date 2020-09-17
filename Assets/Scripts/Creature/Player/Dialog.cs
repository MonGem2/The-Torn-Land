using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Button> Variants;
    public Text text;
    void Start()
    {
            
    }
    public OnChangeParameterTrigger OnVariant1;
    public OnChangeParameterTrigger OnVariant2;
    public OnChangeParameterTrigger OnVariant3;
    public OnChangeParameterTrigger OnVariant4;
    public void SetDialog(string Text, string V1, string V2, string V3, string V4)
    {
        text.text = Text;
        Variants[0].onClick.AddListener(
            () =>
            {
                if (OnVariant1 != null)
                {
                    OnVariant1(V1);
                }
            });
        Variants[1].onClick.AddListener(
    () =>
    {
        if (OnVariant2 != null)
        {
            OnVariant2(V2);
        }
    });
        Variants[2].onClick.AddListener(
    () =>
    {
        if (OnVariant3 != null)
        {
            OnVariant3(V3);
        }
    });
        Variants[3].onClick.AddListener(
    () =>
    {
        if (OnVariant4 != null)
        {
            OnVariant4(V4);
        }
    });
    }
    // Update is called once per frame
    void Update()
    {
        
    }



}
