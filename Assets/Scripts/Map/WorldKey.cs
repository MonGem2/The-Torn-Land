using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldKey : MonoBehaviour
{
    // Start is called before the first frame update
    public DetectorScript detector;
    public Loader loader;
    public MapRogulikeGenerator generator;
    public WorldMapCell mapcell;
    public GameObject button;
    bool Setted = false;
    public WorldMapCell.WorldSides worldSide;
    Image img;
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
        img =button.GetComponentInChildren<Image>();
    }
    public void SetWorldSides(int Worldside)
    {
        worldSide = (WorldMapCell.WorldSides)Worldside;
        if (img == null)
        {
            img = button.GetComponentInChildren<Image>();
        }
        if (worldSide == WorldMapCell.WorldSides.E)
        {
            img.sprite = Resources.Load<Sprite>("EKey");
        }
        if (worldSide == WorldMapCell.WorldSides.N)
        {
            img.sprite = Resources.Load<Sprite>("NKey");
        }
        if (worldSide == WorldMapCell.WorldSides.S)
        {
            img.sprite = Resources.Load<Sprite>("SKey");
        }
        if (worldSide == WorldMapCell.WorldSides.W)
        {
            img.sprite = Resources.Load<Sprite>("WKey");
        }
    }
    public void Set(MapRogulikeGenerator newgameobject, WorldMapCell mapCell, Loader l)
    {
        gameObject.SetActive(true);
        Debug.LogWarning("New world key");
        generator = newgameobject;
        mapcell = mapCell;
        loader = l;
    }

    public void OnClick() 
    {
        loader.MapAccess(mapcell);
        if (generator!=null)
        {
            generator.Setter(loader, mapcell);
        }
        
        Setted = true;
        button.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
