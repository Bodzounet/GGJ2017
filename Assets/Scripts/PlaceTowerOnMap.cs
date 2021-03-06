﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class PlaceTowerOnMap : MonoBehaviour
{
    public Dict_TowerIdToPrefab[] previsualisations;
    public Dict_TowerIdToPrefab[] towers;

    public int XSize;
    public int YSize;

    public Transform[] margins; // topLeft, rightLeft, bottomLeft, bottomRight;

    private Vector2 _currentCoord = new Vector2(0, 0);
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

    public int playerID;

    public GameInfos.e_Team team;

    void Awake()
    {
        sid.OnSwitchItem += UpdatePrevisualisation;
        var v = FindObjectOfType<MapData>();
        XSize = v.XSize;
        YSize = v.YSize;
        _xSize = (margins[1].position.x - margins[0].position.x) / XSize;
        _ySize = (margins[0].position.z - margins[2].position.z) / YSize;
    }

    void Start()
    {

    }

    private bool _IsButtonAPressed = false;

    void Update()
    {
        if (currentTower == TowerEntity.e_TowerId.None)
            return;

        _UpdatePrevisualisationPosition();
        _SetPrevisualisationColor();

        if (jm.state[playerID].Buttons.A == XInputDotNetPure.ButtonState.Pressed && _overlappingTower == false && _IsButtonAPressed == false)
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
        if (jm.state[playerID].Buttons.A == XInputDotNetPure.ButtonState.Released)
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
        _previsualisation = Instantiate(previsualisations.Single(x => x.id == currentTower).prefab) as GameObject;
        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize + margins[2].position.x, 0, _currentCoord.y * _ySize + margins[2].position.z);
        _previsualisation.transform.localScale = (new Vector3(_xSize, _xSize, _xSize) / 1.5f);
        _rd = _previsualisation.GetComponentsInChildren<Renderer>();
        _UpdateOverlappingTower();
        _ComputeOverlappingTower();
    }

    private void _CreateTower()
    {
        var v = GameObject.Instantiate(towers.Single(x => x.id == currentTower).prefab, _previsualisation.transform.position, Quaternion.identity) as GameObject;
        v.transform.localScale = _previsualisation.transform.localScale;

        var e = v.GetComponentInChildren<TowerEntity>();
        e.team = team;
        e.Level = FindObjectsOfType<UpgradeCenter>().Single(x => x.team == team).towerToLevel[currentTower];

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
        if (jm.state[playerID].ThumbSticks.Left.Y < 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.y > 0)
                _currentCoord.y--;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }
        if (jm.state[playerID].ThumbSticks.Left.Y > 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.y < YSize)
                _currentCoord.y++;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }
        if (jm.state[playerID].ThumbSticks.Left.X < 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.x > 0)
                _currentCoord.x--;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }

        if (jm.state[playerID].ThumbSticks.Left.X > 0 && _smoothSlide == false)
        {
            hasMove = true;
            if (_currentCoord.x < XSize)
                _currentCoord.x++;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }

        if (jm.state[playerID].ThumbSticks.Left.Y == 0 && jm.state[playerID].ThumbSticks.Left.X == 0)
        {
            _timeSmoothSlide = _startTimeSmoothSlide;
        }

        _previsualisation.transform.position = new Vector3(_currentCoord.x * _xSize + margins[2].position.x, 0, _currentCoord.y * _ySize + margins[2].position.z);
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

        Debug.Log(v.Length);
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
