using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void onSliderChange(float Value)
    {
        if (Value == 1)
        {
            ChangeScene();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
