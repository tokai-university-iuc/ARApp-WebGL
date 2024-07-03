using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class r03Face : MonoBehaviour
{
    public Material StartColor;
    public Material SelectedColor;

    GameObject[] faces; // �e�ʂ��Q�Ƃ��邽�߂�GameObject�z��

    //�}�e���A�����ύX���ꂽ�񐔂��J�E���g����ϐ�
    int materialChangeCount = 0;

    //�J�E���g��\������e�L�X�g
    Text faceText;

    //�ύX���ꂽface�̒l��ێ����郊�X�g
    List<int> changeFaceIndex = new List<int>();

    Button faceButton;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.57735027f, -0.57735027f, 0.57735027f),
        new Vector3(-0.57735027f, 0.57735027f, 0.57735027f),
        new Vector3(0.57735027f, 0.57735027f, 0.57735027f),
        new Vector3(0.57735027f, -0.57735027f, 0.57735027f),
        new Vector3(-0.57735027f, -0.57735027f, -0.57735027f),
        new Vector3(-0.57735027f, 0.57735027f, -0.57735027f),
        new Vector3(0.57735027f, 0.57735027f, -0.57735027f),
        new Vector3(0.57735027f, -0.57735027f, -0.57735027f)
    };

    //�e�ʂ̒��_

    int[][] facePattern = new int[][]
    {
        new int[] { 0, 1, 5, 4 }, // �P�ڂ̖�
        new int[] { 3, 0, 4, 7 }, // �Q�ڂ̖�
        new int[] { 3, 2, 1, 0 }, // �R�ڂ̖�
        new int[] { 1, 2, 6, 5 }, // �S�ڂ̖�
        new int[] { 2, 3, 7, 6 }, // �T�ڂ̖�
        new int[] { 4, 5, 6, 7 } // �U�ڂ̖�
    };
    
    void Start()
    {
        //�J�E���g��\������Text���擾����
        faceText = GameObject.Find("TextCanvas/FaceText").GetComponent<Text>();

        faceButton = GameObject.Find("ResetCanvas/FaceButton").GetComponent<Button>();
        faceButton.onClick.AddListener(ChangeFace);

        Mesh mesh = new Mesh { name = "ComiX Mesh" };
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // �e�ʂ�GameObject�𐶐����A�����̃}�e���A����ݒ�
        faces = new GameObject[facePattern.Length];
        for (int i = 0; i < faces.Length; i++)
        {
            GameObject face = new GameObject($"Face_{i}");
            face.transform.parent = transform; // �ʂ����b�V���̎q�ɂ���
            face.transform.localPosition = Vector3.zero;//���[�J�����W���[���ɐݒ�

            Mesh meshCopy = new Mesh(); //�e�ʂ��ƂɐV�������b�V�����쐬
            meshCopy.vertices = vertexPosition;
            meshCopy.triangles = new int[] { facePattern[i][0], facePattern[i][1], facePattern[i][2],
                                            facePattern[i][0], facePattern[i][2], facePattern[i][3] };
            
            meshCopy.RecalculateNormals();

            face.AddComponent<MeshFilter>().mesh = meshCopy; // ���b�V���t�B���^�[��ǉ����ē������b�V�����g�p
            face.AddComponent<MeshRenderer>().material = StartColor; // �����̃}�e���A����ݒ�
            face.AddComponent<MeshCollider>();

            faces[i] = face;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            //�ŏ��̃^�b�`���擾
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);
                RaycastHit hit;

                //�^�b�`�����ʒu����Ray���΂��Afaces�ɓ���������F��ύX
                if (Physics.Raycast(ray, out hit))
                {
                    // �^�b�`�����ʂ̃}�e���A����ύX
                    GameObject touchedFace = hit.transform.gameObject;
                    int faceIndex = System.Array.IndexOf(faces, touchedFace);

                    if (faceIndex >= 0 && !changeFaceIndex.Contains(faceIndex))
                    {
                        Renderer faceRenderer = faces[faceIndex].GetComponent<Renderer>();

                        faceRenderer.material = SelectedColor;

                        //�ύX���ꂽface�̒l�����X�g�ɒǉ�
                        changeFaceIndex.Add(faceIndex);

                        //�}�e���A�����ύX���ꂽ�񐔂𑝂₷
                        materialChangeCount++;

                        //�J�E���g��\������e�L�X�g���X�V
                        faceText.text = "�ʁF" + materialChangeCount + "��";
                    }
                }
            }
        }
    }
    //ResetCanvas/FaceButton���������Ƃ��Ɏ��s
    void ChangeFace()
    {
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].GetComponent<MeshRenderer>().material = StartColor;
        }

        //materialChangeCount��0�Ƀ��Z�b�g
        materialChangeCount = 0;

        //�J�E���g��\������e�L�X�g���X�V
        faceText.text = "�ʁF" + materialChangeCount + "��";

        //�X�V���ꂽ���b�V���̒l��ێ����郊�X�g���N���A
        changeFaceIndex.Clear();
    }
}
