using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public int ID;
    public float Duration;
    //public float Timeout;
    public StateType type;
    public string ico;
    public int spriteN=-1;
    public List<float> Params;
}
public enum StateType { 
    ParameterChanger=0,//complete/checked
    SkillAdder,//complete/checked
    SkillHider,//complete/checked
    DazerMovement,//complete/checked
    DazerAttack,//complete/checked
    DazerItems,
    Move,//need move method
    InfinityPower, //complete/checked
    RegenerationStop,//complete/checked
    playerStatsChange,//complete/checked
    ParameterAdder,//complete/checked
    PlayerParameterAdder//complete/checked
}

//how it work?
//you have a state: state
//state.type == ParameterChanger
//then state.params[(int)ParameterChangerListDescription.MaxHP] will return you maxhp modifier 
//ParameterChanger list decription
public enum ParameterChangerLD
{
    MaxHP=0,
    MaxMP=1,
    MaxST,
    MaxSP,
    SumBaseDamage,
    RegSpeedHP,
    RegSpeedMP,
    RegSpeedST,
    RegSpeedSP,
    MagResist,
    PhyResisst,
    SoulResist,
    Speed
}
public enum MoveLD { 
    type,
    directionType,
    Direction_x,
    Direction_y,
    time,
    distance_damagemodifier
    
}
public enum MoveTypeLD { 
    PhysicPower,
    Impulse,
    Teleportation
}
public enum MoveDirectionTypeLD { 
    toMouse,
    to,
    InDirection,
    InDirectionOfMouse,
    staticPoint
}
public enum PlayerStatsChangeLD { 
    HungryRegSpeed,
    ThirstRegSpeed,
    CorruptionRegSpeed,
    XPBonus
}
public enum ParameterAdderLD
{
    HP = 0,
    MP = 1,
    ST,
    SP
}
public enum PlayerParameterAdderLD {
    XP,
    Corruption,
    Hungry,
    Threat
}
public enum RegenerationStopLD {
    HPReg,
    Magreg,
    STReg,
    SPReg
}
public enum InfinityPowerLD { 
    InfinityHP,
    InfinityMP,
    InfinityST,
    InfinitySP,
    OneShot
}
