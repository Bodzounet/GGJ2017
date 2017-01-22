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

    void Start()
    {
        Selected = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Selected = _selected - 1 < 0 ? buttons.Length - 1 : _selected - 1;
            rotateCam.RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Selected = _selected + 1 == buttons.Length ? 0 : _selected + 1;
            rotateCam.RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[Selected].GetComponent<Button>().onClick.Invoke();
        }
    }
}
