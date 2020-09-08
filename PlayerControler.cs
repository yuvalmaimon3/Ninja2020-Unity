using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private float currentManaAmount = 100f;
    private float manaAmount = 150f;
    private Animator playerAnimatror;
    private Health myHealth;
    private bool moveRight = false;
    private bool moveLeft = false;
    private bool isAlive = true;
    private bool jumpButton = true;
    private float jumpMagazine;
    private float startDashTime = 0.1f;
    private float dashSpeed = 50f;
    private float dashTime;
    private bool isDash = false;

    public SkinnedMeshRenderer playerSkin;
    public GameObject dashEffect;
    public PlayerAttack playerAttack;
    public float movementSpeed;
    public Image manaImage;
    public Vector3 Dir;
    public Rigidbody playerPhysic;
    public bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        dashTime = startDashTime;
        currentManaAmount = manaAmount;
        playerAnimatror = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        movementSpeed = 8f;
        // playerAttack = gameObject.transform.Find("FirePoint (1)").GetComponent<PlayerAttack>();

    }

    // Update is called once per frame
    void Update()
    {

        if (myHealth.isAlive)
        {
            if (moveRight = Input.GetKey("d")) { }
            else if (moveLeft = Input.GetKey("a")) { }
            else
            {
                playerAnimatror.SetBool("Go", false);
            }
            if (Input.GetKey("o"))
                jumpButton = true;
            else jumpButton = false;

        }

        Jump();
        Dash();
   

    }
    private void FixedUpdate()
    {

        if (moveRight)
            MoveRight();
        else if (moveLeft)
            MoveLeft();


    }
    private void MoveRight()
    {


        FlipCharacterSide("right");

        playerAnimatror.SetBool("Go", true);
        playerPhysic.transform.position += transform.forward * Time.deltaTime * movementSpeed;

    }

    private void MoveLeft()
    {



        FlipCharacterSide("left");
        playerAnimatror.SetBool("Go", true);
        playerPhysic.transform.position += transform.forward * Time.deltaTime * movementSpeed;



    }

    private void FlipCharacterSide(string side)
    {
        if (side == "right")
        {
            if (playerPhysic.transform.rotation.eulerAngles.y != 90)
            {
                //transform.localRotation = Quaternion.Euler(0, 90, 0);
                playerPhysic.transform.eulerAngles = new Vector3(0, 90, 0);

                //change to global/local rotation relative to unity north
            }
        }
        else if (side == "left")
        {
            if (playerPhysic.transform.rotation.eulerAngles.y != -90)
            {
                //  transform.localRotation = Quaternion.Euler(0, -90, 0);      //change to global/local rotation relative to unity north
                playerPhysic.transform.eulerAngles = new Vector3(0, -90, 0);

            }

        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown("w"))
        {
            if (jumpMagazine > 0)
            {
                playerPhysic.AddForce(new Vector3(0, 7f, 0), ForceMode.Impulse);
                playerAnimatror.SetBool("Jump", true);
            }
            jumpMagazine -= 1;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && (moveRight || moveLeft))
        {
            if (currentManaAmount > 0)
            {
                currentManaAmount -=30f; 
                manaImage.fillAmount = currentManaAmount / manaAmount;
            
            isDash = true;
            Vector3 pos = new Vector3(0, 1.5f, 1);
            Instantiate(dashEffect, transform.position + pos, Quaternion.identity);
            }
        }
        if (isDash)
        {
            if (dashTime <= 0)
            {
                playerPhysic.velocity = Vector3.zero;
                dashTime = startDashTime;
                isDash = false;
                playerSkin.enabled = true;
            }
            else
            if (moveRight)
            {
                playerPhysic.velocity = Vector3.right * dashSpeed;
                dashTime -= Time.deltaTime;
                playerSkin.enabled = false;
            }
            else if (moveLeft)
            {
                playerPhysic.velocity = Vector3.left * dashSpeed;
                playerSkin.enabled = false;
            }

            dashTime -= Time.deltaTime;
        }
        else if (!isDash)
        {
            if (isGrounded)
            {
                if (currentManaAmount < manaAmount)
                    currentManaAmount += 10 * Time.fixedDeltaTime;
                    manaImage.fillAmount = currentManaAmount / manaAmount;

            }
        }
   
    }

    private void OnTriggerExit(Collider other)
    {
        playerAnimatror.SetBool("Jump", true);
        isGrounded = false;
        movementSpeed = 3.5f;
    }
    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
        jumpMagazine = 1;
        movementSpeed = 11f;
        playerAnimatror.SetBool("Jump", false);
    }

}

