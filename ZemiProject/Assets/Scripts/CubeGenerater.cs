using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//追加したもの
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class CubeGenerater : MonoBehaviour
{

    // 平面に生成するオブジェクト
    [SerializeField] GameObject[] gameObjects;// Prefab
    private GameObject instantiatedObject;

    // ARRaycastManager
    ARRaycastManager raycastManager;

    // RaycastとPlaneが衝突した情報を格納
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //public int TimeScale { get; private set; }

    //生成されたCubeの数をカウントする
    //int countCube = 0;

    private int previousDropdownValue = -1;

    public Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        // ARRaycastManager格納する
        raycastManager = GetComponent<ARRaycastManager>();

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame

    void Update()
    {

        // 画面がタッチされたかチェック
        if (Input.touchCount > 0)
        {
            // タッチ情報を格納
            Touch touch = Input.GetTouch(0);

            if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            // タッチした位置からRayを飛ばして、Planeにヒットした情報をhitsに格納する
            {
                if (dropdown.value != previousDropdownValue)
                {
                    DestroyInstantiatedObject();
                    InstantiateSelectedObject();
                }
                // Cubeを生成する

                //instantiatedObject = Instantiate(gameObjects[dropdown.value], hits[0].pose.position, Quaternion.identity);

                //インスタンス化したCubeオブジェクトに名前を付ける
                //instantiatedObject.name = "InstanceObject";


                previousDropdownValue = dropdown.value;
                //countCube++;
            }
        }
    }

    //dropdownのvalueの値が変わったときに呼び出す
    public void Change()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (i == dropdown.value)
            {
                gameObjects[i].SetActive(i == dropdown.value);
            }
        }

    }

    void DestroyInstantiatedObject()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject);
        }
    }

    void InstantiateSelectedObject()
    {
        instantiatedObject = Instantiate(gameObjects[dropdown.value], hits[0].pose.position, Quaternion.identity);
        instantiatedObject.name = "InstancedObject";

        Change();
    }

    public void OnDropdownValueChanged(int newValue)
    {
        if (newValue != previousDropdownValue)
        {
            DestroyInstantiatedObject();
            InstantiateSelectedObject();
        }

        previousDropdownValue = newValue;
    }
}