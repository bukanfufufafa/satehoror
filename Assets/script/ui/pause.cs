using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject SettingsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
        SettingsPanel.SetActive(false);
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
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void back()
    {
        SettingsPanel.SetActive(false );
        pausePanel.SetActive(true);
    }
}
