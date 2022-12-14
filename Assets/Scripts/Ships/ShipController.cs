using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipController : MonoBehaviour
{
    protected Rigidbody2D shipRigidBody;
    protected SpriteRenderer shipSpriteRenderer;
    protected float rotateDirection;
    protected bool shouldMove = false;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float health;
    [SerializeField] protected Sprite healthShip;
    [SerializeField] protected Sprite damagedShip;
    [SerializeField] protected Sprite heavyDamageShip;
    [SerializeField] protected GameObject deathExplosionPrefab;
    [SerializeField] private HealthBar healthBar;
    private float startHealth;
    protected Animator animator;
    protected virtual void Awake()
    {
        shipRigidBody = GetComponent<Rigidbody2D>();
        shipSpriteRenderer = GetComponent<SpriteRenderer>();
        startHealth = health;
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        healthBar.SetupSlider(startHealth);
    }
    protected virtual void MoveForward() {
        shipRigidBody.MovePosition((Vector2)(transform.position + (-transform.up * speed * Time.fixedDeltaTime)));
    }
    protected virtual void Rotate(float rotation)
    {
        shipRigidBody.rotation += rotation * rotationSpeed * Time.fixedDeltaTime;
    }

    protected virtual void ChangeSprite(Sprite newSprite)
    {
        shipSpriteRenderer.sprite = newSprite;
    }
    protected virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        animator.SetTrigger("TakingDamageT");
        if (health <= 0)
        {
            Die();
        }else if(health < startHealth * 0.33f)
        {
            ChangeSprite(heavyDamageShip);
        }else if(health < startHealth * 0.66f)
        {
            ChangeSprite(damagedShip);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (shouldMove)
        {
            MoveForward();
        }
        Rotate(rotateDirection);
    }
    protected abstract void Attack();
    protected virtual void Die()
    {
        GameObject deathEffect = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, 1.5f);
        Destroy(gameObject);
    }
}
