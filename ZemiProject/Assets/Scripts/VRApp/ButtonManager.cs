using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] gameObjects;

    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
        gameObjects[0].SetActive(true);

        // ShowObject��4�b��ɌĂяo��
        Invoke(nameof(ShowObject), 4.0f);
    }

    public void ShowObject(int val)
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
        
        gameObjects[val].SetActive(true);
    }
}
