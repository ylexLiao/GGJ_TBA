using EasyTransition;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TransitionSettings transition;
    void Start()
    {
        
    }
    public void LoadScence()
    {
        TransitionManager.Instance().Transition("Game_1929", transition, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
