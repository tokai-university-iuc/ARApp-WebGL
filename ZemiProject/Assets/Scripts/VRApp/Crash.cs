using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crash : MonoBehaviour
{
    // 衝突したときに適用するマテリアル
    public Material changeMaterial;

    private Text countText;

    private bool isChanged = false;

    // Counterクラスへの参照
    private Counter counter;
    
    public void SetUIText(Text _CountText)
    {
        countText = _CountText;
    }
    
    private void Start()
    {
        counter = countText.GetComponent<Counter>();
    }

    // 衝突したときの処理
    private void OnTriggerEnter(Collider other)
    {
        // マテリアルを変更済みだった場合は、何もしない
        if (isChanged)
        {
            return;
        }

        //衝突したオブジェクトの名前が "LeftHand" または "RightHand"であるか確認
        if (other.gameObject.name == "LeftHand" || other.gameObject.name == "RightHand")
        {
            // 衝突判定を確認
            Debug.Log(gameObject.name + " triggered by " + other.gameObject.name);

            //衝突したオブジェクトのRendererコンポーネントを取得
            //Renderer objectRenderer = this.GetComponent<Renderer>();
            //MeshRenderer objectRenderer = this.GetComponent<MeshRenderer>();
            MeshRenderer objectRenderer = gameObject.GetComponent<MeshRenderer>();

            if (objectRenderer == null)
            {
                Debug.Log(gameObject.name + ": objectRenderer is null");
            }
            Debug.Log(gameObject.name + ": changeMaterial is " + changeMaterial);
            objectRenderer.material = changeMaterial;

            // マテリアルを変更
            objectRenderer.material = changeMaterial;

            // マテリアルを変更したら, trueにする
            isChanged = true;
            Debug.Log(gameObject.name + " material changed by " + other.gameObject.name);
            if (counter != null)
            {
                // 集計結果を1増やす
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