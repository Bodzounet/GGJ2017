using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BtnFcts : MonoBehaviour
{
    public GameObject credits;
    public SelectMenuButton selectMenuButton;

    void Start()
    {
        selectMenuButton.OnSelectedButtonChanged += SelectMenuButton_OnSelectedButtonChanged;
    }

    private void SelectMenuButton_OnSelectedButtonChanged(int id)
    {
        if (id != 2)
            credits.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }
}
