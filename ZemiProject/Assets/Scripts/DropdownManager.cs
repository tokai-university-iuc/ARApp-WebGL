using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    public GameObject[] gameObjects;
  
    public Dropdown dropdown;

    void Start()
    {
        // GameObjectのすべてを一旦非表示にする
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }

        // 1つ目に格納されているGameObjectのみ表示する
        gameObjects[0].SetActive(true);
    }
    // Start is called before the first frame update
    public void Changed()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (i == dropdown.value)
            {
                //選択されたオブジェクトを表示
                gameObjects[i].SetActive(true);
            }
            else
            {
                //それ以外のオブジェクトを非表示
                gameObjects[i].SetActive(false);
            }
        }
    }
}
