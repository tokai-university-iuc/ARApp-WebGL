using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s12L_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.37210275f, -0.37210283f, 0.85033943f),
        new Vector3(-0.37210275f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, -0.37210283f, 0.85033943f),
        new Vector3(-0.68440375f, -0.68440382f, 0.25135842f),
        new Vector3(-0.88671195f, 0.00000000f, 0.46232019f),
        new Vector3(-0.68440375f, 0.68440383f, 0.25135842f),
        new Vector3(0.00000008f, 0.88671203f, 0.46232019f),
        new Vector3(0.68440391f, 0.68440383f, 0.25135842f),
        new Vector3(0.88671211f, 0.00000000f, 0.46232019f),
        new Vector3(0.68440391f, -0.68440382f, 0.25135842f),
        new Vector3(0.00000008f, -0.88671202f, 0.46232019f),
        new Vector3(-0.74420553f, -0.48209560f, -0.46232023f),
        new Vector3(-0.94651373f, 0.20230822f, -0.25135846f),
        new Vector3(-0.48209559f, 0.74420581f, -0.46232023f),
        new Vector3(0.20230824f, 0.94651401f, -0.25135846f),
        new Vector3(0.74420550f, 0.48209553f, -0.46232032f),
        new Vector3(0.94651368f, -0.20230824f, -0.25135852f),
        new Vector3(0.48209586f, -0.74420614f, -0.46232018f),
        new Vector3(-0.20230823f, -0.94651406f, -0.25135841f),
        new Vector3(-0.51460872f, 0.10999275f, -0.85033925f),
        new Vector3(0.10999269f, 0.51461005f, -0.85033999f),
        new Vector3(0.51460748f, -0.10999294f, -0.85033865f),
        new Vector3(-0.10999264f, -0.51460945f, -0.85033947f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 }, new int[] { 1, 5 },
        new int[] { 0, 5 }, new int[] { 5, 4 },
        new int[] { 0, 4 }, new int[] { 4, 11 },
        new int[] { 0, 11 }, new int[] { 11, 3 },
        new int[] { 0, 3 }, new int[] { 3, 2 }, new int[] { 2, 1 },
        new int[] { 2, 7 },
        new int[] { 1, 7 }, new int[] { 7, 6 },
        new int[] { 1, 6 }, new int[] { 6, 5 },
        new int[] { 3, 9 },
        new int[] { 2, 9 }, new int[] { 9, 8 },
        new int[] { 2, 8 }, new int[] { 8, 7 },
        new int[] { 11, 10 },
        new int[] { 3, 10 }, new int[] { 10, 9 },
        new int[] { 5, 13 }, new int[] { 13, 12 },
        new int[] { 4, 12 }, new int[] { 12, 19 },
        new int[] { 4, 19 }, new int[] { 19, 11 },
        new int[] { 6, 13 },
        new int[] { 7, 15 }, new int[] { 15, 14 },
        new int[] { 6, 14 }, new int[] { 14, 13 },
        new int[] { 8, 15 },
        new int[] { 8, 16 }, new int[] { 16, 15 },
        new int[] { 9, 17 }, new int[] { 17, 16 },
        new int[] { 10, 17 },
        new int[] { 19, 18 },
        new int[] { 10, 18 }, new int[] { 18, 17 },
        new int[] { 13, 20 },
        new int[] { 12, 20 },
        new int[] { 20, 23 },
        new int[] { 12, 23 }, new int[] { 23, 19 },
        new int[] { 14, 20 },
        new int[] { 15, 21 },
        new int[] { 14, 21 }, new int[] { 21, 20 },
        new int[] { 16, 21 },
        new int[] { 16, 22 }, new int[] { 22, 21 },
        new int[] { 17, 22 },
        new int[] { 18, 22 },
        new int[] { 18, 23 }, new int[] { 23, 22 },
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
