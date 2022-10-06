using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        Destroy(hitEffect, 3f);
        Destroy(gameObject);
    }
}
