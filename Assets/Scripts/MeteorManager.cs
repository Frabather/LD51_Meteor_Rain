using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorManager : MonoBehaviour
{

    private GameObject Meteor;
    private GameObject Coin;
    private GameObject ProgressBar;
    private int howManySeconds = 10;
    private float timeRemaining;
    private int meteorRainCounter = 0;
    private int timeToShow;
    private Text timeOnScreen;
    private AudioSource playerAudioSource;
    private AudioClip meteorRainStarts;

    private Text timeLeftUI;

    void Start()
    {
        playerAudioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        meteorRainStarts = GameObject.Find("Player").GetComponent<PlayerMovement>().meteorRain;

        timeRemaining = howManySeconds;
        timeOnScreen = GameObject.Find("TimeOnScreen").GetComponent<Text>();
        ProgressBar = GameObject.Find("RedBar");

        timeLeftUI = GameObject.Find("TimeLeft").GetComponent<Text>();

    }


    void Update()
    {
        timeLeftUI.text = meteorRainCounter + " / 60 (10minutes)";

        if (meteorRainCounter > 60) //WIN THE GAME AFTER 10MIN!
        {
            GameObject.Find("EmptyObjectScriptManager").GetComponent<LoadEndScreen>().WonTheGame();
        }


        ProgressBar.GetComponent<RectTransform>().localScale = new Vector3(timeRemaining / 10, 1f, 1f);
        timeOnScreen.text = timeToShow.ToString();

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeToShow = (int)timeRemaining;
        }
        else
        {
            meteorRainCounter++;
            timeRemaining = howManySeconds;
            MeteorRain(meteorRainCounter);
        }
    }


    void MeteorRain(int meteorRainCounter)
    {
        int chance;
        Meteor = GameObject.Find("Meteor");
        Coin = GameObject.Find("Coin");
        int offSet = Random.Range(5, 10);

        if (meteorRainCounter >= 35)
        {
            chance = 100 * (meteorRainCounter / (75));
        }
        else
        {
            chance = 35;
        }

        if (Random.Range(1, 100) <= chance)
        {
            int xPositionCoin = Random.Range(-15, 15);
            Instantiate(Coin, new Vector3(xPositionCoin, -8, 0), Quaternion.identity);
        }


        int meteorCount = (int)(offSet + Random.Range(0f, 2f) * meteorRainCounter);
        if (meteorCount > 35)
        {
            meteorCount = Random.Range(25, 35);
        }




        //Instantiate new meteors
        if (Random.Range(0, 100) < 50 && meteorRainCounter >= 15) // depend on 50% luck and 2.5min in (draw 2nd row)
        {
            for (int i = 1; i <= meteorCount; i++)
            {
                int xPositionMeteor = Random.Range(-15, 15); //randomize X position of Meteor
                float yPositionMeteor = Random.Range(6.5f, 8.5f);
                GameObject Meteor2 = Instantiate(Meteor, new Vector3(xPositionMeteor, yPositionMeteor, 0), Quaternion.identity);
                Meteor2.AddComponent<MeteorBehaviour>();
                Meteor2.GetComponent<Rigidbody>().mass = Random.Range(1f, 7f); // randomize mass

                int xForce, yForce;
                xForce = Random.Range(-2000, 2000);
                yForce = Random.Range(-500, 1000);
                Meteor2.GetComponent<Rigidbody>().AddForce(xForce, yForce, 0); //randomize Force

            }

            for (int i = 1; i <= Random.Range(5, 15); i++)
            {
                int xPositionMeteor = Random.Range(-15, 15); //randomize X position of Meteor in 2nd row
                float yPositionMeteor = Random.Range(4.5f, 6.5f);
                GameObject Meteor2 = Instantiate(Meteor, new Vector3(xPositionMeteor, yPositionMeteor, 0), Quaternion.identity);
                Meteor2.AddComponent<MeteorBehaviour>();
                Meteor2.GetComponent<Rigidbody>().mass = Random.Range(1f, 7f); // randomize mass

                int xForce, yForce;
                xForce = Random.Range(-1500, 1500);
                yForce = Random.Range(-500, 1250);
                Meteor2.GetComponent<Rigidbody>().AddForce(xForce, yForce, 0); //randomize Force

            }

            playerAudioSource.PlayOneShot(meteorRainStarts, 0.3f);

        }
        else
        {
            for (int i = 1; i <= meteorCount; i++)
            {
                int xPositionMeteor = Random.Range(-15, 15); //randomize X position of Meteor
                float yPositionMeteor = Random.Range(6.5f, 8.5f);
                GameObject Meteor2 = Instantiate(Meteor, new Vector3(xPositionMeteor, yPositionMeteor, 0), Quaternion.identity);
                Meteor2.AddComponent<MeteorBehaviour>();
                Meteor2.GetComponent<Rigidbody>().mass = Random.Range(1f, 7f); // randomize mass

                int xForce, yForce;
                xForce = Random.Range(-2000, 2000);
                yForce = Random.Range(-500, 1000);
                Meteor2.GetComponent<Rigidbody>().AddForce(xForce, yForce, 0); //randomize Force

            }
            playerAudioSource.PlayOneShot(meteorRainStarts, 0.3f);
        }
    }

}


