using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s05_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.28108464f, -0.67859834f, 0.67859834f),
        new Vector3(-0.67859834f, -0.28108464f, 0.67859834f),
        new Vector3(-0.67859834f, 0.28108464f, 0.67859834f),
        new Vector3(-0.28108464f, 0.67859834f, 0.67859834f),
        new Vector3(0.28108464f, 0.67859834f, 0.67859834f),
        new Vector3(0.67859834f, 0.28108464f, 0.67859834f),
        new Vector3(0.67859834f, -0.28108464f, 0.67859834f),
        new Vector3(0.28108464f, -0.67859834f, 0.67859834f),
        new Vector3(0.00000000f, -0.95968298f, 0.28108464f),
        new Vector3(-0.95968298f, 0.00000000f, 0.28108464f),
        new Vector3(0.00000000f, 0.95968298f, 0.28108464f),
        new Vector3(0.95968298f, -0.00000000f, 0.28108464f),
        new Vector3(-0.00000000f, -0.95968298f, -0.28108464f),
        new Vector3(-0.95968298f, 0.00000000f, -0.28108464f),
        new Vector3(0.00000000f, 0.95968298f, -0.28108464f),
        new Vector3(0.95968298f, -0.00000000f, -0.28108464f),
        new Vector3(-0.28108464f, -0.67859834f, -0.67859834f),
        new Vector3(-0.67859834f, -0.28108464f, -0.67859834f),
        new Vector3(-0.67859834f, 0.28108464f, -0.67859834f),
        new Vector3(-0.28108464f, 0.67859834f, -0.67859834f),
        new Vector3(0.28108464f, 0.67859834f, -0.67859834f),
        new Vector3(0.67859834f, 0.28108464f, -0.67859834f),
        new Vector3(0.67859834f, -0.28108464f, -0.67859834f),
        new Vector3(0.28108464f, -0.67859834f, -0.67859834f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 9 },
        new int[] { 9, 13 },
        new int[] { 13, 17 },
        new int[] { 17, 16 },
        new int[] { 16, 12 },
        new int[] { 8, 12 },
        new int[] { 0, 8 },
        new int[] { 8, 7 },
        new int[] { 0, 7 },
        new int[] { 7, 6 },
        new int[] { 5, 6 },
        new int[] { 5, 4 },
        new int[] { 4, 3 },
        new int[] { 3, 2 },
        new int[] { 2, 1 },
        new int[] { 2, 9 },
        new int[] { 3, 10 },
        new int[] { 10, 14 },
        new int[] { 14, 19 },
        new int[] { 19, 18 },
        new int[] { 18, 13 },
        new int[] { 10, 4 },
        new int[] { 5, 11 },
        new int[] { 11, 15 },
        new int[] { 15, 21 },
        new int[] { 21, 20 },
        new int[] { 14, 20 },
        new int[] { 6, 11 },
        new int[] { 12, 23 },
        new int[] { 22, 23 },
        new int[] { 22, 15 },
        new int[] { 23, 16 },
        new int[] { 18, 17 },
        new int[] { 20, 19 },
        new int[] { 21, 22 }
    };

    // Start is called before the first frame update
    void Start()
    {
        edgeObjects = new GameObject[edges.Length];
        localCylinderPos = new Vector3[edges.Length];

        //辺オブジェクトを生成
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

            //cylinderの向き
            Vector3 direction = worldPos2 - worldPos1;
            edgeObjects[i].transform.up = direction;

            //スケール設定
            edgeObjects[i].transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);

            //ワールド座標からローカル座標に戻す
            Vector3 localPos = transform.InverseTransformPoint(edgeObjects[i].transform.position);
            localCylinderPos[i] = localPos;

            //子オブジェクトに設定
            edgeObjects[i].transform.SetParent(transform);

            //マテリアル設定
            Renderer cylinderRenderer = edgeObjects[i].GetComponent<Renderer>();
            cylinderRenderer.material = startMaterial;
        }
    }

    public void Edge_Reset()
    {
        for (int i = 0; i < edgeObjects.Length; i++)
        {
            // 各辺オブジェクトのCrashコンポーネントにFlagResetメソッドを追加
            edgeObjects[i].GetComponent<Crash>().FlagReset();

            Renderer reset_Rederer = edgeObjects[i].GetComponent<Renderer>();
            reset_Rederer.material = startMaterial;
        }
    }
}
