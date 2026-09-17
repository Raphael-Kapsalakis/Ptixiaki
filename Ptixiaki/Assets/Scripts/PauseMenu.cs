using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    private ZombieAI[] allZombies;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetZombiesPaused(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause all zombies
        SetZombiesPaused(true);
    }




    void SetZombiesPaused(bool shouldPause)
    {
        allZombies = FindObjectsOfType<ZombieAI>();

        foreach (var zombie in allZombies)
        {
            if (zombie != null)
            {
                zombie.PauseZombie(shouldPause);
            }
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
