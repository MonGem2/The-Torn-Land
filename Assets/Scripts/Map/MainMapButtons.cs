using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMapButtons : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        if (!Loader.IsMapGenered)
        {
            Loader.LoadMap();
        }
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log(((int)Player.transform.localPosition.x).ToString() + ((int)Player.transform.localPosition.y).ToString());
            StaticData.ActiveCell=StaticData.MapData[(int)Player.transform.localPosition.x][((int)Player.transform.localPosition.y)];
            SceneManager.LoadScene("RoguelikeScene");

        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("i'm here");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Debug.Log(mousePos);
            Player.transform.position = mousePos;

        }
    }
}
