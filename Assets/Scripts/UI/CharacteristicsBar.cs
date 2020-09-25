﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float StartIntake = 10;
    public float Increase = 1.1f;
    public float Count;
    public XPBar xp;
    public Text text;
    public Player player;
    public Characteristics param;
    public string StartText;
    public Button Up;
    public Button Down;
    void Start()
    {
        StartText = text.text;
        Up.gameObject.SetActive(false);
        Down.gameObject.SetActive(false);
        if (param == Characteristics.HpRegSpeed)
        {
            text.text = StartText + player.GetHPRegSpeed() + ";" + player.RegSpeedHP;
            player.RegSpeedHPChangeTrigger += (x) => {
                text.text = StartText + player.GetHPRegSpeed() + ";" + player.RegSpeedHP;
            };
        }
        if (param == Characteristics.MagResist)
        {
            text.text = StartText + player.GetMagReist() + ";" + player.MagResist;
            player.MagResistChangeTrigger += (x) => {
                text.text = StartText + player.GetMagReist() + ";" + player.MagResist;
            };
        }
        if (param == Characteristics.MaxHp)
        {

            text.text = StartText + player.GetMaxHP() + ";" + player.MaxHP;
            player.MaxHPChangeTrigger += (x) => {
                Debug.Log("maxHpChanged");
                text.text = StartText + player.GetMaxHP() + ";" + player.MaxHP;
                Debug.LogWarning(x);
                Debug.LogWarning(player.MaxHP);
                Debug.LogWarning(player.GetMaxHP());
            };
        }
        if (param == Characteristics.MaxMp)
        {
            text.text = StartText + player.GetMaxMP() + ";" + player.MaxMP;
            player.MaxMPChangeTrigger += (x) => {
                text.text = StartText + player.GetMaxMP() + ";" + player.MaxMP;
            };
        }
        if (param == Characteristics.MaxSp)
        {
            text.text = StartText + player.GetMaxSP() + ";" + player.MaxSP;
            player.MaxSPChangeTrigger += (x) => {
                text.text = StartText + player.GetMaxSP() + ";" + player.MaxSP;
            };
        }
        if (param == Characteristics.MaxSt)
        {
            text.text = StartText + player.GetMaxST() + ";" + player.MaxST;
            player.MaxSTChangeTrigger += (x) => {
                text.text = StartText + player.GetMaxST() + ";" + player.MaxST;
            };
        }
        if (param == Characteristics.MpRegSpeed)
        {
            text.text = StartText + player.GetMPRegSpeed() + ";" + player.RegSpeedMP;
            player.RegSpeedMPChangeTrigger += (x) => {
                text.text = StartText + player.GetMPRegSpeed() + ";" + player.RegSpeedMP;
            };
        }
        if (param == Characteristics.PhysResist)
        {
            text.text = StartText + player.GetPhyResist() + ";" + player.PhysResist;
            player.PhyResistChangeTrigger += (x) => {
                text.text = StartText + player.GetPhyResist() + ";" + player.PhysResist;
            };
        }
        if (param == Characteristics.SoulResist)
        {
            text.text = StartText + player.GetSoulResist() + ";" + player.SoulResist;
            player.SoulResistChangeTrigger += (x) => {
                text.text = StartText + player.GetSoulResist() + ";" + player.SoulResist;
            };
        }
        if (param == Characteristics.Speed)
        {
            text.text = StartText + player.GetSpeed() + ";" + player.Speed;
            player.OnSpeedChanged += (x) => {
                text.text = StartText + player.GetSpeed() + ";" + player.Speed;
            };
        }
        if (param == Characteristics.SpRegSpeed)
        {
            text.text = StartText + player.GetSPRegSpeed() + ";" + player.RegSpeedSP;
            player.RegSpeedSPChangeTrigger += (x) => {
                text.text = StartText + player.GetSPRegSpeed() + ";" + player.RegSpeedSP;
            };
        }
        if (param == Characteristics.StRegSpeed)
        {
            text.text = StartText + player.GetSTRegSpeed() + ";" + player.RegSpeedST;
            player.RegSpeedSTChangeTrigger += (x) => {
                text.text = StartText + player.GetSTRegSpeed() + ";" + player.RegSpeedST;
            };
        }
        if (param == Characteristics.SumBaseDamage)
        {
            text.text = StartText + player.GetMaxSumBaseDamage() + ";" + player.SumBaseDamage;
            player.SumBaseDamageChangeTrigger += (x) => {
                text.text = StartText + player.GetMaxSumBaseDamage() + ";" + player.SumBaseDamage;
            };
        }
    }

    public float GetPlayerParameter()
    {
        if (param == Characteristics.HpRegSpeed)
        {
            return player.RegSpeedHP;
        }
        if (param == Characteristics.MagResist)
        {
            return player.MagResist;
        }
        if (param == Characteristics.MaxHp)
        {

            return player.MaxHP;
        }
        if (param == Characteristics.MaxMp)
        {
            return player.MaxMP;
        }
        if (param == Characteristics.MaxSp)
        {
            return player.MaxSP;
        }
        if (param == Characteristics.MaxSt)
        {
            return player.MaxST;
        }
        if (param == Characteristics.MpRegSpeed)
        {
            return player.RegSpeedMP;
        }
        if (param == Characteristics.PhysResist)
        {
            return player.PhysResist;
        }
        if (param == Characteristics.SoulResist)
        {
            return player.SoulResist;
        }
        if (param == Characteristics.Speed)
        {
            return player.Speed;
        }
        if (param == Characteristics.SpRegSpeed)
        {
            return player.RegSpeedSP;
        }
        if (param == Characteristics.StRegSpeed)
        {
            return player.RegSpeedST;
        }
        if (param == Characteristics.SumBaseDamage)
        {
            return player.SumBaseDamage;
        }
        return 0;
    }
    public void SetPlayerParameter(float Param)
    {
        if (param == Characteristics.HpRegSpeed)
        {
            player.RegSpeedHP=Param;
        }
        if (param == Characteristics.MagResist)
        {
            player.MagResist=Param;
        }
        if (param == Characteristics.MaxHp)
        {

            player.MaxHP=Param;
        }
        if (param == Characteristics.MaxMp)
        {
            player.MaxMP=Param;
        }
        if (param == Characteristics.MaxSp)
        {
            player.MaxSP=Param;
        }
        if (param == Characteristics.MaxSt)
        {
            player.MaxST=Param;
        }
        if (param == Characteristics.MpRegSpeed)
        {
            player.RegSpeedMP=Param;
        }
        if (param == Characteristics.PhysResist)
        {
            player.PhysResist=Param;
        }
        if (param == Characteristics.SoulResist)
        {
            player.SoulResist=Param;
        }
        if (param == Characteristics.Speed)
        {
            player.Speed=Param;
        }
        if (param == Characteristics.SpRegSpeed)
        {
            player.RegSpeedSP=Param;
        }
        if (param == Characteristics.StRegSpeed)
        {
            player.RegSpeedST=Param;
        }
        if (param == Characteristics.SumBaseDamage)
        {
            player.SumBaseDamage=Param;
        }
        
    }
    public float GetPlayerParameterNoBonus()
    {
        if (param == Characteristics.HpRegSpeed)
        {
            return player.GetHPRegSpeed();
        }
        if (param == Characteristics.MagResist)
        {
            return player.GetMagReist();
        }
        if (param == Characteristics.MaxHp)
        {

            return player.GetMaxHP();
        }
        if (param == Characteristics.MaxMp)
        {
            return player.GetMaxMP();
        }
        if (param == Characteristics.MaxSp)
        {
            return player.GetMaxSP();
        }
        if (param == Characteristics.MaxSt)
        {
            return player.GetMaxST();
        }
        if (param == Characteristics.MpRegSpeed)
        {
            return player.GetMPRegSpeed();
        }
        if (param == Characteristics.PhysResist)
        {
            return player.GetPhyResist();
        }
        if (param == Characteristics.SoulResist)
        {
            return player.GetSoulResist();
        }
        if (param == Characteristics.Speed)
        {
            return player.GetSpeed();
        }
        if (param == Characteristics.SpRegSpeed)
        {
            return player.GetSPRegSpeed();
        }
        if (param == Characteristics.StRegSpeed)
        {
            return player.GetSTRegSpeed();
        }
        if (param == Characteristics.SumBaseDamage)
        {
            return player.GetMaxSumBaseDamage();
        }
        return 0;
    }
    public void SetTextBox(string Text)
    {
        text.text = StartText + Text;
     
    }
    public void SetButtons(bool value)
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            var item = transform.GetChild(i);
            if (item.GetComponent<Button>() != null)
            {
                item.gameObject.SetActive(value);
            }
        }
    }//set child buttons on/off
    bool LvlUp = false;
    public void LevelUP()
    {
        Count = GetPlayerParameterNoBonus();
        LvlUp = true;
        UpdateUI();        
        SetButtons(true);        
        SetTextBox(Count.ToString());
    }
    public void EndLevelUP()
    {
        LvlUp = false;
        SetPlayerParameter(Count);
        UpdateUI();
        SetButtons(false);
        
    }
    public void UpdateUI()
    {
        if (LvlUp)
        {
            text.text = StartText + Count + ";";
            return;
        }

        text.text = StartText + GetPlayerParameterNoBonus() + ";" + GetPlayerParameter();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonUp()
    {
        float Price =  Balancer.GetParameterPrice(Count, player.Lvl);
        if(xp.XPGet(Price))
        {
            if (param == Characteristics.Speed)
                Count += 0.05f;
            else if (param == Characteristics.SumBaseDamage)
                Count++;
            else Count += 10;
        }
        UpdateUI();
    }
    public void OnButtonDown()
    {
        float Price = Balancer.GetParameterPrice(Count-1, player.Lvl);
        if (xp.XPReturn(Price))
        {
            if (param == Characteristics.Speed)
                Count -= 0.05f;
            else if (param == Characteristics.SumBaseDamage)
                Count--;
            else Count -= 10;
        }
        UpdateUI();
    }
}
public enum Characteristics
{
    MaxHp,
    MaxMp,
    MaxSt,
    MaxSp,
    HpRegSpeed,
    MpRegSpeed,
    StRegSpeed,
    SpRegSpeed,
    SumBaseDamage,
    MagResist,
    SoulResist,
    PhysResist,
    Speed
}