using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    // Start is called before the first frame update
    public MapRogulikeGenerator[] MapCells = new MapRogulikeGenerator[4];
    public MapRogulikeGenerator WorldCellPerhub;
    public MapRogulikeGenerator ActiveMapCell;
    public List<WorldMapCell> GeneratorQuery = new List<WorldMapCell>();
    public int cellcount = 1;
    public Loader loader;
    Vector2 ZeroPosition = new Vector2(10, 10);
    public OnChangeParameterTrigger OnGenerationEnded;
    
    class GenerationCallback
    {
        public int[,] Map;
        public WorldMapCell mapCell;
    }
    public void CreateWorldMapCell(int DX, int DY, MapRogulikeGenerator myCanvas, Vector3 position)
    {


        //Debug.Log("HOLHELL");

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
            if (MapCells[i].transform.position == myCanvas.transform.position + new Vector3(DX * StaticData.WorldCellSize, DY * -StaticData.WorldCellSize))
            {
                return;
            }
        }

        //Debug.Log("Creating new cell");
        MapRogulikeGenerator MapCell = Instantiate(WorldCellPerhub);
        MapCell.transform.SetParent(gameObject.transform);
        MapCell.transform.position = myCanvas.transform.position + new Vector3(DX * StaticData.WorldCellSize, DY * -StaticData.WorldCellSize);
        if (StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].Accesed)
        {
            MapCell.Setter(loader, StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX]);
        }
        else if(!StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].Generated) {
            StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].OnMapCellAccesed += (x) => {
                lock (GeneratorQuery)
                {
                    MapCell.Setter(loader, StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX]);
                }
            };
            Debug.LogWarning("Adding to list "+tmpCell.PosY+DY+"  "+ tmpCell.PosX+DX);
            GeneratorQuery.Add(StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX]);
        }
        //Debug.Log($"Coordinate:{DY}, DX:{DX}");
        //Debug.Log($"ResultCoordinate:{tmpCell.PosY + DY},:{tmpCell.PosX + DX}");
        //Debug.Log($"MessageThis:{StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].Message}");
        //Debug.Log($"MessageThis:{StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].PosY}");
        AddMapCell(MapCell, position);
    }
    public void CreateWorldMapCell(Vector2 position)
    {
        Debug.Log("GeneratingMapCell");
        Debug.Log(position);
        position += new Vector2(StaticData.WorldCellSize / 2, StaticData.WorldCellSize / 2);
        Debug.Log(position);
        position /= StaticData.WorldCellSize;
        Debug.Log(position);
        position.x = Mathf.FloorToInt(position.x);
        position.y = Mathf.FloorToInt(position.y);
        Debug.Log((int)position.x+","+(int)position.y);
        Debug.Log((int)-0.1);
        WorldMapCell worldMapCell = StaticData.MapData[(int)position.x+(int)ZeroPosition.x][(int)position.y+(int)ZeroPosition.y];
        Debug.Log(worldMapCell.PosX+"  "+worldMapCell.PosY);
        for (int i = 0; i < 4; i++)
        {
            if (MapCells[i] == null)
            {
                continue;
            }
            if (MapCells[i].ThisCell.PosX == worldMapCell.PosX&& MapCells[i].ThisCell.PosY == worldMapCell.PosY)
            {
                return;
            }
        }
        Debug.Log(new Vector3((position.x * StaticData.WorldCellSize) - (float)StaticData.WorldCellSize / 2, (float)(position.y * StaticData.WorldCellSize) - (float)StaticData.WorldCellSize / 2, 1));
        MapRogulikeGenerator MapCell = Instantiate(WorldCellPerhub);
        MapCell.transform.SetParent(gameObject.transform);
        MapCell.transform.position = new Vector3((position.x * StaticData.WorldCellSize) - (float)StaticData.WorldCellSize / 2, (float)(position.y * StaticData.WorldCellSize) - (float)StaticData.WorldCellSize / 2, 1);

        MapCell.Setter(loader, worldMapCell);
        //Debug.Log($"Coordinate:{DY}, DX:{DX}");
        //Debug.Log($"ResultCoordinate:{tmpCell.PosY + DY},:{tmpCell.PosX + DX}");
        //Debug.Log($"MessageThis:{StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].Message}");
        //Debug.Log($"MessageThis:{StaticData.MapData[tmpCell.PosY + DY][tmpCell.PosX + DX].PosY}");
        AddMapCell(MapCell, position);

    }
    public void AddMapCell(MapRogulikeGenerator mapCell, Vector3 pos)
    {
        //Debug.Log("POEZD_SHIZY");
        float Far=0;
        int k=0;
        for (int i = 0; i < 4; i++)
        {
            if (MapCells[i] == null)
            {
              //  Debug.Log("qwertyuiop");
                MapCells[i] = mapCell;
                return;
            }
            if ((MapCells[i].transform.position + new Vector3(StaticData.WorldCellSize/2, StaticData.WorldCellSize/2) - pos).magnitude > Far)
            {
                Far = (MapCells[i].transform.position - pos + new Vector3(StaticData.WorldCellSize/2, StaticData.WorldCellSize/2)).magnitude;
                k = i;
                
            }
            //Debug.Log(Far);

        }
        //Debug.Log("k:"+k);
        
        Destroy(MapCells[k].gameObject);
        MapCells[k] = mapCell;
    }
    public MapRogulikeGenerator GetCell(Vector3 possition)
    {
        foreach (var item in MapCells)
        {
            if (item == null)
            {
                continue;
            }
            Vector3 point = possition - item.transform.position;
            if (point.x >= 0 && point.x <= StaticData.WorldCellSize && point.y >= 0 && point.y <= StaticData.WorldCellSize)
            {
                return item;
            }
        }
        return null;
    }

    public void LogicGeneration(Vector3 position)
    {

        if (ActiveMapCell == null)
        {
            CreateWorldMapCell(position);
            ActiveMapCell = GetCell(position);
        }

        Vector3 point = position - ActiveMapCell.transform.position;

        //Debug.Log(point);

        if (point.x < 0 || point.x > StaticData.WorldCellSize || point.y < 0 || point.y > StaticData.WorldCellSize)
        {
            //Debug.Log("POEZD");
            //Go to another canvas
            ActiveMapCell = GetCell(position);

            //Debug.Log("i go out from canvas");
            return;
        }
        if (point.x < 5)
        {
            //Debug.Log("POEZD1");
            //Debug.Log("loading -1 0");
            CreateWorldMapCell(-1, 0, ActiveMapCell, position);
        }
        if (point.x > 20)
        {
            //Debug.Log("POEZD2");
            CreateWorldMapCell(+1, 0, ActiveMapCell, position);
            //Debug.Log("loading +1 0");
            //load cell x:+1;y:0
        }
        if (point.y < 5)
        {
            //Debug.Log("POEZD3");
            CreateWorldMapCell(0, +1, ActiveMapCell, position);
            // Debug.Log("loading 0 +1");
            //load cell x:0;y:+1
            if (point.x < 5)
            {
               //Debug.Log("POEZD4");
                CreateWorldMapCell(-1, +1, ActiveMapCell, position);
                //Debug.Log("loading -1 +1");
                //load cell x:-1;y:+1
            }
            if (point.x > 20)
            {
                //Debug.Log("POEZD5");
                CreateWorldMapCell(+1, +1, ActiveMapCell, position);
                //Debug.Log("loading +1 +1");
                //load cell x:+1;y:+1
            }
        }
        if (point.y > 20)
        {
            //Debug.Log("POEZD6");
            CreateWorldMapCell(0, -1, ActiveMapCell, position);
            //Debug.Log("loading 0 -1");
            //load cell x:0;y:-1
            if (point.x < 5)
            {
                //Debug.Log("POEZD7");
                CreateWorldMapCell(-1, -1, ActiveMapCell, position);
                //Debug.Log("loading -1 -1");
                //load cell x:-1;y:-1
            }
            if (point.x > 20)
            {
                //Debug.Log("POEZD8");
                CreateWorldMapCell(+1, -1, ActiveMapCell, position);
                //Debug.Log("loading +1 -1");
                //load cell x:+1;y:-1
            }

        }
    }
    //bool RandomDontWork=false;
    WorldMapCell GetCell()
    {

        
            foreach (var item in StaticData.MapData)
            {
                foreach (var cell in item)
                {
                    if (!cell.Generated) { return cell; }
                }
            }
        return null;
    }
    public void Start()
    {

        loader.LoadMap();

        GeneratorQuery.Add(GetCell());
        if (MapCells[0] != null)
        {
            
            ZeroPosition = new Vector2(10, 10);
            MapCells[0].Setter(loader, StaticData.MapData[10][10]);
        }
        try
        {
            Task.Factory.StartNew(() =>
            {
                try
                {

                Debug.Log("Task");
                while (true)
                {
                    
                    Debug.Log("Stage-1");
                    WorldMapCell worldMapCell = null;
                    Debug.Log("Stage-0.5");
                    try
                    {

                    lock (GeneratorQuery)
                    {
                        worldMapCell = GeneratorQuery[0];
                    }

                    }
                    catch (System.Exception ex)
                    {
                        Debug.Log(ex.Message);
                        
                    }
                    Debug.Log("Stage-0.1");
                    Generator generator1 = new Generator();
                    Debug.Log("Stage0");
                    generator1.Map = loader.GetNeighborWorldMapCell(new Vector2(worldMapCell.PosX, worldMapCell.PosY));
                    Debug.Log("Stage1");
                    generator1.Build(5);
                    Debug.Log("Stage2");
                    generator1.ConnectCaves();
                    Debug.Log("Stage3");
                    generator1.ConnectCaves();
                    Debug.Log("Stage4");
                    generator1.EmptyCellSet();
                    Debug.Log("Stage5");
                    generator1.EndGeneration();

                    loader.MapGenered(worldMapCell, generator1.ResultMap);
                    loader.MapAccess(worldMapCell);
                    lock (GeneratorQuery)
                    {
                        GeneratorQuery.Remove(worldMapCell);
                        if (GeneratorQuery.Count == 0)
                        {
                            GeneratorQuery.Add(GetCell());
                        }
                        if (GeneratorQuery[0] == null)
                        {
                            Debug.Log("QWERTYUIOP");
                            return;
                        }
                        cellcount = GeneratorQuery.Count;
                    }

                    Debug.Log("Stage6");

                }

                }
                catch (System.Exception ex)
                {

                    Debug.Log(ex.Message);
                    
                }
            });


            Debug.Log(StaticData.MapData[10][10].Message);
        }
        catch (System.Exception)
        {

            Debug.LogWarning("Pidr");
        }
        //Generator generator = new Generator();
        //generator.Map = loader.GetNeighborWorldMapCell(new Vector2(ThisCell.PosX, ThisCell.PosY));
        //
        //outer = Task.Factory.StartNew(() =>
        //{
        //    //Debug.Log("Stage1");
        //    generator.Build(UnBorder);
        //    //Debug.Log("Stage2");
        //    generator.ConnectCaves();
        //    //Debug.Log("Stage3");
        //    generator.ConnectCaves();
        //    //Debug.Log("Stage4");
        //    generator.EmptyCellSet();
        //    //Debug.Log("Stage5");
        //    generator.EndGeneration();
        //    //Debug.Log("Stage6");
        //    return generator.ResultMap;
        //});
    }
}
