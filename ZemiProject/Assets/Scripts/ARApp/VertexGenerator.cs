using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexGenerator : MonoBehaviour
{
    [SerializeField] public GameObject spherePrefab;

    public Material StartMaterial;

    //�F��Material���A�^�b�`
    public Material blueMaterial;
    // ���_���Ƃ�Sphere�I�u�W�F�N�g
    GameObject[] spheres;

    Button vertexButton;

    //�}�e���A�����ύX���ꂽ�񐔂��J�E���g����ϐ�
    int materialChangeCount = 0;

    //�J�E���g��\������e�L�X�g
    Text vertexText;

    //�ύX���ꂽSphere�̒l��ێ����郊�X�g
    List<int> changedSphereIndex = new List<int>();

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.81649658f, -0.47140452f, 0.33333333f),
        new Vector3(0.81649658f, -0.47140452f, 0.33333333f),
        new Vector3(0.00000000f, 0.00000000f, -1.00000000f),
        new Vector3(0.00000000f, 0.94280904f, 0.33333333f), 
    };

    void Start()
    {
        //�J�E���g��\������Text���擾
        vertexText = GameObject.Find("TextCanvas/VertexText").GetComponent<Text>();

        vertexButton = GameObject.Find("ResetCanvas/VertexButton").GetComponent<Button>();
        vertexButton.onClick.AddListener(ChangeSphere);

        spheres = new GameObject[localVertexPositions.Length];
        
        for (int i = 0; i < localVertexPositions.Length; i++)
        {
            // ���[�J�����W�����[���h���W�ɕϊ����Ă���Sphere��z�u
            Vector3 worldPosition = transform.TransformPoint(localVertexPositions[i]);

            spheres[i] = Instantiate(spherePrefab, worldPosition, Quaternion.identity);
            spheres[i].GetComponent<Renderer>().material = StartMaterial;
        }

        //Sphere��z�u������A���[�J�����W�ɖ߂�
        for (int i = 0; i < spheres.Length; i++)
        {
            Vector3 localPosition = transform.InverseTransformPoint(spheres[i].transform.position);

            //���[�J�����W��Sphere�̈ʒu���X�V
            localVertexPositions[i] = localPosition;

            spheres[i].name = "InstancedObject";

            //Sphere��VertexGenerator�̎q�I�u�W�F�N�g�ɐݒ�
            spheres[i].transform.SetParent(transform);
        }
    }

    void Update()
    {
        //�^�b�`���͂̏���
        if(Input.touchCount > 0)
        {
            //�ŏ��̃^�b�`���擾
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);

                RaycastHit hit;

                //�^�b�`�����ʒu����Ray���΂��ASphere�ɓ���������F��ύX
                if (Physics.Raycast(ray, out hit)) 
                {
                    GameObject touchedSphere = hit.transform.gameObject;
                    int sphereIndex = System.Array.IndexOf(spheres, touchedSphere);

                    //Sphere���܂��ύX����Ă��Ȃ��ꍇ�̂ݏ����������Ȃ�
                    if (sphereIndex >= 0 && !changedSphereIndex.Contains(sphereIndex))
                    {
                        Renderer sphereRenderer = spheres[sphereIndex].GetComponent<Renderer>();

                        sphereRenderer.material = blueMaterial;

                        //�ύX���ꂽSphere�̒l�����X�g�ɒǉ�
                        changedSphereIndex.Add(sphereIndex);

                        //�}�e���A�����ύX���ꂽ�񐔂𑝂₷
                        materialChangeCount++;

                        //�J�E���g��\������e�L�X�g���X�V
                        vertexText.text = "���_�F" + materialChangeCount + "��";
                    }               
                }
            }
        }
    }

    //ResetCanvas/VertexButton�������ꂽ�Ƃ��Ɏ��s
    void ChangeSphere()
    {
        for(int i = 0; i < spheres.Length; i++)
        {
            spheres[i].GetComponent<Renderer>().material = StartMaterial; 
        }

        //materialChangeCount��0�Ƀ��Z�b�g
        materialChangeCount = 0;

        //�J�E���g��\������e�L�X�g���X�V
        vertexText.text = "���_�F" + materialChangeCount + "��";

        //�X�V���ꂽSphere�̒l��ێ����郊�X�g���N���A
        changedSphereIndex.Clear();
    }
}