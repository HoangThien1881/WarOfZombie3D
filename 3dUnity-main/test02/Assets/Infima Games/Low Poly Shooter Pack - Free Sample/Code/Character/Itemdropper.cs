using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject itemPrefab; // Vật phẩm để thả
    public Transform player; // Đối tượng người chơi (hoặc vị trí của người chơi)
    public float dropHeight = 5.0f; // Chiều cao thả vật phẩm
    public float dropDistance = 2.0f; // Khoảng cách từ người chơi tới vật phẩm thả
    public int itemCount = 5; // Số lượng vật phẩm có thể thả
    public TMPro.TMP_Text itemCountText; // UI hiển thị số lượng vật phẩm còn lại

    void Start()
    {
        // Khởi tạo UI hiển thị số lượng vật phẩm ban đầu
        if (itemCountText != null)
        {
            itemCountText.text = "Gar: " + itemCount;
        }
    }

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím F
        if (Input.GetKeyDown(KeyCode.F) && itemCount > 0)
        {
            DropItem();
        }
    }

    void DropItem()
    {
        // Tạo vị trí thả vật phẩm ở trước mặt người chơi, cách mặt đất một khoảng cách nhất định
        Vector3 dropPosition = player.position + player.forward * dropDistance + Vector3.up * dropHeight;

        // Tạo vật phẩm tại vị trí đã tính
        GameObject droppedItem = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

        // Thêm Rigidbody để đảm bảo vật phẩm có thể rơi xuống
        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = droppedItem.AddComponent<Rigidbody>(); // Nếu không có Rigidbody thì thêm vào
        }

        // Thiết lập trọng lực
        rb.useGravity = true;

        // Giảm số lượng vật phẩm
        itemCount--;

        // Cập nhật UI số lượng vật phẩm
        if (itemCountText != null)
        {
            itemCountText.text = "Gar: " + itemCount;
        }

        // Nếu không còn vật phẩm, hiển thị thông báo
        if (itemCount == 0 && itemCountText != null)
        {
            itemCountText.text = "Gar: 0";
        }
    }
}
