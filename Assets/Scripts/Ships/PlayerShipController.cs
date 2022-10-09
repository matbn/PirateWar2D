using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : ShipController
{
    public static readonly int PlayerLayer = 6;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float specialAttackCooldown;
    [SerializeField] private float cannonBallForce;
    [SerializeField] private Transform[] cannonBallPoints;
    [SerializeField] private GameObject cannonBallPrefab;
    private LaserAim[] aimRefs;
    private float currentAttackCooldown, currentSpecialAttackCooldown;

    protected override void Start()
    {
        base.Start();
        aimRefs = new LaserAim[cannonBallPoints.Length];
        for (int i = 0; i < cannonBallPoints.Length; i++)
        {
            aimRefs[i] = cannonBallPoints[i].GetComponentInChildren<LaserAim>();
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
        if(currentAttackCooldown <= 0)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallPoints[0].position, cannonBallPoints[0].rotation);
            cannonBall.layer = PlayerLayer;
            cannonBall.GetComponent<CannonBallBehaviour>().damage = damage;
            Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();
            cannonBallRB.AddForce(cannonBallPoints[0].right * cannonBallForce, ForceMode2D.Impulse);
            currentAttackCooldown = attackCooldown ;
        }
    }
    protected void SpecialAttack()
    {
        if (currentSpecialAttackCooldown <= 0)
        {
            foreach (Transform cannonBallPoint in cannonBallPoints)
            {
                GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallPoint.position, cannonBallPoint.rotation);
                cannonBall.layer = 6;
                cannonBall.GetComponent<CannonBallBehaviour>().damage = damage;
                Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();
                cannonBallRB.AddForce(cannonBallPoint.right * cannonBallForce, ForceMode2D.Impulse);
            }
            currentSpecialAttackCooldown = specialAttackCooldown;
        }
    }

    protected override void Die()
    {
        base.Die();
        GameSessionManager.instance.EndSession();
    }

    private void ShouldSpecialAim(bool shouldAim)
    {
        foreach (var aim in aimRefs)
            aim.SetShouldAim(shouldAim);
    }

    private void HandleInput()
    {
        shouldMove = (Input.GetAxisRaw("Vertical") > 0);
        rotateDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Fire1")) aimRefs[0].SetShouldAim(true); 
        if (Input.GetButtonUp("Fire1")) { aimRefs[0].SetShouldAim(false); Attack(); }
        if (Input.GetButtonDown("Fire2")) ShouldSpecialAim(true);
        if (Input.GetButtonUp("Fire2")) { ShouldSpecialAim(false); SpecialAttack(); }
    }
}
