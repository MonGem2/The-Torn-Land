using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{
    public float x;
    public float y;
    public Color color;
    private float someParameter;
    private CellType type;

    public float X { get => x; set => x = value; }
    public float Y { get => y; set => y = value; }
    public float mySizeX = 1;
    public float mySizeY = 1;
    public Color Color { get => color; set => color = value; }
    //public float SomeParameter { get => someParameter; set => someParameter = value; }
    public CellType Type { get => type; set => type = value; }
    public float MySizeX { get => mySizeX; set => mySizeX = value; }

    public enum CellType
    {
        Empty = -1,
        Wall,
        Floar,
        Door,
        Checked = -100
    };

    public MapCell() : base()
    {
        //type = CellType.Floar;
        //color = Color.blue;
    }

    public void SetAll(int _x, int _y, Color _color, CellType _type, float sizeX, float sizeY)
    {
        //Debug.Log("i'm here 11");
        x = _x;
        y = _y;
        color = _color;
        type = _type;
        mySizeX = sizeX;
        mySizeY = sizeY;
    }

    // Start is called before the first frame update
    void Start()
    {
        //mySizeY = 1;
        //SizeX = 1;
       // Debug.Log(mySizeX + "   ggg  " + mySizeY);
        gameObject.transform.localPosition = new Vector3(X, Y);
        gameObject.GetComponent<Image>().material = new Material(gameObject.GetComponent<Image>().material);
        gameObject.GetComponent<Image>().material.color = this.color;
        GetComponent<RectTransform>().sizeDelta = new Vector2(mySizeX, mySizeY);
        if (type == CellType.Wall)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(mySizeX, mySizeY);
            GetComponent<BoxCollider2D>().offset = new Vector2(mySizeX * 0.5f, mySizeY * 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<RectTransform>().sizeDelta = new Vector2(mySizeX, mySizeY);
        //if (type == CellType.Wall)
        //{
        //    GetComponent<BoxCollider2D>().size = new Vector2(mySizeX, mySizeY);
        //    GetComponent<BoxCollider2D>().offset = new Vector2(mySizeX * 0.5f, mySizeY * 0.5f);
        //}
        //if (gameObject.GetComponent<Image>().material.color != color)
        //{

        //}
        //Debug.Log(color.ToString() + "    " + x + "     " + y);

    }
}
