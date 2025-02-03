using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;  // EventManager的静态实例

    // 静态属性Instance，获取单例实例
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 如果实例不存在，则查找当前场景中的EventManager对象
                _instance = FindFirstObjectByType<EventManager>();
                if (_instance == null)
                {
                    // 如果仍然找不到，创建一个新的EventManager对象
                    GameObject obj = new GameObject("EventManager");
                    _instance = obj.AddComponent<EventManager>();
                }
            }
            return _instance;
        }
    }

    private List<GameEvent> allEvents = new List<GameEvent>();  // 存储所有事件

    // 注册一个新事件
    public void RegisterEvent(GameEvent newEvent)
    {
        allEvents.Add(newEvent);
    }

    // 检查并触发事件
    public void CheckEvents()
    {
        foreach (var gameEvent in allEvents)
        {
            gameEvent.TriggerEvent();
        }
    }
}
