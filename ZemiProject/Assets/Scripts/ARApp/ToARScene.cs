using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ǉ�
using UnityEngine.SceneManagement;

public class ToARScene : MonoBehaviour
{
    public string ARSceneName;

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene(ARSceneName);
    }
}
