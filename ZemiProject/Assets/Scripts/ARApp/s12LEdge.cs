using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s12LEdge : MonoBehaviour
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
