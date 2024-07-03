using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s08Edge : MonoBehaviour
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
        new Vector3(-0.35740674f, -0.86285621f, 0.35740674f),
        new Vector3(-0.86285621f, -0.35740674f, 0.35740674f),
        new Vector3(-0.86285621f, 0.35740674f, 0.35740674f),
        new Vector3(-0.35740674f, 0.86285621f, 0.35740674f),
        new Vector3(0.35740674f, 0.86285621f, 0.35740674f),
        new Vector3(0.86285621f, 0.35740674f, 0.35740674f),
        new Vector3(0.86285621f, -0.35740674f, 0.35740674f),
        new Vector3(0.35740674f, -0.86285621f, 0.35740674f),
        new Vector3(-0.35740674f, -0.86285621f, -0.35740674f),
        new Vector3(-0.86285621f, -0.35740674f, -0.35740674f),
        new Vector3(-0.86285621f, 0.35740674f, -0.35740674f),
        new Vector3(-0.35740674f, 0.86285621f, -0.35740674f),
        new Vector3(0.35740674f, 0.86285621f, -0.35740674f),
        new Vector3(0.86285621f, 0.35740674f, -0.35740674f),
        new Vector3(0.86285621f, -0.35740674f, -0.35740674f),
        new Vector3(0.35740674f, -0.86285621f, -0.35740674f),
        new Vector3(-0.35740674f, -0.35740674f, 0.86285621f),
        new Vector3(0.35740674f, -0.35740674f, 0.86285621f),
        new Vector3(0.35740674f, 0.35740674f, 0.86285621f),
        new Vector3(-0.35740674f, 0.35740674f, 0.86285621f),
        new Vector3(-0.35740674f, -0.35740674f, -0.86285621f),
        new Vector3(-0.35740674f, 0.35740674f, -0.86285621f),
        new Vector3(0.35740674f, 0.35740674f, -0.86285621f),
        new Vector3(0.35740674f, -0.35740674f, -0.86285621f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 9 },
        new int[] { 9, 8 },
        new int[] { 0, 8 },
        new int[] { 8, 15 },
        new int[] { 15, 7 },
        new int[] { 0, 7 },
        new int[] { 7, 17 },
        new int[] { 17, 16 },
        new int[] { 0, 16 },
        new int[] { 16, 1 },
        new int[] { 1, 2 },
        new int[] { 2, 10 },
        new int[] { 9, 10 },
        new int[] { 19, 16 },
        new int[] { 19, 2 },
        new int[] { 2, 3 },
        new int[] { 3, 11 },
        new int[] { 11, 10 },
        new int[] { 19, 3 },
        new int[] { 3, 4 },
        new int[] { 4, 12 },
        new int[] { 12, 11 },
        new int[] { 19, 18 },
        new int[] { 18, 4 },
        new int[] { 4, 5 },
        new int[] { 5, 13 },
        new int[] { 13, 12 },
        new int[] { 18, 5 },
        new int[] { 5, 6 },
        new int[] { 6, 14 },
        new int[] { 14, 13 },
        new int[] { 18, 17 },
        new int[] { 17, 6 },
        new int[] { 6, 7 },
        new int[] { 15, 14 },
        new int[] { 9, 20 },
        new int[] { 8, 20 },
        new int[] { 20, 23 },
        new int[] { 23, 15 },
        new int[] { 10, 21 },
        new int[] { 21, 20 },
        new int[] { 11, 21 },
        new int[] { 12, 22 },
        new int[] { 22, 21 },
        new int[] { 13, 22 },
        new int[] { 22, 23 },
        new int[] { 14, 23 }
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
