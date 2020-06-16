using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRogulikeGenerator : MonoBehaviour
{
    public WorldMapCellScript ThisCell=StaticData.ActiveCell;
    public GameObject CellPerhub;
    public int mapHeight=50;
    public int mapWidth = 50;
    public List<GameObject> MapCells = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ThisCell.Message);


        for (int i = 0; i < mapWidth; i++)
        {
            for (int k = 0; k < mapHeight; k++)
            {
                GameObject cell = Instantiate(CellPerhub);
                cell.transform.SetParent(gameObject.transform);
                cell.GetComponent<MapCellScript>().X = i;
                cell.GetComponent<MapCellScript>().Y = k;
                MapCells.Add(cell);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
