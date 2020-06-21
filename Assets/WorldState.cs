using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] MapCells=new GameObject[4];
    public GameObject WorldCellPerhub;
    public void CreateWorldMapCell(int DX, int DY, GameObject myCanvas, Vector3 position)
    {
        //if (X < 0 || Y < 0 || X > 100 || Y > 100)
        //{
        //    Debug.Log("ERROR:CreateWorldMapCell Uncorrect (WorldState.css)");
        //    return;
        //}        
        WorldMapCell tmpCell = myCanvas.GetComponent<MapRogulikeGenerator>().ThisCell;
        for (int i = 0; i < 4; i++)
        {
            if (MapCells[i] == null)
            {
                continue;
            }
            if (MapCells[i].transform.position == myCanvas.transform.position + new Vector3(DX * 50, DY * -50))
            {
                return;   
            }
        }
        GameObject MapCell = Instantiate(WorldCellPerhub);
        MapCell.transform.SetParent(gameObject.transform);
        MapCell.transform.position = myCanvas.transform.position + new Vector3(DX * 50, DY * -50);
        
        MapCell.GetComponent<MapRogulikeGenerator>().ThisCell = StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX];
        AddMapCell(MapCell, position);
    }
    public void AddMapCell(GameObject mapCell, Vector3 pos)
    {
        float Far=0;
        int k=0;
        for (int i = 0; i < 4; i++)
        {
            if (MapCells[i] == null)
            {
                MapCells[i] = mapCell;
                return;
            }
            if ((MapCells[i].transform.position - pos).magnitude > Far)
            {
                Far = (MapCells[i].transform.position - pos).magnitude;
                k = i;
            }


        }
        Destroy(MapCells[k]);
        MapCells[k] = mapCell;
    }
    public GameObject GetCell(Vector3 possition)
    {
        foreach (var item in MapCells)
        {
            Vector3 point = possition - item.transform.position;
            if (point.x >= 0 && point.x <= 50 && point.y >= 0 && point.y <= 50)
            {
                return item;
            }
        }
        return null;
    }
}
