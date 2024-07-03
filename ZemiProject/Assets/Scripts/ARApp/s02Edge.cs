using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s02Edge : MonoBehaviour
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
        new Vector3(-0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(-0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(-1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(-0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(-0.30901700f, 0.95105652f, 0.00000000f),
        new Vector3(0.30901699f, 0.95105652f, 0.00000000f),
        new Vector3(0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(0.00000000f, -0.85065081f, -0.52573111f),
        new Vector3(-0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.00000000f, 0.52573111f, -0.85065081f),
        new Vector3(0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, 0.85065081f, 0.52573111f),
        new Vector3(-0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, -0.52573111f, 0.85065081f),
        new Vector3(0.50000000f, -0.16245985f, 0.85065081f),
        new Vector3(0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.50000000f, -0.16245985f, 0.85065081f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 11 },
        new int[] { 11, 15 },
        new int[] { 10, 15 },
        new int[] { 0, 10 },
        new int[] { 0, 9 },
        new int[] { 10, 9 },
        new int[] { 9, 21 },
        new int[] { 21, 25 },
        new int[] { 25, 20 },
        new int[] { 20, 0 },
        new int[] { 1, 20 },
        new int[] { 1, 2 },
        new int[] { 2, 11 },
        new int[] { 20, 29 },
        new int[] { 29, 24 },
        new int[] { 24, 2 },
        new int[] { 2, 3 },
        new int[] { 3, 12 },
        new int[] { 12, 16 },
        new int[] { 16, 11 },
        new int[] { 3, 24 },
        new int[] { 3, 4 },
        new int[] { 4, 12 },
        new int[] { 24, 28 },
        new int[] { 28, 23 },
        new int[] { 23, 4 },
        new int[] { 4, 5 },
        new int[] { 5, 13 },
        new int[] { 13, 17 },
        new int[] { 17, 12 },
        new int[] { 23, 5 },
        new int[] { 5, 6 },
        new int[] { 6, 13 },
        new int[] { 23, 27 },
        new int[] { 27, 22 },
        new int[] { 22, 6 },
        new int[] { 6, 7 },
        new int[] { 7, 14 },
        new int[] { 14, 18 },
        new int[] { 18, 13 },
        new int[] { 22, 7 },
        new int[] { 7, 8 },
        new int[] { 8, 14 },
        new int[] { 22, 26 },
        new int[] { 26, 21 },
        new int[] { 21, 8 },
        new int[] { 8, 9 },
        new int[] { 10, 19 },
        new int[] { 14, 19 },
        new int[] { 15, 19 },
        new int[] { 17, 16 },
        new int[] { 17, 18 },
        new int[] { 19, 18 },
        new int[] { 15, 16 },
        new int[] { 25, 29 },
        new int[] { 26, 25 },
        new int[] { 26, 27 },
        new int[] { 28, 27 },
        new int[] { 29, 28 }
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
