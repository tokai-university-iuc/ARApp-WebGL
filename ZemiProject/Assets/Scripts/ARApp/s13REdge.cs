using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s13REdge : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public Material initialMaterial;
    public Material selectedMaterial;
    GameObject[] cylinders;
    Vector3[] localCylinderPositions;

    //マテリアルが変更された個数をカウントする変数
    int materialChangeCount = 0;

    //カウントを表示するテキスト
    Text edgeText;

    Button edgeButton;

    //変更されたEdgeの値を保持するリスト
    List<int> changedEdgeIndex = new List<int>();

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.23192837f, -0.31922203f, 0.91886124f),
        new Vector3(0.23192838f, -0.31922203f, 0.91886124f),
        new Vector3(0.37526800f, 0.12193196f, 0.91886124f),
        new Vector3(0.00000000f, 0.39458012f, 0.91886124f),
        new Vector3(-0.37526799f, 0.12193196f, 0.91886124f),
        new Vector3(0.00000000f, -0.67692647f, 0.73605027f),
        new Vector3(0.45067192f, -0.62029668f, 0.64197064f),
        new Vector3(0.64379533f, -0.20918178f, 0.73605027f),
        new Vector3(0.72920248f, 0.23693225f, 0.64197064f),
        new Vector3(0.39788740f, 0.54764501f, 0.73605027f),
        new Vector3(0.00000000f, 0.76672886f, 0.64197064f),
        new Vector3(-0.39788739f, 0.54764501f, 0.73605027f),
        new Vector3(-0.72920248f, 0.23693225f, 0.64197064f),
        new Vector3(-0.64379532f, -0.20918178f, 0.73605027f),
        new Vector3(-0.45067191f, -0.62029668f, 0.64197064f),
        new Vector3(-0.19312341f, -0.90425562f, 0.38082092f),
        new Vector3(0.80031978f, -0.46310163f, 0.38082092f),
        new Vector3(0.68774824f, 0.61804307f, 0.38082092f),
        new Vector3(-0.37526799f, 0.84507325f, 0.38082092f),
        new Vector3(-0.91967661f, -0.09575908f, 0.38082092f),
        new Vector3(-0.60719637f, -0.76095695f, 0.22859687f),
        new Vector3(-0.30415069f, -0.95025066f, -0.06719748f),
        new Vector3(0.13819168f, -0.98812298f, 0.06719747f),
        new Vector3(0.53607907f, -0.81262670f, 0.22859687f),
        new Vector3(0.80975434f, -0.58290810f, -0.06719748f),
        new Vector3(0.98246437f, -0.17391870f, 0.06719747f),
        new Vector3(0.93851146f, 0.25872603f, 0.22859687f),
        new Vector3(0.80460641f, 0.58999364f, -0.06719748f),
        new Vector3(0.46900470f, 0.88063531f, 0.06719747f),
        new Vector3(0.04395291f, 0.97252818f, 0.22859687f),
        new Vector3(-0.31248024f, 0.94754422f, -0.06719748f),
        new Vector3(-0.69260352f, 0.71818125f, 0.06719747f),
        new Vector3(-0.91134706f, 0.34232943f, 0.22859687f),
        new Vector3(-0.99772981f, -0.00437910f, -0.06719748f),
        new Vector3(-0.89705721f, -0.43677489f, 0.06719747f),
        new Vector3(-0.66641472f, -0.70967199f, -0.22859688f),
        new Vector3(0.03659896f, -0.92392391f, -0.38082093f),
        new Vector3(0.46900470f, -0.85309878f, -0.22859688f),
        new Vector3(0.89001354f, -0.25070051f, -0.38082093f),
        new Vector3(0.95627558f, 0.18242795f, -0.22859688f),
        new Vector3(0.51345966f, 0.76898245f, -0.38082093f),
        new Vector3(0.12200611f, 0.96584547f, -0.22859688f),
        new Vector3(-0.57267800f, 0.72595780f, -0.38082093f),
        new Vector3(-0.88087165f, 0.41449736f, -0.22859688f),
        new Vector3(-0.86739415f, -0.32031586f, -0.38082093f),
        new Vector3(-0.54955655f, -0.53465945f, -0.64197064f),
        new Vector3(-0.11507021f, -0.66707442f, -0.73605028f),
        new Vector3(0.33866904f, -0.68787822f, -0.64197064f),
        new Vector3(0.59886681f, -0.31557561f, -0.73605027f),
        new Vector3(0.75886555f, 0.10952733f, -0.64197063f),
        new Vector3(0.48519026f, 0.47203794f, -0.73605027f),
        new Vector3(0.13033567f, 0.75556984f, -0.64197064f),
        new Vector3(-0.29900272f, 0.60731111f, -0.73605027f),
        new Vector3(-0.67831368f, 0.35744049f, -0.64197064f),
        new Vector3(-0.66998413f, -0.09669906f, -0.73605027f),
        new Vector3(-0.28281717f, -0.27515070f, -0.91886123f),
        new Vector3(0.17428857f, -0.35400135f, -0.91886120f),
        new Vector3(0.39053348f, 0.05636583f, -0.91886122f),
        new Vector3(0.06707433f, 0.38883729f, -0.91886104f),
        new Vector3(-0.34907938f, 0.18394906f, -0.91886143f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 4 }, new int[] { 4, 13 },
        new int[] { 0, 13 }, new int[] { 13, 14 },
        new int[] { 0, 14 }, new int[] { 14, 5 },
        new int[] { 0, 5 }, new int[] { 5, 1 },
        new int[] { 0, 1 }, new int[] { 1, 2 }, new int[] { 2, 3 }, new int[] { 3, 4 },
        new int[] { 5, 6 },
        new int[] { 1, 6 }, new int[] { 6, 7 },
        new int[] { 1, 7 }, new int[] { 7, 2 },
        new int[] { 7, 8 },
        new int[] { 2, 8 }, new int[] { 8, 9 },
        new int[] { 2, 9 }, new int[] { 9, 3 },
        new int[] { 9, 10 },
        new int[] { 3, 10 }, new int[] { 10, 11 },
        new int[] { 3, 11 }, new int[] { 11, 4 },
        new int[] { 11, 12 },
        new int[] { 4, 12 }, new int[] { 12, 13 },
        new int[] { 14, 15 },
        new int[] { 5, 15 }, new int[] { 15, 22 }, new int[] { 22, 23 }, new int[] { 23, 6 },
        new int[] { 23, 16 },
        new int[] { 6, 16 }, new int[] { 16, 7 },
        new int[] { 16, 25 }, new int[] { 25, 26 }, new int[] { 26, 8 },
        new int[] { 26, 17 },
        new int[] { 8, 17 }, new int[] { 17, 9 },
        new int[] { 17, 28 }, new int[] { 28, 29 }, new int[] { 29, 10 },
        new int[] { 29, 18 },
        new int[] { 10, 18 }, new int[] { 18, 11 },
        new int[] { 18, 31 }, new int[] { 31, 32 }, new int[] { 32, 12 },
        new int[] { 32, 19 },
        new int[] { 12, 19 }, new int[] { 19, 13 },
        new int[] { 19, 34 }, new int[] { 34, 20 }, new int[] { 20, 14 },
        new int[] { 20, 15 },
        new int[] { 20, 21 },
        new int[] { 15, 21 }, new int[] { 21, 22 },
        new int[] { 23, 24 },
        new int[] { 16, 24 },
        new int[] { 24, 25 },
        new int[] { 26, 27 },
        new int[] { 17, 27 }, new int[] { 27, 28 },
        new int[] { 29, 30 },
        new int[] { 18, 30 }, new int[] { 30, 31 },
        new int[] { 32, 33 },
        new int[] { 19, 33 }, new int[] { 33, 34 },
        new int[] { 34, 35 },
        new int[] { 20, 35 }, new int[] { 35, 21 },
        new int[] { 35, 45 }, new int[] { 45, 46 }, new int[] { 46, 36 },
        new int[] { 21 ,36 }, new int[] { 36, 22 },
        new int[] { 36, 37 },
        new int[] { 22, 37 }, new int[] { 37, 23 },
        new int[] { 37, 24 },
        new int[] { 37, 47 }, new int[] { 47, 48 }, new int[] { 48, 38 },
        new int[] { 24, 38 }, new int[] { 38, 25 },
        new int[] { 38, 39 },
        new int[] { 25, 39 }, new int[] { 39, 26 },
        new int[] { 39, 27 },
        new int[] { 27, 40 }, new int[] { 40, 28 },
        new int[] { 39, 49 }, new int[] { 49, 50 }, new int[] { 50, 40 },
        new int[] { 28, 41 }, new int[] { 41, 29 },
        new int[] { 40, 41 },
        new int[] { 41, 30 },
        new int[] { 41, 51 }, new int[] { 51, 52 }, new int[] { 52, 42 },
        new int[] { 30, 42 }, new int[] { 42, 31 },
        new int[] { 42, 43 },
        new int[] { 31, 43 }, new int[] { 43, 32 },
        new int[] { 43, 33 },
        new int[] { 42, 53 }, new int[] { 53, 54 }, new int[] { 54, 44 },
        new int[] { 33, 44 }, new int[] { 44, 34 },
        new int[] { 44, 35 },
        new int[] { 44, 45 },
        new int[] { 46, 47 },
        new int[] { 36, 47 },
        new int[] { 48, 49 },
        new int[] { 38, 49 },
        new int[] { 40, 51 },
        new int[] { 50, 51 },
        new int[] { 52, 53 },
        new int[] { 53, 43 },
        new int[] { 54, 45 },
        new int[] { 45, 55 }, new int[] { 55, 46 },
        new int[] { 54, 55 },
        new int[] { 46, 56 },
        new int[] { 55, 56 },
        new int[] { 47, 56 }, new int[] { 56, 48 },
        new int[] { 48, 57 }, new int[] { 57, 49 },
        new int[] { 56, 57 },
        new int[] { 57, 50 },
        new int[] { 57, 58 },
        new int[] { 50, 58 }, new int[] { 58, 51 },
        new int[] { 58, 52 },
        new int[] { 58, 59 },
        new int[] { 52, 59 }, new int[] { 59, 53 },
        new int[] { 54, 59 },
        new int[] { 59, 55 }
    };
    // Start is called before the first frame update
    void Start()
    {
        //カウントを表示するTextを取得
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

            //Cylinderの向き
            Vector3 direction = worldPos2 - worldPos1;
            cylinders[i].transform.up = direction;

            //適切なスケールを設定
            cylinders[i].transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);

            //最初のcylinderPrefabの色をinitialMaterialに設定
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

                        //変更されたCylinderの値をリストに追加
                        changedEdgeIndex.Add(cylinderIndex);

                        //マテリアルが変更された回数を増やす
                        materialChangeCount++;

                        //カウントを表示するテキストを更新
                        edgeText.text = "辺：" + materialChangeCount + "個";
                    }
                }
            }
        }

    }

    //ResetCanvas/EdgeButtonを押した時に実行
    void ChangeCylinder()
    {
        for (int i = 0; i < cylinders.Length; i++)
        {
            cylinders[i].GetComponent<Renderer>().material = initialMaterial;
        }

        //materialChangeCountを0にリセット
        materialChangeCount = 0;

        //カウントを表示するテキストを更新
        edgeText.text = "辺：" + materialChangeCount + "個";

        //更新されたCylinderの値を保持するリストをクリア
        changedEdgeIndex.Clear();
    }
}
