using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHandMRScene : MonoBehaviour
{
    public string HandMRSceneName;

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene(HandMRSceneName);
    }
}
