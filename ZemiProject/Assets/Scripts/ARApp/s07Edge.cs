using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s07Edge : MonoBehaviour
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
        new Vector3(-0.16838141f, -0.51822468f, 0.83850515f),
        new Vector3(-0.44082824f, -0.32028047f, 0.83850515f),
        new Vector3(-0.54489368f, 0.00000000f, 0.83850515f),
        new Vector3(-0.44082824f, 0.32028047f, 0.83850515f),
        new Vector3(-0.16838141f, 0.51822468f, 0.83850515f),
        new Vector3(0.16838141f, 0.51822468f, 0.83850515f),
        new Vector3(0.44082824f, 0.32028047f, 0.83850515f),
        new Vector3(0.54489368f, 0.00000000f, 0.83850515f),
        new Vector3(0.44082824f, -0.32028047f, 0.83850515f),
        new Vector3(0.16838141f, -0.51822468f, 0.83850515f),
        new Vector3(0.00000000f, -0.74998180f, 0.66145846f),
        new Vector3(-0.00000000f, -0.92702849f, 0.37499090f),
        new Vector3(0.16838141f, -0.98173893f, 0.08852334f),
        new Vector3(0.44082824f, -0.89321558f, -0.08852334f),
        new Vector3(0.71327508f, -0.69527137f, -0.08852334f),
        new Vector3(0.88165649f, -0.46351425f, 0.08852334f),
        new Vector3(0.88165649f, -0.28646756f, 0.37499090f),
        new Vector3(0.71327508f, -0.23175712f, 0.66145846f),
        new Vector3(-0.16838141f, -0.98173893f, 0.08852334f),
        new Vector3(-0.44082824f, -0.89321558f, -0.08852334f),
        new Vector3(-0.71327508f, -0.69527137f, -0.08852334f),
        new Vector3(-0.88165649f, -0.46351425f, 0.08852334f),
        new Vector3(-0.88165649f, -0.28646756f, 0.37499090f),
        new Vector3(-0.71327508f, -0.23175712f, 0.66145846f),
        new Vector3(-0.98572192f, -0.14323378f, 0.08852334f),
        new Vector3(-0.98572192f, 0.14323378f, -0.08852334f),
        new Vector3(-0.88165649f, 0.46351425f, -0.08852334f),
        new Vector3(-0.71327508f, 0.69527137f, 0.08852334f),
        new Vector3(-0.54489368f, 0.74998180f, 0.37499090f),
        new Vector3(-0.44082824f, 0.60674802f, 0.66145846f),
        new Vector3(-0.44082824f, 0.89321558f, 0.08852334f),
        new Vector3(-0.16838141f, 0.98173893f, -0.08852334f),
        new Vector3(0.16838141f, 0.98173893f, -0.08852334f),
        new Vector3(0.44082824f, 0.89321558f, 0.08852334f),
        new Vector3(0.54489368f, 0.74998180f, 0.37499090f),
        new Vector3(0.44082824f, 0.60674802f, 0.66145846f),
        new Vector3(0.71327508f, 0.69527137f, 0.08852334f),
        new Vector3(0.88165649f, 0.46351425f, -0.08852334f),
        new Vector3(0.98572192f, 0.14323378f, -0.08852334f),
        new Vector3(0.98572192f, -0.14323378f, 0.08852334f),
        new Vector3(0.54489368f, -0.74998180f, -0.37499090f),
        new Vector3(0.44082824f, -0.60674802f, -0.66145846f),
        new Vector3(0.16838141f, -0.51822468f, -0.83850515f),
        new Vector3(-0.16838141f, -0.51822468f, -0.83850515f),
        new Vector3(-0.44082824f, -0.60674802f, -0.66145846f),
        new Vector3(-0.54489368f, -0.74998180f, -0.37499090f),
        new Vector3(-0.44082824f, -0.32028047f, -0.83850515f),
        new Vector3(-0.54489367f, 0.00000000f, -0.83850515f),
        new Vector3(-0.71327508f, 0.23175712f, -0.66145846f),
        new Vector3(-0.88165649f, 0.28646756f, -0.37499090f),
        new Vector3(-0.44082824f, 0.32028047f, -0.83850515f),
        new Vector3(-0.16838141f, 0.51822468f, -0.83850515f),
        new Vector3(0.00000000f, 0.74998180f, -0.66145846f),
        new Vector3(0.00000000f, 0.92702849f, -0.37499090f),
        new Vector3(0.16838141f, 0.51822468f, -0.83850515f),
        new Vector3(0.44082824f, 0.32028047f, -0.83850515f),
        new Vector3(0.71327508f, 0.23175712f, -0.66145846f),
        new Vector3(0.88165649f, 0.28646756f, -0.37499090f),
        new Vector3(0.54489368f, -0.00000000f, -0.83850515f),
        new Vector3(0.44082824f, -0.32028047f, -0.83850515f)
    };

    int[][] edges = new int[][]
    {
        new int[] { 0, 1 },
        new int[] { 1, 23 },
        new int[] { 23, 22 },
        new int[] { 22, 21 },
        new int[] { 21, 20 },
        new int[] { 20, 19 },
        new int[] { 19, 18 },
        new int[] { 18, 11 },
        new int[] { 11, 10 },
        new int[] { 0, 10 },
        new int[] { 10, 9 },
        new int[] { 0, 9 },
        new int[] { 9, 8 },
        new int[] { 8, 7 },
        new int[] { 7, 6 },
        new int[] { 6, 5 },
        new int[] { 5, 4 },
        new int[] { 4, 3 },
        new int[] { 3, 2 },
        new int[] { 2, 1 },
        new int[] { 2, 23 },
        new int[] { 3, 29 },
        new int[] { 29, 28 },
        new int[] { 28, 27 },
        new int[] { 27, 26 },
        new int[] { 26, 25 },
        new int[] { 25, 24 },
        new int[] { 24, 22 },
        new int[] { 4, 29 },
        new int[] { 5, 35 },
        new int[] { 35, 34 },
        new int[] { 34, 33 },
        new int[] { 33, 32 },
        new int[] { 32, 31 },
        new int[] { 31, 30 },
        new int[] { 30, 28 },
        new int[] { 6, 35 },
        new int[] { 7, 17 },
        new int[] { 17, 16 },
        new int[] { 16, 39 },
        new int[] { 39, 38 },
        new int[] { 38, 37 },
        new int[] { 37, 36 },
        new int[] { 36, 34 },
        new int[] { 8, 17 },
        new int[] { 11, 12 },
        new int[] { 12, 13 },
        new int[] { 13, 14 },
        new int[] { 14, 15 },
        new int[] { 15, 16 },
        new int[] { 18, 12 },
        new int[] { 19, 45 },
        new int[] { 45, 44 },
        new int[] { 44, 43 },
        new int[] { 43, 42 },
        new int[] { 42, 41 },
        new int[] { 41, 40 },
        new int[] { 40, 13 },
        new int[] { 40, 14 },
        new int[] { 41, 59 },
        new int[] { 59, 58 },
        new int[] { 58, 56 },
        new int[] { 56, 57 },
        new int[] { 57, 38 },
        new int[] { 39, 15 },
        new int[] { 20, 45 },
        new int[] { 21, 24 },
        new int[] { 25, 49 },
        new int[] { 49, 48 },
        new int[] { 48, 47 },
        new int[] { 47, 46 },
        new int[] { 46, 44 },
        new int[] { 26, 49 },
        new int[] { 27, 30 },
        new int[] { 31, 53 },
        new int[] { 53, 52 },
        new int[] { 52, 51 },
        new int[] { 51, 50 },
        new int[] { 50, 48 },
        new int[] { 32, 53 },
        new int[] { 33, 36 },
        new int[] { 37, 57 },
        new int[] { 56, 55 },
        new int[] { 55, 54 },
        new int[] { 54, 52 },
        new int[] { 42, 59 },
        new int[] { 43, 46 },
        new int[] { 47, 50 },
        new int[] { 51, 54 },
        new int[] { 55, 58 }
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
