using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Quit game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
