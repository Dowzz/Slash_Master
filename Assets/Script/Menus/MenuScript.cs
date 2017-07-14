using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public Canvas OptionsMenu;

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
        OptionsMenu.enabled = false;

    }
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }
    public void NoPress()
    {
        quitMenu.enabled = false;
        OptionsMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartOptions()
    {
        SceneManager.LoadScene("Options");
    }
    public void menuPress()
    {
        OptionsMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }




}
