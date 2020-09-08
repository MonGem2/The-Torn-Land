using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public StateIco IcoPerhub;
    void Start()
    {
        Debug.Log(player);
        player.OnStateAdded += NewState;
        
    }
    public void NewState(object value)
    {
        StateIco state = Instantiate(IcoPerhub);
        state.state = (State)value;
        state.player = player;
        state.Set();
        state.transform.SetParent(transform, false);
        player.OnStateEnded += state.OnRemove;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
