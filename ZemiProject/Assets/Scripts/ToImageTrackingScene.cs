using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToImageTrackingScene : MonoBehaviour
{
    public string ImageTrackingSceneName;

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene(ImageTrackingSceneName);
    }
}
