using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static List<Item> Items = new List<Item>();
    public List<Item> Equips;
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _originalParent;
    [SerializeField] private Transform _draggingParent;
    public Equipment equipment;
    //[SerializeField] private Transform _equipParent;
    [SerializeField] private Transform _infoPanel;
    public static int _capacity = 40;
    public Player player;
    ///public Button InvButton;
    private void Start()
    {
        //InvButton.onClick.AddListener(ClickBtn);
        //this._draggingParent.gameObject.SetActive(false);
        Items.Capacity = _capacity;
        
        (_container as RectTransform).sizeDelta =  new Vector2 (0, (float)(107 + (_capacity / 10 - 1) * 105.3));
        transform.parent.parent.gameObject.SetActive(false);
        Render(Items);

    }
    public void AddItem(Item item)
    {
        try
        {

        Items.Add(item);
        Render(Items);

        }
        catch (Exception)
        {

            Debug.Log(item.Name);
        }
    }
    public bool Remove(Item item)
    {
        bool x = Items.Remove(item);
        Render(Items);
        return x;
    }
    //void ClickBtn()
    //{
    //    this._draggingParent.gameObject.active = !this._draggingParent.gameObject.active;
    //    Debug.Log("Inventory");
    //}
    public void SetEquipment(Item item)
    {
        if (item.type <= ItemType.Disposable)
        {
            return;
        }
        var cell = Instantiate(_inventoryCellTemplate, _container);
        cell.Init(_originalParent, _draggingParent, equipment.transform);
        cell.Render(item);

        cell.Ejection += Destroyer;
        cell.Deselection += InfoHide;
        cell.Selection += InfoSet;
        cell.Using += UseItem;
        equipment.AddEquip(cell);
    }
    private void OnEnable()
    {
        Render(Items);
    }

    public void Render(List <Item> items)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);


        foreach (var item in items) 
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_originalParent, _draggingParent, equipment.transform);
            cell.Render(item);

            cell.Ejection += Destroyer;
            cell.Deselection += InfoHide;
            cell.Selection += InfoSet;
            cell.Using += UseItem;
        }
    }

    private void Destroyer(object sender, EventArgs e)
    {
            Destroy(((InventoryCell)sender).gameObject);
            //Items.Remove();
    }

    private void InfoSet(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(!_infoPanel.gameObject.activeInHierarchy);
        if (_infoPanel.gameObject.activeInHierarchy)
        {
            _infoPanel.transform.position = (sender as InventoryCell).transform.position;
            if (_infoPanel.transform.localPosition.y < -100)
            {
                _infoPanel.transform.localPosition = new Vector2(_infoPanel.transform.localPosition.x, -100);
            }
            if (_infoPanel.transform.localPosition.x > 1000)
            {
                _infoPanel.transform.localPosition = new Vector2(1000, _infoPanel.transform.localPosition.y);
            }
            GameObject.Find("NameItem").GetComponent<Text>().text = (sender as InventoryCell)._item.Name;
            GameObject.Find("ImageItem").GetComponent<Image>().sprite = (sender as InventoryCell)._item.UIIcon;
            GameObject.Find("TypeItem").GetComponent<Text>().text = (sender as InventoryCell)._item.type.ToString();
            GameObject.Find("DescrItem").GetComponent<Text>().text = (sender as InventoryCell)._item.Description;
            Debug.LogWarning("//TODO: Effects inventory");
        }
    }


    private void InfoHide(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(false);
    }

    private void ThrowItem(object sender, EventArgs e)
    {

        Debug.LogWarning("Qwerpop");

    }
    
    private void UseItem(object sender, EventArgs e)
    {

        InventoryCell cell = (sender as InventoryCell);
        _infoPanel.gameObject.SetActive(false);
        if (player.CheckItem(cell._item.ID))
        {
            if (cell._item.type > (ItemType)1&& cell != equipment.GetEquip(cell._item.type))//if item type is equipment and it doesn't equiped now
            {
                player.AddEffects(cell._item.EffectIds);
                if (equipment.GetEquip(cell._item.type) != null)//if something equiped now
                {
                    InventoryCell AlreadyEquiped = equipment.GetEquip(cell._item.type);
                    player.RemoveEffects(AlreadyEquiped._item.EffectIds);
                    AlreadyEquiped.transform.SetParent(_originalParent);
                    Equips.Remove(AlreadyEquiped._item);
                    Items.Add(AlreadyEquiped._item);
                }
                equipment.AddEquip(cell);                    
                cell.transform.localPosition = new Vector3(0, 0, 0);
                Equips.Add(cell._item);
                Items.Remove(cell._item);

            }
            else if (cell._item.type > (ItemType)1 && cell == equipment.GetEquip(cell._item.type))//if this item equiped now
            {
                player.RemoveEffects(cell._item.EffectIds);
                cell.transform.SetParent(_originalParent);
                cell.transform.localPosition = new Vector3(0, 0, 0);
                equipment.RemoveEquip(cell._item.type);
                Items.Add(cell._item);
                Equips.Remove(cell._item);
            }
            else if (cell._item.type == ItemType.Disposable)
            {
                player.AddEffects(cell._item.EffectIds);
                Items.Remove(cell._item);
                Destroyer(sender, e);
            }

        }

    }

}
