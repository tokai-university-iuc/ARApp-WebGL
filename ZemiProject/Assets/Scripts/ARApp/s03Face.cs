using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s03Face : MonoBehaviour
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
        new Vector3(-0.42640143f, -0.73854895f, 0.52223297f),
        new Vector3(-0.85280287f, 0.00000000f, 0.52223297f),
        new Vector3(-0.42640143f, 0.73854895f, 0.52223297f),
        new Vector3(0.42640143f, 0.73854895f, 0.52223297f),
        new Vector3(0.85280287f, -0.00000000f, 0.52223297f),
        new Vector3(0.42640143f, -0.73854895f, 0.52223297f),
        new Vector3(-0.85280287f, -0.49236596f, -0.17407766f),
        new Vector3(0.00000000f, 0.98473193f, -0.17407766f),
        new Vector3(0.85280287f, -0.49236596f, -0.17407766f),
        new Vector3(-0.42640143f, -0.24618298f, -0.87038828f),
        new Vector3(0.00000000f, 0.49236596f, -0.87038828f),
        new Vector3(0.42640143f, -0.24618298f, -0.87038828f)
    };

    // �e�ʂ̒��_
    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 6 },
        new int[]{ 0, 6, 9, 11, 8, 5 },
        new int[]{ 0, 5, 4, 3, 2, 1 },
        new int[]{ 1, 2, 7, 10, 9, 6 },
        new int[]{ 2, 3, 7 },
        new int[]{ 3, 4, 8, 11, 10, 7 },
        new int[]{ 4, 5, 8 },
        new int[]{ 9, 10, 11 }
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
                                            triangles[i][0], triangles[i][3], triangles[i][4],
                                            triangles[i][0], triangles[i][4], triangles[i][5] };
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
