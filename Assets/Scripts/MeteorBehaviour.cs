using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class MeteorBehaviour : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.name == "HelperBar(Clone)")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Zombie(Clone)")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }

        //if (collision.gameObject.name == "Wall_L")
        //{
        //    this.gameObject.GetComponent<Rigidbody>().AddForce(1500f, 0f, 0f);
        //}
        //if (collision.gameObject.name == "Wall_P")
        //{
        //    this.gameObject.GetComponent<Rigidbody>().AddForce(-1500f, 0f, 0f);
        //}




        if (collision.gameObject.name == "Player") //GAME LOST
        {
            GameObject.Find("EmptyObjectScriptManager").GetComponent<LoadEndScreen>().EndOfTheGame();
        }
    }

    private void Update()
    {
        if (this.gameObject.transform.position.y < -15f)
        {
            Destroy(this.gameObject);
        }
    }

}
