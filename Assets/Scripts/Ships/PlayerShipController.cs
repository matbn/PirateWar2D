using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : ShipController
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float specialAttackCooldown;
    [SerializeField] private float cannonBallForce;
    [SerializeField] private Transform[] cannonBallPoints;
    [SerializeField] private GameObject cannonBallPrefab;
    private LaserAim[] aimRefs;
    private float currentAttackCooldown, currentSpecialAttackCooldown;

    private void Start()
    {
        for (int i = 0; i < cannonBallPoints.Length; i++)
        {
            aimRefs[i] = cannonBallPoints[i].GetComponent<LaserAim>();
        }
    }
    void Update()
    {
        HandleInput();
        if(currentAttackCooldown > 0)
            currentAttackCooldown -= Time.deltaTime;
        if(currentSpecialAttackCooldown > 0)
            currentSpecialAttackCooldown -= Time.deltaTime;
    }

    
    protected override void Attack()
    {
        if(currentAttackCooldown < 0)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallPoints[0].position, cannonBallPoints[0].rotation);
            Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();
            cannonBallRB.AddForce(cannonBallPoints[0].up * cannonBallForce, ForceMode2D.Impulse);
            currentAttackCooldown = attackCooldown ;
        }
    }
    protected void SpecialAttack()
    {
        if (currentSpecialAttackCooldown < 0)
        {
            foreach (Transform cannonBallPoint in cannonBallPoints)
            {
                GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallPoint.position, cannonBallPoint.rotation);
                Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();
                cannonBallRB.AddForce(cannonBallPoint.up * cannonBallForce, ForceMode2D.Impulse);
            }
            currentSpecialAttackCooldown = specialAttackCooldown;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    private void ShouldSpecialAim(bool shouldAim)
    {
        foreach (var aim in aimRefs)
            aim.SetShouldAim(shouldAim);
    }

    private void HandleInput()
    {
        if (Input.GetAxis("Vertical") > 0) shouldMove = true;
        rotateDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Fire1")) aimRefs[0].SetShouldAim(true); 
        if (Input.GetButtonUp("Fire1")) { aimRefs[0].SetShouldAim(false); Attack(); }
        if (Input.GetButtonDown("Fire2")) ShouldSpecialAim(true);
        if (Input.GetButtonUp("Fire2")) { ShouldSpecialAim(false); SpecialAttack(); }
    }
}
