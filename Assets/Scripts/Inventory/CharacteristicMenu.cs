using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicMenu : MonoBehaviour
{      
    //UI
    public GameObject inventory;
    public Toggle togleInventory;
    public Toggle togleSkills;
    public Toggle togleCharacteristics;
    public GameObject ExitMenu;
    public GameObject Buttons;
    public GameObject ExitButton;


    public Player player;
    public XPBar xP;
    public List<CharacteristicsBar> Bars;


    
    bool Levelup=false;
    void Start()
    {
        player.OnLevelChange += LevelUp;
    }
    public void LevelUp(object x)
    {
        xP.LevelUpBegin();
        foreach (var item in Bars)
        {
            item.LevelUP();
        }
        Debug.LogWarning("levelup");
        Levelup = true;               
        Time.timeScale = 0;


        ExitButton.SetActive(true);
        Buttons.SetActive(false);
        inventory.SetActive(true);
        togleCharacteristics.isOn = true;
        togleCharacteristics.enabled = false;
        togleSkills.enabled = false;
        togleInventory.enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close()
    {
        foreach (var item in Bars)
        {
            item.EndLevelUP();
        }
        player.XP = 0;
        player.MaxXP = player.MaxXP * 1.5f;
        xP.LevelUpEnd();
        Debug.LogWarning("close");
        Levelup = false;
        Buttons.SetActive(true);
        ExitButton.SetActive(false);
        togleCharacteristics.enabled = true;
        togleSkills.enabled = true;
        togleInventory.enabled = true;
        Time.timeScale = 1;
        //inventory.SetActive(false);
        ExitMenu.SetActive(false);
    }
    //private void OnDisable()
    //{
    //    Debug.LogWarning("Disabling");
    //    if (Levelup)
    //    {
    //        if (!ExitMenu.active)
    //        {
    //            ExitMenu.SetActive(true);
    //            if (!inventory.active)
    //            {
    //                inventory.SetActive(true);
    //            }
    //            return;
    //        }
    //
    //        
    //    }
    //
    //}
}
