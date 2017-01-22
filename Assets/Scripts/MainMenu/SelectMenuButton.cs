using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectMenuButton : MonoBehaviour
{
    public delegate void SelectedButtonChanged(int id);
    public event SelectedButtonChanged OnSelectedButtonChanged;

    int _selected = 0;

    public GameObject[] buttons;
    public RotateCam rotateCam;

    public GameObject credits;

    public int Selected
    {
        get
        {
            return _selected;
        }

        set
        {
            buttons[Selected].GetComponent<Animator>().SetBool("Selected", false);
            _selected = value;
            buttons[Selected].GetComponent<Animator>().SetBool("Selected", true);

            if (OnSelectedButtonChanged != null)
                OnSelectedButtonChanged(_selected);
        }
    }

    JoystickManager jm;
    private bool _smoothSlide = false;
    void SlideSmooth()
    {
        _smoothSlide = false;
    }

    void Start()
    {
        Selected = 0;
        jm = this.GetComponent<JoystickManager>();
    }

    void Update()
    {
        if (jm.state[1].ThumbSticks.Left.Y > 0 && _smoothSlide == false)
        {
            Selected = _selected - 1 < 0 ? buttons.Length - 1 : _selected - 1;
            Invoke("SlideSmooth", 0.2f);
            rotateCam.RotateLeft();
            _smoothSlide = true;
        }

        if (jm.state[1].ThumbSticks.Left.Y < 0 && _smoothSlide == false)
        {
            Selected = _selected + 1 == buttons.Length ? 0 : _selected + 1;
            Invoke("SlideSmooth", 0.2f);
            rotateCam.RotateRight();
            _smoothSlide = true;
        }

        if (jm.state[1].Buttons.A == XInputDotNetPure.ButtonState.Pressed)
        {
            buttons[Selected].GetComponent<Button>().onClick.Invoke();
        }
    }
}
