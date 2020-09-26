using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCell : MonoBehaviour
{
    public AIForm _aIFormPrefab;
    public List<AIForm> Mobs = new List<AIForm>();
    public Player _player;
    public Loader _loader;


    public void Setter(Player player, Loader loader)
    {
        _loader = loader;
        _player = player;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mobs.RemoveAll(item => item == null);


        if (Mobs.Count == 0)
        {
            if (Vector2.Distance(transform.position, _player.transform.position) >= 8 && Vector2.Distance(transform.position, _player.transform.position) < 12)
            {
                foreach (var item in Mobs)
                {
                    item.gameObject.SetActive(true);
                }
                var mob = Instantiate(_aIFormPrefab);
                mob.transform.position = transform.position;
                mob.loader = _loader;
                mob.Loot.Add(_loader.Items[UnityEngine.Random.Range(0, 12)]);
                mob.Loot.Add(_loader.Items[UnityEngine.Random.Range(0, 12)]);
                mob.Loot.Add(_loader.Items[UnityEngine.Random.Range(0, 12)]);
                mob.Loot.Add(_loader.Items[UnityEngine.Random.Range(0, 12)]);
                mob.Loot.Add(_loader.Items[1]);
                // TODO: LootFill

                Mobs.Add(mob);
                Debug.LogError("End spawn");
            }
            
        }
        else
        if (Vector2.Distance(Mobs[0].transform.position, _player.transform.position) < 8)
        {
            foreach (var item in Mobs)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        if (Vector2.Distance(Mobs[0].transform.position, _player.transform.position) >= 12)
        {
            foreach (var item in Mobs)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
