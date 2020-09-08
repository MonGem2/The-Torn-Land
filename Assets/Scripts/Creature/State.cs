using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public int ID;
    public float Duration;
    //public float Timeout;
    public StateType type;

    public List<int> Params;
}
public enum StateType { 
    ParameterChanger=0,//complete
    SkillAdder,//complete
    SkillHider,//complete
    DazerMovement,
    DazerAttack,
    DazerItems,
    RegenerationChanger,//complete
    Move,
    InfinityPower, //complete
    RegenerationStop//complete
}

