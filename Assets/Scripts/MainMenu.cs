using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    public Player player;
    public Loader loader;
    public GameObject Continue;
    public GameObject Inventory;
    public GameObject LittleMenu;
    // Start is called before the first frame update
    void Start()
    {
        try
        {

            if (!File.Exists("player.save"))
            {
                Continue.SetActive(false);
            }

        }
        catch (System.Exception)
        {

         
        }
    }

    public void OnStartNewGame() {
        File.Delete("player.save");
        SceneManager.LoadScene("RoguelikeScene");
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
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
    public void OnMenuButtonClick()
    {
        if (LittleMenu.active)
        {

            Time.timeScale = 1;
            Debug.LogWarning("!@#$%^&*()    ");
            Debug.Log("0");
            Inventory.SetActive(true);
            LittleMenu.SetActive(false);

        }else
        {

            Time.timeScale = 0;
            Debug.LogWarning("!@#$%^&*()    ");
            Debug.Log("1");
            Inventory.SetActive(false);
            LittleMenu.SetActive(true);

        }
    }
}
