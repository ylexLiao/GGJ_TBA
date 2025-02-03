using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image cardImage; // 背景图片
    public TMP_Text descriptionText; // 人物介绍文本

    private Color originalColor; // 原始颜色
    private Color hoverColor = new Color(0.5f, 0.5f, 0.5f, 0.8f); // 灰色透明

    void Start()
    {
        // 保存原始颜色
        originalColor = cardImage.color;

        // 确保描述文本初始不可见
        if (descriptionText != null)
        {
            descriptionText.color = new Color(descriptionText.color.r, descriptionText.color.g, descriptionText.color.b, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 图片变灰
        if (cardImage != null)
        {
            cardImage.color = hoverColor;
        }

        // 显示描述文本
        if (descriptionText != null)
        {
            descriptionText.color = new Color(descriptionText.color.r, descriptionText.color.g, descriptionText.color.b, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 恢复原始颜色
        if (cardImage != null)
        {
            cardImage.color = originalColor;
        }

        // 隐藏描述文本
        if (descriptionText != null)
        {
            descriptionText.color = new Color(descriptionText.color.r, descriptionText.color.g, descriptionText.color.b, 0);
        }
    }
}
