using UnityEngine;
using System.Collections;

public class SwitchMobController : MonoBehaviour
{

    // Use this for initialization
    public JoystickManager jm;
    public int playerID;
    public SwitchItemData _switchMinionHolder1;

    private bool _lockSlideMinion = false;

    void Start()
    {
        if (jm._playerIndexSet[playerID] == true)
        {
            Debug.Log("playerID = " + playerID + " " + jm.state[playerID].Triggers.Left);
            if (jm.state[playerID].Triggers.Left > 0 && _lockSlideMinion == false)
            {
                Invoke("DelockUISlideMinion", 0.5f);
                _switchMinionHolder1.SwitchPositionFromRightToLeft();
                _lockSlideMinion = true;
            }
            if (jm.state[playerID].Triggers.Right > 0 && _lockSlideMinion == false)
            {
                Invoke("DelockUISlideMinion", 0.5f);
                _switchMinionHolder1.SwitchPositionFromLeftToRight();
                _lockSlideMinion = true;
            }
        }
        

    }

    void DelockUISlideMinion()
    {
        _lockSlideMinion = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
