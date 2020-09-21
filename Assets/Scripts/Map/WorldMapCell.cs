using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCell
{
    private string message = "";
    private int mapHeight ;
    private int mapWidth ;
    private int dangerousLvl = 0;
    private EnvironType environ = EnvironType.Plain;

    public enum WorldSides { 
        N=0,
    W,      E,
        S

    }
    public WorldKey[] keys = new WorldKey[4];
    public void AddKey(WorldKey key)
    {
        for (int blat = 0; blat < this.keys.Length; blat++)
        {
            if (this.keys[blat] == null)
            {
                this.keys[blat] = key;
                key.SetWorldSides(blat);
                return;
            }
        }
    }
    public WorldKey GetKey(int DX, int DY)
    {
        if (DX != 0 && DY != 0)
        {
            return null;
        }
        if (DX==1)
        {
            return keys[(int)WorldSides.E];
        }
        if (DX == -1)
        {
            return keys[(int)WorldSides.W];
        }
        if (DY == 1)
        {
            return keys[(int)WorldSides.S];
        }
        if (DY == -1)
        {
            return keys[(int)WorldSides.N];
        }
        return null;
    }
    public int MapHeight { get => mapHeight; set => mapHeight = value; }
    public int MapWidth { get => mapWidth; set => mapWidth = value; }
    public int PosX;
    public int PosY;
    public string Message { get => message; set => message = value; }
    public int DangerousLvl { get => dangerousLvl; set => dangerousLvl = value; }
    public EnvironType Environ { get => environ; set => environ = value; }
    public bool Generated;
    bool _accesed;
    public bool Accesed { get => _accesed; set
        {
            if (OnMapCellAccesed != null)
            {
                OnMapCellAccesed(this);
            }
            _accesed = value;
        } }
    public OnChangeParameterTrigger OnMapCellAccesed;






    public enum EnvironType
    {
        Wasteland,
        Desert,
        Plain,
        Highlands,
        City,
        Mountains,
        Dungeon
    };

    public WorldMapCell(int posx, int posy)
    {
        PosX = posx;
        PosY = posy;
    }

}
