using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<MapCellScript> Cells;
    public GameObject CellPerhub;
    public int mapHeight=100;
    public int mapWidth = 100;
    // Start is called before the first frame update
    void Start()
    {



        for (int i = 0; i < mapWidth; i++)
        {
            for (int k = 0; k < mapHeight; k++)
            {

                MapCellScript gg = Instantiate(CellPerhub).GetComponent<MapCellScript>();
                gg.gameObject.transform.SetParent(this.gameObject.transform);
                gg.X = i;
                gg.Y = k;

                Cells.Add(gg);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
