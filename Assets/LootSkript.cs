using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSkript : MonoBehaviour
{
    public Canvas LootView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LootView.gameObject.SetActive(true);
            Debug.Log("Heeey");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LootView.gameObject.SetActive(false);
            Debug.Log("Heeey11");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Heey1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
