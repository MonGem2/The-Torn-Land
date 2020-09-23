using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    // Start is called before the first frame update
    public List<EquipCell> equipment;
    void Start()
    {
        
    }
    public InventoryCell RemoveEquip(ItemType type)
    {
        return equipment[(int)type - 2].RemoveEquip();
    }
    public InventoryCell GetEquip(ItemType type)
    {
        return equipment[(int)type - 2].GetEquip();
    }
    public void AddEquip(InventoryCell cell)
    {
        equipment[(int)cell._item.type - 2].SetEquip(cell);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
