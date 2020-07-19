using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Loader loader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(loader.BulletsPerhubs[0]);
            Bullet bl = bullet.GetComponent<Bullet>();
            BulletData data = new BulletData();
            data.AdditionalAngle = 0;
            data.DeltaAngle = 15;
            data.Distance = new Vector2(0.5f, 0.5f);
            data.DontAttack = new List<string>();
            data.DontAttack.Add("player");
            data.EffectsIDs = new List<int>();
            data.FlyTime = 5;
            data.ManaDamage = 0;
            data.PerhubID = 0;
            data.PhysicDamage = 5;
            data.Range = 1;
            data.SoulDamage = 0;
            data.Through = false;
            data.type = BulletType.Bullet;
            bl.data = data;
            bl.loader = loader;

            bl.Shoot(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
