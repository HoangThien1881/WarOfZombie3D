using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    public Slider expSlider; // Thanh slider hiển thị EXP
    public PlayerExp playerExp; // Tham chiếu tới script PlayerExp

    void Start()
    {
        expSlider.maxValue = playerExp.maxExp; // Đặt giá trị tối đa của thanh EXP
        expSlider.value = playerExp.currentExp; // Đặt giá trị ban đầu của thanh EXP
    }

    void Update()
    {
        expSlider.value = playerExp.currentExp; // Cập nhật giá trị thanh EXP mỗi frame
    }
}
