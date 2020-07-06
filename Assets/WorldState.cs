using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    // Start is called before the first frame update
    public MapRogulikeGenerator[] MapCells=new MapRogulikeGenerator[4];
    public MapRogulikeGenerator WorldCellPerhub;
    public MapRogulikeGenerator ActiveMapCell;
    public void CreateWorldMapCell(int DX, int DY, MapRogulikeGenerator myCanvas, Vector3 position)
    {


        Debug.Log("HOLHELL");

        //if (X < 0 || Y < 0 || X > 100 || Y > 100)
        //{
        //    Debug.Log("ERROR:CreateWorldMapCell Uncorrect (WorldState.css)");
        //    return;
        //}        
        WorldMapCell tmpCell = myCanvas.ThisCell;
        for (int i = 0; i < 4; i++)
        {
            if (MapCells[i] == null)
            {
                continue;
            }
            if (MapCells[i].transform.position == myCanvas.transform.position + new Vector3(DX * 25, DY * -25))
            {
                return;
            }
        }


        MapRogulikeGenerator MapCell = Instantiate(WorldCellPerhub);
        MapCell.transform.SetParent(gameObject.transform);
        MapCell.transform.position = myCanvas.transform.position + new Vector3(DX * 25, DY * -25);
        
        MapCell.ThisCell = StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX];
        AddMapCell(MapCell, position);
    }
    public void AddMapCell(MapRogulikeGenerator mapCell, Vector3 pos)
    {
        Debug.Log("POEZD_SHIZY");
        float Far=0;
        int k=0;
        for (int i = 0; i < 4; i++)
        {
            if (MapCells[i] == null)
            {
                MapCells[i] = mapCell;
                return;
            }
            if ((MapCells[i].transform.position + new Vector3(12.5f,12.5f) - pos).magnitude > Far)
            {
                Far = (MapCells[i].transform.position - pos + new Vector3(12.5f, 12.5f)).magnitude;
                k = i;
                
            }
            Debug.Log(Far);

        }
        Debug.Log("k:"+k);
        Destroy(MapCells[k].gameObject);
        MapCells[k] = mapCell;
    }
    public MapRogulikeGenerator GetCell(Vector3 possition)
    {
        foreach (var item in MapCells)
        {
            Vector3 point = possition - item.transform.position;
            if (point.x >= 0 && point.x <= 25 && point.y >= 0 && point.y <= 25)
            {
                return item;
            }
        }
        return null;
    }

    public void LogicGeneration(Vector3 position)
    {

        

        Vector3 point = position - ActiveMapCell.transform.position;

        //Debug.Log(point);

        if (point.x < 0 || point.x > 25 || point.y < 0 || point.y > 25)
        {
            Debug.Log("POEZD");
            //Go to another canvas
            ActiveMapCell = GetCell(position);
            Debug.Log("i go out from canvas");
            return;
        }
        if (point.x < 5)
        {
            Debug.Log("POEZD1");
            //Debug.Log("loading -1 0");
            CreateWorldMapCell(-1, 0, ActiveMapCell, position);
        }
        if (point.x > 20)
        {
            Debug.Log("POEZD2");
            CreateWorldMapCell(+1, 0, ActiveMapCell, position);
            //Debug.Log("loading +1 0");
            //load cell x:+1;y:0
        }
        if (point.y < 5)
        {
            Debug.Log("POEZD3");
            CreateWorldMapCell(0, +1, ActiveMapCell, position);
            // Debug.Log("loading 0 +1");
            //load cell x:0;y:+1
            if (point.x < 5)
            {
                Debug.Log("POEZD4");
                CreateWorldMapCell(-1, +1, ActiveMapCell, position);
                //Debug.Log("loading -1 +1");
                //load cell x:-1;y:+1
            }
            if (point.x > 20)
            {
                Debug.Log("POEZD5");
                CreateWorldMapCell(+1, +1, ActiveMapCell, position);
                //Debug.Log("loading +1 +1");
                //load cell x:+1;y:+1
            }
        }
        if (point.y > 20)
        {
            Debug.Log("POEZD6");
            CreateWorldMapCell(0, -1, ActiveMapCell, position);
            //Debug.Log("loading 0 -1");
            //load cell x:0;y:-1
            if (point.x < 5)
            {
                Debug.Log("POEZD7");
                CreateWorldMapCell(-1, -1, ActiveMapCell, position);
                //Debug.Log("loading -1 -1");
                //load cell x:-1;y:-1
            }
            if (point.x > 20)
            {
                Debug.Log("POEZD8");
                CreateWorldMapCell(+1, -1, ActiveMapCell, position);
                //Debug.Log("loading +1 -1");
                //load cell x:+1;y:-1
            }

        }
    }

}
