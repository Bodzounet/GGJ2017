using UnityEngine;
using System.Collections;

public class PlaceTowerOnMap : MonoBehaviour
{
    public GameObject previsualisation;
    public GameObject tower;
    public int XSize = 10;
    public int YSize = 20;

    public Transform[] margins; // topLeft, rightLeft, bottomLeft, bottomRight;

    private Vector2 _currentCoord = new Vector2(0, 0);
    private Vector2 _correction;
    private GameObject _previsualisation;
    private Renderer _rd;

    private GameObject _overlappingTower = null;

    private float _xSize;
    private float _ySize;
	
    void Start()
    {
        _CreatePrevisualisation();

        // to make it work, _xsize has to be equal to _ySize
        _xSize = (margins[1].position.x - margins[0].position.x) / XSize;
        _ySize = (margins[0].position.z - margins[2].position.z) / YSize;
        _previsualisation.transform.localScale = new Vector3(_xSize, _xSize, _xSize);

        _correction = new Vector2(-_xSize * XSize / 2, -_ySize * YSize / 2);
    }

	void Update ()
    {
        _UpdatePrevisualisationPosition();
        _SetPrevisualisationColor();

        if (Input.GetKeyDown(KeyCode.Space) && _overlappingTower == false)
        {
            _CreateTower();
        }
    }

    private void _CreatePrevisualisation()
    {
        //_currentCoord = Vector2.zero;
        _previsualisation = Instantiate(previsualisation) as GameObject;
        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize + _correction.x, 0, _currentCoord.y * _ySize + _correction.y);
        _previsualisation.transform.localScale = new Vector3(_xSize, _xSize, _xSize);
        _rd = _previsualisation.GetComponent<Renderer>();
        _UpdateOverlappingTower();
        _ComputeOverlappingTower();
    }

    private void _CreateTower()
    {
        var v = GameObject.Instantiate(tower, _previsualisation.transform.position, Quaternion.identity) as GameObject;
        v.transform.localScale = _previsualisation.transform.localScale;
        Destroy(_previsualisation);
        _CreatePrevisualisation();
    }

    private void _UpdatePrevisualisationPosition()
    {
        bool hasMove = false;
        if (Input.GetKeyDown(KeyCode.S))
        {
            hasMove = true;
            if (_currentCoord.y > 0)
                _currentCoord.y--;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hasMove = true;
            if (_currentCoord.y < YSize)
                _currentCoord.y++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hasMove = true;
            if (_currentCoord.x > 0)
                _currentCoord.x--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            hasMove = true;
            if (_currentCoord.x < XSize)
                _currentCoord.x++;
        }

        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize + _correction.x, 0, _currentCoord.y * _ySize + _correction.y);

        if (hasMove)
        {
            _ComputeOverlappingTower();
        }
    }

    private void _SetPrevisualisationColor()
    {
        if (_overlappingTower != null)
        {
            _rd.material.color = Color.red;
        }
        else
        {
            _rd.material.color = Color.green;
        }
    }

    private void _UpdateOverlappingTower()
    {
        var v = Physics.OverlapSphere(_previsualisation.transform.position, 0.01f, 1 << LayerMask.NameToLayer("Tower"));
        if (v.Length == 0)
        {
            _overlappingTower = null;
        }
        else
        {
            _overlappingTower = v[0].gameObject;
        }
    }

    private void _ComputeOverlappingTower()
    {
        if (_overlappingTower != null)
            _overlappingTower.GetComponent<Renderer>().enabled = true;
        _UpdateOverlappingTower();
        if (_overlappingTower != null)
            _overlappingTower.GetComponent<Renderer>().enabled = false;
    }
}
