using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStartShuriken : MonoBehaviour
{
    public float Speed = 7;
    public float Dmg;
    Rigidbody projectailRB;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, 6);
        projectailRB = GetComponent<Rigidbody>();
        Dmg = 30;
    }

    private void FixedUpdate()
    {
        projectailRB.velocity = transform.forward * Speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.Find("Blood").gameObject.SetActive(true);
            transform.Find("Blood").parent = other.transform;
            Health player_hp = other.GetComponent<Health>();
            player_hp.Damage(Dmg);
            Destroy(gameObject);

        }
    }
}
