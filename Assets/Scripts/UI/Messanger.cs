using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messanger : MonoBehaviour
{
    // Start is called before the first frame update
    bool setted = false;
    public Text txt;
    public Coroutine coroutine;
    //public float DefaultDuration;
    void Start()
    {
        transform.position = transform.position + new Vector3(0, 0, -11);
        txt = GetComponent<Text>();
    }
    public void SetMessage(string text, float duration=1)
    {
        txt.text = text;
        if (!setted)
        {
            setted = true;
            transform.position = transform.position - new Vector3(0, 0, -11);
            coroutine = StartCoroutine(Hide(duration));
        }
        else {
            Debug.Log("Messanger:corutine stop");
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(Hide(duration));
        }
        
    }
    public IEnumerator Hide(float duration)
    {
        yield return new WaitForSeconds(duration);
//        Debug.Log("Messanger:corutine ended");
        transform.position = transform.position + new Vector3(0, 0, -11);
        setted = false;
    }
    // Update is called once per frame
}
