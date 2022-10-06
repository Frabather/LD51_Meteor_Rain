using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{

    private GameObject Zombie;
    private float timeRemaining, howManySecondsDelay = 1;
    private int xPositionZombie;


    void Start()
    {
        Zombie = GameObject.Find("Zombie");
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = Random.Range(0.75f, 3f);
            ZombieSpawner();
        }
    }

    void ZombieSpawner()
    {
        if (Random.Range(1, 3) <= 1)
        {
            xPositionZombie = -15;
            Zombie.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            xPositionZombie = 15;
            Zombie.GetComponent<SpriteRenderer>().flipX = true;
        }

        GameObject Zombie2 = Instantiate(Zombie, new Vector3(xPositionZombie, -8, 0), Quaternion.identity);
        Zombie2.AddComponent<ZombieMovement>();
        Zombie2.tag = "Zombie";
    }
}
