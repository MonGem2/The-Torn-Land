using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Loader:MonoBehaviour
{
    public int WorldSize = 100;
    public bool IsMapGenered=false;
    public List<GameObject> BulletsPerhubs;
    public List<State> States=new List<State>();
    public List<Skill> Skills=new List<Skill>();
    public void LoadMap()
    {
        //LoadSkills();
        for (int i = 0; i < WorldSize; i++)
        {
            StaticData.MapData.Add(new List<WorldMapCell>());

            for (int k = 0; k < WorldSize; k++)
            {
                WorldMapCell gg = new WorldMapCell(k, i);
                gg.MapHeight = StaticData.WorldCellSize;
                gg.MapWidth = StaticData.WorldCellSize;
                gg.Message = i.ToString() + "HI" + k.ToString();
                StaticData.MapData[i].Add(gg);
                
            }
        }
        
        IsMapGenered = true;
    }
    public void SaveInFileMapCell(Vector2 position,int[,] map)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        Debug.Log("i'm here 0");
        //File.Create($"{position.x}_{position.y}.mapcell");
        Debug.Log("i'm here 1");
        using (FileStream fs = new FileStream($"{position.x}_{position.y}.mapcell", FileMode.OpenOrCreate))
        {
            Debug.Log("i'm here 2");
            formatter.Serialize(fs, map);
            
        }                                                                                   
        Debug.Log("i'm here 3");                                                            
        // десериализация из файла people.dat                                               
                                                                                            
    }                                                                                       
    public int[,] LoadFromFile(Vector2 position)
    {
        
        if (File.Exists($"{position.x}_{position.y}.mapcell"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            int[,] result;
            using (FileStream fs = new FileStream($"{position.x}_{position.y}.mapcell", FileMode.Open, FileAccess.Read))
            {
                result = (int[,])formatter.Deserialize(fs);
            }
            //Debug.Log("Loading from file");
            return result;
        }
        return null;
    }                                  
    public int[,] GetNeighborWorldMapCell(Vector2 position)
    {
        bool Cell0_0 = false;
        bool Cell1_0 = false;
        bool Cell2_0 = false;
        bool Cell0_1 = false;
        bool Cell2_1 = false;
        bool Cell0_2 = false;
        bool Cell1_2 = false; 
        bool Cell2_2 = false;
        int[,] Result = new int[StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 3];
        //Debug.Log("Stage1");
        //100
        //100
        //100
        if (position.x == 0)
        {
            Cell0_0 = true;
            Cell0_1 = true;
            Cell0_2 = true;
            Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), 0);
        }
        //Debug.Log("Stage2");
        //111
        //000
        //000
        if (position.y == 0)
        {
            Cell0_0 = true;
            Cell1_0 = true;
            Cell2_0 = true;
            Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize*3, StaticData.WorldCellSize), 0);
        }
        //Debug.Log("Stage3");
        //001
        //001
        //001
        if (position.x == WorldSize-1)
        {
            Cell2_0 = true;
            Cell2_1 = true;
            Cell2_2 = true;
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize*2, 0), new Vector2(StaticData.WorldCellSize*3, StaticData.WorldCellSize * 3), 0);
        }
        //Debug.Log("Stage4");
        //000
        //000
        //111
        if (position.y == WorldSize)
        {
            Cell0_2 = true;
            Cell1_2 = true;
            Cell2_2 = true;
            Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize*2), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize*3), 0);
        }
        //Debug.Log("Stage5");
        //100
        //000
        //000
        int[,] array;
        if (!Cell0_0)
        {
            array = LoadFromFile(position + new Vector2(-1, -1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize), array);
            }
        }
        //Debug.Log("Stage6");
        //010
        //000
        //000
        if (!Cell1_0)
        {
            array = LoadFromFile(position + new Vector2(0, -1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, 0), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, 0), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), array);
            }
        }
        //Debug.Log("Stage7");
        //001
        //000
        //000
        if (!Cell2_0)
        {
            array = LoadFromFile(position + new Vector2(1, -1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, 0), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, 0), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize), array);
            }
        }
        //Debug.Log("Stage8");
        //000
        //100
        //000
        if (!Cell0_1)
        {
            array = LoadFromFile(position + new Vector2(-1, 0));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), array);
            }
        }
        //Debug.Log("Stage9");
        //000
        //001
        //000
        if (!Cell2_1)
        {
            array = LoadFromFile(position + new Vector2(+1, 0));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 2), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 2), array);
            }
        }
        //Debug.Log("Stage10");
        //000
        //000
        //100
        if (!Cell0_2)
        {
            array = LoadFromFile(position + new Vector2(-1, 1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), array);
            }
        }
        //Debug.Log("Stage11");
        //000
        //000
        //010
        if (!Cell1_2)
        {
            array = LoadFromFile(position + new Vector2(0, 1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 3), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 3), array);
            }
        }
        //Debug.Log("Stage12");
        //000
        //000
        //001
        if (!Cell2_2)
        {
            array = LoadFromFile(position + new Vector2(+1, 1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 3), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 3), array);
            }
        }
       
        //OutArray(Result);
        return Result;
    }
    public int[,] GetNeighborWorldMapCell(bool UP=true, bool Down=true, bool Left=true,
        bool Right=true, bool UP_Left=true, bool UP_Right=true, bool Down_Left=true, bool Down_Right=true)
    {
        int[,] Result = new int[StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 3];
        //100
        //000
        //000        
        if (UP_Left)
        {
            Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize), 0);
        }
        
        //Debug.Log("Stage6");
        //010
        //000
        //000

        if (UP)
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, 0), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, 0), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), 0);
        }
        
        //Debug.Log("Stage7");
        //001
        //000
        //000
        if (UP_Right)
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, 0), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, 0), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize), 0);
        }
        
        //Debug.Log("Stage8");
        //000
        //100
        //000
        if (Left)
        {
            Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), 0);
        }
        
        //Debug.Log("Stage9");
        //000
        //001
        //000
        if (Right)
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 2), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 2), 0);
        }
        
        //Debug.Log("Stage10");
        //000
        //000
        //100
        if (Down_Left)
        {
            Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), 0);
        }
        
        //Debug.Log("Stage11");
        //000
        //000
        //010
        if (Down)
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 3), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 3), 0);
        }
   
        //Debug.Log("Stage12");
        //000
        //000
        //001
        if (Down_Right)
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 3), 1);
        }
        else
        {
            Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 3), 0);
        }
        

        //OutArray(Result);
        return Result;

    }    
    int[,] FillArrayRange(int[,] array, Vector2 from, Vector2 to, int[,] Content)
    {
        int di=0, dk = 0;
        for (int i = (int)from.y; i < to.y; i++,di++)
        {
            dk = 0;
            for (int k = (int)from.x; k < to.x; k++, dk++)
            {
                array[i, k] = Content[di, dk];
                
                
            }
        }
        return array;
    }
    int[,] FillArrayRange(int[,] array, Vector2 from, Vector2 to, int Content)
    {
        
        for (int i = (int)from.y; i < to.y; i++)
        {
            for (int k = (int)from.x; k < to.x; k++)
            {
                //Debug.Log($"My Indexes:y:{i}, x:{k};");
                array[i, k] = Content;
            }
        }
        return array;
    }
    public void OutArray(int[,] array)
    {
        string str = "";
        for (int i = 0; i < 75; i++)
        {
           
            for (int k = 0; k < 75; k++)
            {
                if (array[i, k].ToString() == "1")
                {
                    str += "a";
                }
                if (array[i, k].ToString() == "0")
                {
                    str += "q";
                }
                if (array[i, k].ToString() == "-1")
                {
                    str += "c";
                }
                if (array[i, k].ToString() == "5")
                {
                    str += "b";
                }
                //str += array[i, k].ToString();
            }
            str += "\n";
        }
        Debug.Log(str);
    }
    public void LoadSkills()
    {
        {
            Skill skill = new Skill();
            skill.Cooldown = 3;
            skill.EffectsIds = new List<int>();
            skill.MPIntake = 15;
            skill.STIntake = 15;
            skill.SPIntake = 15;
            skill.Bullets = new List<BulletData>();
            {
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 5;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 3;
                    data.ManaDamage = 0;
                    data.PerhubID = 0;
                    data.PhysicDamage = 5;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    skill.Bullets.Add(data);
                }
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 5;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 3;
                    data.ManaDamage = 0;
                    data.PerhubID = 0;
                    data.PhysicDamage = 5;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    skill.Bullets.Add(data);
                }
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 5;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 3;
                    data.ManaDamage = 0;
                    data.PerhubID = 0;
                    data.PhysicDamage = 5;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    skill.Bullets.Add(data);
                }
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 5;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 3;
                    data.ManaDamage = 0;
                    data.PerhubID = 0;
                    data.PhysicDamage = 5;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    skill.Bullets.Add(data);
                }
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 5;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 3;
                    data.ManaDamage = 0;
                    data.PerhubID = 0;
                    data.PhysicDamage = 5;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    skill.Bullets.Add(data);
                }

            }
            Skills.Add(skill);
        }
    
    }
    public void LoadStates()
    {
        {
            State state = new State();
            state.Duration = 10;
            state.type = StateType.SkillHider;
            state.Params = new List<int>();
            States.Add(state);
        }
    }


}
