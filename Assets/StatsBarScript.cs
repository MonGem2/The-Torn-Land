using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBarScript : MonoBehaviour
{
    public Player player;
    public Slider HpBar;
    public Slider CpBar;
    public Slider StBar;
    public Slider HBar;
    public Slider TBar;
    public Slider MpBar;
    public Slider SpBar;
    // Start is called before the first frame update

    // Update is called once per frame
    public void SetMaxParams(float MaxHp, float MaxCp, float MaxSt, float MaxH, float MaxT, float MaxMp, float MaxSp)
    {
        HpBar.maxValue = MaxHp;
        CpBar.maxValue = MaxCp;
        StBar.maxValue = MaxSt;
        HBar.maxValue = MaxH;
        TBar.maxValue = MaxT;
        MpBar.maxValue = MaxMp;
        SpBar.maxValue = MaxSp;
    }
    public void SetValues(float Hp, float Cp, float St, float H, float T, float Mp, float Sp)
    {
        HpBar.value = Hp;
        CpBar.value = Cp;
        StBar.value = St;
        HBar.value = H;
        TBar.value = T;
        MpBar.value = Mp;
        SpBar.value = Sp;
    }
    public void SetHp(float Hp)
    {
        //Debug.Log(HpBar.maxValue);        
        HpBar.value = Hp;
    }
    public void SetCp(float Cp)
    {
        CpBar.value = Cp;
    }
    public void SetSt(float St)
    {
        StBar.value=St;
    }
    public void SetH(float H)
    {
        HBar.value = H;
    }
    public void SetT(float T)
    {
        TBar.value = T;
    }
    public void SetMp(float Mp)
    {
        MpBar.value = Mp;
    }
    public void SetSp(float Sp)
    {
        SpBar.value = Sp;
    }
}
