using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSlider : MonoBehaviour
{
    // Inspector���畡����GameObject���i�[
    public Transform[] targetTransforms;

    Quaternion[] defaultRotations;
    
    // Start is called before the first frame update
    void Start()
    {
        // �z��̏�����
        defaultRotations = new Quaternion[targetTransforms.Length];

        // �eTransform�̏�����]��ۑ�
        for(int i = 0; i < targetTransforms.Length; i++)
        {
            defaultRotations[i] = targetTransforms[i].rotation;
        }
    }

    // ���p
    public void OnSliderValueChanged(float val)
    {
        // Slider�̒l�ɉ����ĉ�]�p�x���v�Z
        float rotationAngle = val * 360f;

        // �eGameObject����]
        for(int i = 0; i < targetTransforms.Length; i++)
        {
            targetTransforms[i].rotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    // �c�p
    public void Tate_SliderValueChanged(float val)
    {
        // Slider�̒l�ɉ����ĉ�]�p�x���v�Z
        float rotationAngle = val * 360f;

        // �eGameObject����]
        for (int i = 0; i < targetTransforms.Length; i++)
        {
            targetTransforms[i].rotation = Quaternion.Euler(rotationAngle, 0, 0);
        }
    }
}