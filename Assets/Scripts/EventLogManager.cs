using UnityEngine;
using UnityEngine.UI;

public class EventLogger : MonoBehaviour
{
    public Text logText;

    public void AddEvent(string message)
    {
        logText.text += $"{message}\n";
        Debug.Log($"Event Logged: {message}");
    }
}
