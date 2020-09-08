using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Projectail;
    public Animator playerAnimator;
    public Transform playerSward;
    public BoxCollider swardHitDetector;
    public float attckSpeed = 2f;
    public Transform playerHandTranform;
    public Transform hipsTransform;
    public AudioClip throwShurikenSound;
    public AudioClip swardAttackSound;
    public bool isAttack;

    private Health playerHealth;
    private bool isGrounded = true;
    private AudioSource playerAudioSource;

    void Start()
    {
        isAttack = false;
        playerAudioSource = GetComponent<AudioSource>();
        playerHealth = transform.parent.GetComponent<Health>();
        
    }

    void Update()
    {
        isGrounded = transform.parent.GetComponent<PlayerControler>().isGrounded;
        if (Input.GetKey("space"))
            StartCoroutine(Attacking());
        if (Input.GetKey(KeyCode.LeftControl))
            StartCoroutine(PlayerSwardAttack());
    }

    //Attack terms order soft by the less existing term to the most existing term. In order to save ineffective checks

    private IEnumerator Attacking()
    {

        if (isAttack == false)
            if (playerHealth.isAlive)
            {
                isAttack = true;
                playerAnimator.SetTrigger("Attack");
                yield return new WaitForSeconds(0.2f);
                playerAudioSource.clip = throwShurikenSound;
                playerAudioSource.Play();
                if (transform.parent.rotation.y > 0.70)
                    Instantiate(Projectail, transform.position, Quaternion.Euler(90, 0, -90));
                else
                    Instantiate(Projectail, transform.position, Quaternion.Euler(90, 0, 90));
                yield return new WaitForSeconds(attckSpeed);

                isAttack = false;
            }

    } 

    private IEnumerator PlayerSwardAttack()
    {

        if (isAttack == false)
                if (playerHealth.isAlive)
            {
                playerAnimator.SetBool("IsAttack", true);
                isAttack = true;
                playerSward.parent = playerHandTranform;
                playerSward.localPosition = new Vector3(10.7f, 3.2f, 1.7f);
                playerAnimator.SetTrigger("SwardAttack");
                playerSward.localRotation = Quaternion.Euler(new Vector3(-164f, -91.43f, 86.5f));
                yield return new WaitForSeconds(0.05f);
                playerAudioSource.clip = swardAttackSound;
                playerAudioSource.Play();
                yield return new WaitForSeconds(0.2f);
                swardHitDetector.enabled = true; //The sward dosent collid or hit the enemy. It just simulate.
                yield return new WaitForSeconds(0.1f);
                swardHitDetector.enabled = false;
                playerSward.parent = hipsTransform;
                playerSward.localPosition = new Vector3(-17.4f, 17.4f, -17.2f);
                playerSward.localRotation = Quaternion.Euler(new Vector3(216.45f, 272.48f, 4.6f));
                isAttack = false;
                playerAnimator.SetBool("IsAttack", false);
            }

    }



    
}
