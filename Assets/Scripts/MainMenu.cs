using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class MainMenu : MonoBehaviour
{
    private GameObject Instructions;

    private GameObject startButton;
    private Button startComponent;
    private GameObject instructionButton;
    private Button instructionComponent;
    private GameObject backButton;
    private Button backComponent;

    void Start()
    {
        Instructions = GameObject.Find("InstructionOptions");

        startButton = GameObject.Find("StartGame");
        startComponent = startButton.GetComponent<Button>();
        startComponent.onClick.AddListener(StartGame);

        instructionButton = GameObject.Find("Instructions");
        instructionComponent = instructionButton.GetComponent<Button>();
        instructionComponent.onClick.AddListener(InstructionPanel);

        Instructions.SetActive(false);

        backButton = GameObject.Find("Back");
        backComponent = backButton.GetComponent<Button>();
        backComponent.onClick.AddListener(Back);
        backButton.SetActive(false);
    }


    void StartGame()
    {
        startButton.SetActive(false);
        instructionButton.SetActive(false);
        backButton.SetActive(false);
        SceneManager.UnloadScene("MainMenu");
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    void InstructionPanel()
    {
        startButton.SetActive(false);
        instructionButton.SetActive(false);
        backButton.SetActive(true);
        Instructions.SetActive(true);
    }

    void Back()
    {
        startButton.SetActive(true);
        instructionButton.SetActive(true);
        backButton.SetActive(false);
        Instructions.SetActive(false);
    }
}
