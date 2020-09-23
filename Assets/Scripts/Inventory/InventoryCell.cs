using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Timers;
using System.Collections;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerClickHandler,/* ISelectHandler,*/ IDeselectHandler
{
    public bool isIn = true;
    public event EventHandler Ejection;
    public event EventHandler Deselection;
    public event EventHandler Selection;
    public event EventHandler Using;
    //[SerializeField] private Text _nameField;
    //[SerializeField] private Text _descrField;
    [SerializeField] private Image _iconField;

    private Transform _draggingParent;
    private Transform _originalParent;
    private Transform _equipParent;
    public Item _item;


    public void Init(Transform originalParent, Transform draggingParent, Transform equipParent)
    {
        _draggingParent = draggingParent;
        _originalParent = originalParent;
        _equipParent = equipParent;
    }

    public void Render(Item item)
    {
        //_nameField.text = item.Name;
        //_descrField.text = item.Description;
        try
        {

            _iconField.sprite = item.UIIcon;
        }
        catch {
            Debug.LogError("WTF");
        }
        _item = item;
    }

    private void Eject()
    {
        Ejection?.Invoke(this, new EventArgs());
    }

    private void Select()
    {
        Selection?.Invoke(this, new EventArgs());
    }

    private void Use()
    {
        Using?.Invoke(this, new EventArgs());
    }

    private void Deselect()
    {
        Deselection?.Invoke(this, new EventArgs());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isIn)
        {
            isDrag = true;
            transform.SetParent(_draggingParent, true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isIn)
            transform.position = Vector2.Lerp(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isIn)
        {
            if (IsInRemover((RectTransform)_originalParent))
            {
                //Debug.LogError("IM IN");
                InsertInGrid();
            }
            else
            {
                //Debug.LogError("Im not in");
                Eject();
                //Destroy(gameObject);
            }
            isDrag = false;
        }
    }

    private bool IsInRemover(RectTransform originalParent)
    {
        return true /*originalParent.rect.Contains(transform.position)*/;
    }

    private void InsertInGrid()
    {
        int closestIndex = 0;

        for (int i = 0; i < _originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
            {
                closestIndex = i;
            }
        }
        transform.SetParent(_originalParent, true);
        transform.SetSiblingIndex(closestIndex);
        transform.localPosition = new Vector3(0, 0, 0);
    }


    private bool isDrag = false;

    private readonly Timer _MouseSingleClickTimer = new Timer();

    private float doubleClickTimeLimit = 0.35f;
    bool clickedOnce = false;
    float count = 0f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDrag)
            StartCoroutine(ClickEvent());
    }

    public IEnumerator ClickEvent()
    {
        if (!clickedOnce && count < doubleClickTimeLimit)
        {
            clickedOnce = true;
        }
        else
        {
            clickedOnce = false;
            yield break; 
        }
        yield return new WaitForEndOfFrame();

        while (count < doubleClickTimeLimit)
        {
            if (!clickedOnce)
            {
                DoubleClick();
                count = 0f;
                clickedOnce = false;
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }
        SingleClick();
        count = 0f;
        clickedOnce = false;
    }
    private void SingleClick()
    {
        Debug.LogWarning("Single Click");
        Select();
    }

    private void DoubleClick()
    {
        Use();
        Debug.LogWarning("Double Click");
    }


    public void OnDeselect(BaseEventData eventData)
    {
        Deselect();
    }
}

