using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuUi : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void PauseGame()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf) {
                ResumeGame();
            } else {
                PauseGame();
            }
            
        }
    }

}
