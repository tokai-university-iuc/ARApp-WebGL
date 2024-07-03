using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSlider : MonoBehaviour
{
    // Inspectorから複数のGameObjectを格納
    public Transform[] targetTransforms;

    Quaternion[] defaultRotations;
    
    // Start is called before the first frame update
    void Start()
    {
        // 配列の初期化
        defaultRotations = new Quaternion[targetTransforms.Length];

        // 各Transformの初期回転を保存
        for(int i = 0; i < targetTransforms.Length; i++)
        {
            defaultRotations[i] = targetTransforms[i].rotation;
        }
    }

    // 横用
    public void OnSliderValueChanged(float val)
    {
        // Sliderの値に応じて回転角度を計算
        float rotationAngle = val * 360f;

        // 各GameObjectを回転
        for(int i = 0; i < targetTransforms.Length; i++)
        {
            targetTransforms[i].rotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    // 縦用
    public void Tate_SliderValueChanged(float val)
    {
        // Sliderの値に応じて回転角度を計算
        float rotationAngle = val * 360f;

        // 各GameObjectを回転
        for (int i = 0; i < targetTransforms.Length; i++)
        {
            targetTransforms[i].rotation = Quaternion.Euler(rotationAngle, 0, 0);
        }
    }
}