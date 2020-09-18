﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float XpIntake = 10;
    public float Increase = 1.1f;
    public Text text;
    public Player player;
    public Characteristics param;
    public string StartText;
    void Start()
    {
        StartText = text.text;
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
    public void OnLevelUpStart()
    {

    }
    public void OnLevelUpEnded()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonUp()
    {

    }
    public void OnButtonDown()
    {

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