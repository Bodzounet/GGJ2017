using UnityEngine;
using System.Collections;

public class PlaceArrowOnMap : MonoBehaviour
{
    public GameObject arrowModel;

    public Transform[] margins; // LeftArrow, RightArrow

    private GameObject _arrow;
    private float _xSize;

    void Start ()
    {
        _xSize = (margins[1].position.x - margins[0].position.x) / FindObjectOfType<MapData>().XSize;
        _arrow = GameObject.Instantiate(arrowModel);
    }

    void Update()
    {
        //if (Input.GetButtonDown(KeyCode.LeftArrow))
        //{

        //}
    }
}
