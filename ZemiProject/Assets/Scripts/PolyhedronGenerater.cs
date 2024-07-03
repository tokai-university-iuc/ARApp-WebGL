using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PolyhedronGenerater : MonoBehaviour
{
    //平面に生成するオブジェクト
    [SerializeField] GameObject[] PolyhedronPrefabs;
    GameObject instantiatedObject;

    ARRaycastManager raycastManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private int previousDropdownValue = -1;

    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        //ARRaycastManagerを格納
        raycastManager = GetComponent<ARRaycastManager>();

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        //画面がタッチされたかチェック
        if(Input.touchCount > 0)
        {
            //タッチ情報を格納
            Touch touch = Input.GetTouch(0);

            //タッチした位置からRayを飛ばして, Planeにヒットした情報をhitsに格納
            if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            {
                if(dropdown.value != previousDropdownValue)
                {
                    DestroyInstantiatedObject();
                    InstantiateSelectedObject();
                }

                previousDropdownValue = dropdown.value;
            }
        }
    }

    //dropdownのvalueの値が変わったときに呼び出す
    public void Change()
    {
        for (int i = 0; i < PolyhedronPrefabs.Length; i++)
        {
            if (i == dropdown.value)
            {
                PolyhedronPrefabs[i].SetActive(i == dropdown.value);
            }
        }

    }
    void DestroyInstantiatedObject()
    {
        if(instantiatedObject != null)
        {
            Destroy(instantiatedObject);
        }
    }

    void InstantiateSelectedObject()
    {
        instantiatedObject = Instantiate(PolyhedronPrefabs[dropdown.value], hits[0].pose.position, Quaternion.identity);
        instantiatedObject.name = "InstancedObject";

        Change();
    }

    public void OnDropdownValueChanged(int newValue)
    {
        if (newValue != previousDropdownValue)
        {
            DestroyInstantiatedObject();
            InstantiateSelectedObject(); ;
        }

        previousDropdownValue = newValue;
    }
}
