using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s06_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.20177411f, -0.27771823f, 0.93923362f),
        new Vector3(-0.40354821f, -0.55543646f, 0.72707577f),
        new Vector3(-0.20177411f, -0.83315470f, 0.51491792f),
        new Vector3(0.20177411f, -0.83315470f, 0.51491792f),
        new Vector3(0.40354821f, -0.55543646f, 0.72707577f),
        new Vector3(0.20177411f, -0.27771823f, 0.93923362f),
        new Vector3(-0.32647736f, 0.10607893f, 0.93923362f),
        new Vector3(-0.65295472f, 0.21215785f, 0.72707577f),
        new Vector3(-0.85472883f, -0.06556038f, 0.51491792f),
        new Vector3(-0.73002557f, -0.44935754f, 0.51491792f),
        new Vector3(-0.73002557f, -0.66151539f, 0.17163931f),
        new Vector3(-0.40354821f, -0.89871508f, 0.17163931f),
        new Vector3(-0.20177411f, -0.96427546f, -0.17163931f),
        new Vector3(0.20177411f, -0.96427546f, -0.17163931f),
        new Vector3(0.40354821f, -0.89871508f, 0.17163931f),
        new Vector3(0.73002557f, -0.66151539f, 0.17163931f),
        new Vector3(0.73002557f, -0.44935754f, 0.51491792f),
        new Vector3(0.85472883f, -0.06556038f, 0.51491792f),
        new Vector3(0.65295472f, 0.21215785f, 0.72707577f),
        new Vector3(0.32647736f, 0.10607893f, 0.93923362f),
        new Vector3(-0.00000000f, 0.34327861f, 0.93923362f),
        new Vector3(-0.65295472f, 0.55543646f, 0.51491792f),
        new Vector3(-0.85472883f, 0.48987608f, 0.17163931f),
        new Vector3(-0.97943209f, 0.10607893f, 0.17163931f),
        new Vector3(-0.97943209f, -0.10607892f, -0.17163931f),
        new Vector3(-0.85472883f, -0.48987608f, -0.17163931f),
        new Vector3(-0.65295472f, -0.55543646f, -0.51491792f),
        new Vector3(-0.32647736f, -0.79263615f, -0.51491792f),
        new Vector3(-0.00000000f, -0.68655723f, -0.72707577f),
        new Vector3(0.32647736f, -0.79263615f, -0.51491792f),
        new Vector3(0.65295472f, -0.55543646f, -0.51491792f),
        new Vector3(0.85472883f, -0.48987608f, -0.17163931f),
        new Vector3(0.97943209f, -0.10607893f, -0.17163931f),
        new Vector3(0.97943209f, 0.10607893f, 0.17163931f),
        new Vector3(0.85472883f, 0.48987608f, 0.17163931f),
        new Vector3(0.65295472f, 0.55543646f, 0.51491792f),
        new Vector3(0.32647736f, 0.79263615f, 0.51491792f),
        new Vector3(0.00000000f, 0.68655723f, 0.72707577f),
        new Vector3(-0.32647736f, 0.79263615f, 0.51491792f),
        new Vector3(-0.73002557f, 0.66151539f, -0.17163931f),
        new Vector3(-0.73002557f, 0.44935754f, -0.51491792f),
        new Vector3(-0.85472883f, 0.06556038f, -0.51491792f),
        new Vector3(-0.65295472f, -0.21215785f, -0.72707577f),
        new Vector3(-0.32647736f, -0.10607892f, -0.93923362f),
        new Vector3(0.00000000f, -0.34327861f, -0.93923362f),
        new Vector3(0.32647736f, -0.10607892f, -0.93923362f),
        new Vector3(0.65295472f, -0.21215785f, -0.72707577f),
        new Vector3(0.85472883f, 0.06556038f, -0.51491792f),
        new Vector3(0.73002557f, 0.44935754f, -0.51491792f),
        new Vector3(0.73002557f, 0.66151539f, -0.17163931f),
        new Vector3(0.40354821f, 0.89871508f, -0.17163931f),
        new Vector3(0.20177411f, 0.96427546f, 0.17163931f),
        new Vector3(-0.20177411f, 0.96427546f, 0.17163931f),
        new Vector3(-0.40354821f, 0.89871508f, -0.17163931f),
        new Vector3(-0.20177411f, 0.83315469f, -0.51491792f),
        new Vector3(-0.40354821f, 0.55543646f, -0.72707577f),
        new Vector3(-0.20177411f, 0.27771823f, -0.93923362f),
        new Vector3(0.20177410f, 0.27771823f, -0.93923362f),
        new Vector3(0.40354821f, 0.55543646f, -0.72707577f),
        new Vector3(0.20177411f, 0.83315469f, -0.51491792f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 5 },
        new int[] { 5, 19 },
        new int[] { 19, 20 },
        new int[] { 20, 6 },
        new int[] { 0, 6 },
        new int[] { 6, 7 },
        new int[] { 8, 7 },
        new int[] { 9, 8 },
        new int[] { 9, 1 },
        new int[] { 0, 1 },
        new int[] { 1, 2 },
        new int[] { 2, 3 },
        new int[] { 3, 4 },
        new int[] { 4, 5 },
        new int[] { 9, 10 },
        new int[] { 10, 11 },
        new int[] { 11, 2 },
        new int[] { 13, 12 },
        new int[] { 13, 14 },
        new int[] { 14, 3 },
        new int[] { 14, 15 },
        new int[] { 15, 16 },
        new int[] { 16, 4 },
        new int[] { 16, 17 },
        new int[] { 17, 18 },
        new int[] { 18, 19 },
        new int[] { 37, 20 },
        new int[] { 37, 38 },
        new int[] { 38, 21 },
        new int[] { 7, 21 },
        new int[] { 21, 22 },
        new int[] { 22, 23 },
        new int[] { 23, 8 },
        new int[] { 23, 24 },
        new int[] { 24, 25 },
        new int[] { 25, 10 },
        new int[] { 25, 26 },
        new int[] { 26, 27 },
        new int[] { 27, 12 },
        new int[] { 11, 12 },
        new int[] { 27, 28 },
        new int[] { 28, 29 },
        new int[] { 29, 13 },
        new int[] { 29, 30 },
        new int[] { 30, 31 },
        new int[] { 15, 31 },
        new int[] { 32, 31 },
        new int[] { 33, 32 },
        new int[] { 17, 33 },
        new int[] { 33, 34 },
        new int[] { 34, 35 },
        new int[] { 35, 18 },
        new int[] { 35, 36 },
        new int[] { 36, 37 },
        new int[] { 38, 52 },
        new int[] { 52, 53 },
        new int[] { 53, 39 },
        new int[] { 39, 22 },
        new int[] { 39, 40 },
        new int[] { 40, 41 },
        new int[] { 41, 24 },
        new int[] { 41, 42 },
        new int[] { 42, 26 },
        new int[] { 42, 43 },
        new int[] { 43, 44 },
        new int[] { 44, 28 },
        new int[] { 44, 45 },
        new int[] { 45, 46 },
        new int[] { 46, 30 },
        new int[] { 46, 47 },
        new int[] { 47, 32 },
        new int[] { 47, 48 },
        new int[] { 48, 49 },
        new int[] { 49, 34 },
        new int[] { 49, 50 },
        new int[] { 50, 51 },
        new int[] { 51, 36 },
        new int[] { 51, 52 },
        new int[] { 53, 54 },
        new int[] { 54, 55 },
        new int[] { 55, 40 },
        new int[] { 55, 56 },
        new int[] { 56, 43 },
        new int[] { 56, 57 },
        new int[] { 57, 45 },
        new int[] { 57, 58 },
        new int[] { 58, 48 },
        new int[] { 58, 59 },
        new int[] { 59, 50 },
        new int[] { 59, 54 }
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
