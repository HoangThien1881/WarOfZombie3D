using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour
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

    public AudioSource audioSource; // Thêm biến cho AudioSource
    public AudioClip idleSound; // Thêm biến cho âm thanh idle
    private bool isPlayingIdleSound = false; // Trạng thái phát âm thanh idle

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
        if (healthBar != null)
        {
            healthBar.maxValue = health.maxHP;
            healthBar.value = health.currentHP;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
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

            StopIdleSound();

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
                PlayIdleSound();
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
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Attack");
                damageZone.BeginAttack();
                StopIdleSound();
                Invoke(nameof(ResumeMovement), 1.5f);
                break;

            case CharacterState.Die:
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Die");
                DropHamburger();
                StopIdleSound();
                Destroy(gameObject, 3f);
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

    private void PlayIdleSound()
    {
        if (!isPlayingIdleSound && idleSound != null && audioSource != null)
        {
            audioSource.clip = idleSound;
            audioSource.loop = true;
            audioSource.Play();
            isPlayingIdleSound = true;
        }
    }

    private void StopIdleSound()
    {
        if (isPlayingIdleSound && audioSource != null)
        {
            audioSource.Stop();
            isPlayingIdleSound = false;
        }
    }
}
