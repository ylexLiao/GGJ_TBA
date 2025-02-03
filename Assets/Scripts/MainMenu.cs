using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // 切换到泡泡选择界面
        SceneManager.LoadScene("BubbleSelection");
    }

    public void LoadGame()
    {
        // 加载存档逻辑（此处用Debug代替）
        Debug.Log("Load Game Feature Coming Soon!");
    }

    public void ExitGame()
    {
        // 退出游戏
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
