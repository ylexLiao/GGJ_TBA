using UnityEngine;
using UnityEngine.UI;

public class EventLogManager : MonoBehaviour
{
    public Text eventLogText;

    public void AddEvent(string message)
    {
        eventLogText.text += message + "\n";
        Debug.Log("新事件：" + message);
    }
}
