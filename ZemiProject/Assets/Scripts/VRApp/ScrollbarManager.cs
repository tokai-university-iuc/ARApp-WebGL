using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarManager : MonoBehaviour
{
    public Scrollbar scrollbar;

    public RectTransform content;
    
    public void OnScrollbarValueChanged(float val)
    {
        //Scrollbar��Value�̒l�ɉ�����, �R���e���c�̈ʒu�����炷
        float contentPos = 1 - val;
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, contentPos * (content.sizeDelta.y - GetComponent<RectTransform>().sizeDelta.y));
    }
}
