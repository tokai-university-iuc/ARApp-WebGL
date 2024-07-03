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
        // GameObject�̂��ׂĂ���U��\���ɂ���
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }

        // 1�ڂɊi�[����Ă���GameObject�̂ݕ\������
        gameObjects[0].SetActive(true);
    }
    // Start is called before the first frame update
    public void Changed()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (i == dropdown.value)
            {
                //�I�����ꂽ�I�u�W�F�N�g��\��
                gameObjects[i].SetActive(true);
            }
            else
            {
                //����ȊO�̃I�u�W�F�N�g���\��
                gameObjects[i].SetActive(false);
            }
        }
    }
}
