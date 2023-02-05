using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public string Prologue;
    public string next;
    // Start is called before the first frame update
    void Start()
    {
        print(StateNameController.Visited);
        print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "prologue")
        {
            print("in");
            StateNameController.Visited = true;
        }
    }

    public void onSliderChange(float Value)
    {
        if (Value == 1)
        {
            if (StateNameController.Visited)
            {
                ChangeScene(next);
            }
            else
            {
                ChangeScene(Prologue);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }
}
