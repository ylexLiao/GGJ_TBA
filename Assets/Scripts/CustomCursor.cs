using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // 针的图片
    public Vector2 hotspot = Vector2.zero; // 热点（针的尖端位置）

    void Start()
    {
        // 设置自定义光标
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
}
