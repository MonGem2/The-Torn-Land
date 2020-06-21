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
    public Color Color { get => color; set => color = value; }
    //public float SomeParameter { get => someParameter; set => someParameter = value; }
    public CellType Type { get => type; set => type = value; }

    public enum CellType
    {
        Empty = -1,
        Wall,
        Floar,
        Door
    };

    public MapCell():base()
    {
        //type = CellType.Floar;
        //color = Color.blue;
    }

    public MapCell(int _x, int _y, Color _color, CellType _type):base()
    {
        x = _x;
        y = _y;
        color = _color;
        type = _type;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(X, Y);
        gameObject.GetComponent<Image>().material = new Material(gameObject.GetComponent<Image>().material);
        gameObject.GetComponent<Image>().material.color = this.color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameObject.GetComponent<Image>().material.color != color)
        //{

        //}
        //Debug.Log(color.ToString() + "    " + x + "     " + y);
       
    }
}
