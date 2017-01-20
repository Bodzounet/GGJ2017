using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class JoystickManager : MonoBehaviour
{

    bool[] playerIndexSet;
    PlayerIndex[] playerIndex;
    GamePadState[] state;
    GamePadState[] prevState;

    // Use this for initialization
    void Start()
    {
        playerIndex = new PlayerIndex[2];
        state = new GamePadState[2];
        prevState = new GamePadState[2];
        playerIndexSet = new bool[2];
        playerIndexSet[0] = false;
        playerIndexSet[1] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIndexSet[0] || !prevState[0].IsConnected /*|| !prevState[1].IsConnected || !playerIndexSet[1] */)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex[i] = testPlayerIndex;
                    playerIndexSet[0] = true;
                }
            }
        }

        prevState[0] = state[0];
        state[0] = GamePad.GetState(playerIndex[0]);

        if (playerIndexSet[1])
        {
            prevState[1] = state[1];
            state[1] = GamePad.GetState(playerIndex[1]);
        }
    }


  /*  void OnGUI()
    {
        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state[0].ThumbSticks.Left.X, state[0].ThumbSticks.Left.Y, state[0].ThumbSticks.Right.X, state[0].ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
    */
}
