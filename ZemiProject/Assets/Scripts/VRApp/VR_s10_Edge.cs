using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s10_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.21573941f, -0.52084100f, 0.82594259f),
        new Vector3(-0.52084100f, -0.21573941f, 0.82594259f),
        new Vector3(-0.52084100f, 0.21573941f, 0.82594259f),
        new Vector3(-0.21573941f, 0.52084100f, 0.82594259f),
        new Vector3(0.21573941f, 0.52084100f, 0.82594259f),
        new Vector3(0.52084100f, 0.21573941f, 0.82594259f),
        new Vector3(0.52084100f, -0.21573941f, 0.82594259f),
        new Vector3(0.21573941f, -0.52084100f, 0.82594259f),
        new Vector3(-0.21573941f, -0.82594259f, 0.52084100f),
        new Vector3(-0.82594259f, -0.21573941f, 0.52084100f),
        new Vector3(-0.82594259f, 0.21573941f, 0.52084100f),
        new Vector3(-0.21573941f, 0.82594259f, 0.52084100f),
        new Vector3(0.21573941f, 0.82594259f, 0.52084100f),
        new Vector3(0.82594259f, 0.21573941f, 0.52084100f),
        new Vector3(0.82594259f, -0.21573941f, 0.52084100f),
        new Vector3(0.21573941f, -0.82594259f, 0.52084100f),
        new Vector3(-0.52084100f, -0.82594259f, 0.21573941f),
        new Vector3(-0.82594259f, -0.52084100f, 0.21573941f),
        new Vector3(-0.82594259f, 0.52084100f, 0.21573941f),
        new Vector3(-0.52084100f, 0.82594259f, 0.21573941f),
        new Vector3(0.52084100f, 0.82594259f, 0.21573941f),
        new Vector3(0.82594259f, 0.52084100f, 0.21573941f),
        new Vector3(0.82594259f, -0.52084100f, 0.21573941f),
        new Vector3(0.52084100f, -0.82594259f, 0.21573941f),
        new Vector3(0.52084100f, -0.82594259f, -0.21573941f),
        new Vector3(0.21573940f, -0.82594259f, -0.52084100f),
        new Vector3(-0.21573941f, -0.82594259f, -0.52084100f),
        new Vector3(-0.52084100f, -0.82594259f, -0.21573941f),
        new Vector3(-0.82594259f, -0.52084100f, -0.21573941f),
        new Vector3(-0.82594259f, -0.21573941f, -0.52084100f),
        new Vector3(-0.82594259f, 0.21573941f, -0.52084100f),
        new Vector3(-0.82594259f, 0.52084100f, -0.21573941f),
        new Vector3(-0.52084100f, 0.82594259f, -0.21573941f),
        new Vector3(-0.21573941f, 0.82594259f, -0.52084100f),
        new Vector3(0.21573941f, 0.82594259f, -0.52084100f),
        new Vector3(0.52084100f, 0.82594259f, -0.21573941f),
        new Vector3(0.82594259f, 0.52084100f, -0.21573941f),
        new Vector3(0.82594259f, 0.21573941f, -0.52084100f),
        new Vector3(0.82594259f, -0.21573941f, -0.52084100f),
        new Vector3(0.82594259f, -0.52084100f, -0.21573941f),
        new Vector3(0.52084100f, -0.21573941f, -0.82594259f),
        new Vector3(0.52084100f, 0.21573941f, -0.82594259f),
        new Vector3(0.21573941f, 0.52084100f, -0.82594259f),
        new Vector3(-0.21573941f, 0.52084100f, -0.82594259f),
        new Vector3(-0.52084100f, 0.21573941f, -0.82594259f),
        new Vector3(-0.52084100f, -0.21573941f, -0.82594259f),
        new Vector3(-0.21573941f, -0.52084100f, -0.82594259f),
        new Vector3(0.21573940f, -0.52084100f, -0.82594259f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },   new int[] { 1, 9 },   new int[] { 9, 17 },
        new int[] { 16, 17 }, new int[] { 8, 16 },  new int[] { 0, 8 },
        new int[] { 8, 15 },  new int[] { 7, 15 },  new int[] { 0, 7 },
        new int[] { 7, 6 },   new int[] { 6, 5 },   new int[] { 5, 4 },
        new int[] { 4, 3 },   new int[] { 3, 2 },   new int[] { 2, 1 },
        new int[] { 2, 10 },  new int[] { 10, 9 },  new int[] { 3, 11 },
        new int[] { 11, 19 }, new int[] { 19, 18 }, new int[] { 18, 10 },
        new int[] { 4, 12 },  new int[] { 12, 11 }, new int[] { 5, 13 },
        new int[] { 13, 21 }, new int[] { 21, 20 }, new int[] { 20, 12 },
        new int[] { 6, 14 },  new int[] { 14, 13 }, new int[] { 15, 23 },
        new int[] { 22, 23 }, new int[] { 22, 14 }, new int[] { 16, 27 },
        new int[] { 27, 26 }, new int[] { 26, 25 }, new int[] { 25, 24 },
        new int[] { 24, 23 }, new int[] { 18, 31 }, new int[] { 31, 30 },
        new int[] { 30, 29 }, new int[] { 29, 28 }, new int[] { 28, 17 },
        new int[] { 20, 35 }, new int[] { 35, 34 }, new int[] { 34, 33 },
        new int[] { 33, 32 }, new int[] { 32, 19 }, new int[] { 22, 39 },
        new int[] { 38, 39 }, new int[] { 37, 38 }, new int[] { 37, 36 },
        new int[] { 36, 21 }, new int[] { 28, 27 }, new int[] { 32, 31 },
        new int[] { 36, 35 }, new int[] { 24, 39 }, new int[] { 25, 47 },
        new int[] { 47, 40 }, new int[] { 40, 38 }, new int[] { 26, 46 },
        new int[] { 46, 47 }, new int[] { 29, 45 }, new int[] { 45, 46 },
        new int[] { 44, 45 }, new int[] { 30, 44 }, new int[] { 33, 43 },
        new int[] { 43, 44 }, new int[] { 34, 42 }, new int[] { 42, 43 },
        new int[] { 37, 41 }, new int[] { 41, 42 }, new int[] { 40, 41 },
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
