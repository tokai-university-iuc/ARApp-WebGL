using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private int count = 0;

    // テキストの前をInspectorから手動設定
    public string prefixText = "";

    // 変更の数を増やすメソッド
    public void IncrementChangeCount()
    {
        count++;

        UpdateText();
    }

    // 変更の数を取得する
    public int GetCount
    {
        get { return count; }
    }

    private void UpdateText()
    {
        this.gameObject.GetComponent<Text>().text = prefixText + count + "個";
    }

    // リセットボタンが押された時に実行
    public void CountReset()
    {
        // カウントを0に戻す
        count = 0;

        // テキスト更新
        this.gameObject.GetComponent<Text>().text = prefixText + count + "個";
    }
}