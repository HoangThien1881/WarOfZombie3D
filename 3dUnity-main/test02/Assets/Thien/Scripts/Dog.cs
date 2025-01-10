using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;  // ?i?u khi?n di chuy?n
    public Transform player;            // Ng??i ch?i
    public Animator animator;           // Animator ?? ?i?u khi?n animation

    public float detectionRadius = 10f; // B�n k�nh ph�t hi?n zombie
    public float attackRadius = 2f;     // B�n k�nh t?n c�ng zombie
    public float attackDelay = 1f;      // Th?i gian gi?a c�c ?�n t?n c�ng

    private Transform targetZombie;     // Zombie m� con ch� ?ang t?n c�ng
    private bool isAttacking = false;   // Ki?m tra xem con ch� c� ?ang t?n c�ng kh�ng

    void Update()
    {
        // Ki?m tra n?u zombie ?� b? ti�u di?t, v� n?u ?� th� reset targetZombie
        if (targetZombie != null && targetZombie.GetComponent<Health>().currentHP <= 0)
        {
            targetZombie = null;
        }

        // N?u con ch� ?ang t?n c�ng, h�y ti?p t?c t?n c�ng
        if (isAttacking && targetZombie != null)
        {
            AttackZombie();
        }
        else
        {
            // T�m zombie g?n nh?t n?u kh�ng c� m?c ti�u
            FindZombie();
        }

        // N?u kh�ng t?n c�ng v� kh�ng c� zombie ?? t?n c�ng, quay l?i v?i ng??i ch?i
        if (!isAttacking && targetZombie == null)
        {
            navMeshAgent.SetDestination(player.position);  // Quay l?i v?i player
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
    }

    // T�m zombie trong b�n k�nh x�c ??nh
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

    // B?t ??u t?n c�ng zombie
    void StartAttacking(Transform zombie)
    {
        // Di chuy?n con ch� ??n zombie
        navMeshAgent.SetDestination(zombie.position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        // Khi con ch� ??n g?n zombie, chuy?n sang tr?ng th�i t?n c�ng
        float distanceToZombie = Vector3.Distance(transform.position, zombie.position);
        if (distanceToZombie < attackRadius)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    // T?n c�ng zombie
    void AttackZombie()
    {
        // Gi? s? zombie c� script Health, b?n c� th? g?i h�m TakeDamage ? ?�y
        Health zombieHealth = targetZombie.GetComponent<Health>();
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(0.5f);  // G�y 10 s�t th??ng cho zombie
            StartCoroutine(AttackCooldown());
        }
    }

    // Th?c hi?n t?n c�ng l?i sau m?t kho?ng th?i gian
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        if (targetZombie != null && targetZombie.GetComponent<Health>().currentHP <= 0)
        {
            // N?u zombie ch?t, quay l?i v? tr� ng??i ch?i
            animator.SetFloat("Speed", 0);  // D?ng animation ch?y khi quay l?i
            navMeshAgent.SetDestination(player.position);  // Quay l?i ng??i ch?i
            isAttacking = false;
            targetZombie = null;  // ??t l?i targetZombie
        }
    }

    // H�m OnTrigger ?? con ch� nh?n di?n zombie khi ch�ng v�o ph?m vi t?n c�ng
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            targetZombie = other.transform;
            StartAttacking(targetZombie);
        }
    }
}
