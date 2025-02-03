using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class BubbleSelection : MonoBehaviour
{
    public Texture2D cursorTexture; // 针的图片
    public Vector2 cursorHotspot = Vector2.zero; // 热点位置（针尖）
    public AudioClip popSound; // 戳破泡泡的音效
    private AudioSource audioSource;

    void Start()
    {
        // 自动计算针尖热点为图片底部中央
        if (cursorTexture != null)
        {
            cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height); // 底部中央为针尖
        }

        // 设置自定义鼠标光标
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        // 添加音效播放器
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // 鼠标进入泡泡
    public void OnBubbleHover(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;

        // 获取整个按钮对象
        GameObject bubble = pointerEventData.pointerEnter;

        // 向上寻找根按钮对象
        while (bubble != null && bubble.GetComponent<Button>() == null)
        {
            bubble = bubble.transform.parent?.gameObject;
        }

        // 放大按钮
        if (bubble != null)
        {
            bubble.transform.localScale = Vector3.one * 1.2f; // 放大效果
        }
    }

    // 鼠标离开泡泡
    public void OnBubbleExit(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;

        // 获取整个按钮对象
        GameObject bubble = pointerEventData.pointerEnter;

        // 向上寻找根按钮对象
        while (bubble != null && bubble.GetComponent<Button>() == null)
        {
            bubble = bubble.transform.parent?.gameObject;
        }

        // 恢复按钮大小
        if (bubble != null)
        {
            bubble.transform.localScale = Vector3.one; // 恢复原始大小
        }
    }

    // 鼠标点击泡泡
    // public void OnBubbleSelected(BaseEventData eventData)
    // {
    //     PointerEventData pointerEventData = eventData as PointerEventData;

    //     // 获取被点击的泡泡对象
    //     GameObject bubble = pointerEventData.pointerPress;

    //     if (bubble != null)
    //     {
    //         Debug.Log("Bubble Popped: " + bubble.name);
    //         StartCoroutine(PopBubble(bubble)); // 播放破裂动画
    //     }
    // }

    public void OnBubbleSelected(BaseEventData eventData)
{
    PointerEventData pointerEventData = eventData as PointerEventData;

    // 获取被点击的泡泡对象
    GameObject bubble = pointerEventData.pointerPress;

    if (bubble != null)
    {
        Debug.Log("Bubble Popped: " + bubble.name);

        // 如果点击的是 1929 泡泡，加载 CharacterSelection 场景
        if (bubble.name == "Bubble_1929")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelection");
        }
        else
        {
            Debug.Log("This bubble does not have an associated scene.");
        }
    }
}


    private IEnumerator PopBubble(GameObject bubble)
    {
        // 播放破裂音效
        if (popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }

        // 模拟破裂动画（缩小泡泡）
        Vector3 originalScale = bubble.transform.localScale;
        Vector3 targetScale = Vector3.zero; // 缩小到0
        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            bubble.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最终销毁泡泡
        Destroy(bubble);
    }
}
