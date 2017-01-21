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

    public JoystickManager jm;
    public SwitchItemData sid;
    public CurrenciesManager cm;
    public GoldCost gc;
    public int playerdID;

    void Start()
    {
        _XSize = FindObjectOfType<MapData>().XSize;

        _xSize = (margins[1].position.x - margins[0].position.x) / _XSize;;

        _arrow = GameObject.Instantiate(arrowModel);
        _arrow.transform.position = new Vector3(_currentCoord.x * _xSize + margins[0].position.x, 0, margins[0].position.z);
        _arrow.transform.localScale = new Vector3(_xSize, _xSize, _xSize);
    }

    private bool isButtonBPressed = false;
    private bool _smoothSlide = false;
    public float _startTimeSmoothSlide = 0.2f;
    public float _timeSmoothSlide = 0.2f;
    public float _opSmoothSlide = 1.1f;

    void Update()
    {
        if (jm.state[playerdID].ThumbSticks.Right.X < 0 && _smoothSlide == false)
        {
            if (_currentCoord.x > 0)
                _currentCoord.x--;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }

        if (jm.state[playerdID].ThumbSticks.Right.X > 0 && _smoothSlide == false)
        {
            if (_currentCoord.x < _XSize)
                _currentCoord.x++;
            Invoke("SlideSmooth", _timeSmoothSlide);
            _timeSmoothSlide = _timeSmoothSlide / _opSmoothSlide;
            _smoothSlide = true;
        }
        Debug.Log(playerdID);
        _arrow.transform.position = new Vector3(_currentCoord.x * _xSize + margins[0].position.x, 0, margins[0].position.z);

        if (jm.state[playerdID].Buttons.B == XInputDotNetPure.ButtonState.Pressed && !isButtonBPressed)
        {
            isButtonBPressed = true;
            // valeurs en dur pour tester, à modifier
            if (team == GameInfos.e_Team.TEAM1)
            {
                mobSpawner.CreateMob(MobEntity.e_MobId.SOLDIER, new Vector3(_currentCoord.x * _xSize + margins[0].position.x, 0, margins[0].position.z), GameInfos.e_Team.TEAM2);
            }
            else
            {
                mobSpawner.CreateMob(MobEntity.e_MobId.SOLDIER, new Vector3(_currentCoord.x * _xSize + margins[0].position.x, 0, margins[0].position.z), GameInfos.e_Team.TEAM1);
            }
            // valeurs en dur pour tester, à modifier
        }
        if (jm.state[playerdID].Buttons.B == XInputDotNetPure.ButtonState.Released)
        {
            isButtonBPressed = false;
        }
        if (jm.state[playerdID].ThumbSticks.Right.Y == 0 && jm.state[playerdID].ThumbSticks.Right.X == 0)
        {
            _timeSmoothSlide = _startTimeSmoothSlide;
        }

    }

    void SlideSmooth()
    {
        _smoothSlide = false;
    }
}
