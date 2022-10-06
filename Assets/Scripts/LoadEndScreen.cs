using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class LoadEndScreen : MonoBehaviour
{
    public void EndOfTheGame()
    {
        SceneManager.UnloadSceneAsync("MainGame");
        SceneManager.LoadScene("GameLost", LoadSceneMode.Single);
    }

    public void WonTheGame()
    {
        SceneManager.UnloadSceneAsync("MainGame");
        SceneManager.LoadScene("GameWon", LoadSceneMode.Single);
    }

}
