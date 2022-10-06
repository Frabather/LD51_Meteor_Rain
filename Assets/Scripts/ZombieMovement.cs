using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private int coinChance;

    private Transform Zombie;
    private Rigidbody ZombieRb;
    private float ZombieSpeed = 3f;
    private float zombieDirection;
    void Start()
    {
        Zombie = this.gameObject.GetComponent<Transform>();
        ZombieRb = this.gameObject.GetComponent<Rigidbody>();
        if (Zombie.position.x > 0)
        {
            zombieDirection = -1f;
        }
        else
        {
            zombieDirection = 1f;
        }
    }

    void Update()
    {
        coinChance = GameObject.Find("Player").GetComponent<PlayerMovement>().coinChance;
         ZombieRb.MovePosition(Zombie.position + new Vector3(zombieDirection, 0f, 0f) * Time.deltaTime * ZombieSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall_L" || collision.gameObject.name == "Wall_P" || collision.gameObject.name == "Zombie(Clone)")
        {
            zombieDirection = zombieDirection * -1f;
            if (Zombie.GetComponent<SpriteRenderer>().flipX)
            {
                Zombie.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                Zombie.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (collision.gameObject.tag == "Bullet")
        {
            int random = Random.Range(1, 11);
            if (random < coinChance)
            {
                GameObject.Find("EmptyObjectScriptManager").GetComponent<ShopLogic>().playerMoney++;
            }
        }
    }
}
