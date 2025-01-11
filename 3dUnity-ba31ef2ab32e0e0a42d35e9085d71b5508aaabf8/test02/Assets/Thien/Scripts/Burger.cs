using UnityEngine;

public class Burger : MonoBehaviour
{
    public float rotationSpeed = 50f;  // T?c ?? xoay c?a burger
    public float healAmount = 20f;     // S? máu h?i khi ch?m vào burger
    public Health playerHealth;        // Tham chi?u ??n script Health c?a ng??i ch?i

    void Update()
    {
        // Xoay burger liên t?c
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u ??i t??ng va ch?m là Player
        if (other.gameObject.CompareTag("Player"))
        {
            // H?i máu cho ng??i ch?i
            var healthScript = other.GetComponent<Health>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(-healAmount); // Ch?a b?ng cách gi?m damage (âm)
                Debug.Log("H?i máu 20!");
                Destroy(gameObject);
            }
        }
    }
}
