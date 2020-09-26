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
        LimitMobs = 1;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    float gameTimer = 0;

    // Update is called once per frame
    void Update()
    {
        Mobs.RemoveAll(item => item == null);
        gameTimer += Time.deltaTime;

        if (gameTimer!= 0 && gameTimer < 30)
        {
            return;
        }

        if (Mobs.Count == 0)
        {
            if (Vector2.Distance(transform.position, _player.transform.position) < 8)
            {
                Debug.LogError("Start spawn");
                foreach (var item in Mobs)
                {
                    item.gameObject.SetActive(true);
                }
                var mob = Instantiate(_aIFormPrefab);
                mob.transform.position = transform.position;
                mob.loader = _loader;

                // TODO: LootFill

                Mobs.Add(mob);
                Debug.LogError("End spawn");
                gameTimer = 0;
            }
            
        }
        else 
        if(Vector2.Distance(Mobs[0].transform.position, _player.transform.position) >= 8)
        {
            foreach (var item in Mobs)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
