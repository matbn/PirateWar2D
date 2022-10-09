using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    public float damage;
    private void Start()
    {
        Destroy(gameObject, 1f); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SendMessage("TakeDamage", damage);
        GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        Destroy(hitEffect, 1.5f);
        Destroy(gameObject);
    }
}
