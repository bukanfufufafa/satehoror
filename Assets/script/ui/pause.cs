using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pause();
        }
    }

    public void resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void RestartGame()
    {
        // Pastikan waktu berjalan normal
        Time.timeScale = 1f;

        // Ambil nama scene aktif dan muat ulang
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void back()
    {
       
        pausePanel.SetActive(true);
    }
}
