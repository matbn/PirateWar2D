using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.GridBrushBase;

public class EnemyShooterController : ShipController
{
    public static readonly int EnemyLayer = 7;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float cannonBallForce;
    [SerializeField] private Transform cannonBallPoint;
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private float attackDistance = 8f;
    private NavMeshAgent navMeshAgent;
    private float currentAttackCooldown;
    private Transform target;
    private Vector2 rotationDirection;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null)
        {
            if (currentAttackCooldown > 0)
                currentAttackCooldown -= Time.deltaTime;
            if (Vector2.Distance(transform.position, target.position) > attackDistance)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
    }
    protected override void Attack()
    {
        if (currentAttackCooldown <= 0)
        {
            LookAtTarget();
            GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallPoint.position, cannonBallPoint.rotation);
            cannonBall.layer = EnemyLayer;
            cannonBall.GetComponent<CannonBallBehaviour>().damage = damage;
            Rigidbody2D cannonBallRB = cannonBall.GetComponent<Rigidbody2D>();
            cannonBallRB.AddForce(cannonBallPoint.right * cannonBallForce, ForceMode2D.Impulse);
            currentAttackCooldown = attackCooldown;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    private void Move()
    {
        navMeshAgent.SetDestination(target.position);
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        rotationDirection = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, -rotationDirection);
    }
}
