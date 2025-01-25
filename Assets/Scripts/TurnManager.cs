using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int currentTurn = 1;

    public void EndTurn()
    {
        currentTurn++;
        Debug.Log($"Turn {currentTurn} has started.");
        // 调用更新方法
        UpdateGameState();
    }

    void UpdateGameState()
    {
        // 更新股票
        Debug.Log("Updating stock prices...");
        // 更新其他逻辑
    }
}
