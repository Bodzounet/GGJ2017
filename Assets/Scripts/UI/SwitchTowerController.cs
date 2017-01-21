using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SwitchTowerController : MonoBehaviour
{
    public JoystickManager jm;
    public int playerID;
    public SwitchItemData _switchTowerHolder1;


    private bool _lockSlideTower = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (jm.state[playerID].Buttons.LeftShoulder == ButtonState.Pressed && _lockSlideTower == false)
        {
            Invoke("DelockUISlideTower", 0.5f);
            _switchTowerHolder1.SwitchPositionFromRightToLeft();
            _lockSlideTower = true;
        }
        if (jm.state[playerID].Buttons.RightShoulder == ButtonState.Pressed && _lockSlideTower == false)
        {
            Invoke("DelockUISlideTower", 0.5f);
            _switchTowerHolder1.SwitchPositionFromLeftToRight();
            _lockSlideTower = true;
        }

    }

    void DelockUISlideTower()
    {
        _lockSlideTower = false;
    }
}
