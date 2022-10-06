using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float rotation;
    public bool isShootingLeft;
    private GameObject zombieDeath;

    private void Awake()
    {
        zombieDeath = GameObject.Find("ZombieDeath");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Zombie(Clone)")
        {
            Instantiate(zombieDeath, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Wall_L" || collision.gameObject.name == "Wall_P" || collision.gameObject.name == "HelperBar")
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (isShootingLeft)
        {
            if (this.gameObject.GetComponent<Rigidbody>().velocity.x != GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed * (-1f))
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed * (-1f), 0, 0);
            }
        }
        else
        {
            if (this.gameObject.GetComponent<Rigidbody>().velocity.x != GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed, 0, 0);
            }
        }

        if (this.gameObject.name == "Arrow")
        {
            rotation += 50f;
            Quaternion eulerRotation = Quaternion.Euler(rotation, 0f, 0f);
            this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, eulerRotation, Time.deltaTime * 25f);

        }


        if (this.gameObject.GetComponent<Rigidbody>().velocity.x != GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed)
        {
            //this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed, 0, 0);
        }
    }
}
