using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class EndGameManager : MonoBehaviour
{
    private GameObject backButton;
    private Button backComponent;

    void Start()
    {
        backButton = GameObject.Find("ReturnToMenu");
        backComponent = backButton.GetComponent<Button>();
        backComponent.onClick.AddListener(BackToMenu);
    }

    void BackToMenu()
    {
        backButton.SetActive(false);
        if (SceneManager.GetActiveScene().name == "GameLost")
        {
            SceneManager.UnloadSceneAsync("GameLost");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.UnloadSceneAsync("GameWon");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        } 
    }
}
