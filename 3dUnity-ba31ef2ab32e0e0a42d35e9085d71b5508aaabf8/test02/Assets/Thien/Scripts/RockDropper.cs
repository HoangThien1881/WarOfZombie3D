using UnityEngine;

public class RockDropper : MonoBehaviour
{
    public GameObject rockPrefab;  // Prefab c?a c?c ?á
    public Transform dropPoint;    // ?i?m phát ra c?c ?á (có th? gán v? trí này trong Inspector)
    public float dropInterval = 5f;  // Th?i gian gi?a các l?n th? c?c ?á

    private float nextDropTime = 0f;  // Th?i gian ti?p theo ?? th? c?c ?á

    void Update()
    {
        // Ki?m tra th?i gian ?? th? c?c ?á
        if (Time.time >= nextDropTime)
        {
            DropRock();
            nextDropTime = Time.time + dropInterval; // C?p nh?t th?i gian ti?p theo ?? th? c?c ?á
        }
    }

    private void DropRock()
    {
        if (rockPrefab != null && dropPoint != null)
        {
            // Spawn c?c ?á t?i v? trí th? ?á
            GameObject rock = Instantiate(rockPrefab, dropPoint.position, Quaternion.identity);

            // Áp d?ng m?t l?c ?? c?c ?á r?i xu?ng (s? d?ng Rigidbody2D ?? áp d?ng tr?ng l?c)
            Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1;  // B?t tr?ng l?c cho c?c ?á
                rb.isKinematic = false; // Cho phép c?c ?á b? ?nh h??ng b?i v?t lý
            }
        }
    }
}
