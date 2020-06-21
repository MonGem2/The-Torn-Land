using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCell
{
    private string message = "";
    private int mapHeight = 50;
    private int mapWidth = 50;
    private int dangerousLvl = 0;
    private EnvironType environ = EnvironType.Plain;



    public int MapHeight { get => mapHeight; set => mapHeight = value; }
    public int MapWidth { get => mapWidth; set => mapWidth = value; }
    public int PosX;
    public int PosY;
    public string Message { get => message; set => message = value; }
    public int DangerousLvl { get => dangerousLvl; set => dangerousLvl = value; }
    public EnvironType Environ { get => environ; set => environ = value; }








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

    public WorldMapCell(string message, int height, int width)
    {
        Message = message;
        MapHeight = height;
        MapWidth = width;
    }

}
