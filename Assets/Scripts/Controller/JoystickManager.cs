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
                    _playerIndex[i] = testPlayerIndex;
                    _playerIndexSet[i] = true;
                }
            }
        }

        _prevState[0] = state[0];
        state[0] = GamePad.GetState(_playerIndex[0]);

        if (_playerIndexSet[1])
        {
            _prevState[1] = state[1];
            state[1] = GamePad.GetState(_playerIndex[1]);
        }
    }

    private int playervibID = 0;
    public void LaunchVib(int playerID, float vibtime)
    {
        playervibID = playerID;
        StartCoroutine("LaunchVibCoroutine", vibtime);
    }

    IEnumerator LaunchVibCoroutine(float VibTime)
    {
        GamePad.SetVibration(_playerIndex[playervibID], 1f, 1f);
        yield return new WaitForSeconds(VibTime);
        GamePad.SetVibration(_playerIndex[playervibID], 0f, 0f);

    }
}
