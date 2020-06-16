using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMapButtons : MonoBehaviour
{
    public GameObject Player;
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log(((int)Player.transform.localPosition.x).ToString() + ((int)Player.transform.localPosition.y).ToString());
            StaticData.ActiveCell=StaticData.MapData[(int)Player.transform.localPosition.x][((int)Player.transform.localPosition.y)];
            SceneManager.LoadScene("RoguelikeScene");

        }
    }
}
