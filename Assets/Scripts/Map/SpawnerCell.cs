using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCell : MonoBehaviour
{
    public int LimitMobs = 0;
    public AIForm _aIFormPrefab;
    public List<AIForm> Mobs = new List<AIForm>();
    public Player _player;
    public Loader _loader;


    public void Setter(Player player, Loader loader)
    {
        _loader = loader;
        _player = player;
        LimitMobs = 3;
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        
        while (true)
        {
            
            Mobs.RemoveAll(item => item == null);


            if (Mobs.Count >= LimitMobs)
            {
                yield return null;
            }

            if (Vector2.Distance(transform.position, _player.transform.position) < 10)
            {
                Debug.LogError("Start spawn");
                foreach (var item in Mobs)
                {
                    item.gameObject.SetActive(true);
                }
                var mob = Instantiate(_aIFormPrefab);
                mob.transform.position = transform.position;
                mob.loader = _loader;
                Mobs.Add(mob);
                yield return new WaitForSeconds(20);
                Debug.LogError("End spawn");
            }
            else
            {
                foreach (var item in Mobs)
                {
                    item.gameObject.SetActive(false);
                }
            }


        }
    
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
