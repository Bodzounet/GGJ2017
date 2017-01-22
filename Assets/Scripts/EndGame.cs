using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public DestroyAI[] destroyAI;
    bool stopUpdade = false;

    public GameObject VictoryPanelTeam1;
    public GameObject VictoryPanelTeam2;
    public GameObject DefeatPanelTeam1;
    public GameObject DefeatPanelTeam2;

    void Update ()
    {
        if (stopUpdade)
            return;

	    foreach (var v in destroyAI)
        {
            if (v.gameEnded)
            {
                stopUpdade = true;
                DoEndGame(v.team);
            }
        }
	}

    void DoEndGame(GameInfos.e_Team team)
    {
        Time.timeScale = 0;
        if (team == GameInfos.e_Team.TEAM1)
        {
            VictoryPanelTeam1.SetActive(true);
            DefeatPanelTeam2.SetActive(true);
        }
        else
        {
            VictoryPanelTeam2.SetActive(true);
            DefeatPanelTeam1.SetActive(true);
        }

        StartCoroutine("BackToMenu");
    }

    private IEnumerable BackToMenu()
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + 3)
            yield return 0;

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
