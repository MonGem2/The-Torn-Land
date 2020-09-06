using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartGameClick() {
        Debug.Log("4");
        SceneManager.LoadScene("RoguelikeScene");
    }
    public void OnSettingsClick()
    {
        Debug.Log("3");
        Settings.SetActive(true);
    }
    public void OnExitClick()
    {
        Debug.Log("2");
        Application.Quit();
    }
    public void OnSettingsReturnClick()
    {
        Debug.Log("1");
        Settings.SetActive(false);
    }
}
