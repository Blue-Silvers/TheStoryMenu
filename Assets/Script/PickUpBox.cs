using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpBox : MonoBehaviour
{
    [Header("Don't touch it")]
    private Animator animator;
    [SerializeField] private PlayerMovement gunScript;

    float actualLife;
    [SerializeField] float maxLife;
    [SerializeField] float heal;
    [SerializeField] int enemyDamage;
    [SerializeField] int money;
    [SerializeField] int upgradePrice;
    [SerializeField] int healPrice;
    [SerializeField] int ammoPrice;
    [SerializeField] TextMeshProUGUI MoneyTxt, BobsTxt;
    float bobFontSizeStart;
    float bobFontSizeEvolve;
    [SerializeField] int ammoCharge;

    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject Shop, deathScreen;

    bool gameIsPaused;
    int nbUpgrade = 1;
    [SerializeField] TextMeshProUGUI nbUpgradeTxt;
    [SerializeField] GameObject upgradeButton, interactButton;

    GameObject[] Bobs;
    int nbOfBob;
    int nbOfDeath;

    int RdEscape;

    [Header("Input Manager (don't touch)")]
    public InputActionReference interact;
    bool interactOk = false;
    bool interactAction = false;

    private void Start()
    {
        money = 0;
        MoneyTxt.text = money.ToString();
        actualLife = maxLife;
        healthBar.SetMaxHealth(maxLife);
        animator = GetComponent<Animator>();

        Bobs = GameObject.FindGameObjectsWithTag("Bob");
        nbOfBob = Bobs.Length;
        nbOfDeath = Bobs.Length;
        BobsTxt.text = nbOfBob.ToString();
        bobFontSizeStart = BobsTxt.fontSize;
    }

    void Update()
    {
        if(BobsTxt.fontSize != bobFontSizeStart)
        {
            BobsTxt.fontSize = Mathf.SmoothStep(BobsTxt.fontSize, bobFontSizeStart, 0.05f);
            BobsTxt.color = new Color(Mathf.SmoothStep(BobsTxt.color.r, 1, 0.05f), Mathf.SmoothStep(BobsTxt.color.g, 1, 0.05f), Mathf.SmoothStep(BobsTxt.color.b, 1, 0.05f), 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            RdEscape = Random.Range(1, 10);
            if (RdEscape != 1)
            {
                actualLife -= enemyDamage;
                healthBar.SetHealth(actualLife);
                if (actualLife <= 0)
                {
                    Invoke("Death",1f);
                }
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (interactAction == true && (other.gameObject.tag == "Ammo"))
        {
            gunScript.Reload(ammoCharge);

            interactAction = false;

            Destroy(other.gameObject);
            Invoke("DontShowInteract", 0.1f);
        }

        if (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money" || other.gameObject.tag == "Upgrade")
        {
            interactOk = true;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money" || other.gameObject.tag == "Upgrade")
        {
            interactButton.SetActive(false);
            interactOk = false;
        }
        
    }

    void DontShowInteract()
    {
        interactButton.SetActive(false);
    }

    public void ActualisateNbOfBob()
    {
        Bobs = GameObject.FindGameObjectsWithTag("Bob");
        nbOfBob = Bobs.Length;
        if (nbOfBob < nbOfDeath)
        {
            nbOfDeath -= 1;
            money += 10;
            MoneyTxt.text = money.ToString();
            BobsTxt.text = nbOfBob.ToString();
            BobsTxt.fontSize = BobsTxt.fontSize + 30;
            BobsTxt.color = Color.red;
        }
    }


    private void OnEnable()
    {
        interact.action.started += Interaction;
    }
    private void Interaction(InputAction.CallbackContext obj)
    {
        if (interactOk == true)
        {
            interactAction = true;
        }
    }
}
