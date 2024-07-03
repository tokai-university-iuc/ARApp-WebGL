using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s01_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.50000000f, -0.86602540f, 0.00000000f),
        new Vector3(-1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(-0.50000000f, 0.86602540f, 0.00000000f),
        new Vector3(0.50000000f, 0.86602540f, 0.00000000f),
        new Vector3(1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(0.50000000f, -0.86602540f, 0.00000000f),
        new Vector3(-0.50000000f, -0.28867513f, -0.81649658f),
        new Vector3(0.00000000f, 0.57735027f, -0.81649658f),
        new Vector3(0.50000000f, -0.28867513f, -0.81649658f),
        new Vector3(0.00000000f, -0.57735027f, 0.81649658f),
        new Vector3(0.50000000f, 0.28867513f, 0.81649658f),
        new Vector3(-0.50000000f, 0.28867513f, 0.81649658f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 6 },
        new int[] { 0, 6 },
        new int[] { 6, 8 },
        new int[] { 8, 5 },
        new int[] { 0, 5 },
        new int[] { 5, 9 },
        new int[] { 0, 9 },
        new int[] { 9, 11 },
        new int[] { 11, 1 },
        new int[] { 1, 2 },
        new int[] { 2, 7 },
        new int[] { 6, 7 },
        new int[] { 11, 2 },
        new int[] { 2, 3 },
        new int[] { 3, 7 },
        new int[] { 11, 10 },
        new int[] { 10, 3 },
        new int[] { 3, 4 },
        new int[] { 4, 8 },
        new int[] { 8, 7 },
        new int[] { 10, 4 },
        new int[] { 4, 5 },
        new int[] { 10, 9 }
    };

    // Start is called before the first frame update
    void Start()
    {
        edgeObjects = new GameObject[edges.Length];
        localCylinderPos = new Vector3[edges.Length];

        //�ӃI�u�W�F�N�g�𐶐�
        for (int i = 0; i < edges.Length; i++)
        {
            int vertex1 = edges[i][0];
            int vertex2 = edges[i][1];

            Vector3 worldPos1 = transform.TransformPoint(localVertexPositions[vertex1]);
            Vector3 worldPos2 = transform.TransformPoint(localVertexPositions[vertex2]);
            Vector3 midPoint = (worldPos1 + worldPos2) / 2;
            float distance = Vector3.Distance(worldPos1, worldPos2);

            edgeObjects[i] = Instantiate(cylinder, midPoint, Quaternion.identity);
            edgeObjects[i].GetComponent<Crash>().SetUIText(countText);

            //cylinder�̌���
            Vector3 direction = worldPos2 - worldPos1;
            edgeObjects[i].transform.up = direction;

            //�X�P�[���ݒ�
            edgeObjects[i].transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);

            //���[���h���W���烍�[�J�����W�ɖ߂�
            Vector3 localPos = transform.InverseTransformPoint(edgeObjects[i].transform.position);
            localCylinderPos[i] = localPos;

            //�q�I�u�W�F�N�g�ɐݒ�
            edgeObjects[i].transform.SetParent(transform);

            //�}�e���A���ݒ�
            Renderer cylinderRenderer = edgeObjects[i].GetComponent<Renderer>();
            cylinderRenderer.material = startMaterial;
        }
    }

    public void Edge_Reset()
    {
        for (int i = 0; i < edgeObjects.Length; i++)
        {
            // �e�ӃI�u�W�F�N�g��Crash�R���|�[�l���g��FlagReset���\�b�h��ǉ�
            edgeObjects[i].GetComponent<Crash>().FlagReset();

            Renderer reset_Rederer = edgeObjects[i].GetComponent<Renderer>();
            reset_Rederer.material = startMaterial;
        }
    }
}
