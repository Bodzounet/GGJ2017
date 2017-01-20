using UnityEngine;
using System.Collections;

public class PlaceTowerOnMap : MonoBehaviour
{
    public GameObject TowerModel;
    public int XSize = 10;
    public int YSize = 20;

    public Transform[] margins; // topLeft, rightLeft, bottomLeft, bottomRight;

    private Vector2 _currentCoord = new Vector2(0, 0);
    private GameObject _previsualisation;

    private float _xSize;
    private float _ySize;
	
    void Start()
    {
        _previsualisation = Instantiate(TowerModel) as GameObject;
        _previsualisation.transform.position = margins[0].position;

        _xSize = (margins[1].position.x - margins[0].position.x) / XSize;
        _ySize = (margins[0].position.z - margins[2].position.z) / YSize;
    }

	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentCoord.y > 0)
                _currentCoord.y--;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_currentCoord.y < YSize)
                _currentCoord.y++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentCoord.x > 0)
                _currentCoord.x--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_currentCoord.x < XSize)
                _currentCoord.x++;
        }

        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize, 0, _currentCoord.y * _ySize);
    }
}
