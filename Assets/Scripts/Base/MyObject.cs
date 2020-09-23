using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyObject : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Damage(BulletData bulletData, bool MaxEffectSet = true) { 
        
    }
}
