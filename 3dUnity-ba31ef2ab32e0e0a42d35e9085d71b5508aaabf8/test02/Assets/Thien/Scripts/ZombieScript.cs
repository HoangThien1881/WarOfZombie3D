using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // Thêm dòng này n?u c?n Slider

public class ZombieScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public float radius = 10f;
    public Vector3 originalePosition;
    public float maxDistance = 50f;
    public Health health;
    public PlayerExp playerExp; // Thêm tham chi?u t?i PlayerExp
    public Animator animator;
    public DamageZone damageZone;
    public GameObject hamburgerPrefab;
    public float dropChance = 100f;

    private bool hasDroppedBurger = false;
    public Slider healthBar; // Slider ?? hi?n th? HP
    public Canvas healthBarCanvas;

    // Thêm bi?n ?? l?u AudioClip và AudioSource
    public AudioClip attackSound; // Âm thanh khi t?n công
    public AudioClip deathSound;  // Âm thanh khi ch?t
    private AudioSource audioSource; // AudioSource ?? phát âm thanh

    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }
    public CharacterState currentState;

    private bool isAttacking = false;

    void Start()
    {
        originalePosition = transform.position;

        // Kh?i t?o AudioSource
        audioSource = GetComponent<AudioSource>();

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
            playerExp.AddExp(15f); // C?ng EXP cho ng??i ch?i khi zombie ch?t
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

                // Phát âm thanh t?n công
                if (attackSound != null)
                {
                    audioSource.PlayOneShot(attackSound); // Phát âm thanh t?n công
                }

                Invoke(nameof(ResumeMovement), 1.5f); // Ti?p t?c di chuy?n sau 1.5 giây
                break;

            case CharacterState.Die:
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Die");

                // Phát âm thanh khi ch?t
                if (deathSound != null)
                {
                    audioSource.PlayOneShot(deathSound); // Phát âm thanh ch?t
                }

                DropHamburger();
                Destroy(gameObject, 3f); // Hu? zombie sau khi ch?t
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
}
