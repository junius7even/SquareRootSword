using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        SampleScene,
        ShopScene,
        Victory,
    }

    public static void Load(Scene scene){
        SceneManager.LoadScene(scene.ToString());   
    }

    public static void AdditiveLoad(Scene scene) {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);   
    }

    public static void UnloadAdditive(Scene scene) {
        SceneManager.UnloadSceneAsync(scene.ToString());   
    }

}
