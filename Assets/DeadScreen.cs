using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Reload()
    {
        Time.timeScale = 1;
        
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);


    }
    public void GoInMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
