using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public int ID;
    public float Duration;
    //public float Timeout;
    public int Type;
    public StateType type { get { return (StateType)Type; } set { Type = (int)value; } }
    public string ico;
    public int spriteN=-1;
    public List<float> Params;
}
public enum StateType {
    /// <summary>
    ///     MaxHP=0,
    ///     MaxMP = 1,
    ///     MaxST,
    ///     MaxSP,
    ///     SumBaseDamage,
    ///     RegSpeedHP,
    ///     RegSpeedMP,
    ///     RegSpeedST,
    ///     RegSpeedSP,
    ///     MagResist,
    ///     PhyResisst,
    ///     SoulResist,
    ///     Speed
    /// </summary>
    ParameterChanger =0,//complete/checked
    /// <summary>
    /// Skills ids
    /// </summary>
    SkillAdder,//complete/checked
    /// <summary>
    /// Skills ids
    /// </summary>
    SkillHider,//complete/checked
    /// <summary>
    /// No params
    /// </summary>
    DazerMovement,//complete/checked
    /// <summary>
    /// No Params
    /// </summary>
    DazerAttack,//complete/checked
    /// <summary>
    /// Items ids or no params
    /// </summary>
    DazerItems,
    /// <summary>
    ///     type,
    ///     directionType,
    ///     Direction_x,
    ///     Direction_y,
    ///     time,
    ///     distance_damagemodifier
    /// </summary>
    Move,//need move method
    /// <summary>
    ///     InfinityHP,
    ///     InfinityMP,
    ///     InfinityST,
    ///     InfinitySP,
    ///     OneShot
    /// </summary>
    InfinityPower, //complete/checked
    /// <summary>
    ///     HPReg,
    ///     Magreg,
    ///     STReg,
    ///     SPReg
    /// </summary>
    RegenerationStop,//complete/checked
    /// <summary>
    ///     HungryRegSpeed,
    ///     ThirstRegSpeed,
    ///     CorruptionRegSpeed,
    ///     XPBonus
    /// </summary>
    playerStatsChange,//complete/checked
    /// <summary>
    ///     HP = 0,
    ///     MP = 1,
    ///     ST,
    ///     SP
    /// </summary>
    ParameterAdder,//complete/checked
    /// <summary>
    ///     XP,
    ///     Corruption,
    ///     Hungry,
    ///     Threat
    /// </summary>
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
