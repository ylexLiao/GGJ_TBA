using UnityEngine;

public class SetResolution : MonoBehaviour
{
    void Start()
    {
        // 设置固定分辨率，例如 1280x720
        Screen.SetResolution(1920, 1080, false); // false 表示窗口模式
    }
}
