using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s09Edge : MonoBehaviour
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
        new Vector3(-0.22391898f, -0.68915176f, 0.68915176f),
        new Vector3(-0.5862275f, -0.42591921f, 0.68915176f),
        new Vector3(-0.72461704f, 0.0f, 0.68915176f),
        new Vector3(-0.5862275f, 0.42591921f, 0.68915176f),
        new Vector3(-0.22391898f, 0.68915176f, 0.68915176f),
        new Vector3(0.22391898f, 0.68915176f, 0.68915176f),
        new Vector3(0.5862275f, 0.42591921f, 0.68915176f),
        new Vector3(0.72461704f, 0.0f, 0.68915176f),
        new Vector3(0.5862275f, -0.42591921f, 0.68915176f),
        new Vector3(0.22391898f, -0.68915176f, 0.68915176f),
        new Vector3(-0.22391898f, -0.92459411f, 0.30819804f),
        new Vector3(-0.81014648f, -0.4986749f, 0.30819804f),
        new Vector3(-0.94853602f, -0.07275569f, 0.30819804f),
        new Vector3(-0.72461704f, 0.61639607f, 0.30819804f),
        new Vector3(-0.36230852f, 0.87962862f, 0.30819804f),
        new Vector3(0.36230852f, 0.87962862f, 0.30819804f),
        new Vector3(0.72461704f, 0.61639607f, 0.30819804f),
        new Vector3(0.94853602f, -0.07275569f, 0.30819804f),
        new Vector3(0.81014648f, -0.4986749f, 0.30819804f),
        new Vector3(0.22391898f, -0.92459411f, 0.30819804f),
        new Vector3(0.0f, -0.99734979f, -0.07275569f),
        new Vector3(-0.5862275f, -0.80687293f, 0.07275569f),
        new Vector3(-0.94853602f, 0.30819804f, 0.07275569f),
        new Vector3(0.0f, 0.99734979f, 0.07275569f),
        new Vector3(0.94853602f, 0.30819803f, 0.07275569f),
        new Vector3(0.5862275f, -0.80687293f, 0.07275569f),
        new Vector3(-0.36230852f, -0.87962862f, -0.30819804f),
        new Vector3(-0.22391898f, -0.68915176f, -0.68915176f),
        new Vector3(-0.22391898f, -0.30819804f, -0.92459411f),
        new Vector3(-0.36230852f, 0.11772117f, -0.92459411f),
        new Vector3(-0.5862275f, 0.42591921f, -0.68915176f),
        new Vector3(-0.81014648f, 0.4986749f, -0.30819804f),
        new Vector3(0.36230852f, -0.87962862f, -0.30819804f),
        new Vector3(0.22391898f, -0.68915176f, -0.68915176f),
        new Vector3(0.22391898f, -0.30819803f, -0.92459411f),
        new Vector3(0.36230852f, 0.11772117f, -0.92459411f),
        new Vector3(0.5862275f, 0.42591921f, -0.68915176f),
        new Vector3(0.81014648f, 0.4986749f, -0.30819804f),
        new Vector3(0.0f, 0.38095372f, -0.9245941f),
        new Vector3(-0.22391898f, 0.68915176f, -0.68915176f),
        new Vector3(0.22391898f, 0.68915176f, -0.68915176f),
        new Vector3(-0.22391898f, 0.92459411f, -0.30819804f),
        new Vector3(0.22391898f, 0.92459411f, -0.30819804f),
        new Vector3(0.5862275f, 0.80687293f, -0.07275569f),
        new Vector3(-0.5862275f, 0.80687293f, -0.07275569f),
        new Vector3(0.0f, -0.38095372f, 0.92459411f),
        new Vector3(0.36230852f, -0.11772117f, 0.92459411f),
        new Vector3(0.22391898f, 0.30819804f, 0.92459411f),
        new Vector3(-0.22391898f, 0.30819804f, 0.92459411f),
        new Vector3(-0.36230852f, -0.11772117f, 0.92459411f),
        new Vector3(-0.94853602f, -0.30819804f, -0.07275569f),
        new Vector3(-0.94853602f, 0.07275569f, -0.30819804f),
        new Vector3(-0.72461704f, 0.0f, -0.68915176f),
        new Vector3(-0.5862275f, -0.42591921f, -0.68915176f),
        new Vector3(-0.72461704f, -0.61639607f, -0.30819804f),
        new Vector3(0.94853602f, -0.30819804f, -0.07275569f),
        new Vector3(0.72461704f, -0.61639607f, -0.30819804f),
        new Vector3(0.5862275f, -0.42591921f, -0.68915176f),
        new Vector3(0.72461704f, 0.0f, -0.68915176f),
        new Vector3(0.94853602f, 0.07275569f, -0.30819804f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },   new int[] { 1, 11 },  new int[] { 11, 21 },
        new int[] { 21, 10 }, new int[] { 0, 10 },  new int[] { 10, 19 },
        new int[] { 19, 9 },  new int[] { 0, 9 },   new int[] { 9, 45 },
        new int[] { 0, 45 },  new int[] { 45, 49 }, new int[] { 49, 1 },
        new int[] { 1, 2 },   new int[] { 2, 12 },  new int[] { 12, 11 },
        new int[] { 49, 2 },  new int[] { 2, 3 },   new int[] { 3, 13 },
        new int[] { 13, 22 }, new int[] { 12, 22 }, new int[] { 3, 48 },
        new int[] { 48, 4 },  new int[] { 4, 5 },   new int[] { 3, 4 },
        new int[] { 4, 14 },  new int[] { 5, 15 },  new int[] { 15, 23 },
        new int[] { 23, 14 }, new int[] { 48, 47 }, new int[] { 47, 5 },
        new int[] { 5, 6 },   new int[] { 6, 16 },  new int[] { 16, 15 },
        new int[] { 47, 6 },  new int[] { 6, 7 },   new int[] { 7, 17 },
        new int[] { 17, 24 }, new int[] { 24, 16 }, new int[] { 47, 46 },
        new int[] { 46, 7 },  new int[] { 7, 8 },   new int[] { 8, 18 },
        new int[] { 18, 17 }, new int[] { 46, 8 },  new int[] { 8, 9 },
        new int[] { 19, 25 }, new int[] { 25, 18 }, new int[] { 46, 45 }, 
        new int[] { 21, 26 }, new int[] { 26, 20 }, new int[] { 10, 20 },
        new int[] { 20, 19 }, new int[] { 12, 50 }, new int[] { 11, 50 },
        new int[] { 50, 54 }, new int[] { 54, 21 }, new int[] { 13 ,44 },
        new int[] { 22, 51 }, new int[] { 51, 50 }, new int[] { 13, 14 },
        new int[] { 14, 44 }, new int[] { 44, 31 }, new int[] { 31, 22 },
        new int[] { 23, 41 }, new int[] { 41, 44 }, new int[] { 16, 43 },
        new int[] { 15, 43 }, new int[] { 43, 42 }, new int[] { 42, 23 },
        new int[] { 24, 37 }, new int[] { 37, 43 }, new int[] { 18, 55 },
        new int[] { 17, 55 }, new int[] { 55, 59 }, new int[] { 59, 24 },
        new int[] { 25, 56 }, new int[] { 56, 55 }, new int[] { 20, 32 },
        new int[] { 32, 25 }, new int[] { 26, 27 }, new int[] { 27, 33 },
        new int[] { 33, 32 }, new int[] { 54, 26 }, new int[] { 31, 51 },
        new int[] { 42, 41 }, new int[] { 59, 37 }, new int[] { 32, 56 },
        new int[] { 54, 53 }, new int[] { 53, 27 }, new int[] { 27, 28 },
        new int[] { 28, 34 }, new int[] { 34, 33 }, new int[] { 53, 28 },
        new int[] { 28, 29 }, new int[] { 29, 38 }, new int[] { 38, 35 },
        new int[] { 35, 34 }, new int[] { 53, 52 }, new int[] { 52, 29 },
        new int[] { 29, 30 }, new int[] { 30, 39 }, new int[] { 39, 38 },
        new int[] { 30, 31 }, new int[] { 41, 39 }, new int[] { 30, 52 },
        new int[] { 52, 51 }, new int[] { 33, 57 }, new int[] { 56, 57 },
        new int[] { 34, 57 }, new int[] { 35, 58 }, new int[] { 58, 57 },
        new int[] { 38, 40 }, new int[] { 40, 36 }, new int[] { 35, 36 },
        new int[] { 36, 58 }, new int[] { 36, 37 }, new int[] { 59, 58 }, 
        new int[] { 39, 40 }, new int[] { 42, 40 }, new int[] { 48, 49 }
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
