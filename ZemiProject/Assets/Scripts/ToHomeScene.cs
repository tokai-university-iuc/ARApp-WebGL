using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHomeScene : MonoBehaviour
{
    public string HomeSceneName;

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene(HomeSceneName);
    }
}
