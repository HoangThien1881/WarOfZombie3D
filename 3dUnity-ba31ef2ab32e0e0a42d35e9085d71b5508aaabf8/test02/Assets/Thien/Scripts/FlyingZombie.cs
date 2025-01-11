using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
public class FlyingZombie : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public float radius = 10f; // Phạm vi để zombie phát hiện player
    public float descendHeight = 1.5f; // Độ cao mà zombie hạ xuống gần player
    public Vector3 originalePosition;
    public float maxDistance = 50f; // Khoảng cách tối đa zombie có thể bay xa khỏi vị trí ban đầu
    public Health health;
    public PlayerExp playerExp;
    // Xóa Animator và các tham chiếu liên quan đến animation
    // public Animator animator; 

    public DamageZone damageZone;
    public GameObject hamburgerPrefab;
    public float dropChance = 100f;

    private bool hasDroppedBurger = false;
    public Slider healthBar;
    public Canvas healthBarCanvas;

    public float throwInterval = 5f; // Thời gian giữa các lần thả cục đá
    private float nextThrowTime = 0f; // Thời gian tiếp theo để thả cục đá

    private float floatingSpeed = 2f; // Tốc độ bay lên xuống
    public float floatingHeight = 1f; // Biên độ của hiệu ứng bay lên xuống
    public float originalYPosition; // Lưu vị trí Y ban đầu của zombie
    public enum CharacterState
    {
        Normal,
        FlyToTarget,
        Attack,
        Die
    }
    public CharacterState currentState;

    private bool isDescending = false; // Kiểm tra trạng thái hạ xuống
    private bool isAttacking = false; // Kiểm tra zombie có đang tấn công không

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
            playerExp.AddExp(15f);
            Destroy(gameObject);
            return;
        }
        if (isAttacking) return;
        HandleFloatingEffect();

        float distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= radius && distanceToOriginal <= maxDistance)
        {
            FlyTowardsTarget(distanceToTarget);
        }
        else
        {
            FlyBackToOriginalPosition(distanceToOriginal);
        }
    }

    private void FlyTowardsTarget(float distanceToTarget)
    {
        navMeshAgent.SetDestination(target.position);

        if (distanceToTarget <= 2f && !isDescending)
        {
            // Khi đến gần player, zombie sẽ hạ xuống
            StartDescending();
        }
        else if (isDescending && distanceToTarget > 2f)
        {
            // Nếu player rời xa, zombie sẽ dừng hạ xuống
            StopDescending();
        }
    }

    private void FlyBackToOriginalPosition(float distanceToOriginal)
    {
        navMeshAgent.SetDestination(originalePosition);

        StopDescending();
        ChangeState(CharacterState.Normal);
    }

    private void StartDescending()
    {
        isDescending = true;
        Vector3 descendPosition = new Vector3(transform.position.x, descendHeight, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, descendPosition, Time.deltaTime * 2);
    }

    private void StopDescending()
    {
        isDescending = false;
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
                damageZone.BeginAttack();
                Invoke(nameof(ResumeMovement), 1.5f); // Tiếp tục di chuyển sau 1.5 giây
                break;


        }

        currentState = newState;
    }
    private void HandleFloatingEffect()
    {
        // Tạo hiệu ứng bay lên xuống
        float newYPosition = originalYPosition + Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
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