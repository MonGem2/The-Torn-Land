using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Item
{
    public int ID;
    public List<int> EffectIds;
    public int Type;
    public ItemType type
    {
        get
        {
            return (ItemType)Type;
        }
        set
        {
            Type = (int)value;
        }
    }
    public Sprite UIIcon { get {
            if (spriteN == -1)
            {
                return Resources.Load<Sprite>(Sprite);
            }
            return Resources.LoadAll<Sprite>(Sprite)[spriteN];
        
        }}
    public string Sprite;
    public int spriteN = -1;
    public string Description;
    public string Name;

}
/// <summary>
///     Unused,
///    Disposable,
///    Helmet,
///    Necklace,
///    Armor,
///    Backpack,
///    Weapon,
///    Bracers,
///    Belt,
///    Ring,
///    Pants,
///    Boots
/// </summary>
public enum ItemType
{
    Unused,
    Disposable,
    Helmet,
    Necklace,
    Armor,
    Backpack,
    Weapon,
    Bracers,
    Belt,
    Ring,
    Pants,
    Boots

}