using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLogic : MonoBehaviour
{
    public int playerMoney = 0;
    private GameObject shopButton;
    private Button shopComponent;
    private string[] shopItems;
    public Sprite[] shopItemsSprites;
    private Image shopItem1;
    private Image shopItem2;
    private Image shopItem3;
    int[] randomItems;
    

    private GameObject buy1Button;
    private Button buy1Component;
    private GameObject buy2Button;
    private Button buy2Component;
    private GameObject buy3Button;
    private Button buy3Component;
    private GameObject cancelButton;
    private Button cancelComponent;
    private Text ShopCoins;

    private GameObject ShopMenu;

    void Start()
    {
        ShopMenu = GameObject.Find("ShopMenu");

        shopItem1 = GameObject.Find("shopItem1").GetComponent<Image>();
        shopItem2 = GameObject.Find("shopItem2").GetComponent<Image>();
        shopItem3 = GameObject.Find("shopItem3").GetComponent<Image>();

        shopButton = GameObject.Find("ShopIcon");
        shopComponent = shopButton.GetComponent<Button>();
        shopComponent.onClick.AddListener(Shopping);

        ShopCoins = GameObject.Find("ShopCoins").GetComponent<Text>();

        randomItems = new int[3];

        shopItems = new string[8]; //list of items in shop
        shopItems[0] = "Jump Power +1";
        shopItems[1] = "Jump Power -1";
        shopItems[2] = "Player Speed +1";
        shopItems[3] = "Player Speed -1";
        shopItems[4] = "Buy a Rifle";
        shopItems[5] = "Buy a Pistol";
        shopItems[6] = "Increase chance of money dropped";
        shopItems[7] = "FireRate +1";


        buy1Button = GameObject.Find("Buy");
        buy1Component = buy1Button.GetComponent<Button>();
        buy1Component.onClick.AddListener(Buy1st);

        buy2Button = GameObject.Find("Buy2");
        buy2Component = buy2Button.GetComponent<Button>();
        buy2Component.onClick.AddListener(Buy2nd);

        buy3Button = GameObject.Find("Buy3");
        buy3Component = buy3Button.GetComponent<Button>();
        buy3Component.onClick.AddListener(Buy3rd);

        cancelButton = GameObject.Find("Cancel");
        cancelComponent = cancelButton.GetComponent<Button>();
        cancelComponent.onClick.AddListener(CancelBuy);

        ShopMenu.SetActive(false);
    }

    void Update()
    {
        if (playerMoney < 10) //POPRAW NA DOBRA WARTOSC KLOCKU
        {
            shopButton.SetActive(false);
        }
        else
        {
            shopButton.SetActive(true);
        }

        if (Input.GetKeyDown("b") && !GameObject.Find("Player").GetComponent<PlayerMovement>().isShopping && shopButton.active)
        {
            Shopping();
        }
        ShopCoins.text = "$ " + playerMoney.ToString();
    }

    void Shopping()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().isShopping = true;


        ShopMenu.SetActive(true);
        Time.timeScale = 0f;

        Debug.Log("len " + (shopItems.Length));

        for (int i = 0; i < 3; i++) //randomize item to buy
        {
            int random = Random.Range(0, shopItems.Length);
            while (randomItems[0] == random || randomItems[1] == random || randomItems[2] == random)
            {
                random = Random.Range(0, shopItems.Length);
            }
            randomItems[i] = random;

        }

        for (int i = 0; i < 3; i++) //check items id
        {
            Debug.Log(randomItems[i]);
        }

        //show those items in shop
        shopItem1.sprite = shopItemsSprites[randomItems[0]];
        shopItem2.sprite = shopItemsSprites[randomItems[1]];
        shopItem3.sprite = shopItemsSprites[randomItems[2]];
    }


    void Buy1st()
    {
        Buy(0);
    }

    void Buy2nd()
    {
        Buy(1);
    }
    void Buy3rd()
    {
        Buy(2);
    }

    void CancelBuy()
    {
        ShopMenu.SetActive(false);
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerMovement>().isShopping = false;
    }

    void Buy(int choose)
    {
        //Debug.Log(shopItems[randomItems[choose]]);

        

        switch (randomItems[choose])
        {
            case 0: //"Jump Power +1"
                if (playerMoney >= 10)
                {
                    GameObject.Find("Player").GetComponent<PlayerMovement>().jumpPower = GameObject.Find("Player").GetComponent<PlayerMovement>().jumpPower + 0.2f;
                    playerMoney -= 10;
                    Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                }
                break;


            case 1: //"Jump Power -1"
                if (playerMoney >= 10)
                {
                    GameObject.Find("Player").GetComponent<PlayerMovement>().jumpPower = GameObject.Find("Player").GetComponent<PlayerMovement>().jumpPower - 0.2f;
                    playerMoney -= 10;
                    Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                }
                break;


            case 2: //"Player Speed +1"
                if (playerMoney >= 10)
                {
                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed = GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed + 0.2f;
                    playerMoney -= 10;
                    Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                }
                break;


            case 3: //"Player Speed -1"
                if (playerMoney >= 10)
                {
                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed = GameObject.Find("Player").GetComponent<PlayerMovement>().playerSpeed - 0.2f;
                    playerMoney -= 10;
                    Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                }
                break;


            case 4: //"Buy a Rifle"
                if(playerMoney >= 75)
                {
                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerWeapon[0].SetActive(false); //disable bow
                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerWeapon[1].SetActive(false); //disable pistol

                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerWeapon[2].SetActive(true); // enable rifle
                    GameObject.Find("Player").GetComponent<PlayerMovement>().currentWeapon = GameObject.Find("rifle");
                    GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed = 50f;
                    GameObject.Find("Player").GetComponent<PlayerMovement>().fireRate = 0.5f;
                    playerMoney -= 75;
                    Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                }
                break;


            case 5: //"Buy a Pistol"
                if(playerMoney >= 30)
                {
                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerWeapon[0].SetActive(false); //disable bow
                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerWeapon[2].SetActive(false); //disable rifle

                    GameObject.Find("Player").GetComponent<PlayerMovement>().playerWeapon[1].SetActive(true); // enable pistol
                    GameObject.Find("Player").GetComponent<PlayerMovement>().currentWeapon = GameObject.Find("pistol");
                    GameObject.Find("Player").GetComponent<PlayerMovement>().bulletSpeed = 30f;
                    GameObject.Find("Player").GetComponent<PlayerMovement>().fireRate = 1f;
                    playerMoney -= 30;
                    Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                }
                break;


            case 6: //"Increase chance of money dropped"
                if (playerMoney >= 10)
                {
                    if (GameObject.Find("Player").GetComponent<PlayerMovement>().coinChance <= 10);
                    {
                        GameObject.Find("Player").GetComponent<PlayerMovement>().coinChance++;
                        playerMoney -= 10;
                        Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                    }
                }
                break;

            case 7: //"Fire Rate +1"
                if (playerMoney >= 20)
                {
                    if (GameObject.Find("Player").GetComponent<PlayerMovement>().fireRate - 0.1 < 0) ;
                    {
                        GameObject.Find("Player").GetComponent<PlayerMovement>().fireRate = GameObject.Find("Player").GetComponent<PlayerMovement>().fireRate - 0.1f;
                        playerMoney -= 20;
                        Debug.Log("Successfully bought: " + shopItems[randomItems[choose]]);
                    }
                }
                break;


            default:
                Debug.Log("Expect... the unexpected...");
                break;
        }


        ShopMenu.SetActive(false);
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerMovement>().isShopping = false;
    }


}
