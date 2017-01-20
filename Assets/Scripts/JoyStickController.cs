using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class JoyStickController : MonoBehaviour
{
    public enum e_XBoxControllerAxis
    {
        Horizontal,
        Vertical,
        HCross,
        VCross,
        HJoyStick,
        VJoyStick,
        RT,
        LT
    }

    public enum e_XBoxControllerButtons
    {
        A,
        B,
        Y,
        X,
        Start,
        Back,
        RB,
        LB,
        LeftJSClick,
        RightJSClick,
        RT,
        LT
    }

    [SerializeField]
    private int _joystickId;
    public int JoystickId
    {
        get { return _joystickId; }
        set
        {
            _joystickId = value;
            setMaps((value + 1).ToString());
        }
    }

    private Dictionary<e_XBoxControllerAxis, string> _givenNameToAxis;
    private Dictionary<e_XBoxControllerButtons, string> _givenNameToButton;

    GamePadState state;
    GamePadState prevState;

    public void Reset(int id)
    {
        _givenNameToAxis = new Dictionary<e_XBoxControllerAxis, string>();
        _givenNameToButton = new Dictionary<e_XBoxControllerButtons, string>();
        JoystickId = id;
    }

    /// <summary>
    /// Axis are : 
    /// Horizontal (left joystick, left/right)
    /// Vertical (left joystick, up/down)
    /// HCross (cross, left/right)
    /// VCross (cross, up/down)
    /// HJoystick (right joystick, left/right)
    /// VJoystick (right joystick, up/down)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetAxis(e_XBoxControllerAxis axis)
    {
        return GetAxisState(axis);
    }

    /// <summary>
    /// the same as GetAxis, but instead of returning a value clamped in [-1;1], it only returns {-1, 0, 1}
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetAxisClamped(e_XBoxControllerAxis axis)
    {
        var val = GetAxis(axis);
        if (val < 0)
            return -1;
        else if (val > 0)
            return 1;
        return 0;
    }

    public float GetAxisAngle()
    {
        // minus -> trigo rotation
        return -Mathf.Atan2(GetAxis(e_XBoxControllerAxis.Vertical), GetAxis(e_XBoxControllerAxis.Horizontal)) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// names are : 
    /// A, B, Y, X, Start, Back, RB, RT, LB, LT, LeftJSClick, RightJSClick
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool GetButtonDown(e_XBoxControllerButtons btn)
    {
        if (btn == e_XBoxControllerButtons.RT || btn == e_XBoxControllerButtons.LT)
        {
            float res = 0.0f;

            if (btn == e_XBoxControllerButtons.RT)
                res = GetAxis(e_XBoxControllerAxis.RT);
            else
                res = GetAxis(e_XBoxControllerAxis.LT);

            return res != 0;
        }
        return GetBtnState(btn) == ButtonState.Pressed && GetBtnPrevState(btn) == ButtonState.Released;
    }

    public bool GetButton(e_XBoxControllerButtons btn)
    {
        if (btn == e_XBoxControllerButtons.RT || btn == e_XBoxControllerButtons.LT)
        {
            float res = 0.0f;

            if (btn == e_XBoxControllerButtons.RT)
                res = GetAxis(e_XBoxControllerAxis.RT);
            else
                res = GetAxis(e_XBoxControllerAxis.LT);

            return res != 0;
        }
        return GetBtnState(btn) == ButtonState.Pressed;
    }

    public bool GetButtonUp(e_XBoxControllerButtons btn)
    {
        if (btn == e_XBoxControllerButtons.RT || btn == e_XBoxControllerButtons.LT)
        {
            float res = 0.0f;

            if (btn == e_XBoxControllerButtons.RT)
                res = GetAxis(e_XBoxControllerAxis.RT);
            else
                res = GetAxis(e_XBoxControllerAxis.LT);

            return res == 0;
        }
        return GetBtnState(btn) == ButtonState.Released && GetBtnPrevState(btn) == ButtonState.Pressed;
    }

    private ButtonState GetBtnState(e_XBoxControllerButtons btn)
    {
        if (!state.IsConnected)
            return ButtonState.Released;

        if (btn == e_XBoxControllerButtons.A)
            return state.Buttons.A;
        if (btn == e_XBoxControllerButtons.B)
            return state.Buttons.B;
        if (btn == e_XBoxControllerButtons.X)
            return state.Buttons.X;
        if (btn == e_XBoxControllerButtons.Y)
            return state.Buttons.Y;
        if (btn == e_XBoxControllerButtons.Start)
            return state.Buttons.Start;
        if (btn == e_XBoxControllerButtons.Back)
            return state.Buttons.Back;
        if (btn == e_XBoxControllerButtons.RB)
            return state.Buttons.RightShoulder;
        if (btn == e_XBoxControllerButtons.LB)
            return state.Buttons.LeftShoulder;
        if (btn == e_XBoxControllerButtons.RightJSClick)
            return state.Buttons.RightStick;
        if (btn == e_XBoxControllerButtons.LeftJSClick)
            return state.Buttons.LeftStick;


        return ButtonState.Released;
    }

    private ButtonState GetBtnPrevState(e_XBoxControllerButtons btn)
    {
        if (!prevState.IsConnected)
            return ButtonState.Released;

        if (btn == e_XBoxControllerButtons.A)
            return prevState.Buttons.A;
        if (btn == e_XBoxControllerButtons.B)
            return prevState.Buttons.B;
        if (btn == e_XBoxControllerButtons.X)
            return prevState.Buttons.X;
        if (btn == e_XBoxControllerButtons.Y)
            return prevState.Buttons.Y;
        if (btn == e_XBoxControllerButtons.Start)
            return prevState.Buttons.Start;
        if (btn == e_XBoxControllerButtons.Back)
            return prevState.Buttons.Back;
        if (btn == e_XBoxControllerButtons.RB)
            return prevState.Buttons.RightShoulder;
        if (btn == e_XBoxControllerButtons.LB)
            return prevState.Buttons.LeftShoulder;
        if (btn == e_XBoxControllerButtons.RightJSClick)
            return prevState.Buttons.RightStick;
        if (btn == e_XBoxControllerButtons.LeftJSClick)
            return prevState.Buttons.LeftStick;


        return ButtonState.Released;
    }

    private float GetAxisState(e_XBoxControllerAxis axis)
    {
        if (!state.IsConnected)
            return .0f;

        if (axis == e_XBoxControllerAxis.Horizontal)
            return state.ThumbSticks.Left.X;
        if (axis == e_XBoxControllerAxis.Vertical)
            return state.ThumbSticks.Left.Y;
        if (axis == e_XBoxControllerAxis.HCross)
            return state.DPad.Left == ButtonState.Pressed ? -1.0f : (state.DPad.Right == ButtonState.Pressed ? 1.0f : 0.0f);
        if (axis == e_XBoxControllerAxis.VCross)
            return state.DPad.Down == ButtonState.Pressed ? -1.0f : (state.DPad.Up == ButtonState.Pressed ? 1.0f : 0.0f);
        if (axis == e_XBoxControllerAxis.HJoyStick)
            return state.ThumbSticks.Right.X;
        if (axis == e_XBoxControllerAxis.VJoyStick)
            return state.ThumbSticks.Right.Y;
        if (axis == e_XBoxControllerAxis.RT)
            return state.Triggers.Right;
        if (axis == e_XBoxControllerAxis.LT)
            return state.Triggers.Left;

        return .0f;
    }

    public void Update()
    {
        //Debug.Log("Kikoo "+ _joystickId);
        prevState = state;
        state = GamePad.GetState((PlayerIndex)_joystickId);
    }

    public void SetVibration(float left, float right)
    {
        GamePad.SetVibration((PlayerIndex)_joystickId, left, right);
    }

    private void setMaps(string id)
    {
        _givenNameToAxis[e_XBoxControllerAxis.Horizontal] = "Horizontal" + id;
        _givenNameToAxis[e_XBoxControllerAxis.Vertical] = "Vertical" + id;
        _givenNameToAxis[e_XBoxControllerAxis.HCross] = "HCross" + id;
        _givenNameToAxis[e_XBoxControllerAxis.VCross] = "VCross" + id;
        _givenNameToAxis[e_XBoxControllerAxis.HJoyStick] = "HJoystick" + id;
        _givenNameToAxis[e_XBoxControllerAxis.VJoyStick] = "VJoystick" + id;

        _givenNameToButton[e_XBoxControllerButtons.A] = "A" + id;
        _givenNameToButton[e_XBoxControllerButtons.B] = "B" + id;
        _givenNameToButton[e_XBoxControllerButtons.Y] = "Y" + id;
        _givenNameToButton[e_XBoxControllerButtons.X] = "X" + id;
        _givenNameToButton[e_XBoxControllerButtons.Start] = "Start" + id;
        _givenNameToButton[e_XBoxControllerButtons.Back] = "Back" + id;
        _givenNameToButton[e_XBoxControllerButtons.RB] = "RB" + id;
        _givenNameToButton[e_XBoxControllerButtons.LB] = "LB" + id;
        _givenNameToButton[e_XBoxControllerButtons.LeftJSClick] = "LeftJSClick" + id;
        _givenNameToButton[e_XBoxControllerButtons.RightJSClick] = "RightJSCLick" + id;
        _givenNameToButton[e_XBoxControllerButtons.RT] = "RT" + id;
        _givenNameToButton[e_XBoxControllerButtons.LT] = "LT" + id;
    }
}