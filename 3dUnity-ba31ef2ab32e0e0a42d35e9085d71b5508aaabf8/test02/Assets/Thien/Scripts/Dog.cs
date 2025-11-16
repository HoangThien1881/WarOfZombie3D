using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;  // ?i?u khi?n di chuy?n
    public Transform player;            // Ng??i ch?i
    public Animator animator;           // Animator ?? ?i?u khi?n animation

    public float detectionRadius = 10f; // Bán kính phát hi?n zombie
    public float attackRadius = 2f;     // Bán kính t?n công zombie
    public float attackDelay = 1f;      // Th?i gian gi?a các ?òn t?n công

    private Transform targetZombie;     // Zombie mà con chó ?ang t?n công
    private bool isAttacking = false;   // Ki?m tra xem con chó có ?ang t?n công không

    void Update()
    {
        // Ki?m tra n?u zombie ?ã b? tiêu di?t, và n?u ?ã thì reset targetZombie
        if (targetZombie != null && targetZombie.GetComponent<Health>().currentHP <= 0)
        {
            targetZombie = null;
        }

        // N?u con chó ?ang t?n công, hãy ti?p t?c t?n công
        if (isAttacking && targetZombie != null)
        {
            AttackZombie();
        }
        else
        {
            // Tìm zombie g?n nh?t n?u không có m?c tiêu
            FindZombie();
        }

        // N?u không t?n công và không có zombie ?? t?n công, quay l?i v?i ng??i ch?i
        if (!isAttacking && targetZombie == null)
        {
            navMeshAgent.SetDestination(player.position);  // Quay l?i v?i player
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
    }

    // Tìm zombie trong bán kính xác ??nh
    void FindZombie()
    {
        Collider[] zombies = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var zombie in zombies)
        {
            if (zombie.CompareTag("Zombie"))
            {
                targetZombie = zombie.transform;
                StartAttacking(targetZombie);
                break;
            }
        }
    }

    // B?t ??u t?n công zombie
    void StartAttacking(Transform zombie)
    {
        // Di chuy?n con chó ??n zombie
        navMeshAgent.SetDestination(zombie.position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        // Khi con chó ??n g?n zombie, chuy?n sang tr?ng thái t?n công
        float distanceToZombie = Vector3.Distance(transform.position, zombie.position);
        if (distanceToZombie < attackRadius)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    // T?n công zombie
    void AttackZombie()
    {
        // Gi? s? zombie có script Health, b?n có th? g?i hàm TakeDamage ? ?ây
        Health zombieHealth = targetZombie.GetComponent<Health>();
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(10);  // Gây 10 sát th??ng cho zombie
            StartCoroutine(AttackCooldown());
        }
    }

    // Th?c hi?n t?n công l?i sau m?t kho?ng th?i gian
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        if (targetZombie != null && targetZombie.GetComponent<Health>().currentHP <= 0)
        {
            // N?u zombie ch?t, quay l?i v? trí ng??i ch?i
            animator.SetFloat("Speed", 0);  // D?ng animation ch?y khi quay l?i
            navMeshAgent.SetDestination(player.position);  // Quay l?i ng??i ch?i
            isAttacking = false;
            targetZombie = null;  // ??t l?i targetZombie
        }
    }

    // Hàm OnTrigger ?? con chó nh?n di?n zombie khi chúng vào ph?m vi t?n công
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            targetZombie = other.transform;
            StartAttacking(targetZombie);
        }
    }
}
