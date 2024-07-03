using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PolyhedronGenerater : MonoBehaviour
{
    //���ʂɐ�������I�u�W�F�N�g
    [SerializeField] GameObject[] PolyhedronPrefabs;
    GameObject instantiatedObject;

    ARRaycastManager raycastManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private int previousDropdownValue = -1;

    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        //ARRaycastManager���i�[
        raycastManager = GetComponent<ARRaycastManager>();

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        //��ʂ��^�b�`���ꂽ���`�F�b�N
        if(Input.touchCount > 0)
        {
            //�^�b�`�����i�[
            Touch touch = Input.GetTouch(0);

            //�^�b�`�����ʒu����Ray���΂���, Plane�Ƀq�b�g��������hits�Ɋi�[
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

    //dropdown��value�̒l���ς�����Ƃ��ɌĂяo��
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
