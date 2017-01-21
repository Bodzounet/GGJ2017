using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class JoystickManager : MonoBehaviour
{

    private bool[] _playerIndexSet;
    private PlayerIndex[] _playerIndex;
    private GamePadState[] _state;
    private GamePadState[] _prevState;
    private bool _lockSlideTower = false;

    public GameObject dataHolder1;
    private SwitchItemData _switchDataholder1;

    public GameObject dataHolder2;
    // Use this for initialization
    void Start()
    {
        _playerIndex = new PlayerIndex[2];
        _state = new GamePadState[2];
        _prevState = new GamePadState[2];
        _playerIndexSet = new bool[2];
        _playerIndexSet[0] = false;
        _playerIndexSet[1] = false;
        _switchDataholder1 = dataHolder1.GetComponent<SwitchItemData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerIndexSet[0] || !_prevState[0].IsConnected /*|| !_prevState[1].IsConnected || !_playerIndexSet[1] */)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    _playerIndex[i] = testPlayerIndex;
                    _playerIndexSet[0] = true;
                }
            }
        }

        _prevState[0] = _state[0];
        _state[0] = GamePad.GetState(_playerIndex[0]);

        if (_playerIndexSet[1])
        {
            _prevState[1] = _state[1];
            _state[1] = GamePad.GetState(_playerIndex[1]);
        }
        if (_state[0].Triggers.Left > 0 && _lockSlideTower == false)
        {
            Invoke("DelockUISlideTower", 0.5f);
            _switchDataholder1.SwitchPositionFromLeftToRight();
            _lockSlideTower = true;
        }
        if (_state[0].Triggers.Right > 0 && _lockSlideTower == false)
        {
            Invoke("DelockUISlideTower", 0.5f);
            _switchDataholder1.SwitchPositionFromRightToLeft();
            _lockSlideTower = true;
        }

        if (_state[0].Buttons.A == ButtonState.Pressed)
        {
            LaunchPrevisualisationTower();
        }

    }

    public void LaunchVib(float vibtime)
    {
        StartCoroutine("LaunchVibCoroutine", vibtime);
    }

    IEnumerator LaunchVibCoroutine(float VibTime)
    {
        GamePad.SetVibration(_playerIndex[0], 1f, 1f);
        Debug.Log(VibTime);
        yield return new WaitForSeconds(VibTime);
        GamePad.SetVibration(_playerIndex[0], 0f, 0f);
    }

    void DelockUISlideTower()
    {
        _lockSlideTower = false;
    }

    void LaunchPrevisualisationTower()
    {
        Debug.Log("Lancement de la prévisualisation ... Instanciation de la tour : " + _switchDataholder1.GetCurrentNameTower());
    }


  /*  void OnGUI()
    {
        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", _state[0].ThumbSticks.Left.X, _state[0].ThumbSticks.Left.Y, _state[0].ThumbSticks.Right.X, _state[0].ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
    */
}
