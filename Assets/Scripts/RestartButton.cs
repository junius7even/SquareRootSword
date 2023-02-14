using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Loader.Load(Loader.Scene.BattleScene);
        BattleSystem.levelNumber = 1;
        Debug.Log("Button was clicked!");
    }
}
