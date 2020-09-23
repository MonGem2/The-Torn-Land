using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCell : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    public InventoryCell child;
    void Start()
    {
        
    }
    public InventoryCell GetEquip()
    {
        return child;
    }
    public InventoryCell RemoveEquip()
    {
        InventoryCell cell = child;
        child = null;
        return child;
    }
    public void SetEquip(InventoryCell cell)
    {
        cell.transform.SetParent(transform);
        cell.transform.localPosition = new Vector3();
        child = cell;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
