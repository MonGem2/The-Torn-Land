using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateIco : MonoBehaviour
{
    public State state;
    public Image ico;
    public Player player;
    void Start()
    {
        
    }
    // Start is called before the first frame update
    public void Set()
    {
        if (state.type == StateType.ParameterAdder || state.type == StateType.PlayerParameterAdder)
        {
            Destroy(gameObject);
            return;
        }
        if (state.spriteN == -1)
        {
            Debug.Log(state.ico);
            //Resources.Load<Sprite>(state.ico);
            ico = GetComponent<Image>();
            ico.sprite = Resources.Load<Sprite>(state.ico);
        }
        else {
            
                ico = GetComponent<Image>();

                ico.sprite = Resources.LoadAll<Sprite>(state.ico)[state.spriteN];
           
        }
    }

    // Update is called once per frame
    public void OnRemove(object value)
    {
        if (((State)value).ID == state.ID)
        {
            player.OnStateEnded -= OnRemove;
            Debug.LogWarning("zxcvbnm,.");
            Destroy(this.gameObject);
        }
    }
}
