using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s11_Edge : MonoBehaviour
{
    public GameObject cylinder;
    public Material startMaterial;

    GameObject[] edgeObjects;
    Vector3[] localCylinderPos;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.13149606f, -0.40470333f, 0.90494412f),
        new Vector3(-0.34426119f, -0.25012042f, 0.90494412f),
        new Vector3(-0.42553024f, -0.00000001f, 0.90494412f),
        new Vector3(-0.34426119f, 0.25012040f, 0.90494412f),
        new Vector3(-0.13149606f, 0.40470332f, 0.90494412f),
        new Vector3(0.13149611f, 0.40470332f, 0.90494412f),
        new Vector3(0.34426124f, 0.25012040f, 0.90494412f),
        new Vector3(0.42553028f, -0.00000001f, 0.90494412f),
        new Vector3(0.34426124f, -0.25012042f, 0.90494412f),
        new Vector3(0.13149611f, -0.40470333f, 0.90494412f),
        new Vector3(-0.13149606f, -0.62841783f, 0.76668096f),
        new Vector3(-0.34426119f, -0.69754941f, 0.62841780f),
        new Vector3(-0.42553024f, -0.80940666f, 0.40470330f),
        new Vector3(-0.34426119f, -0.92126391f, 0.18098881f),
        new Vector3(-0.13149606f, -0.99039549f, 0.04272564f),
        new Vector3(0.13149611f, -0.99039549f, 0.04272564f),
        new Vector3(0.34426124f, -0.92126391f, 0.18098881f),
        new Vector3(0.42553028f, -0.80940666f, 0.40470330f),
        new Vector3(0.34426124f, -0.69754941f, 0.62841780f),
        new Vector3(0.13149611f, -0.62841783f, 0.76668096f),
        new Vector3(-0.55702632f, -0.54296650f, 0.62841780f),
        new Vector3(-0.55702632f, -0.31925200f, 0.76668096f),
        new Vector3(-0.63829537f, -0.06913159f, 0.76668096f),
        new Vector3(-0.76979145f, 0.11185724f, 0.62841780f),
        new Vector3(-0.90128753f, 0.15458291f, 0.40470330f),
        new Vector3(-0.98255658f, 0.04272566f, 0.18098880f),
        new Vector3(-0.98255658f, -0.18098884f, 0.04272564f),
        new Vector3(-0.90128753f, -0.43110925f, 0.04272564f),
        new Vector3(-0.76979145f, -0.61209808f, 0.18098881f),
        new Vector3(-0.63829537f, -0.65482375f, 0.40470330f),
        new Vector3(-0.82001848f, 0.54296648f, 0.18098881f),
        new Vector3(-0.82001848f, 0.40470332f, 0.40470330f),
        new Vector3(-0.68852240f, 0.36197765f, 0.62841780f),
        new Vector3(-0.47575727f, 0.43110923f, 0.76668096f),
        new Vector3(-0.26299214f, 0.58569215f, 0.76668096f),
        new Vector3(-0.13149606f, 0.76668098f, 0.62841780f),
        new Vector3(-0.13149606f, 0.90494414f, 0.40470330f),
        new Vector3(-0.26299214f, 0.94766981f, 0.18098881f),
        new Vector3(-0.47575727f, 0.87853823f, 0.04272564f),
        new Vector3(-0.68852240f, 0.72395531f, 0.04272564f),
        new Vector3(0.47575732f, 0.87853821f, 0.04272564f),
        new Vector3(0.26299219f, 0.94766979f, 0.18098880f),
        new Vector3(0.13149611f, 0.90494413f, 0.40470330f),
        new Vector3(0.13149611f, 0.76668098f, 0.62841780f),
        new Vector3(0.26299219f, 0.58569215f, 0.76668096f),
        new Vector3(0.47575732f, 0.43110923f, 0.76668096f),
        new Vector3(0.68852245f, 0.36197765f, 0.62841780f),
        new Vector3(0.82001853f, 0.40470330f, 0.40470331f),
        new Vector3(0.82001853f, 0.54296646f, 0.18098881f),
        new Vector3(0.68852245f, 0.72395529f, 0.04272564f),
        new Vector3(0.63829541f, -0.65482375f, 0.40470330f),
        new Vector3(0.76979150f, -0.61209808f, 0.18098881f),
        new Vector3(0.90128758f, -0.43110925f, 0.04272564f),
        new Vector3(0.98255663f, -0.18098884f, 0.04272564f),
        new Vector3(0.98255647f, 0.04272561f, 0.18098879f),
        new Vector3(0.90128750f, 0.15458288f, 0.40470331f),
        new Vector3(0.76979150f, 0.11185724f, 0.62841780f),
        new Vector3(0.63829541f, -0.06913159f, 0.76668096f),
        new Vector3(0.55702637f, -0.31925200f, 0.76668096f),
        new Vector3(0.55702637f, -0.54296650f, 0.62841780f),
        new Vector3(-0.47575727f, -0.87853824f, -0.04272569f),
        new Vector3(-0.68852240f, -0.72395533f, -0.04272569f),
        new Vector3(-0.82001848f, -0.54296650f, -0.18098886f),
        new Vector3(-0.82001848f, -0.40470333f, -0.40470335f),
        new Vector3(-0.68852240f, -0.36197767f, -0.62841785f),
        new Vector3(-0.47575727f, -0.43110925f, -0.76668101f),
        new Vector3(-0.26299214f, -0.58569216f, -0.76668101f),
        new Vector3(-0.13149605f, -0.76668099f, -0.62841784f),
        new Vector3(-0.13149606f, -0.90494416f, -0.40470335f),
        new Vector3(-0.26299214f, -0.94766982f, -0.18098885f),
        new Vector3(-0.90128753f, -0.15458292f, -0.40470335f),
        new Vector3(-0.98255658f, -0.04272567f, -0.18098885f),
        new Vector3(-0.98255658f, 0.18098882f, -0.04272569f),
        new Vector3(-0.90128753f, 0.43110923f, -0.04272569f),
        new Vector3(-0.76979145f, 0.61209806f, -0.18098885f),
        new Vector3(-0.63829536f, 0.65482373f, -0.40470335f),
        new Vector3(-0.55702632f, 0.54296648f, -0.62841785f),
        new Vector3(-0.55702632f, 0.31925199f, -0.76668101f),
        new Vector3(-0.63829537f, 0.06913157f, -0.76668101f),
        new Vector3(-0.76979145f, -0.11185726f, -0.62841785f),
        new Vector3(-0.13149606f, 0.62841781f, -0.76668101f),
        new Vector3(-0.34426119f, 0.69754940f, -0.62841785f),
        new Vector3(-0.42553023f, 0.80940665f, -0.40470335f),
        new Vector3(-0.34426119f, 0.92126389f, -0.18098885f),
        new Vector3(-0.13149606f, 0.99039547f, -0.04272569f),
        new Vector3(0.13149611f, 0.99039546f, -0.04272569f),
        new Vector3(0.34426124f, 0.92126387f, -0.18098886f),
        new Vector3(0.42553028f, 0.80940661f, -0.40470335f),
        new Vector3(0.34426124f, 0.69754939f, -0.62841785f),
        new Vector3(0.13149611f, 0.62841781f, -0.76668101f),
        new Vector3(0.76979153f, -0.11185725f, -0.62841782f),
        new Vector3(0.63829541f, 0.06913155f, -0.76668097f),
        new Vector3(0.55702634f, 0.31925197f, -0.76668097f),
        new Vector3(0.55702635f, 0.54296649f, -0.62841782f),
        new Vector3(0.63829541f, 0.65482370f, -0.40470335f),
        new Vector3(0.76979149f, 0.61209803f, -0.18098885f),
        new Vector3(0.90128750f, 0.43110919f, -0.04272571f),
        new Vector3(0.98255648f, 0.18098877f, -0.04272572f),
        new Vector3(0.98255640f, -0.04272561f, -0.18098874f),
        new Vector3(0.90128711f, -0.15458281f, -0.40470320f),
        new Vector3(0.26299219f, -0.94766982f, -0.18098885f),
        new Vector3(0.13149611f, -0.90494416f, -0.40470335f),
        new Vector3(0.13149611f, -0.76668098f, -0.62841784f),
        new Vector3(0.26299219f, -0.58569215f, -0.76668099f),
        new Vector3(0.47575682f, -0.43110886f, -0.76668009f),
        new Vector3(0.68852237f, -0.36197738f, -0.62841730f),
        new Vector3(0.82001805f, -0.40470325f, -0.40470322f),
        new Vector3(0.82001853f, -0.54296650f, -0.18098885f),
        new Vector3(0.68852245f, -0.72395533f, -0.04272569f),
        new Vector3(0.47575732f, -0.87853824f, -0.04272569f),
        new Vector3(-0.13149606f, -0.40470333f, -0.90494417f),
        new Vector3(-0.34426119f, -0.25012042f, -0.90494417f),
        new Vector3(-0.42553024f, -0.00000000f, -0.90494417f),
        new Vector3(-0.34426119f, 0.25012040f, -0.90494417f),
        new Vector3(-0.13149606f, 0.40470332f, -0.90494417f),
        new Vector3(0.13149611f, 0.40470332f, -0.90494417f),
        new Vector3(0.34426120f, 0.25012038f, -0.90494414f),
        new Vector3(0.42553027f, -0.00000003f, -0.90494414f),
        new Vector3(0.34426060f, -0.25012001f, -0.90494332f),
        new Vector3(0.13149611f, -0.40470332f, -0.90494416f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 }, new int[] { 1, 21 }, new int[] { 21, 20 }, new int[] { 20, 11 }, new int[] { 11, 10 },
        new int[] { 0, 10 }, new int[] { 10, 19 }, new int[] { 19, 9 },
        new int[] { 0, 9 }, new int[] { 9, 8 }, new int[] { 8, 7 }, new int[] { 7, 6 }, new int[] { 6, 5 }, new int[] { 5, 4 }, new int[] { 4, 3 }, new int[] { 3, 2 }, new int[] { 2, 1 },
        new int[] { 2, 22 }, new int[] { 22, 21 },
        new int[] { 3, 33 }, new int[] { 33, 32 }, new int[] { 32, 23 }, new int[] { 23, 22 },
        new int[] { 4, 34 }, new int[] { 34, 33 },
        new int[] { 5, 44 }, new int[] { 44, 43 }, new int[] { 43, 35 }, new int[] { 35, 34 },
        new int[] { 6, 45 }, new int[] { 45, 44 },
        new int[] { 7, 57 }, new int[] { 57, 56 }, new int[] { 56, 46 }, new int[] { 46, 45 },
        new int[] { 8, 58 }, new int[] { 58, 57 },
        new int[] { 19, 18 }, new int[] { 18, 59 }, new int[] { 59, 58 },
        new int[] { 11, 12 }, new int[] { 12, 13 }, new int[] { 13, 14 }, new int[] { 14, 15 }, new int[] { 15, 16 }, new int[] { 16, 17 }, new int[] { 17, 18 },
        new int[] { 20, 29 }, new int[] { 29, 12 },
        new int[] { 29, 28 }, new int[] { 28, 61 }, new int[] { 61, 60 }, new int[] { 60, 13 },
        new int[] { 60, 69 }, new int[] { 69, 14 },
        new int[] { 69, 68 }, new int[] { 68, 101 }, new int[] { 101, 100 }, new int[] { 100, 15 },
        new int[] { 100, 109 }, new int[] { 109, 16 },
        new int[] { 109, 108 }, new int[] { 108, 51 }, new int[] { 51, 50 }, new int[] { 50, 17 },
        new int[] { 50, 59 },
        new int[] { 23, 24 }, new int[] { 24, 25 }, new int[] { 25, 26 }, new int[] { 26, 27 }, new int[] { 27, 28 },
        new int[] { 32, 31 }, new int[] { 31, 24 },
        new int[] { 31, 30 }, new int[] { 30, 73 }, new int[] { 73, 72 }, new int[] { 72, 25 },
        new int[] { 72, 71 }, new int[] { 71, 26 },
        new int[] { 71, 70 }, new int[] { 70, 63 }, new int[] { 63, 62 }, new int[] { 62, 27 },
        new int[] { 62, 61 },
        new int[] { 35, 36 }, new int[] { 36, 37 }, new int[] { 37, 38 }, new int[] { 38, 39 },
        new int[] { 30, 39 }, new int[] { 39, 74 }, new int[] { 74, 73 },
        new int[] { 43, 42 }, new int[] { 42, 36 },
        new int[] { 42, 41 }, new int[] { 41, 85 }, new int[] { 85, 84 }, new int[] { 84, 37 },
        new int[] { 84, 83 }, new int[] { 83, 38 },
        new int[] { 83, 82 }, new int[] { 82, 75 }, new int[] { 75, 74 },
        new int[] { 40, 41 }, new int[] { 46, 47 }, new int[] { 47, 48 }, new int[] { 48, 49 },
        new int[] { 40, 49 }, new int[] { 49, 95 }, new int[] { 95, 94 }, new int[] { 94, 87 }, new int[] { 87, 86 },
        new int[] { 40, 86 }, new int[] { 86, 85 },
        new int[] { 56, 55 }, new int[] { 55, 47 },
        new int[] { 55, 54 }, new int[] { 54, 97 }, new int[] { 97, 96 }, new int[] { 96, 48 },
        new int[] { 96, 95 },
        new int[] { 51, 52 }, new int[] { 52, 53 },new int[] { 53, 54 },
        new int[] { 108, 107 }, new int[] { 107, 52 },
        new int[] { 107, 106 }, new int[] { 106, 99 },
        new int[] { 53, 98 }, new int[] { 98, 97 },
        new int[] { 63, 64 }, new int[] { 64, 65 }, new int[] { 65, 66 }, new int[] { 66, 67 }, new int[] { 67, 68 },
        new int[] { 70, 79 }, new int[] { 79, 64 },
        new int[] { 79, 78 }, new int[] { 78, 112 }, new int[] { 112, 111}, new int[] { 111, 65 },
        new int[] { 111, 110}, new int[] { 110, 66 },
        new int[] { 110, 119 }, new int[] { 119, 103 }, new int[] { 103, 102 }, new int[] { 102, 67 },
        new int[] { 102, 101 },
        new int[] { 75, 76 }, new int[] { 76, 77 }, new int[] { 77, 78 },
        new int[] { 82, 81 }, new int[] { 81, 76 },
        new int[] { 81, 80 }, new int[] { 80, 114 }, new int[] { 114, 113 }, new int[] { 113, 77 },
        new int[] { 113, 112 },
        new int[] { 87, 88 }, new [] { 88, 89 },
        new int[] { 80, 89 }, new int[] { 89, 115 }, new int[] { 115, 114 },
        new int[] { 94, 93 }, new int[] { 93, 88 },
        new int[] { 93, 92 }, new int[] { 92, 116 }, new int[] { 116, 115 },
        new int[] { 90, 91 }, new int[] { 91, 92 }, new int[] { 98, 99 },
        new int[] { 90, 99 }, new int[] { 106, 105 },
        new int[] { 90, 105 }, new int[] { 105, 104 }, new int[] { 104, 118 }, new int[] { 118, 117 }, new int[] { 117, 91 },
        new int[] { 117, 116 },
        new int[] { 103, 104 },
        new int[] { 119, 118 },
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
