using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    //shop modifiers
    public float playerSpeed;
    public float jumpPower;
    public GameObject[] playerWeapon;
    public float bulletSpeed;
    public float fireRate;
    public int coinChance;
    public GameObject currentWeapon;
    public bool isShopping = false;


    private Rigidbody rb;
    private GameObject HelperBar;
    private GameObject BulletToShoot;
    private GameObject Bullet;
    private GameObject Arrow;
    private Transform PlayerTransform;
    private GameObject Player;
    private bool isGrounded;
    private bool isHelperBarPlacable = false;
    private float timeToShoot;
    [SerializeField]
    private int collectedCoins = 0;
    private Text isBuildingAvailable;


    public AudioSource audioSource;
    [SerializeField]
    private AudioClip gunShot;
    [SerializeField]
    private AudioClip bowShot;
    public AudioClip meteorRain;


    void Start()
    {

        coinChance = 5;
        bulletSpeed = 15f;
        fireRate = 1.5f;
        jumpPower = 1f;

        currentWeapon = GameObject.Find("bow");
        playerWeapon = new GameObject[3];
        playerWeapon[0] = GameObject.Find("bow");
        playerWeapon[1] = GameObject.Find("pistol");
        playerWeapon[2] = GameObject.Find("rifle");

        playerWeapon[1].SetActive(false);
        playerWeapon[2].SetActive(false);

        timeToShoot = Time.time;
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerSpeed = 7.5f;
        isGrounded = false;
        HelperBar = GameObject.Find("HelperBar");
        isBuildingAvailable = GameObject.Find("IsBuildingAvailable").GetComponent<Text>();
        Bullet = GameObject.Find("Bullet");
        Arrow = GameObject.Find("Arrow");
        Player = GameObject.Find("Player");
        PlayerTransform = Player.GetComponent<Transform>();


        
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = true;
        }

        if (collision.gameObject.name == "Zombie(Clone)") //GAME LOST
        {
            GameObject.Find("EmptyObjectScriptManager").GetComponent<LoadEndScreen>().EndOfTheGame();
        }

        if (collision.gameObject.name == "Wall_L")
        {
            Player.GetComponent<Rigidbody>().AddForce(35000f * Time.fixedDeltaTime, 0f, 0f);
        }

        if (collision.gameObject.name == "Wall_P")
        {
            Player.GetComponent<Rigidbody>().AddForce(-35000f * Time.fixedDeltaTime, 0f, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        collectedCoins++;
        Destroy(GameObject.Find("Coin(Clone)"));
    }


    void FixedUpdate()
    {
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            {
                Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                rb.MovePosition(transform.position + playerInput * Time.deltaTime * playerSpeed);

                if (playerInput.x > 0)
                {
                    Player.GetComponent<SpriteRenderer>().flipX = false;
                    if (currentWeapon.name == "bow")
                    {
                        currentWeapon.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        currentWeapon.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    currentWeapon.GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x + 0.5f, currentWeapon.GetComponent<Transform>().position.y, currentWeapon.GetComponent<Transform>().position.z);
                }
                else
                {
                    Player.GetComponent<SpriteRenderer>().flipX = true;
                    if (currentWeapon.name == "bow")
                    {
                        currentWeapon.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        currentWeapon.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    currentWeapon.GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x - 0.5f, currentWeapon.GetComponent<Transform>().position.y, currentWeapon.GetComponent<Transform>().position.z);
                }
            }
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(0, 2500f * Time.deltaTime * playerSpeed * jumpPower, 0);
            isGrounded = false;
        }
    }

    private void Update()
    {
        GameObject.Find("Progress Bar").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, 138);


        if (PlayerTransform.position.y <= -10) //GAME LOST
        {
            {
                GameObject.Find("EmptyObjectScriptManager").GetComponent<LoadEndScreen>().EndOfTheGame();
            }
        }

        if (isHelperBarPlacable) //UI texts to display if you can build
        {
            if (collectedCoins == 1)
            {
                isBuildingAvailable.text = collectedCoins.ToString() + " block left";
                isBuildingAvailable.color = Color.green;
            }
            else
            {
                isBuildingAvailable.text = collectedCoins.ToString() + " blocks left";
                isBuildingAvailable.color = Color.green;
            }
        }
        else
        {
            isBuildingAvailable.text = collectedCoins.ToString() + " block(s) left. Find a Magic Coin!";
            isBuildingAvailable.color = Color.red;
        }



        if (collectedCoins > 0) //check if you can build
        {
            isHelperBarPlacable = true;
        }
        else
        {
            isHelperBarPlacable = false;
        }

        if (Input.GetMouseButtonDown(0) && isHelperBarPlacable && !isShopping)
        {
            collectedCoins--;

            Vector3 mousePosition = Input.mousePosition;
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            objectPosition = new Vector3(objectPosition.x, objectPosition.y, 0);
            Instantiate(HelperBar, objectPosition, Quaternion.identity);
        }

        if (Input.GetMouseButton(1)) //shooting
        {
            if (playerWeapon[0].active)
            {
                BulletToShoot = Arrow;
            }
            else
            {
                BulletToShoot = Bullet;
            }


            if (Time.time >= timeToShoot)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                if (objectPosition.x > 0)
                {
                    GameObject Bullet2 = Instantiate(BulletToShoot, new Vector3(PlayerTransform.position.x + 2, PlayerTransform.position.y, PlayerTransform.position.z), Quaternion.identity);
                    Bullet2.AddComponent<BulletBehaviour>();
                    Bullet2.GetComponent<BulletBehaviour>().isShootingLeft = false;
                    Bullet2.tag = "Bullet";
                    Bullet2.GetComponent<Rigidbody>().velocity = new Vector3(bulletSpeed, 0f, 0f);
                    Bullet2.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GameObject Bullet2 = Instantiate(BulletToShoot, new Vector3(PlayerTransform.position.x - 2, PlayerTransform.position.y, PlayerTransform.position.z), Quaternion.identity);
                    Bullet2.AddComponent<BulletBehaviour>();
                    Bullet2.GetComponent<BulletBehaviour>().isShootingLeft = true;
                    Bullet2.tag = "Bullet";
                    Bullet2.GetComponent<Rigidbody>().velocity = new Vector3(-bulletSpeed, 0f, 0f);
                    Bullet2.GetComponent<SpriteRenderer>().flipX = false;
                }

                if (currentWeapon.name == "bow")
                {
                    audioSource.pitch = 1f;
                    audioSource.PlayOneShot(bowShot, 0.3f);
                }

                if (currentWeapon.name == "pistol")
                {
                    audioSource.pitch = 1.15f;
                    audioSource.PlayOneShot(gunShot, 0.3f);
                }

                if (currentWeapon.name == "rifle")
                {
                    audioSource.pitch = 2f;
                    audioSource.PlayOneShot(gunShot, 0.3f);
                }

                timeToShoot = Time.time + fireRate;
            }

        }
    }

}
