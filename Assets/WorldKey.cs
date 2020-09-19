using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldKey : MonoBehaviour
{
    // Start is called before the first frame update
    public DetectorScript detector;
    public Loader loader;
    public MapRogulikeGenerator generator;
    public WorldMapCell mapcell;
    public GameObject button;
    bool Setted = false;
    void Start()
    {
        detector.ColisionEnter += (x) =>
        {
            if(((Transform)x).tag=="Player"&&!Setted)
            {
                button.SetActive(true);
            }
        };
        detector.ColisionExit += (x) =>
        {
            if (((Transform)x).tag == "Player"&&!Setted)
            {
                button.SetActive(false);
            }
        };
    }
    public void Set(MapRogulikeGenerator newgameobject, WorldMapCell mapCell, Loader l)
    {
        generator = newgameobject;
        mapcell = mapCell;
        loader = l;
    }

    public void OnClick() 
    {
        generator.Setter(loader, mapcell);
        Setted = true;
        button.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
