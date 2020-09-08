using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwardDamage : MonoBehaviour
{
    float Dmg = 50f;

    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            enemy.GetComponent<EnemyHealth>().Damaged(Dmg);
        }
    }
}
