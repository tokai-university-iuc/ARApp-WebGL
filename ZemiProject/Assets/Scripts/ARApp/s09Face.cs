using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s09Face : MonoBehaviour
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

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.22391898f, -0.68915176f, 0.68915176f),
        new Vector3(-0.5862275f, -0.42591921f, 0.68915176f),
        new Vector3(-0.72461704f, 0.0f, 0.68915176f),
        new Vector3(-0.5862275f, 0.42591921f, 0.68915176f),
        new Vector3(-0.22391898f, 0.68915176f, 0.68915176f),
        new Vector3(0.22391898f, 0.68915176f, 0.68915176f),
        new Vector3(0.5862275f, 0.42591921f, 0.68915176f),
        new Vector3(0.72461704f, 0.0f, 0.68915176f),
        new Vector3(0.5862275f, -0.42591921f, 0.68915176f),
        new Vector3(0.22391898f, -0.68915176f, 0.68915176f),
        new Vector3(-0.22391898f, -0.92459411f, 0.30819804f),
        new Vector3(-0.81014648f, -0.4986749f, 0.30819804f),
        new Vector3(-0.94853602f, -0.07275569f, 0.30819804f),
        new Vector3(-0.72461704f, 0.61639607f, 0.30819804f),
        new Vector3(-0.36230852f, 0.87962862f, 0.30819804f),
        new Vector3(0.36230852f, 0.87962862f, 0.30819804f),
        new Vector3(0.72461704f, 0.61639607f, 0.30819804f),
        new Vector3(0.94853602f, -0.07275569f, 0.30819804f),
        new Vector3(0.81014648f, -0.4986749f, 0.30819804f),
        new Vector3(0.22391898f, -0.92459411f, 0.30819804f),
        new Vector3(0.0f, -0.99734979f, -0.07275569f),
        new Vector3(-0.5862275f, -0.80687293f, 0.07275569f),
        new Vector3(-0.94853602f, 0.30819804f, 0.07275569f),
        new Vector3(0.0f, 0.99734979f, 0.07275569f),
        new Vector3(0.94853602f, 0.30819803f, 0.07275569f),
        new Vector3(0.5862275f, -0.80687293f, 0.07275569f),
        new Vector3(-0.36230852f, -0.87962862f, -0.30819804f),
        new Vector3(-0.22391898f, -0.68915176f, -0.68915176f),
        new Vector3(-0.22391898f, -0.30819804f, -0.92459411f),
        new Vector3(-0.36230852f, 0.11772117f, -0.92459411f),
        new Vector3(-0.5862275f, 0.42591921f, -0.68915176f),
        new Vector3(-0.81014648f, 0.4986749f, -0.30819804f),
        new Vector3(0.36230852f, -0.87962862f, -0.30819804f),
        new Vector3(0.22391898f, -0.68915176f, -0.68915176f),
        new Vector3(0.22391898f, -0.30819803f, -0.92459411f),
        new Vector3(0.36230852f, 0.11772117f, -0.92459411f),
        new Vector3(0.5862275f, 0.42591921f, -0.68915176f),
        new Vector3(0.81014648f, 0.4986749f, -0.30819804f),
        new Vector3(0.0f, 0.38095372f, -0.9245941f),
        new Vector3(-0.22391898f, 0.68915176f, -0.68915176f),
        new Vector3(0.22391898f, 0.68915176f, -0.68915176f),
        new Vector3(-0.22391898f, 0.92459411f, -0.30819804f),
        new Vector3(0.22391898f, 0.92459411f, -0.30819804f),
        new Vector3(0.5862275f, 0.80687293f, -0.07275569f),
        new Vector3(-0.5862275f, 0.80687293f, -0.07275569f),
        new Vector3(0.0f, -0.38095372f, 0.92459411f),
        new Vector3(0.36230852f, -0.11772117f, 0.92459411f),
        new Vector3(0.22391898f, 0.30819804f, 0.92459411f),
        new Vector3(-0.22391898f, 0.30819804f, 0.92459411f),
        new Vector3(-0.36230852f, -0.11772117f, 0.92459411f),
        new Vector3(-0.94853602f, -0.30819804f, -0.07275569f),
        new Vector3(-0.94853602f, 0.07275569f, -0.30819804f),
        new Vector3(-0.72461704f, 0.0f, -0.68915176f),
        new Vector3(-0.5862275f, -0.42591921f, -0.68915176f),
        new Vector3(-0.72461704f, -0.61639607f, -0.30819804f),
        new Vector3(0.94853602f, -0.30819804f, -0.07275569f),
        new Vector3(0.72461704f, -0.61639607f, -0.30819804f),
        new Vector3(0.5862275f, -0.42591921f, -0.68915176f),
        new Vector3(0.72461704f, 0.0f, -0.68915176f),
        new Vector3(0.94853602f, 0.07275569f, -0.30819804f)
    };

    // �e�ʂ̒��_
    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 11, 21, 10 },
        new int[] { 0, 10, 19, 9 },
        new int[] { 0, 9, 45 },
        new int[] { 0, 45, 49, 1 },
        new int[] { 1, 2, 12, 11 },
        new int[] { 1, 49, 2 },
        new int[] { 2, 3, 13, 22, 12 },
        new int[] { 2, 49, 48, 3 },
        new int[] { 3, 4, 14, 13 },
        new int[] { 3, 48, 4 },
        new int[] { 4, 5, 15, 23, 14 },
        new int[] { 4, 48, 47, 5 },
        new int[] { 5, 6, 16, 15 },
        new int[] { 5, 47, 6 },
        new int[] { 6, 7, 17, 24, 16 },
        new int[] { 6, 47, 46, 7 },
        new int[] { 7, 8, 18, 17 },
        new int[] { 7, 46, 8 },
        new int[] { 8, 9, 19, 25, 18 },
        new int[] { 8, 46, 45, 9 },
        new int[] { 10, 21, 26, 20 },
        new int[] { 10, 20, 19 },
        new int[] { 11, 12, 50 },
        new int[] { 11, 50, 54, 21 },
        new int[] { 12, 22, 51, 50 },
        new int[] { 13, 14, 44 },
        new int[] { 13, 44, 31, 22 },
        new int[] { 14, 23, 41, 44 },
        new int[] { 15, 16, 43 },
        new int[] { 15, 43, 42, 23 },
        new int[] { 16, 24, 37, 43 },
        new int[] { 17, 18, 55 },
        new int[] { 17, 55, 59, 24 },
        new int[] { 18, 25, 56, 55 },
        new int[] { 19, 20, 32, 25 },
        new int[] { 20, 26, 27, 33, 32 },
        new int[] { 21, 54, 26 },
        new int[] { 22, 31, 51 },
        new int[] { 23, 42, 41 },
        new int[] { 24, 59, 37 },
        new int[] { 25, 32, 56 },
        new int[] { 26, 54, 53, 27 },
        new int[] { 27, 28, 34, 33 },
        new int[] { 27, 53, 28 },
        new int[] { 28, 29, 38, 35, 34 },
        new int[] { 28, 53, 52, 29 },
        new int[] { 29, 30, 39, 38 },
        new int[] { 29, 52, 30 },
        new int[] { 30, 31, 44, 41, 39 },
        new int[] { 30, 52, 51, 31 },
        new int[] { 32, 33, 57, 56 },
        new int[] { 33, 34, 57 },
        new int[] { 34, 35, 58, 57 },
        new int[] { 35, 38, 40, 36 },
        new int[] { 35, 36, 58 },
        new int[] { 36, 40, 42, 43, 37 },
        new int[] { 36, 37, 59, 58 },
        new int[] { 38, 39, 40 },
        new int[] { 39, 41, 42, 40 },
        new int[] { 45, 46, 47, 48, 49 },
        new int[] { 50, 51, 52, 53, 54 },
        new int[] { 55, 56, 57, 58, 59 }
    };

    void Start()
    {
        //�J�E���g��\������Text���擾����
        faceText = GameObject.Find("TextCanvas/FaceText").GetComponent<Text>();

        faceButton = GameObject.Find("ResetCanvas/FaceButton").GetComponent<Button>();
        faceButton.onClick.AddListener(ChangeFace);

        Mesh mesh = new Mesh { name = "s01 Face Mesh" };
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // �e�ʂ�GameObject�𐶐����A�����̃}�e���A����ݒ�
        faces = new GameObject[triangles.Length];
        for (int i = 0; i < faces.Length; i++)
        {
            GameObject face = new GameObject($"Face_{i}");
            face.transform.parent = transform; // �ʂ����b�V���̎q�ɂ���
            face.transform.localPosition = Vector3.zero; // ���[�J�����W���[���ɐݒ�

            Mesh meshCopy = new Mesh(); // �e�ʂ��ƂɐV�������b�V�����쐬
            meshCopy.vertices = localVertexPositions;

            if (triangles[i].Length == 4)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3] };
            }
            else if(triangles[i].Length == 3)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
            }
            else
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4]};
            }

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
            // �ŏ��̃^�b�`���擾
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // �^�b�`�����ʒu����Ray���΂��Afaces�ɓ���������F��ύX
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
