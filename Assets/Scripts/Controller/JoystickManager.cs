using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class JoystickManager : MonoBehaviour
{

    public bool[] _playerIndexSet;
    private PlayerIndex[] _playerIndex;
    public GamePadState[] state;
    private GamePadState[] _prevState;

    // Use this for initialization
    void Start()
    {
        _playerIndex = new PlayerIndex[2];
        state = new GamePadState[2];
        _prevState = new GamePadState[2];
        _playerIndexSet = new bool[2];
        _playerIndexSet[0] = false;
        _playerIndexSet[1] = false;
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
                    _playerIndexSet[i] = true;
                }
            }
        }

        _prevState[0] = state[0];
        state[0] = GamePad.GetState(_playerIndex[0]);

        if (_playerIndexSet[1])
        {
            Debug.Log("deuxième joueur");
            _prevState[1] = state[1];
            state[1] = GamePad.GetState(_playerIndex[1]);
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
}
