using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAttack : MonoBehaviour
{
    private bool hitReload = true;

    private void OnTriggerStay(Collider other)
    {
        if(hitReload)
            if(other.gameObject.CompareTag("Player"))
            {
                hitReload = false;
                StartCoroutine(HitMe(other));
            }
    }


    private IEnumerator HitMe(Collider other)
    {
        for (int i = 0; i < 1; i++)
        {
            
            Health player_health = other.gameObject.GetComponent<Health>();
            player_health.Damage(15);
            yield return new WaitForSeconds(1f);
            hitReload = true;
        }
    }

}
