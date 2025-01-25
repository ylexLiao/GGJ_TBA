using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public Button capitalistButton; // 资本家按钮
    public Button workerButton; // 工人按钮
    public Button politicianButton; // 政客按钮
    public Button confirmButton; // 确认选择按钮
    public Button backButton; // 返回按钮

    private string selectedCharacter = ""; // 当前选中的角色

    void Start()
    {
        // 初始化按钮功能
        capitalistButton.onClick.AddListener(() => SelectCharacter("Capitalist"));
        workerButton.onClick.AddListener(() => ShowLockedMessage("工人"));
        politicianButton.onClick.AddListener(() => ShowLockedMessage("政客"));

        confirmButton.onClick.AddListener(ConfirmSelection);
        backButton.onClick.AddListener(GoBack);

        // 初始化确认按钮不可用
        confirmButton.interactable = false;
    }

    void SelectCharacter(string character)
    {
        selectedCharacter = character;
        Debug.Log("Selected Character: " + character);

        // 确认按钮激活
        confirmButton.interactable = true;
    }

    void ShowLockedMessage(string character)
    {
        Debug.Log(character + " 未解锁！");
        // 可以在这里显示一个提示 UI，比如弹窗或文字
    }

    void ConfirmSelection()
    {
        if (selectedCharacter == "Capitalist")
        {
            // 进入游戏场景
            SceneManager.LoadScene("Game_1929");
        }
    }

    void GoBack()
    {
        // 返回上一界面
        SceneManager.LoadScene("BubbleSelection");
    }
}
