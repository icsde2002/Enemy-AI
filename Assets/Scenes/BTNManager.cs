using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//adds a new library.
public class BTNManager : MonoBehaviour
{
    public void StoryButton(string StoryScreen)
    {
        SceneManager.LoadScene(StoryScreen);
    }
    public void BeginPlay(string GameScreen)
    {
        SceneManager.LoadScene(GameScreen);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
