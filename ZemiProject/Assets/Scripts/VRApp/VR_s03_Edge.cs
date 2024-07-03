using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s03_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

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

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 6 },
        new int[] { 0, 6 },
        new int[] { 6, 9 },
        new int[] { 9, 11 },
        new int[] { 11, 8 },
        new int[] { 8, 5 },
        new int[] { 0, 5 },
        new int[] { 5, 4 },
        new int[] { 4, 3 },
        new int[] { 1, 2 },
        new int[] { 2, 7 },
        new int[] { 10, 7 },
        new int[] { 10, 9 },
        new int[] { 2, 3 },
        new int[] { 3, 7 },
        new int[] { 4, 8 },
        new int[] { 10, 11 }
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
