using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        Debug.Log("Button was clicked in Start!");
    }

    void TaskOnClick()
    {
        Loader.Load(Loader.Scene.BattleScene);
        Debug.Log("Button was clicked!");
    }
}