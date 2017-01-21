using UnityEngine;
using System.Collections;
using System.Linq;

public class PlaceTowerOnMap : MonoBehaviour
{
    public Dict_TowerIdToPrefab[] previsualisations;
    public Dict_TowerIdToPrefab[] towers;

    public int XSize = 10;
    public int YSize = 20;

    public Transform[] margins; // topLeft, rightLeft, bottomLeft, bottomRight;

    private Vector2 _currentCoord = new Vector2(0, 0);
    private Vector2 _correction;
    private GameObject _previsualisation;
    private Renderer[] _rd;

    private GameObject _overlappingTower = null;

    private float _xSize;
    private float _ySize;

    private TowerEntity.e_TowerId currentTower = TowerEntity.e_TowerId.None;

    public JoystickManager jm;
    public SwitchItemData sid;
    public CurrenciesManager cm;

    public GoldCost gc;

    void Start()
    {
        // to make it work, _xsize has to be equal to _ySize
        _xSize = (margins[1].position.x - margins[0].position.x) / XSize;
        _ySize = (margins[0].position.z - margins[2].position.z) / YSize;

        _correction = new Vector2(-_xSize * XSize / 2, -_ySize * YSize / 2);
    //    sid.OnSwitchItem += UpdatePrevisualisation;
       // SetTowerModel(TowerEntity.e_TowerId.XRAY);
    }

    private bool _IsButtonAPressed = false;

    void Update()
    {
        if (currentTower == TowerEntity.e_TowerId.None)
            return;

        _UpdatePrevisualisationPosition();
        _SetPrevisualisationColor();

        if (jm.state[0].Buttons.A == XInputDotNetPure.ButtonState.Pressed && _overlappingTower == false && _IsButtonAPressed == false)
        {
            _IsButtonAPressed = true;
            int gold = gc.GetTowerCost(currentTower);
            if (cm.currencies[CurrenciesManager.e_Currencies.Gold].HasEnoughCurrency(gold))
            {
                _CreateTower();
                cm.currencies[CurrenciesManager.e_Currencies.Gold].UseCurrency(gold);
            }
            _timeSmoothSlide = _startTimeSmoothSlide;

        }
        if (jm.state[0].Buttons.A == XInputDotNetPure.ButtonState.Released)
        {
            _IsButtonAPressed = false;
        }
    }

    public void SetTowerModel(TowerEntity.e_TowerId id)
    {
        currentTower = id;
        if (id == TowerEntity.e_TowerId.None)
        {
            Destroy(_previsualisation);
        }
        else
        {
            Destroy(_previsualisation);
            _CreatePrevisualisation();
        }
    }

    private void _CreatePrevisualisation()
    {
        //_currentCoord = Vector2.zero;
        _previsualisation = Instantiate(previsualisations.Single(x => x.id == currentTower).prefab) as GameObject;
        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize + _correction.x, 0, _currentCoord.y * _ySize + _correction.y);
        _previsualisation.transform.localScale = (new Vector3(_xSize, _xSize, _xSize) / 1.5f);
        _rd = _previsualisation.GetComponentsInChildren<Renderer>();
        _UpdateOverlappingTower();
        _ComputeOverlappingTower();
    }

    private void _CreateTower()
    {
        Debug.Log("caca");
        var v = GameObject.Instantiate(towers.Single(x => x.id == currentTower).prefab, _previsualisation.transform.position, Quaternion.identity) as GameObject;
        v.transform.localScale = _previsualisation.transform.localScale;
        Destroy(_previsualisation);
        _CreatePrevisualisation();
    }

    void UpdatePrevisualisation(TowerEntity.e_TowerId towerId, MobEntity.e_MobId mobID)
    {
        SetTowerModel(towerId);
    }

    private bool _smoothSlide = false;

    public float _startTimeSmoothSlide = 0.2f;
    public float _timeSmoothSlide = 0.2f;
    public float _opSmoothSlide = 1.1f;

    private void _UpdatePrevisualisationPosition()
    {
        bool hasMove = false;
        if (jm.state[0].ThumbSticks.Left.Y < 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.y > 0)
                _currentCoord.y--;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }
        if (jm.state[0].ThumbSticks.Left.Y > 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.y < YSize)
                _currentCoord.y++;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }
        if (jm.state[0].ThumbSticks.Left.X < 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.x > 0)
                _currentCoord.x--;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }

        if (jm.state[0].ThumbSticks.Left.X > 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.x < XSize)
                _currentCoord.x++;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }

        if (jm.state[0].ThumbSticks.Left.Y == 0 && jm.state[0].ThumbSticks.Left.X == 0)
        {
            _timeSmoothSlide = _startTimeSmoothSlide;
        }
        

        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize + _correction.x, 0, _currentCoord.y * _ySize + _correction.y);

        if (hasMove)
        {
            _ComputeOverlappingTower();
        }
    }

    void SlideSmooth()
    {
        _smoothSlide = false;
    }

    private void _SetPrevisualisationColor()
    {
        if (_overlappingTower != null)
        {
            foreach (var v in _rd)
                v.material.color = Color.red;
        }
        else
        {
            foreach (var v in _rd)
                v.material.color = Color.green;
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
            foreach (var v in _overlappingTower.GetComponentsInChildren<Renderer>())
                v.enabled = true;
        _UpdateOverlappingTower();
        if (_overlappingTower != null)
            foreach (var v in _overlappingTower.GetComponentsInChildren<Renderer>())
                v.enabled = false;
    }

    [System.Serializable]
    public struct Dict_TowerIdToPrefab
    {
        public TowerEntity.e_TowerId id;
        public GameObject prefab;
    }
}
