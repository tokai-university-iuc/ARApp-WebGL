using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private int count = 0;

    // �e�L�X�g�̑O��Inspector����蓮�ݒ�
    public string prefixText = "";

    // �ύX�̐��𑝂₷���\�b�h
    public void IncrementChangeCount()
    {
        count++;

        UpdateText();
    }

    // �ύX�̐����擾����
    public int GetCount
    {
        get { return count; }
    }

    private void UpdateText()
    {
        this.gameObject.GetComponent<Text>().text = prefixText + count + "��";
    }

    // ���Z�b�g�{�^���������ꂽ���Ɏ��s
    public void CountReset()
    {
        // �J�E���g��0�ɖ߂�
        count = 0;

        // �e�L�X�g�X�V
        this.gameObject.GetComponent<Text>().text = prefixText + count + "��";
    }
}