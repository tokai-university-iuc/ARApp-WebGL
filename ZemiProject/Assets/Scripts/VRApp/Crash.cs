using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crash : MonoBehaviour
{
    // �Փ˂����Ƃ��ɓK�p����}�e���A��
    public Material changeMaterial;

    private Text countText;

    private bool isChanged = false;

    // Counter�N���X�ւ̎Q��
    private Counter counter;
    
    public void SetUIText(Text _CountText)
    {
        countText = _CountText;
    }
    
    private void Start()
    {
        counter = countText.GetComponent<Counter>();
    }

    // �Փ˂����Ƃ��̏���
    private void OnTriggerEnter(Collider other)
    {
        // �}�e���A����ύX�ς݂������ꍇ�́A�������Ȃ�
        if (isChanged)
        {
            return;
        }

        //�Փ˂����I�u�W�F�N�g�̖��O�� "LeftHand" �܂��� "RightHand"�ł��邩�m�F
        if (other.gameObject.name == "LeftHand" || other.gameObject.name == "RightHand")
        {
            // �Փ˔�����m�F
            Debug.Log(gameObject.name + " triggered by " + other.gameObject.name);

            //�Փ˂����I�u�W�F�N�g��Renderer�R���|�[�l���g���擾
            //Renderer objectRenderer = this.GetComponent<Renderer>();
            //MeshRenderer objectRenderer = this.GetComponent<MeshRenderer>();
            MeshRenderer objectRenderer = gameObject.GetComponent<MeshRenderer>();

            if (objectRenderer == null)
            {
                Debug.Log(gameObject.name + ": objectRenderer is null");
            }
            Debug.Log(gameObject.name + ": changeMaterial is " + changeMaterial);
            objectRenderer.material = changeMaterial;

            // �}�e���A����ύX
            objectRenderer.material = changeMaterial;

            // �}�e���A����ύX������, true�ɂ���
            isChanged = true;
            Debug.Log(gameObject.name + " material changed by " + other.gameObject.name);
            if (counter != null)
            {
                // �W�v���ʂ�1���₷
                counter.IncrementChangeCount();
            }

            Debug.Log(gameObject.name + ":" + gameObject.GetInstanceID());
            Debug.Log(gameObject.name + " Crash :" + this.GetInstanceID());
        }
    }

    public void FlagReset()
    {
        isChanged = false;
    }
}