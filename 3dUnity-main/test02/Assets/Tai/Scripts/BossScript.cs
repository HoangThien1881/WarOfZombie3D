using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public float radius = 40f;
    public Vector3 originalePosition;
    public float maxDistance = 50f;
    public Health health;

    public Animator animator;
    public DamageZone damageZone;
    public GameObject hamburgerPrefab;
    public float dropChance = 100f;

    public GameObject rockPrefab; // Prefab viên đá
    public Transform throwPoint; // Vị trí xuất hiện viên đá (tay boss)
    public float throwSpeed = 10f; // Tốc độ bay của viên đá
    private int meleeAttackCount = 0; // Đếm số lần đánh gần
    private int maxMeleeAttacksBeforeThrow = 3; // Số lần đánh trước khi ném đá


    private bool hasDroppedBurger = false;
    public Slider healthBar;
    public Canvas healthBarCanvas;

    public enum CharacterState
    {
        Normal,
        Attack,
        ThrowRock, // Ném đá
        Die
    }
    public CharacterState currentState;

    private bool isAttacking = false; // Ki?m tra zombie c� ?ang t?n c�ng kh�ng

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
        // Cập nhật thanh máu
        if (healthBar != null)
        {
            healthBar.value = health.currentHP;
        }

        // Dừng nếu boss đã chết
        if (currentState == CharacterState.Die)
        {
            return;
        }

        // Chuyển sang trạng thái chết nếu máu <= 0
        if (health.currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
            return;
        }

        // Dừng các hành động nếu đang tấn công hoặc ném đá
        if (isAttacking || currentState == CharacterState.ThrowRock) return;

        // Tính toán khoảng cách
        var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        var distanceToTarget = Vector3.Distance(target.position, transform.position);

        // Nếu người chơi trong tầm phát hiện và boss không vượt quá phạm vi di chuyển tối đa
        if (distanceToTarget <= radius && distanceToOriginal <= maxDistance)
        {
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            // Khi người chơi ở khoảng cách gần
            if (distanceToTarget < 2f)
            {
                if (meleeAttackCount >= maxMeleeAttacksBeforeThrow)
                {
                    // Chuyển sang trạng thái ném đá
                    ChangeState(CharacterState.ThrowRock);
                }
                else
                {
                    // Đánh gần
                    ChangeState(CharacterState.Attack);
                }
            }
        }
        else
        {
            // Quay về vị trí ban đầu nếu người chơi rời khỏi tầm phát hiện
            navMeshAgent.SetDestination(originalePosition);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distanceToOriginal < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            // Chuyển về trạng thái bình thường
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

                meleeAttackCount++; // Tăng số lần đánh gần
                Debug.Log("Melee attack count: " + meleeAttackCount);

                // Nếu đã đủ số lần tấn công gần thì chuyển sang ném đá
                if (meleeAttackCount >= maxMeleeAttacksBeforeThrow)
                {
                    ChangeState(CharacterState.ThrowRock);
                }
                else
                {
                    Invoke(nameof(ResumeMovement), 1.5f); // Tiếp tục di chuyển sau 1.5 giây
                }
                break;

            case CharacterState.ThrowRock:
                navMeshAgent.isStopped = true; // Dừng di chuyển
                animator.SetTrigger("Throw"); // Kích hoạt animation ném đá
                Invoke(nameof(PerformThrow), 1f); // Ném đá sau một khoảng thời gian
                break;


            case CharacterState.Die:
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Die");
                DropHamburger();
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
        // Lo?i b? ?i?u ki?n ki?m tra t? l?, burger lu�n ???c t?o
        if (!hasDroppedBurger)
        {
            Instantiate(hamburgerPrefab, transform.position, Quaternion.identity);
            hasDroppedBurger = true;
        }
    }

    private void PerformThrow()
    {
        if (currentState == CharacterState.Die) return; // Không ném nếu boss đã chết

        // Tạo viên đá
        GameObject rock = Instantiate(rockPrefab, throwPoint.position, Quaternion.identity);

        // Hướng ném về phía người chơi
        Vector3 direction = (target.position - throwPoint.position).normalized;

        // Thêm lực vào viên đá
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * throwSpeed;
        }

        Debug.Log("Rock instantiated at " + throwPoint.position);
        Debug.Log("Direction: " + direction);


        // Sau khi ném, quay lại trạng thái Normal
        ChangeState(CharacterState.Normal);
        meleeAttackCount = 0; // Reset số lần đánh gần
    }
}
