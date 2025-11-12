using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Credit;
    public GameObject Tutorial;
    private bool inCredit = false;
    private bool inTutorial = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Credit.SetActive(false);
        Tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene("nabil");
    }

    public void credit()
    {
        Credit.SetActive(true);
        inCredit = true;
        Tutorial.SetActive(false);
        inTutorial = false;
    }

    public void tutorial()
    {
        Credit.SetActive(false);
        inCredit = false;
        Tutorial.SetActive(true);
        inTutorial = true;
    }

    public void exit()
    {
        Application.Quit();
    }

    public void back()
    {
        Credit.SetActive(false);
        inCredit = false;
        Tutorial.SetActive(false);
        inTutorial = false;
    }
}
