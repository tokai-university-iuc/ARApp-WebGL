using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s02Face : MonoBehaviour
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
        new Vector3(-0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(-0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(-1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(-0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(-0.30901700f, 0.95105652f, 0.00000000f),
        new Vector3(0.30901699f, 0.95105652f, 0.00000000f),
        new Vector3(0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(0.00000000f, -0.85065081f, -0.52573111f),
        new Vector3(-0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.00000000f, 0.52573111f, -0.85065081f),
        new Vector3(0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, 0.85065081f, 0.52573111f),
        new Vector3(-0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, -0.52573111f, 0.85065081f),
        new Vector3(0.50000000f, -0.16245985f, 0.85065081f),
        new Vector3(0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.50000000f, -0.16245985f, 0.85065081f)
    };

    // �e�ʂ̒��_
    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 11, 15, 10 },
        new int[]{ 0, 10, 9 },
        new int[]{ 0, 9, 21, 25, 20 },
        new int[]{ 0, 20, 1 },
        new int[]{ 1, 2, 11 },
        new int[]{ 1, 20, 29, 24, 2 },
        new int[]{ 2, 3, 12, 16, 11 },
        new int[]{ 2, 24, 3 },
        new int[]{ 3, 4, 12 },
        new int[]{ 3, 24, 28, 23, 4 },
        new int[]{ 4, 5, 13, 17, 12 },
        new int[]{ 4, 23, 5 },
        new int[]{ 5, 6, 13 },
        new int[]{ 5, 23, 27, 22, 6 },
        new int[]{ 6, 7, 14, 18, 13 },
        new int[]{ 6, 22, 7 },
        new int[]{ 7, 8, 14 },
        new int[]{ 7, 22, 26, 21, 8 },
        new int[]{ 8, 9, 10, 19, 14 },
        new int[]{ 8, 21, 9 },
        new int[]{ 10, 15, 19 },
        new int[]{ 11, 16, 15 },
        new int[]{ 12, 17, 16 },
        new int[]{ 13, 18, 17 },
        new int[]{ 14, 19, 18 },
        new int[]{ 15, 16, 17, 18, 19 },
        new int[]{ 20, 25, 29 },
        new int[]{ 21, 26, 25 },
        new int[]{ 22, 27, 26 },
        new int[]{ 23, 28, 27 },
        new int[]{ 24, 29, 28 },
        new int[]{ 25, 26, 27, 28, 29 }
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

            if (triangles[i].Length == 3)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
            }
            else
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3],
                                            triangles[i][0], triangles[i][3], triangles[i][4] };
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
