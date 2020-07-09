using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static List<List<WorldMapCell>> MapData=new List<List<WorldMapCell>>();
    public static WorldMapCell ActiveCell=null;
    public static int WorldCellSize = 25;
}
