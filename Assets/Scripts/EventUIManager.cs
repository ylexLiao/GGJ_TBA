using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class EventUIManager : MonoBehaviour
{
    public GameObject eventWindow;  // 事件窗口
    public Text eventDescriptionText;  // 事件描述文本
    public Image eventImage;  // 事件图片（例如，图标或背景图）
    public Button buttonOptionA;  // 按钮A
    public Button buttonOptionB;  // 按钮B

    // 用于管理事件的UI展示
    public void ShowEventUI(GameEvent gameEvent)
    {
        // 显示事件窗口
        eventWindow.SetActive(true);

        // 设置事件描述文本
        eventDescriptionText.text = gameEvent.description;

        // 设置事件相关图片
        eventImage.sprite = gameEvent.eventImage;

        // 设置按钮A的文本和行为
        buttonOptionA.gameObject.SetActive(true);
        buttonOptionA.GetComponentInChildren<Text>().text = gameEvent.optionAButtonText;
        buttonOptionA.onClick.RemoveAllListeners();  // 清除旧的监听器
        buttonOptionA.onClick.AddListener(() => {
            gameEvent.optionAAction.Invoke();  // 执行按钮A的行为
            CloseEventWindow();  // 关闭事件窗口
        });

        // 设置按钮B的文本和行为
        if(gameEvent.optionBButtonText == null)
        {
            buttonOptionB.gameObject.SetActive(false);  // 如果按钮B的文本为空，则隐藏按钮B
        }
        else
        {
            buttonOptionB.gameObject.SetActive(true);
            buttonOptionB.GetComponentInChildren<Text>().text = gameEvent.optionBButtonText;
            buttonOptionB.onClick.RemoveAllListeners();  // 清除旧的监听器
            buttonOptionB.onClick.AddListener(() => {
                gameEvent.optionBAction.Invoke();  // 执行按钮B的行为
                CloseEventWindow();  // 关闭事件窗口
            });
        }
        
    }

    // 关闭事件窗口
    private void CloseEventWindow()
    {
        eventWindow.SetActive(false);
    }
}