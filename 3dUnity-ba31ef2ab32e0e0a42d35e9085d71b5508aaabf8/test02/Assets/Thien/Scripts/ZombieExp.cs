using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieExp : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public float radius = 10f;
    public Vector3 originalePosition;
    public float maxDistance = 50f;
    public Health health;

    public Animator animator;
    public DamageZone damageZone;
    public GameObject hamburgerPrefab;

    public float dropChance = 100f;
    private bool hasDroppedBurger = false;
    public Slider healthBar;
    public Canvas healthBarCanvas;

    [Header("Explosion Options")]
    public Transform explosionPrefab; // Prefab v? n?
    public float explosionRadius = 12.5f; // Bán kính v? n?
    public float explosionForce = 4000.0f; // L?c n?

    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }
    public CharacterState currentState;

    private bool isAttacking = false; // Ki?m tra zombie có ?ang t?n công không

    void Start()
    {
        originalePosition = transform.position;
        if (healthBar != null)
        {
            healthBar.maxValue = health.maxHP;
            healthBar.value = health.currentHP;
        }
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.value = health.currentHP;
        }

        if (currentState == CharacterState.Die)
        {
            return;
        }

        if (health.currentHP <= 0)
        {

            ChangeState(CharacterState.Die);
            return;
        }

        if (isAttacking) return;

        var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        var distance = Vector3.Distance(target.position, transform.position);

        if (distance <= radius && distanceToOriginal <= maxDistance)
        {
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distance < 2f)
            {
                ChangeState(CharacterState.Attack);
            }
        }
        else
        {
            navMeshAgent.SetDestination(originalePosition);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distanceToOriginal < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            ChangeState(CharacterState.Normal);
        }
    }

    private void ChangeState(CharacterState newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                isAttacking = false;
                navMeshAgent.isStopped = false;
                break;

            case CharacterState.Attack:
                isAttacking = true;
                navMeshAgent.isStopped = true; // D?ng di chuy?n
                animator.SetTrigger("Attack");
                damageZone.BeginAttack();
                Invoke(nameof(ResumeMovement), 1.5f); // Ti?p t?c di chuy?n sau 1.5 giây
                break;

            case CharacterState.Die:
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Die");
                DropHamburger();
                Explode(); // Kích ho?t n?
                Destroy(gameObject, 0.1f); // Phá h?y zombie ngay l?p t?c sau n?
                break;
        }

        currentState = newState;
    }

    private void ResumeMovement()
    {
        isAttacking = false;
        navMeshAgent.isStopped = false;
        ChangeState(CharacterState.Normal);
    }

    private void DropHamburger()
    {
        if (!hasDroppedBurger)
        {
            Instantiate(hamburgerPrefab, transform.position, Quaternion.identity);
            hasDroppedBurger = true;
        }
    }

    private void Explode()
    {
        // Spawn the explosion prefab
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        // Apply explosion force to nearby objects
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            }

            // Apply damage to objects with Health component
            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(100); // Gây -100 máu cho ??i t??ng xung quanh
            }
        }
    }
}
