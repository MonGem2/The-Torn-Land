using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Creature
{

   // Races race { get; set; }


    //public float MaxFood;
    //public float MaxWater;
    float MaxHP { get; set; }
    float MaxMP { get; set; }
    float MaxST { get; set; }
    float MaxSP { get; set; }

    float HP { get; set; }
    float MP { get; set; }
    float ST { get; set; }
    float SP { get; set; }
    //public float Lucky;
    //public float Food;
    //public float Water;

    float SumBaseDamage { get; set; }

    //public float XPBonus;
    float HPBonus { get; set; }
    float STBonus { get; set; }
    float MPBonus { get; set; }
    float SPBonus { get; set; }

    float RegSpeedHP { get; set; }
    float RegSpeedMP { get; set; }
    float RegSpeedST { get; set; }
    float RegSpeedSP { get; set; }

    float MagResist { get; set; }
    float PhysResist { get; set; }
    float PsyhResist { get; set; }
    float SoulResist { get; set; }



    //public float CraftTalent;
    //public float PhysicalFightTalent;
    //public float ManaFightTalent;
    //public float GodlessTalent;


}
