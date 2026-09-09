using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void OnPause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Show the menu
        Time.timeScale = 0f;       // FREEZE TIME
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Hide the menu
        Time.timeScale = 1f;        // START TIME
        isPaused = false;
    }
}