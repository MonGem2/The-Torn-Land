using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public List<BulletData> Bullets;
    public List<int> EffectsIds;
    public float Cooldown;
    public bool CanBeUsed = true;
    public float MPIntake;
    public float SPIntake;
    public float STIntake;
}
