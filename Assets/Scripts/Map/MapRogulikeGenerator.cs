using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;

public class MapRogulikeGenerator : MonoBehaviour
{
    public Loader loader;
    public WorldMapCell ThisCell;//StaticData.MapData[0][0];//ActiveCell;
    public GameObject CellPerhub;
    public GameObject EmptyPerhub;
    public int UnBorder = 5;
    private Generator generator;
    private bool Result;
    Task<int[,]> outer;

    public void Setter(Loader loader, WorldMapCell thisscell, int UnBorder=5)
    {
        Debug.Log("asdfghjkl;:"+loader.IsMapGenered);
        this.loader = loader;
        ThisCell = thisscell;
        this.UnBorder = UnBorder;
        if (!loader.IsMapGenered)
        {
            Debug.Log("coo");
            loader.LoadMap();
            StaticData.ActiveCell = StaticData.MapData[10][10];
            ThisCell = StaticData.ActiveCell;
        }
        //Debug.Log(StaticData.MapData[9][10].Message);
        //ThisCell = StaticData.ActiveCell;
        //Debug.Log(ThisCell.Message);

        generator = new Generator();
        //Debug.Log($"{this.transform.position}CoordinateInside:{ThisCell.PosY},{ThisCell.PosX}");
        //Debug.Log($"{this.transform.position}MessageInside:{ThisCell.Message}");
        generator.ResultMap = loader.LoadFromFile(new Vector2(ThisCell.PosX, ThisCell.PosY));

        if (generator.ResultMap == null)
        {
            generator.Map = loader.GetNeighborWorldMapCell();

            outer = Task.Factory.StartNew(() =>
            {
                Debug.Log("Stage1");
                generator.Build(UnBorder);
                Debug.Log("Stage2");
                generator.ConnectCaves();
                Debug.Log("Stage3");
                generator.ConnectCaves();
                Debug.Log("Stage4");
                generator.EmptyCellSet();
                Debug.Log("Stage5");
                generator.EndGeneration();
                Debug.Log("Stage6");
                return generator.ResultMap;
            });
            return;
        }
        else
        {
            Debug.Log("MapSet");
            MapSet();
        }
    }
    public List<GameObject> MapCells = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(loader == null)
        { return; }
        if (!loader.IsMapGenered)
        {
            //Debug.Log("coo");
            loader.LoadMap();
            StaticData.ActiveCell = StaticData.MapData[10][10];
            ThisCell = StaticData.ActiveCell;
        }
        //Debug.Log(StaticData.MapData[9][10].Message);
        //ThisCell = StaticData.ActiveCell;
        //Debug.Log(ThisCell.Message);
        
        generator = new Generator();
        //Debug.Log($"{this.transform.position}CoordinateInside:{ThisCell.PosY},{ThisCell.PosX}");
        //Debug.Log($"{this.transform.position}MessageInside:{ThisCell.Message}");
        generator.ResultMap = loader.LoadFromFile(new Vector2(ThisCell.PosX, ThisCell.PosY));

