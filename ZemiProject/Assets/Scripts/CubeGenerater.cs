using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ǉ���������
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class CubeGenerater : MonoBehaviour
{

    // ���ʂɐ�������I�u�W�F�N�g
    [SerializeField] GameObject[] gameObjects;// Prefab
    private GameObject instantiatedObject;

    // ARRaycastManager
    ARRaycastManager raycastManager;

    // Raycast��Plane���Փ˂��������i�[
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //public int TimeScale { get; private set; }

    //�������ꂽCube�̐����J�E���g����
    //int countCube = 0;

    private int previousDropdownValue = -1;

    public Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        // ARRaycastManager�i�[����
        raycastManager = GetComponent<ARRaycastManager>();

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // Update is called once per frame

    void Update()
    {

        // ��ʂ��^�b�`���ꂽ���`�F�b�N
        if (Input.touchCount > 0)
        {
            // �^�b�`�����i�[
            Touch touch = Input.GetTouch(0);

            if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            // �^�b�`�����ʒu����Ray���΂��āAPlane�Ƀq�b�g��������hits�Ɋi�[����
            {
                if (dropdown.value != previousDropdownValue)
                {
                    DestroyInstantiatedObject();
                    InstantiateSelectedObject();
                }
                // Cube�𐶐�����

                //instantiatedObject = Instantiate(gameObjects[dropdown.value], hits[0].pose.position, Quaternion.identity);

                //�C���X�^���X������Cube�I�u�W�F�N�g�ɖ��O��t����
                //instantiatedObject.name = "InstanceObject";


                previousDropdownValue = dropdown.value;
                //countCube++;
            }
        }
    }

    //dropdown��value�̒l���ς�����Ƃ��ɌĂяo��
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