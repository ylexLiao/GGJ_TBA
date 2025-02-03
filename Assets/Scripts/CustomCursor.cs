using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // 针的图片
    public Vector2 hotspot = Vector2.zero; // 热点位置（针尖）

    void Start()
    {
        if (cursorTexture != null)
        {
            // 自动设置热点为图片底部中央
            hotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height);

            // 设置鼠标光标
            Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("Cursor texture is missing or not assigned!");
        }
    }
}