        if (generator.ResultMap == null)
        {
            generator.Map = loader.GetNeighborWorldMapCell(new Vector2(ThisCell.PosX, ThisCell.PosY));

            outer = Task.Factory.StartNew(() =>
            {
                Debug.Log("Stage1");
                generator.Build(UnBorder);
                Debug.Log("Stage2");
                generator.ConnectCaves();
                Debug.Log("Stage3");
                generator.ConnectCaves();
                Debug.Log("Stage4");
                generator.EmptyCellSet();
                Debug.Log("Stage5");
                generator.EndGeneration();
                Debug.Log("Stage6");
                return generator.ResultMap;
            });
            return;
        }
        else
        {
            //Debug.Log("MapSet");
            MapSet();
        }
    }

    void MapSet()
    {

        for (int i = 0; i < ThisCell.MapWidth; i++)
        {
            for (int k = 0; k < ThisCell.MapHeight; k++)
            {
                //Debug.Log($"Loading cell:{i},{k} and it's :{generator.ResultMap[i, k]}");
                if (generator.ResultMap[i, k] == (int)MapCell.CellType.Wall)
                {
                   // Debug.Log($"it's wall");
                    GameObject cell = Instantiate(CellPerhub);
                    cell.transform.SetParent(gameObject.transform);
                    MapCells.Add(cell);
                    int temp = GetCountRightNeighbours(k, i, MapCell.CellType.Wall, true);
                    if (temp > 1)
                    {
                       // Debug.Log("GG:1");
                        cell.GetComponent<MapCell>().SetAll(k, i, UnityEngine.Color.gray, MapCell.CellType.Wall, temp, 1);
                        continue;
                    }
                    generator.ResultMap[i, k] = (int)MapCell.CellType.Wall;
                    temp = GetCountUpNeighbours(k, i, MapCell.CellType.Wall, true);
                    if (temp > 1)
                    {
                       // Debug.Log("GG:2");
                        cell.GetComponent<MapCell>().SetAll(k, i, UnityEngine.Color.gray, MapCell.CellType.Wall, 1, temp);
                        continue;
                    }
                    cell.GetComponent<MapCell>().SetAll(k, i, UnityEngine.Color.gray, MapCell.CellType.Wall, 1, 1);

                }
                if (generator.ResultMap[i, k] == (int)MapCell.CellType.Empty)
                {
                  //  Debug.Log($"it's empty");
                    GameObject cell = Instantiate(EmptyPerhub);
                    cell.transform.SetParent(gameObject.transform);
                    MapCells.Add(cell);
                    int temp = GetCountRightNeighbours(k, i, MapCell.CellType.Empty, true);
                    if (temp > 1)
                    {
                      //  Debug.Log("GG:1");
                        cell.GetComponent<MapCell>().SetAll(k, i, UnityEngine.Color.black, MapCell.CellType.Empty, temp, 1);
                        continue;
                    }
                    generator.ResultMap[i, k] = (int)MapCell.CellType.Empty;
                    temp = GetCountUpNeighbours(k, i, MapCell.CellType.Empty, true);
                    if (temp > 1)
                    {
                      //  Debug.Log("GG:2");
                        cell.GetComponent<MapCell>().SetAll(k, i, UnityEngine.Color.black, MapCell.CellType.Empty, 1, temp);
                        continue;
                    }
                    cell.GetComponent<MapCell>().SetAll(k, i, UnityEngine.Color.gray, MapCell.CellType.Empty, 1, 1);
                }
            }
        }
        //loader.OutArray(generator.ResultMap, 25);
    }
    int GetCountRightNeighbours(int x, int y, MapCell.CellType cellType, bool SetChecked = false)
    {
        if (x == StaticData.WorldCellSize)
        {
            return 0;
        }
        if (generator.ResultMap[y, x] == (int)cellType)
        {
            if (SetChecked)
            {
                generator.ResultMap[y, x] = (int)MapCell.CellType.Checked;
            }
            return 1 + GetCountRightNeighbours(x + 1, y, cellType, SetChecked);
        }
        return 0;
    }
    int GetCountUpNeighbours(int x, int y, MapCell.CellType cellType, bool SetChecked = false)
    {
        if (y == StaticData.WorldCellSize)
        {
            return 0;
        }
        if (generator.ResultMap[y, x] == (int)cellType)
        {
            if (SetChecked)
            {
                generator.ResultMap[y, x] = (int)MapCell.CellType.Checked;
            }
            return 1 + GetCountUpNeighbours(x, y + 1, cellType, SetChecked);
        }
        return 0;
    }



    // Update is called once per frame
    void Update()
    {
        if (outer!=null && outer.IsCompleted)
        {
            
            
            loader.SaveInFileMapCell(new Vector2(ThisCell.PosX, ThisCell.PosY), generator.ResultMap);
            MapSet();
            outer = null;
        }
    }






    /*
 * Iterations

	50000	nice and smooth, 25 - 35 caves on average

	10000	jagged, more caves, more likely to touch, 35 - 55 caves on average

	250000	15 - 25 caves, more "bloblike"


Cave

	MinSizeToRemove	
	maxSizeToRemove

	Increasing former and decreasing latter results in few, archipelago like caves


Interesting Patterns

	Many small caves
		Iterations 10,000
		Smoothing>EmptyNeighbours 3

	
	Many small caves with open cells in them
		Iterations 10,000
		Smoothing>EmptyNeighbours 3
		EmptyCells>EmptyCellNeighbours 4
    
     
    Fewer, very large caves
        CloseCellProb: 55
        MaxCaveSize: 750
     
    Fewer, larger more connected caves
        CloseCellProb: 50
     
    More, smaller caves
        CloseCellProb: 50
 * 
 */

    /// <summary>
    /// csCaveGenerator - generate a cave system and connect the caves together.
    /// 
    /// For more info on it's use see http://www.evilscience.co.uk/?p=624
    /// </summary>
    class Generator
    {
        //public System.Random rnd = new System.Random();

        #region НЕВАЖНО

        private static readonly System.Random random = new System.Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        #endregion

        #region properties

        [Category("Cave Generation"), Description("The number of closed neighbours a cell must have in order to invert it's state")]
        public int Neighbours { get; set; }
        [Category("Cave Generation"), Description("The probability of closing a visited cell")]
        public int CloseCellProb { get; set; } //55 tends to produce 1 cave, 40 few and small caves
        [Category("Cave Generation"), Description("The number of times to visit cells"), DisplayName("Cells to visit")]
        public int Iterations { get; set; }
        [Category("Cave Generation"), Description("The size of the map"), DisplayName("Map Size")]
        public Size MapSize { get; set; }



        [Category("Cave Cleaning"), Description("Remove rooms smaller than this value"), DisplayName("Lower Limit")]
        public int LowerLimit { get; set; }
        [Category("Cave Cleaning"), Description("Remove rooms larger than this value"), DisplayName("Upper Limit")]
        public int UpperLimit { get; set; }
        [Category("Cave Cleaning"), DisplayName("Smoothing"), Description("Removes single cells from cave edges: a cell with this number of empty neighbours is removed")]
        public int EmptyNeighbours { get; set; }
        [Category("Cave Cleaning"), DisplayName("Filling"), Description("Fills in holes within caves: an open cell with this number closed neighbours is filled")]
        public int EmptyCellNeighbours { get; set; }


        //corridor properties
        [Category("Corridor"), Description("Minimum corridor length"), DisplayName("Min length")]
        public int Corridor_Min { get; set; }
        [Category("Corridor"), Description("Maximum corridor length"), DisplayName("Max length")]
        public int Corridor_Max { get; set; }
        [Category("Corridor"), Description("Maximum turns"), DisplayName("Max Turns")]
        public int Corridor_MaxTurns { get; set; }
        [Category("Corridor"), Description("The distance a corridor has to be away from a closed cell for it to be built"), DisplayName("Corridor Spacing")]
        public int CorridorSpace { get; set; }
        [Category("Corridor"), Description("When this value is exceeded, stop attempting to connect caves. Prevents the algorithm from getting stuck.")]
        public int BreakOut { get; set; }

        [Category("Generated Map"), Description("Number of caves generated"), DisplayName("Caves")]
        public int CaveNumber { get { return Caves == null ? 0 : Caves.Count; } }


        #endregion

        #region map structures

        /// <summary>
        /// Caves within the map are stored here
        /// </summary>
        private List<List<Point>> Caves;

        /// <summary>
        /// Corridors within the map stored here
        /// </summary>
        private List<Point> Corridors;

        /// <summary>
        /// Contains the map
        /// </summary>
        public int[,] Map;
        public int[,] ResultMap;
        #endregion

        #region lookups

        /// <summary>
        /// Generic list of points which contain 4 directions
        /// </summary>
        List<Point> Directions = new List<Point>()
        {
            new Point (0,-1)    //north
            , new Point(0,1)    //south
            , new Point (1,0)   //east
            , new Point (-1,0)  //west
        };

        List<Point> Directions1 = new List<Point>()
        {
            new Point (0,-1)    //north
            , new Point(0,1)    //south
            , new Point (1,0)   //east
            , new Point (-1,0)  //west
            , new Point (1,-1)  //northeast
            , new Point(-1,-1)  //northwest
            , new Point (-1,1)  //southwest
            , new Point (1,1)   //southeast
            , new Point(0,0)    //centre
        };

        #endregion
        #region misc

        /// <summary>
        /// Constructor
        /// </summary>
        public Generator()
        {
            Neighbours = 4;
            Iterations = 50000;
            CloseCellProb = 45;//RandomNumber(45,80);

            LowerLimit = 16;
            UpperLimit = 10000;

            MapSize = new Size(StaticData.WorldCellSize, StaticData.WorldCellSize);

            EmptyNeighbours = 3;
            EmptyCellNeighbours = 4;

            CorridorSpace = 2;
            Corridor_MaxTurns = 10;
            Corridor_Min = 2;
            Corridor_Max = 5;

            BreakOut = 100000;
        }


        public int Build(int unBorderLim)
        {
            BuildCaves(unBorderLim);
            GetCaves();
            return Caves.Count();
        }

        public void EmptyCellSet()
        {
            Point cell;
            for (int x = 0; x < MapSize.Width*3; x++)
                for (int y = 0; y < MapSize.Height*3; y++)
                {
                    cell = new Point(x, y);

                    if (
                            Point_Get(cell) == 0
                            && Neighbours_Get1(cell).Where(n => Point_Get(n) == 0 || Point_Get(n) == -1).Count() == 9
                        )
                        Point_Set(cell, -1);
                }
        }

        public void SetTreasures()
        {
            for (int x = 0; x < MapSize.Width; x++)
                for (int y = 0; y < MapSize.Height; y++)
                {
                    if (Map[x, y] == 1 && Neighbours_Get1(new Point(x, y)).Where(n => Point_Get(n) == 0).Count() >= 3)
                        if (RandomNumber(0, 100) < 3)
                            Map[x, y] = 5;
                }
        }


        #endregion


        #region cave related

        #region make caves




        /// <summary>
        /// Calling this method will build caves, smooth them off and fill in any holes
        /// </summary>
        private void BuildCaves(int unBorderLimits)
        {

            //Map = new int[MapSize.Width, MapSize.Height];


            //go through each map cell and randomly determine whether to close it
            //the +5 offsets are to leave an empty border round the edge of the map
            for (int x = StaticData.WorldCellSize; x < MapSize.Width*2; x++)
                for (int y = StaticData.WorldCellSize; y < MapSize.Height*2; y++)
                {
                    Map[x, y] = 0;
                    if (RandomNumber(0, 100) < CloseCellProb)
                        Map[x, y] = 1;
                }

            //for (int x = 0; x < unBorderLimits; x++)
            //    for (int y = 0; y < MapSize.Height; y++)
            //    {
            //        if (RandomNumber(0, 100) < 50)
            //            Map[x, y] = 1;
            //    }
            //for (int x = MapSize.Width - unBorderLimits; x < MapSize.Width; x++)
            //    for (int y = 0; y < MapSize.Height; y++)
            //    {
            //        if (RandomNumber(0, 100) < 50)
            //            Map[x, y] = 1;
            //    }
            //for (int x = 0; x < MapSize.Width; x++)
            //    for (int y = 0; y < unBorderLimits; y++)
            //    {
            //        if (RandomNumber(0, 100) < 50)
            //            Map[x, y] = 1;
            //    }
            //for (int x = 0; x < MapSize.Width; x++)
            //    for (int y = MapSize.Height - unBorderLimits; y < MapSize.Height; y++)
            //    {
            //        if (RandomNumber(0, 100) < 50)
            //            Map[x, y] = 1;
            //    }

            Point cell;

            //Pick cells at random
            for (int x = 0; x <= Iterations; x++)
            {
                cell = new Point(RandomNumber(MapSize.Width, MapSize.Width*2), RandomNumber(MapSize.Height, MapSize.Height*2));

                //if the randomly selected cell has more closed neighbours than the property Neighbours
                //set it closed, else open it
                if (Neighbours_Get1(cell).Where(n => Point_Get(n) == 1).Count() > Neighbours)
                    Point_Set(cell, 1);
                else
                    Point_Set(cell, 0);
            }

            

            //
            //  Smooth of the rough cave edges and any single blocks by making several 
            //  passes on the map and removing any cells with 3 or more empty neighbours
            //
            for (int ctr = 0; ctr < 5; ctr++)
            {
                //examine each cell individually
                for (int x = MapSize.Width; x < MapSize.Width*2; x++)
                    for (int y = MapSize.Height; y < MapSize.Height*2; y++)
                    {
                        cell = new Point(x, y);

                        if (
                                Point_Get(cell) > 0
                                && Neighbours_Get(cell).Where(n => Point_Get(n) == 0).Count() >= EmptyNeighbours
                            )
                            Point_Set(cell, 0);
                    }
            }

           
            //
            //  fill in any empty cells that have 4 full neighbours
            //  to get rid of any holes in an cave
            //
            for (int x = MapSize.Width; x < MapSize.Width*2; x++)
                for (int y = MapSize.Height; y < MapSize.Height*2; y++)
                {
                    cell = new Point(x, y);

                    if (
                            Point_Get(cell) == 0
                            && Neighbours_Get(cell).Where(n => Point_Get(n) == 1).Count() >= EmptyCellNeighbours
                        )
                        Point_Set(cell, 1);
                }

            

        }

        

        #endregion

        #region locate caves
        /// <summary>
        /// Locate the edge of the specified cave
        /// </summary>
        /// <param name="pCaveNumber">Cave to examine</param>
        /// <param name="pCavePoint">Point on the edge of the cave</param>
        /// <param name="pDirection">Direction to start formting the tunnel</param>
        /// <returns>Boolean indicating if an edge was found</returns>
        private void Cave_GetEdge(List<Point> pCave, ref Point pCavePoint, ref Point pDirection)
        {
            do
            {

                //random point in cave
                pCavePoint = pCave.ToList()[RandomNumber(0, pCave.Count())];

                pDirection = Direction_Get(pDirection);

                do
                {
                    pCavePoint.Offset(pDirection);

                    if (!Point_Check(pCavePoint))
                        break;
                    else if (Point_Get(pCavePoint) == 0)
                        return;

                } while (true);



            } while (true);
        }

        /// <summary>
        /// Locate all the caves within the map and place each one into the generic list Caves
        /// </summary>
        private void GetCaves()
        {
            Caves = new List<List<Point>>();

            List<Point> Cave;
            Point cell;

            //examine each cell in the map...
            for (int x = 0; x < MapSize.Width*3; x++)
                for (int y = 0; y < MapSize.Height*3; y++)
                {
                    cell = new Point(x, y);
                    //if the cell is closed, and that cell doesn't occur in the list of caves..
                    if (Point_Get(cell) > 0 && Caves.Count(s => s.Contains(cell)) == 0)
                    {
                        Cave = new List<Point>();

                        //launch the recursive
                        LocateCave(cell, Cave);

                        //check that cave falls with the specified property range size...
                        if (Cave.Count() <= LowerLimit | Cave.Count() > UpperLimit)
                        {
                            //it does, so bin it
                            foreach (Point p in Cave)
                                Point_Set(p, 0);
                        }
                        else
                            Caves.Add(Cave);
                    }
                }

        }

        /// <summary>
        /// Recursive method to locate the cells comprising a cave, 
        /// based on flood fill algorithm
        /// </summary>
        /// <param name="cell">Cell being examined</param>
        /// <param name="current">List containing all the cells in the cave</param>
        private void LocateCave(Point pCell, List<Point> pCave)
        {
            foreach (Point p in Neighbours_Get(pCell).Where(n => Point_Get(n) > 0))
            {
                if (!pCave.Contains(p))
                {
                    pCave.Add(p);
                    LocateCave(p, pCave);
                }
            }
        }

        #endregion

        #region connect caves

        /// <summary>
        /// Attempt to connect the caves together
        /// </summary>
        public bool ConnectCaves()
        {


            if (Caves.Count() == 0)
                return false;



            List<Point> currentcave;
            List<List<Point>> ConnectedCaves = new List<List<Point>>();
            Point cor_point = new Point();
            Point cor_direction = new Point();
            List<Point> potentialcorridor = new List<Point>();
            int breakoutctr = 0;

            Corridors = new List<Point>(); //corridors built stored here

            //get started by randomly selecting a cave..
            currentcave = Caves[RandomNumber(0, Caves.Count())];
            ConnectedCaves.Add(currentcave);
            Caves.Remove(currentcave);



            //starting builder
            do
            {

                //no corridors are present, sp build off a cave
                if (Corridors.Count() == 0)
                {
                    currentcave = ConnectedCaves[RandomNumber(0, ConnectedCaves.Count())];
                    Cave_GetEdge(currentcave, ref cor_point, ref cor_direction);
                }
                else
                    //corridors are presnt, so randomly chose whether a get a start
                    //point from a corridor or cave
                    if (RandomNumber(0, 100) > 50)
                {
                    currentcave = ConnectedCaves[RandomNumber(0, ConnectedCaves.Count())];
                    Cave_GetEdge(currentcave, ref cor_point, ref cor_direction);
                }
                else
                {
                    currentcave = null;
                    Corridor_GetEdge(ref cor_point, ref cor_direction);
                }



                //using the points we've determined above attempt to build a corridor off it
                potentialcorridor = Corridor_Attempt(cor_point
                                                , cor_direction
                                                , true);


                //if not null, a solid object has been hit
                if (potentialcorridor != null)
                {

                    //examine all the caves
                    for (int ctr = 0; ctr < Caves.Count(); ctr++)
                    {

                        //check if the last point in the corridor list is in a cave
                        if (Caves[ctr].Contains(potentialcorridor.Last()))
                        {
                            if (
                                    currentcave == null //we've built of a corridor
                                    | currentcave != Caves[ctr] //or built of a room
                                )
                            {
                                //the last corridor point intrudes on the room, so remove it
                                potentialcorridor.Remove(potentialcorridor.Last());
                                //add the corridor to the corridor collection
                                Corridors.AddRange(potentialcorridor);
                                //write it to the map
                                foreach (Point p in potentialcorridor)
                                    Point_Set(p, 1);


                                //the room reached is added to the connected list...
                                ConnectedCaves.Add(Caves[ctr]);
                                //...and removed from the Caves list
                                Caves.RemoveAt(ctr);

                                break;

                            }
                        }
                    }
                }

                //breakout
                if (breakoutctr++ > BreakOut)
                    return false;

            } while (Caves.Count() > 0);

            Caves.AddRange(ConnectedCaves);
            ConnectedCaves.Clear();
            return true;
        }

        #endregion

        #endregion

        #region corridor related

        /// <summary>
        /// Randomly get a point on an existing corridor
        /// </summary>
        /// <param name="Location">Out: location of point</param>
        /// <returns>Bool indicating success</returns>
        private void Corridor_GetEdge(ref Point pLocation, ref Point pDirection)
        {
            List<Point> validdirections = new List<Point>();

            do
            {
                //the modifiers below prevent the first of last point being chosen
                pLocation = Corridors[RandomNumber(1, Corridors.Count - 1)];

                //attempt to locate all the empy map points around the location
                //using the directions to offset the randomly chosen point
                foreach (Point p in Directions)
                    if (Point_Check(new Point(pLocation.X + p.X, pLocation.Y + p.Y)))
                        if (Point_Get(new Point(pLocation.X + p.X, pLocation.Y + p.Y)) == 0)
                            validdirections.Add(p);


            } while (validdirections.Count == 0);

            pDirection = validdirections[RandomNumber(0, validdirections.Count)];
            pLocation.Offset(pDirection);

        }

        /// <summary>
        /// Attempt to build a corridor
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pDirection"></param>
        /// <param name="pPreventBackTracking"></param>
        /// <returns></returns>
        private List<Point> Corridor_Attempt(Point pStart, Point pDirection, bool pPreventBackTracking)
        {

            List<Point> lPotentialCorridor = new List<Point>();
            lPotentialCorridor.Add(pStart);

            int corridorlength;
            Point startdirection = new Point(pDirection.X, pDirection.Y);

            int pTurns = Corridor_MaxTurns;

            while (pTurns >= 0)
            {
                pTurns--;

                corridorlength = RandomNumber(Corridor_Min, Corridor_Max);
                //build corridor
                while (corridorlength > 0)
                {
                    corridorlength--;

                    //make a point and offset it
                    pStart.Offset(pDirection);

                    if (Point_Check(pStart) && Point_Get(pStart) == 1)
                    {
                        lPotentialCorridor.Add(pStart);
                        return lPotentialCorridor;
                    }

                    if (!Point_Check(pStart))
                        return null;
                    else if (!Corridor_PointTest(pStart, pDirection))
                        return null;

                    lPotentialCorridor.Add(pStart);

                }

                if (pTurns > 1)
                    if (!pPreventBackTracking)
                        pDirection = Direction_Get(pDirection);
                    else
                        pDirection = Direction_Get(pDirection, startdirection);
            }

            return null;
        }

        private bool Corridor_PointTest(Point pPoint, Point pDirection)
        {

            //using the property corridor space, check that number of cells on
            //either side of the point are empty
            foreach (int r in Enumerable.Range(-CorridorSpace, 2 * CorridorSpace + 1).ToList())
            {
                if (pDirection.X == 0)//north or south
                {
                    if (Point_Check(new Point(pPoint.X + r, pPoint.Y)))
                        if (Point_Get(new Point(pPoint.X + r, pPoint.Y)) != 0)
                            return false;
                }
                else if (pDirection.Y == 0)//east west
                {
                    if (Point_Check(new Point(pPoint.X, pPoint.Y + r)))
                        if (Point_Get(new Point(pPoint.X, pPoint.Y + r)) != 0)
                            return false;
                }

            }

            return true;
        }

        #endregion

        #region direction related

        /// <summary>
        /// Return a list of the valid neighbouring cells of the provided point
        /// using only north, south, east and west
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private List<Point> Neighbours_Get(Point p)
        {
            return Directions.Select(d => new Point(p.X + d.X, p.Y + d.Y))
                    .Where(d => Point_Check(d)).ToList();
        }

        /// <summary>
        /// Return a list of the valid neighbouring cells of the provided point
        /// using north, south, east, ne,nw,se,sw
        private List<Point> Neighbours_Get1(Point p)
        {
            return Directions1.Select(d => new Point(p.X + d.X, p.Y + d.Y))
                    .Where(d => Point_Check(d)).ToList();
        }

        /// <summary>
        /// Get a random direction, provided it isn't equal to the opposite one provided
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Point Direction_Get(Point p)
        {
            Point newdir;
            do
            {
                newdir = Directions[RandomNumber(0, Directions.Count())];

            } while (newdir.X != -p.X & newdir.Y != -p.Y);

            return newdir;
        }

        /// <summary>
        /// Get a random direction, excluding the provided directions and the opposite of 
        /// the provided direction to prevent a corridor going back on it's self.
        /// 
        /// The parameter pDirExclude is the first direction chosen for a corridor, and
        /// to prevent it from being used will prevent a corridor from going back on 
        /// it'self
        /// </summary>
        /// <param name="dir">Current direction</param>
        /// <param name="pDirectionList">Direction to exclude</param>
        /// <param name="pDirExclude">Direction to exclude</param>
        /// <returns></returns>
        private Point Direction_Get(Point pDir, Point pDirExclude)
        {
            Point NewDir;
            do
            {
                NewDir = Directions[RandomNumber(0, Directions.Count())];
            } while (
                        Direction_Reverse(NewDir) == pDir
                         | Direction_Reverse(NewDir) == pDirExclude
                    );


            return NewDir;
        }

        private Point Direction_Reverse(Point pDir)
        {
            return new Point(-pDir.X, -pDir.Y);
        }

        #endregion

        #region cell related

        /// <summary>
        /// Check if the provided point is valid
        /// </summary>
        /// <param name="p">Point to check</param>
        /// <returns></returns>
        private bool Point_Check(Point p)
        {
            return p.X >= 0 & p.X < MapSize.Width*3 & p.Y >= 0 & p.Y < MapSize.Height*3;
        }

        /// <summary>
        /// Set the map cell to the specified value
        /// </summary>
        /// <param name="p"></param>
        /// <param name="val"></param>
        private void Point_Set(Point p, int val)
        {
            Map[p.X, p.Y] = val;
        }

        /// <summary>
        /// Get the value of the provided point
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int Point_Get(Point p)
        {
            return Map[p.X, p.Y];
        }

        #endregion

        public void EndGeneration()
        {

            Debug.Log("Pidr");
            int di = 0;
            int dk;
            ResultMap = new int[StaticData.WorldCellSize, StaticData.WorldCellSize];
            for (int i = StaticData.WorldCellSize; i < StaticData.WorldCellSize*2; i++, di++)
            {
                dk = 0;
                for (int k = StaticData.WorldCellSize; k < StaticData.WorldCellSize*2; k++, dk++)
                {

                    ResultMap[di, dk] = Map[i, k];
                }
            }
        }

    }





}
