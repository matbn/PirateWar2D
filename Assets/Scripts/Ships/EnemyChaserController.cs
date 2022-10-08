using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserController : ShipController
{
    [SerializeField] private float attackDistance = 10f;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform target;
    private Vector2 rotationDirection;
    private bool isAttacking;
    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null)
        {
            Move();
            if (Vector2.Distance(transform.position, target.position) > attackDistance)
            {
                if (isAttacking) StopAttacking();
            }
            else
            {
                if (!isAttacking) Attack();
            }
        }

    }
    protected override void Attack()
    {
        navMeshAgent.speed *= 2;
        isAttacking = true;
        animator.SetBool("Attacking", true);
    }

    private void StopAttacking()
    {
        navMeshAgent.speed /= 2;
        isAttacking=false;
        animator.SetBool("Attacking", false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
