using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class r04Edge : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public Material initialMaterial;
    public Material selectedMaterial;
    GameObject[] cylinders;
    Vector3[] localCylinderPositions;

    //�}�e���A�����ύX���ꂽ�����J�E���g����ϐ�
    int materialChangeCount = 0;

    //�J�E���g��\������e�L�X�g
    Text edgeText;

    Button edgeButton;

    //�ύX���ꂽEdge�̒l��ێ����郊�X�g
    List<int> changedEdgeIndex = new List<int>();

    Vector3[] localVertexPositions = new Vector3[]
     {
        new Vector3(-0.35682209f, -0.49112347f, 0.79465447f),
        new Vector3(0.35682209f, -0.49112347f, 0.79465447f),
        new Vector3(0.57735027f, -0.79465447f, 0.18759247f),
        new Vector3(0.00000000f, -0.98224695f, -0.18759247f),
        new Vector3(-0.57735027f, -0.79465447f, 0.18759247f),
        new Vector3(-0.57735027f, 0.18759247f, 0.79465447f),
        new Vector3(0.57735027f, 0.18759247f, 0.79465447f),
        new Vector3(0.93417236f, -0.30353100f, -0.18759247f),
        new Vector3(0.00000000f, -0.60706200f, -0.79465447f),
        new Vector3(-0.93417236f, -0.30353100f, -0.18759247f),
        new Vector3(-0.93417236f, 0.30353100f, 0.18759247f),
        new Vector3(0.00000000f, 0.60706200f, 0.79465447f),
        new Vector3(0.93417236f, 0.30353100f, 0.18759247f),
        new Vector3(0.57735027f, -0.18759247f, -0.79465447f),
        new Vector3(-0.57735027f, -0.18759247f, -0.79465447f),
        new Vector3(-0.57735027f, 0.79465447f, -0.18759247f),
        new Vector3(0.00000000f, 0.98224695f, 0.18759247f),
        new Vector3(0.57735027f, 0.79465447f, -0.18759247f),
        new Vector3(0.35682209f, 0.49112347f, -0.79465447f),
        new Vector3(-0.35682209f, 0.49112347f, -0.79465447f)
     };

    // ��12�ʑ̂̕ӂ�\���z��
    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 6 },
        new int[] { 6, 11 },
        new int[] { 5, 11 },
        new int[] { 5, 0 },
        new int[] { 10, 5 },
        new int[] { 9, 10 },
        new int[] { 4, 9 },
        new int[] { 4, 0 },
        new int[] { 2, 1 },
        new int[] { 2, 7 },
        new int[] { 7, 13 },
        new int[] { 12, 7 },
        new int[] { 3, 8 },
        new int[] { 8, 13 },
        new int[] { 19, 14 },
        new int[] { 14, 8 },
        new int[] { 15, 16 },
        new int[] { 16, 11 },
        new int[] { 15, 10 },
        new int[] { 12, 17 },
        new int[] { 17, 16 },
        new int[] { 17, 18 },
        new int[] { 19, 18 },
        new int[] { 15, 19 },
        new int[] { 3, 4 },
        new int[] { 2, 3 },
        new int[] { 6, 12 },
        new int[] { 14, 9 },
        new int[] { 18, 13 }
    };

    // Start is called before the first frame update
    void Start()
    {
        //�J�E���g��\������Text���擾
        edgeText = GameObject.Find("TextCanvas/EdgeText").GetComponent<Text>();

        edgeButton = GameObject.Find("ResetCanvas/EdgeButton").GetComponent<Button>();
        edgeButton.onClick.AddListener(ChangeCylinder);

        cylinders = new GameObject[edges.Length];
        localCylinderPositions = new Vector3[edges.Length];

        for (int i = 0; i < edges.Length; i++)
        {
            int vertex1 = edges[i][0];
            int vertex2 = edges[i][1];

            Vector3 worldPos1 = transform.TransformPoint(localVertexPositions[vertex1]);
            Vector3 worldPos2 = transform.TransformPoint(localVertexPositions[vertex2]);
            Vector3 midPoint = (worldPos1 + worldPos2) / 2;
            float distance = Vector3.Distance(worldPos1, worldPos2);

            cylinders[i] = Instantiate(cylinderPrefab, midPoint, Quaternion.identity);

            //Cylinder�̌���
            Vector3 direction = worldPos2 - worldPos1;
            cylinders[i].transform.up = direction;

            //�K�؂ȃX�P�[����ݒ�
            cylinders[i].transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);

            //�ŏ���cylinderPrefab�̐F��initialMaterial�ɐݒ�
            Renderer cylinderRenderer = cylinders[i].GetComponent<Renderer>();
            cylinderRenderer.material = initialMaterial;
        }

        for (int i = 0; i < edges.Length; i++)
        {
            Vector3 localPos = transform.InverseTransformPoint(cylinders[i].transform.position);
            localCylinderPositions[i] = localPos;
            cylinders[i].name = "InstancedObject";
            cylinders[i].transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedCylinder = hit.transform.gameObject;
                    int cylinderIndex = System.Array.IndexOf(cylinders, touchedCylinder);

                    if (cylinderIndex >= 0 && !changedEdgeIndex.Contains(cylinderIndex))
                    {
                        Renderer cylinderRenderer = cylinders[cylinderIndex].GetComponent<Renderer>();
                        cylinderRenderer.material = selectedMaterial;

                        //�ύX���ꂽCylinder�̒l�����X�g�ɒǉ�
                        changedEdgeIndex.Add(cylinderIndex);

                        //�}�e���A�����ύX���ꂽ�񐔂𑝂₷
                        materialChangeCount++;

                        //�J�E���g��\������e�L�X�g���X�V
                        edgeText.text = "�ӁF" + materialChangeCount + "��";
                    }
                }
            }
        }
    }

    //ResetCanvas/EdgeButton�����������Ɏ��s
    void ChangeCylinder()
    {
        for (int i = 0; i < cylinders.Length; i++)
        {
            cylinders[i].GetComponent<Renderer>().material = initialMaterial;
        }

        //materialChangeCount��0�Ƀ��Z�b�g
        materialChangeCount = 0;

        //�J�E���g��\������e�L�X�g���X�V
        edgeText.text = "�ӁF" + materialChangeCount + "��";

        //�X�V���ꂽCylinder�̒l��ێ����郊�X�g���N���A
        changedEdgeIndex.Clear();
    }
}