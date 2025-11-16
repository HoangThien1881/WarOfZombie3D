using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 5f; // T?c ?? di chuy?n
    public Vector3 direction = Vector3.forward; // H??ng di chuy?n (m?c ??nh là ?i th?ng)

    private Rigidbody rb;

    void Start()
    {
        // Ki?m tra và g?n thành ph?n Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // ??t thành isKinematic ?? không ?nh h??ng b?i v?t lý
        }
    }

    void Update()
    {
        // Di chuy?n nhân v?t liên t?c theo h??ng
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
