using UnityEngine;
using System.Collections;

public class PlaceArrowOnMap : MonoBehaviour
{
    public GameInfos.e_Team team;

    public GameObject arrowModel;
    public MobSpawner mobSpawner;

    public Transform[] margins; // LeftArrow, RightArrow

    private GameObject _arrow;
    private int _XSize;
    private float _xSize;

    private Vector2 _currentCoord = new Vector2(0, 0);
    private Vector2 _correction;


    public JoystickManager jm;
    public SwitchItemData sid;
    public CurrenciesManager cm;
    public GoldCost gc;

    void Start ()
    {
        _XSize = FindObjectOfType<MapData>().XSize;

        _xSize = (margins[1].position.x - margins[0].position.x) / _XSize;
        _correction = new Vector2(-_xSize * _XSize / 2, 0);

        _arrow = GameObject.Instantiate(arrowModel);
        _arrow.transform.position = new Vector3(_currentCoord.x * _xSize + _correction.x, 0, margins[0].position.z);
        _arrow.transform.localScale = new Vector3(_xSize, _xSize, _xSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_currentCoord.x > 0)
                _currentCoord.x--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_currentCoord.x < _XSize)
                _currentCoord.x++;
        }
        _arrow.transform.position = new Vector3(_currentCoord.x * _xSize + _correction.x, 0, margins[0].position.z);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // valeurs en dur pour tester, à modifier
            mobSpawner.CreateMob(MobEntity.e_MobId.SOLDIER, new Vector3(_currentCoord.x * _xSize + _correction.x, 0, margins[0].position.z), GameInfos.e_Team.TEAM1);
            // valeurs en dur pour tester, à modifier
        }
    }
}
