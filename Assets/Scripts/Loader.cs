using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;
using System;

public class Loader:MonoBehaviour
{
    public Player player;
    public int WorldSize = 100;
    public bool IsMapGenered=false;
    public WorldState world;

    public List<GameObject> BulletsPerhubs;
    public List<State> States=new List<State>();
    public List<Skill> Skills=new List<Skill>();
    public List<Item> Items=new List<Item>();
    bool SkillsLoaded = false;
    bool StatesLoaded = false;
    bool MapLoaded = false;
    bool ItemsLoaded = false;
    string MapAccesed = "Map/Accesed/";
    string MapGenerated = "Map/Generated/";
    

    public void LoadMap()
    {
        //LoadSkills();
        if (MapLoaded)
        {
            return;
        }
        MapLoaded = true;

      //  //Debug.LogError("Pidr 1");
        for (int i = 0; i < WorldSize; i++)
        {
            StaticData.MapData.Add(new List<WorldMapCell>());

            for (int k = 0; k < WorldSize; k++)
            {
                WorldMapCell gg = new WorldMapCell(k, i);
                if (File.Exists(MapGenerated+$"{k}_{i}.mapcell"))
                {
                    gg.Generated = true;
                    gg.Accesed = false;
                  //  //Debug.LogWarning("map generated");
                }
                if (File.Exists(MapAccesed+$"{k}_{i}.mapcell"))
                {
                    gg.Generated = true;
                    gg.Accesed = true;
                   // //Debug.LogWarning("map accesed");
                }
                gg.MapHeight = StaticData.WorldCellSize;
                gg.MapWidth = StaticData.WorldCellSize;
                gg.Message = i.ToString() + "HI" + k.ToString();
                StaticData.MapData[i].Add(gg);
                
            }
        }
        
        //IsMapGenered = true;
    }
    public void MapGenered(WorldMapCell position,int[,] map)
    {
        try
        {

        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        //Debug.Log("saving "+ MapGenerated + $"{position.PosX}_{position.PosY}.mapcell");
        //File.Create($"{position.x}_{position.y}.mapcell");
       // //Debug.Log("i'm here 1");
            position.Generated = true;
        using (FileStream fs = new FileStream(MapGenerated+$"{position.PosX}_{position.PosY}.mapcell", FileMode.OpenOrCreate))
        {
         //   //Debug.Log("i'm here 2");
            formatter.Serialize(fs, map);
            
        }
       // //Debug.Log("i'm here 3");
            // десериализация из файла people.dat                                               

        }
        catch (Exception ex)
        {

            //Debug.Log(ex.Message);
            throw;
        }
    }
    public void MapAccess(WorldMapCell position)
    {
        
        try
        {

        if (!File.Exists(MapGenerated + $"{position.PosX}_{position.PosY}.mapcell"))
        {
            return;
        }
            //Debug.Log("Moving "+ MapAccesed + $"{position.PosX}_{position.PosY}.mapcell");
        File.Move(MapGenerated + $"{position.PosX}_{position.PosY}.mapcell", MapAccesed + $"{position.PosX}_{position.PosY}.mapcell");
        position.Accesed = true;

        }
        catch (Exception ex)
        {
            //Debug.Log(ex.Message);
            //Debug.Log($"{ position.PosX}_{ position.PosY}.mapcell");
            throw;
        }
    }
    public int[,] GetMap(WorldMapCell position)
    {
        if (position.Accesed)
        {


            if (File.Exists(MapAccesed +$"{position.PosX}_{position.PosY}.mapcell"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                int[,] result;
                using (FileStream fs = new FileStream(MapAccesed+ $"{position.PosX}_{position.PosY}.mapcell", FileMode.Open, FileAccess.Read))
                {
                    result = (int[,])formatter.Deserialize(fs);
                }
                ////Debug.Log("Loading from file");
                return result;
            }
        }
        return null;
    }
    int[,] GetMapAll(WorldMapCell position)
    {
        if (position.Accesed)
        {


            if (File.Exists(MapAccesed + $"{position.PosX}_{position.PosY}.mapcell"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                int[,] result;
                using (FileStream fs = new FileStream(MapAccesed + $"{position.PosX}_{position.PosY}.mapcell", FileMode.Open, FileAccess.Read))
                {
                    result = (int[,])formatter.Deserialize(fs);
                }
                ////Debug.Log("Loading from file");
                return result;
            }
        }
        else if (position.Generated)
        {


            if (File.Exists(MapAccesed + $"{position.PosX}_{position.PosY}.mapcell"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                int[,] result;
                using (FileStream fs = new FileStream(MapAccesed + $"{position.PosX}_{position.PosY}.mapcell", FileMode.Open, FileAccess.Read))
                {
                    result = (int[,])formatter.Deserialize(fs);
                }
                ////Debug.Log("Loading from file");
                return result;
            }
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
        ////Debug.Log("Stage1");
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
        ////Debug.Log("Stage2");
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
        ////Debug.Log("Stage3");
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
        ////Debug.Log("Stage4");
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
        ////Debug.Log("Stage5");
        //100
        //000
        //000
        int[,] array;
        if (!Cell0_0)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x-1][(int)position.y-1]);// position + new Vector2(-1, -1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(0, 0), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize), array);
            }
        }
        ////Debug.Log("Stage6");
        //010
        //000
        //000
        if (!Cell1_0)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x ][(int)position.y - 1]);// LoadFromFile(position + new Vector2(0, -1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, 0), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, 0), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), array);
            }
        }
        ////Debug.Log("Stage7");
        //001
        //000
        //000
        if (!Cell2_0)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x + 1][(int)position.y - 1]);// LoadFromFile(position + new Vector2(1, -1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, 0), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, 0), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize), array);
            }
        }
        ////Debug.Log("Stage8");
        //000
        //100
        //000
        if (!Cell0_1)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x - 1][(int)position.y ]);// LoadFromFile(position + new Vector2(-1, 0));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), array);
            }
        }
        ////Debug.Log("Stage9");
        //000
        //001
        //000
        if (!Cell2_1)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x + 1][(int)position.y ]);// LoadFromFile(position + new Vector2(+1, 0));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 2), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize), new Vector2(StaticData.WorldCellSize * 3, StaticData.WorldCellSize * 2), array);
            }
        }
        ////Debug.Log("Stage10");
        //000
        //000
        //100
        if (!Cell0_2)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x - 1][(int)position.y + 1]);// LoadFromFile(position + new Vector2(-1, 1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(0, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 3), array);
            }
        }
        ////Debug.Log("Stage11");
        //000
        //000
        //010
        if (!Cell1_2)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x ][(int)position.y + 1]);// LoadFromFile(position + new Vector2(0, 1));
            if (array == null)
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 3), 1);
            }
            else
            {
                Result = FillArrayRange(Result, new Vector2(StaticData.WorldCellSize, StaticData.WorldCellSize * 2), new Vector2(StaticData.WorldCellSize * 2, StaticData.WorldCellSize * 3), array);
            }
        }
        ////Debug.Log("Stage12");
        //000
        //000
        //001
        if (!Cell2_2)
        {
            array = GetMapAll(StaticData.MapData[(int)position.x + 1][(int)position.y + 1]);// LoadFromFile(position + new Vector2(+1, 1));
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
        
        ////Debug.Log("Stage6");
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
        
        ////Debug.Log("Stage7");
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
        
        ////Debug.Log("Stage8");
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
        
        ////Debug.Log("Stage9");
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
        
        ////Debug.Log("Stage10");
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
        
        ////Debug.Log("Stage11");
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
   
        ////Debug.Log("Stage12");
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
                ////Debug.Log($"My Indexes:y:{i}, x:{k};");
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
        //Debug.Log(str);
    }
    public void LoadItems()
    {
        //Debug.LogError(ItemsLoaded);
        if (ItemsLoaded)
        {
            
            return;
        }
        ItemsLoaded = true;
        LoadStates();
        //Debug.LogError("Loading items");
        int index = 0;
        {
            Item item = new Item();
            item.ID = index;
            item.Sprite = "Potions/RedLittlePotion";
            //item.spriteN = -1;
            item.type = ItemType.Disposable;
            item.Description = "Heal 100";
            item.Name = "Little heal potion";
            item.EffectIds = new List<int>();
            item.EffectIds.Add(11);
            Items.Add(item);
            index++;
        }
        {
            Item item = new Item();
            item.ID = index;
            item.Sprite = "Potions/YellowLittlePotion";
            //item.spriteN = -1;
            item.type = ItemType.Disposable;
            item.Description = "Add 10 xp";
            item.Name = "Little xp potion";
            item.EffectIds = new List<int>();
            item.EffectIds.Add(15);
            Items.Add(item);
            index++;
        }
        {
            Item item = new Item();
            item.ID = index;
            item.Sprite = "Potions/BlackLittlePotion";
            //item.spriteN = -1;
            item.type = ItemType.Disposable;
            item.Description = "Add 10 xp";
            item.Name = "Little potion of purity";
            item.EffectIds = new List<int>();
            item.EffectIds.Add(16);
            Items.Add(item);
            index++;
        }
        {
            Item item = new Item();
            item.ID = index;
            item.Sprite = "Potions/GreenLittlePotion";
            //item.spriteN = -1;
            item.type = ItemType.Disposable;
            item.Description = "This legendary potion can up all your parameters 10%";
            item.Name = "Potion unlimited power";
            item.EffectIds = new List<int>();
            item.EffectIds.Add(17);
            Items.Add(item);
            index++;
        }
        {
            Item item = new Item();
            item.ID = index;
            item.Sprite = "Equip/Gear";
            item.spriteN = 0;
            item.type = ItemType.Weapon;
            item.Description = "This sword up your damage 15%";
            item.Name = "Demonic sword";
            item.EffectIds = new List<int>();
            item.EffectIds.Add(18);
            item.EffectIds.Add(19);
            Items.Add(item);
            index++;
        }
        {
            Item item = new Item();
            item.ID = index;
            item.Sprite = "Potions/YellowLittlePotion";
            //item.spriteN = -1;
            item.type = ItemType.Disposable;
            item.Description = "Add 10xp";
            item.Name = "Potion of xp";
            item.EffectIds = new List<int>();
            item.EffectIds.Add(15);
            Items.Add(item);
            index++;
        }
        //Debug.Log(Items[0]);

    }
    public void LoadSkills()
    {
        if (SkillsLoaded)
        {
            return;
        }
        SkillsLoaded = true;
        LoadStates();
        {
            Skill skill = new Skill();
            skill.ID = 0;
            skill.Cooldown = 3;
            skill.EffectsIds = new List<State>();
           // skill.EffectsIds.Add(States[0]);
            skill.MPIntake = 2;
            skill.STIntake = 2;
            skill.SPIntake = 2;
            skill.Range = 10.5f;
            skill.skillType = GeneralSkillType.Control;
            skill.SkillBehaviourType = SkillBehaviourType.DefendAttack;
            skill.SPParameter = SkillParameterType.Litle;
            skill.STParameter = SkillParameterType.Litle;
            skill.MPParameter = SkillParameterType.Litle;
            skill.ico = "56245112ff2decfca762c7e591ff898b";
            skill.spriteN = 1;
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
                    data.PhysicDamage = 1.1f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    data.Binded = true;
                    data.SelfAttack = false;
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
                    data.PhysicDamage = 1.1f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    data.Binded = true;
                    data.SelfAttack = false;
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
                    data.PhysicDamage = 1.1f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    data.Binded = true;
                    data.SelfAttack = false;
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
                    data.PhysicDamage = 1.1f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    data.Binded = true;
                    data.SelfAttack = false;
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
                    data.PhysicDamage = 1.1f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0.3f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }

            }
            Skills.Add(skill);
        }
        {
            Skill skill = new Skill();
            skill.Cooldown = 1;
            skill.ID = 1;
            skill.skillType = GeneralSkillType.TargetAttack;
            skill.SkillBehaviourType = SkillBehaviourType.DeefDefend;
            skill.SPParameter = SkillParameterType.None;
            skill.STParameter = SkillParameterType.None;
            skill.MPParameter = SkillParameterType.Avarage;
            skill.EffectsIds = new List<State>();
            skill.MPIntake = 15;
            skill.STIntake = 0;
            skill.SPIntake = 0;
            skill.Range = 15f;
            skill.ico = "21ae3d0ffb602ef2035864c42cd9d7aa";
            skill.spriteN = 68;
            skill.Bullets = new List<BulletData>();
            {
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 0;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 0.2f;
                    data.ManaDamage = 1.2f;
                    data.PerhubID = 1;
                    data.PhysicDamage = 0;
                    data.Range = 15;
                    data.SoulDamage = 0;
                    data.Through = true;
                    data.type = BulletType.Ray;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = -15;
                    data.DeltaAngle = 0;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 0.2f;
                    data.ManaDamage = 1.2f;
                    data.PerhubID = 1;
                    data.PhysicDamage = 0;
                    data.Range = 15;
                    data.SoulDamage = 0;
                    data.Through = true;
                    data.type = BulletType.Ray;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 15;
                    data.DeltaAngle = 0;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 0.2f;
                    data.ManaDamage = 1.2f;
                    data.PerhubID = 1;
                    data.PhysicDamage = 0;
                    data.Range = 15;
                    data.SoulDamage = 0;
                    data.Through = true;
                    data.type = BulletType.Ray;
                    data.AttackTimeout = 0.05f;
                    data.ShootPeriod = 0f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }

            }
            Skills.Add(skill);
        }
        {
            Skill skill = new Skill();
            skill.Cooldown = 0.3f;
            skill.ID = 2;
            skill.skillType =  GeneralSkillType.TargetAttack;
            skill.SkillBehaviourType = SkillBehaviourType.Attack;
            skill.SPParameter = SkillParameterType.None;
            skill.STParameter = SkillParameterType.Avarage;
            skill.MPParameter = SkillParameterType.None;
            skill.EffectsIds = new List<State>();
            skill.MPIntake = 0;
            skill.STIntake = 15;
            skill.SPIntake = 0;
            skill.Range = 1;
            skill.ico = "64faf27d4a83c39035a45e001e6d95a1";
            skill.spriteN = 9;
            skill.Bullets = new List<BulletData>();
            {
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 60;
                    data.Distance = new Vector2(0.5f, 0.5f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 0.3f;
                    data.ManaDamage = 0;
                    data.PerhubID = 3;
                    data.PhysicDamage = 1.2f;
                    data.Range = 1;
                    data.SoulDamage = 0;
                    data.Through = true;
                    data.type = BulletType.Swing;
                    data.AttackTimeout = 0.1f;
                    data.ShootPeriod = 0f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }             

            }
            Skills.Add(skill);
        }
        {
            Skill skill = new Skill();
            skill.ID = 3;
            skill.Cooldown = 2;
            skill.EffectsIds = new List<State>();
            //skill.EffectsIds.Add(States[0]);
            skill.MPIntake = 2;
            skill.STIntake = 2;
            skill.SPIntake = 2;
            skill.Range = 10.5f;
            skill.skillType = GeneralSkillType.Control;
            skill.SkillBehaviourType = SkillBehaviourType.Attack;
            skill.SPParameter = SkillParameterType.Litle;
            skill.STParameter = SkillParameterType.Litle;
            skill.MPParameter = SkillParameterType.Litle;
            skill.ico = "9f39f4a735d044598164d3b63609c4eb";
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
                    data.FlyTime = 5;
                    data.ManaDamage = 0;
                    data.PerhubID = 0;
                    data.PhysicDamage = 1.1f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = false;
                    data.type = BulletType.Bullet;
                    //data.AttackTimeout = 0.05f; -ray attack
                    data.ShootPeriod = 0.3f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }
            }
            Skills.Add(skill);
        }
        {
            Skill skill = new Skill();
            skill.ID = 3;            
            skill.Cooldown = 2;
            skill.EffectsIds = new List<State>();
            skill.EffectsIds.Add(States[0]);
            skill.MPIntake = 2;
            skill.STIntake = 2;
            skill.SPIntake = 2;
            skill.Range = 10.5f;
            skill.skillType = GeneralSkillType.Move;
            skill.SkillBehaviourType = SkillBehaviourType.RunAway;
            skill.SPParameter = SkillParameterType.Litle;
            skill.STParameter = SkillParameterType.Litle;
            skill.MPParameter = SkillParameterType.Litle;
            skill.ico = "87087784c56e9b45bd9a3c63a35b2e93";
            skill.spriteN = 45;
            skill.Bullets = new List<BulletData>();
            Skills.Add(skill);
        }
        {
            Skill skill = new Skill();
            skill.ID = 4;
            skill.Cooldown = 2f;
            skill.EffectsIds = new List<State>();
            //skill.EffectsIds.Add(States[0]);
            skill.MPIntake = 20;
            skill.STIntake = 0;
            skill.SPIntake = 0;
            skill.Range = 15.5f;
            skill.skillType = GeneralSkillType.Control;
            skill.SkillBehaviourType = SkillBehaviourType.Attack;
            skill.SPParameter = SkillParameterType.Litle;
            skill.STParameter = SkillParameterType.Litle;
            skill.MPParameter = SkillParameterType.Litle;
            skill.ico = "9f39f4a735d044598164d3b63609c4eb";
            skill.Bullets = new List<BulletData>();
            {
                {
                    BulletData data = new BulletData();
                    data.AdditionalAngle = 0;
                    data.DeltaAngle = 5;
                    data.Distance = new Vector2(0f, 0f);
                    data.DontAttack = new List<string>();
                    //data.DontAttack.Add("player");
                    data.EffectsIDs = new List<int>();
                    data.FlyTime = 3;
                    data.ManaDamage = 0;
                    data.PerhubID = 2;
                    data.PhysicDamage = 0.5f;
                    data.Range = 10;
                    data.SoulDamage = 0;
                    data.Through = true;
                    data.type = BulletType.Area;
                    data.AttackTimeout = 0.1f;
                    data.ShootPeriod = 0f;
                    data.Binded = true;
                    data.SelfAttack = false;
                    skill.Bullets.Add(data);
                }
            }

            Skills.Add(skill);
        }
        {
            Skill skill = new Skill();
            skill.ID = 5;
            skill.Cooldown = 5f;
            skill.EffectsIds = new List<State>();
            skill.EffectsIds.Add(States[4]);
            skill.EffectsIds.Add(States[9]);
            skill.MPIntake = 0;
            skill.STIntake = 0;
            skill.SPIntake = 25;
            skill.Range = 15.5f;
            skill.skillType = GeneralSkillType.Control;
            skill.SkillBehaviourType = SkillBehaviourType.Attack;
            skill.SPParameter = SkillParameterType.Litle;
            skill.STParameter = SkillParameterType.Litle;
            skill.MPParameter = SkillParameterType.Litle;
            skill.ico = "9f39f4a735d044598164d3b63609c4eb";
            skill.Bullets = new List<BulletData>();
          

            Skills.Add(skill);
        }

    }
    public void LoadStates()
    {
        if (StatesLoaded)
        {
            return;
        }
        StatesLoaded = true;
        int index = 0;
        {

            State state = new State();
            state.ID = index;
            state.Duration = 0.5f;
            state.ico = "qq";
            state.spriteN = -1;
            state.type = StateType.Move;
            state.Params = new List<float>() { (float)MoveTypeLD.Teleportation, (float)MoveDirectionTypeLD.toMouse, 0, 0, 1, 10 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 0.5f;
            state.ico = "qq";
            state.spriteN = -1;
            state.type = StateType.Move;
            state.Params = new List<float>() { (float)MoveTypeLD.Teleportation, (float)MoveDirectionTypeLD.staticPoint, 1.15f, -9f, 5, 10 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 0.5f;
            state.ico = "qq";
            state.spriteN = -1;
            state.type = StateType.InfinityPower;
            state.Params = new List<float>() { 1, 1, 1, 1, 1 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 100;
            state.ico = "2e19d99d5e16baa41d790c34a9e9d155";
            state.spriteN = -1;
            state.type = StateType.RegenerationStop;
            state.Params = new List<float>() {1, 0, 1, 1 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 2;
            state.ico = "a6ca1c0c128e16cb796c712fd65bcc35";
            state.spriteN = -1;
            state.type = StateType.DazerAttack;
            state.Params = new List<float>() { };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 2;
            state.ico = "83f1bb86821f7a0a772a4d17109bb07f";
            state.spriteN = -1;
            state.type = StateType.DazerMovement;
            state.Params = new List<float>() {};
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
            state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
            state.spriteN = 28;
            state.type = StateType.SkillAdder;
            state.Params = new List<float>() { 3 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
            state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
            state.spriteN = 6;
            state.type = StateType.SkillHider;
            state.Params = new List<float>() { 2 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
            state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
            state.spriteN = 6;
            state.type = StateType.ParameterAdder;
            state.Params = new List<float>() {10, 10, 10, 10 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 30;
            state.ico = "5dd6b30614b14477e4f3157fe4c405cc";
            //state.spriteN = -1;
            state.type = StateType.ParameterChanger;
            state.Params = new List<float>() { 0.2f, 0, 0, 0, 0.01f, 0, 0, 0, 0,0, 0, 0, 0.5f  };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 30;
            state.ico = "5dd6b30614b14477e4f3157fe4c405cc";
            //state.spriteN = -1;
            state.type = StateType.PlayerParameterAdder;
            state.Params = new List<float>() { 10, 0,0,0 };
            States.Add(state);
            index++;
        }
        {//11

            State state = new State();
            state.ID = index;
            state.Duration = 10;
            state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
            state.spriteN = 6;
            state.type = StateType.ParameterAdder;
            state.Params = new List<float>() { 100, 0, 0, 0 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
         //   state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
         //   state.spriteN = 6;
            state.type = StateType.ParameterAdder;
            state.Params = new List<float>() { 0, 100, 0, 0 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
          //  state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
          //  state.spriteN = 6;
            state.type = StateType.ParameterAdder;
            state.Params = new List<float>() { 0, 0, 100, 0 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
           // state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
           // state.spriteN = 6;
            state.type = StateType.ParameterAdder;
            state.Params = new List<float>() { 0, 0, 0, 100 };
            States.Add(state);
            index++;
        }
        {//add 10 xp

            State state = new State();
            state.ID = index;
            state.Duration = 10;
            // state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
            // state.spriteN = 6;
            state.type = StateType.PlayerParameterAdder;
            state.Params = new List<float>() { 10, 0, 0, 0 };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = 10;
            // state.ico = "1a8bd0a22186f7b3a3d1056fbc806d35";
            // state.spriteN = 6;
            state.type = StateType.PlayerParameterAdder;
            state.Params = new List<float>() { 0, -10, 0, 0 };
            States.Add(state);
            index++;
        }
        //17
        {

            State state = new State();
            state.ID = index;
            state.Duration = -1;
            state.ico = "83f1bb86821f7a0a772a4d17109bb07f";
            state.spriteN = -1;
            state.type = StateType.ParameterChanger;
            state.Params = new List<float>() { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };
            States.Add(state);
            index++;
        }
        {

            State state = new State();
            state.ID = index;
            state.Duration = -1;
            state.ico = "7e1740d8a7c5902df38f30bff77b7fa9";
            state.spriteN = -1;
            state.type = StateType.ParameterChanger;
            state.Params = new List<float>() { 0, 0, 0, 0, 0.15f, 0, 0, 0, 0, 0, 0, 0, 0 };
            States.Add(state);
            index++;
        }
        //19
        {

            State state = new State();
            state.ID = index;
            state.Duration = -1;
            state.ico = "Equip/Gear";
            state.spriteN = 0;
            state.type = StateType.SkillAdder;
            state.Params = new List<float>() { 0 };
            States.Add(state);
            index++;
        }

    }


    public bool LoadPlayer(Player player)
    {
        LoadMap();
        LoadSkills();
        LoadStates();
        LoadItems();
        //Debug.Log("Loader:load player");
        if (!File.Exists("player.save"))
        {
            //Debug.Log("Loader:load player false");
            return false;
        }
        List<object> Data;
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream($"player.save", FileMode.Open))
        {
            //Debug.Log("i'm here 2");
            Data = (List<object>)formatter.Deserialize(fs);

        }
        player.transform.position = ((MyVector3)Data[0]).Get();
        // player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        player.MaxHP = (float)Data[1];
        player.HP = (float)Data[2];
        player.RegSpeedHP = (float)Data[3];
        player.MaxMP = (float)Data[4];
        player.MP = (float)Data[5];
        player.RegSpeedMP = (float)Data[6];
        player.MagResist = (float)Data[7];
        player.MaxSP = (float)Data[8];
        player.SP = (float)Data[9];
        player.RegSpeedSP = (float)Data[10];
        player.SoulResist = (float)Data[11];
        player.MaxST = (float)Data[12];
        player.ST = (float)Data[13];
        player.RegSpeedST = (float)Data[14];
        player.PhysResist = (float)Data[15];
        player.SumBaseDamage = (float)Data[16];
        player.Speed = (float)Data[17];
        if (!SkillsLoaded)
        {
            LoadSkills();
        }
        foreach (var item in (List<int>)Data[18])
        {
            player.AddSkill(this.Skills[item].Clone());
        }
        player.MaxHungry = (float)Data[19];
        player.Hungry = (float)Data[20];
        player.RegSpeedH = (float)Data[21];
        player.MaxThirst = (float)Data[22];
        player.Thirst = (float)Data[23];
        player.RegSpeedT = (float)Data[24];
        player.MaxCorruption = (float)Data[25];
        player.Corruption = (float)Data[26];
        player.RegSpeedCP = (float)Data[27];
        world.StartGeneration((MyVector3)Data[28]);
        player.MaxXP = (float)Data[29];
        player.XP = (float)Data[30];
        player.Lvl = (int)Data[31];

        player.AddEffects((List<int>)Data[32]);
        foreach (var item in (List<int>)Data[33])
        {
            Inventory.Items.Add(Items[item]);
        }
        player.inventory.Render(Inventory.Items);
        foreach (var item in (List<int>)Data[34])
        {
            if (item == -1)
            {
                continue;
            }
            player.inventory.SetEquipment(Items[item]);

        }
        if ((int)Data[35]!=-1)
        {
            player.OnRightClickSkill = Skills[(int)Data[35]];
        }
        if ((int)Data[36] != -1)
        {
            player.OnLeftClickSkill = Skills[(int)Data[36]];
        }
        player.SetSkillsOnButtons((List<int>)Data[37]);
        //Debug.Log("Loader:load player true");
        return true;
    }
    public void CreatePlayer(Player player)
    {
        LoadMap();
        LoadSkills();
        LoadStates();
        LoadItems();
        //Debug.Log("Loader:create player");
        //player.transform.position = new Vector3();
        player.MaxHP = 100;
        player.HP = 100;
        player.RegSpeedHP = 1;
        player.MaxMP = 100;
        player.MP = 100;
        player.RegSpeedMP = 1;
        player.MagResist = 1;
        player.MaxSP = 100;
        player.SP = 100;
        player.RegSpeedSP = 1;
        player.SoulResist = 1;
        player.MaxST = 100;
        player.ST = 100;
        player.RegSpeedST = 1;
        player.PhysResist = 1;
        player.SumBaseDamage = 10;
        player.Speed = 1;
        player.MaxHungry = 100;
        player.Hungry = 0;
        player.RegSpeedH = 0.2f;
        player.MaxThirst = 100;
        player.Thirst = 0;
        player.RegSpeedT = 0.2f;
        player.MaxCorruption = 100;
        player.Corruption = 0;
        player.RegSpeedCP = 0.2f;
        player.Skills = new List<Skill>();
        player.MaxXP = 100;
        player.Lvl = 1;
        if (!SkillsLoaded)
        {
            LoadSkills();
        }
        //player.Skills.Add(Skills[0].Clone());
        //player.Skills.Add(Skills[1].Clone());
        //player.Skills.Add(Skills[2].Clone());
        //player.Skills.Add(Skills[4].Clone());
        //player.Skills.Add(Skills[5].Clone());
        //player.Skills.Add(Skills[6].Clone());
        //Debug.Log(Items[0].Description);
        player.inventory.AddItem(Items[0]);
        player.inventory.AddItem(Items[0]);
        player.inventory.AddItem(Items[1]);
        player.inventory.AddItem(Items[1]);
        player.inventory.AddItem(Items[2]);
        player.inventory.AddItem(Items[2]);
        player.inventory.AddItem(Items[3]);
        player.inventory.AddItem(Items[4]);
        player.inventory.AddItem(Items[5]);
        player.inventory.AddItem(Items[5]);
        //Debug.Log(".mapcell");
        world.StartGeneration(new MyVector3(10, 10));
    }
    public void SavePlayer()
    {
        //Debug.Log("Loader: player saved");
        List<object> Data = new List<object>() {MyVector3.Set(world.localPosition(player.transform.position)),
        player.GetMaxHP(), player.HP,  player.GetHPRegSpeed(),
        player.GetMaxMP(), player.MP, player.GetMPRegSpeed(),player.GetMagReist(),
        player.GetMaxSP(), player.SP, player.GetSPRegSpeed(),player.GetSoulResist(),
        player.GetMaxST(), player.ST, player.GetSTRegSpeed(), player.GetPhyResist(),
        player.GetMaxSumBaseDamage(),
        player.GetSpeed(),
        player.Skills.Select(x=>x.ID).ToList(),
        player.MaxHungry, player.Hungry, player.RegSpeedH,
        player.MaxThirst, player.Thirst, player.RegSpeedT,
        player.MaxCorruption, player.Corruption, player.RegSpeedCP,
        new MyVector3(world.ActiveMapCell.ThisCell.PosX, world.ActiveMapCell.ThisCell.PosY),
        player.MaxXP, player.XP, player.Lvl,
        player.States.Where(x=>x.Duration==-1).Select(x=>x.ID).ToList(),
        Inventory.Items.Select(x=>x.ID).ToList(),//33
        player.inventory.equipment.equipment.Select((x)=>{
            if(x.child==null)
            {
                return -1;
            }
            return x.child._item.ID;

        }).ToList(),
        player.OnRightClickSkill==null?-1:player.OnRightClickSkill.ID,
        player.OnLeftClickSkill==null?-1:player.OnLeftClickSkill.ID,
        player.SkillBar.buttons.Select((x)=>{
            if(x.skill==null)
            {
                return -1;
            }
            return x.skill.ID;

        }).ToList()
        };
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream($"player.save", FileMode.OpenOrCreate))
        {
            //Debug.Log("i'm here 2");
            formatter.Serialize(fs, Data);
        }
        
    }

}

[Serializable]
public class MyVector3 {
    
    public float x;
    public float y;
    public float z;
    public MyVector3(float X, float Y)
    {
        x = X;
        y = Y;
    }
    public static MyVector3 Set(Vector2 vector3)
    {
        MyVector3 vec=new MyVector3(vector3.x, vector3.y);                
        return vec;
    }
    public Vector3 Get()
    {
        Vector3 vector3 = new Vector3();
        vector3.x=x;
        vector3.y=y;
        vector3.z=z;
        return vector3;
    }
}