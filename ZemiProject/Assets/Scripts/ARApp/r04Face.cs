using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class r04Face : MonoBehaviour
{
    public Material StartColor;
    public Material SelectedColor;

    GameObject[] faces; // 各面を参照するためのGameObject配列

    //マテリアルが変更された回数をカウントする変数
    int materialChangeCount = 0;

    //カウントを表示するテキスト
    Text faceText;

    //変更されたfaceの値を保持するリスト
    List<int> changeFaceIndex = new List<int>();

    Button faceButton;

    // 正十二面体の頂点座標
    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.35682209f, -0.49112347f, 0.79465447f),
        new Vector3(0.35682209f, -0.49112347f, 0.79465447f),
        new Vector3(0.57735027f, -0.79465447f, 0.18759247f),
        new Vector3(0.00000000f, -0.98224695f, -0.18759247f),
        new Vector3(-0.57735027f, -0.79465447f, 0.18759247f),
        new Vector3(-0.57735027f, 0.18759247f, 0.79465447f),
        new Vector3(0.57735027f, 0.18759247f, 0.79465447f),
        new Vector3(0.93417236f, -0.30353100f, -0.18759247f),
        new Vector3(0.00000000f, -0.60706200f, -0.79465447f),
        new Vector3(-0.93417236f, -0.30353100f, -0.18759247f),
        new Vector3(-0.93417236f, 0.30353100f, 0.18759247f),
        new Vector3(0.00000000f, 0.60706200f, 0.79465447f),
        new Vector3(0.93417236f, 0.30353100f, 0.18759247f),
        new Vector3(0.57735027f, -0.18759247f, -0.79465447f),
        new Vector3(-0.57735027f, -0.18759247f, -0.79465447f),
        new Vector3(-0.57735027f, 0.79465447f, -0.18759247f),
        new Vector3(0.00000000f, 0.98224695f, 0.18759247f),
        new Vector3(0.57735027f, 0.79465447f, -0.18759247f),
        new Vector3(0.35682209f, 0.49112347f, -0.79465447f),
        new Vector3(-0.35682209f, 0.49112347f, -0.79465447f)
    };

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 6, 11, 5 },
        new int[]{ 0, 5, 10, 9, 4 },
        new int[]{ 0, 4, 3, 2, 1 },
        new int[]{ 1, 2, 7, 12, 6 },
        new int[]{ 2, 3, 8, 13, 7 },
        new int[]{ 3, 4, 9, 14, 8 },
        new int[]{ 5, 11, 16, 15, 10 },
        new int[]{ 6, 12, 17, 16, 11 },
        new int[]{ 7, 13, 18, 17, 12 },
        new int[]{ 8, 14, 19, 18, 13 },
        new int[]{ 9, 10, 15, 19, 14 },
        new int[]{ 15, 16, 17, 18, 19 }
    };

    void Start()
    {
        //カウントを表示するTextを取得する
        faceText = GameObject.Find("TextCanvas/FaceText").GetComponent<Text>();

        faceButton = GameObject.Find("ResetCanvas/FaceButton").GetComponent<Button>();
        faceButton.onClick.AddListener(ChangeFace);

        Mesh mesh = new Mesh { name = "Twelve Face Mesh" };
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // 各面のGameObjectを生成し、初期のマテリアルを設定
        faces = new GameObject[triangles.Length];
        for (int i = 0; i < faces.Length; i++)
        {
            GameObject face = new GameObject($"Face_{i}");
            face.transform.parent = transform; // 面をメッシュの子にする
            face.transform.localPosition = Vector3.zero; // ローカル座標をゼロに設定

            Mesh meshCopy = new Mesh(); // 各面ごとに新しいメッシュを作成
            meshCopy.vertices = localVertexPositions;
            meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3],
                                            triangles[i][0], triangles[i][3], triangles[i][4] };
            
            meshCopy.RecalculateNormals();

            face.AddComponent<MeshFilter>().mesh = meshCopy; // メッシュフィルターを追加して同じメッシュを使用
            face.AddComponent<MeshRenderer>().material = StartColor; // 初期のマテリアルを設定
            face.AddComponent<MeshCollider>();
            faces[i] = face;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // 最初のタッチを取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // タッチした位置からRayを飛ばし、facesに当たったら色を変更
                if (Physics.Raycast(ray, out hit))
                {
                    // タッチした面のマテリアルを変更
                    GameObject touchedFace = hit.transform.gameObject;
                    int faceIndex = System.Array.IndexOf(faces, touchedFace);

                    if (faceIndex >= 0 && !changeFaceIndex.Contains(faceIndex))
                    {
                        Renderer faceRenderer = faces[faceIndex].GetComponent<Renderer>();

                        faceRenderer.material = SelectedColor;

                        //変更されたfaceの値をリストに追加
                        changeFaceIndex.Add(faceIndex);

                        //マテリアルが変更された回数を増やす
                        materialChangeCount++;

                        //カウントを表示するテキストを更新
                        faceText.text = "面：" + materialChangeCount + "個";
                    }
                }
            }
        }
    }

    void ChangeFace()
    {
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].GetComponent<MeshRenderer>().material = StartColor;
        }

        //materialChangeCountを0にリセット
        materialChangeCount = 0;

        //カウントを表示するテキストを更新
        faceText.text = "面：" + materialChangeCount + "個";

        //更新されたメッシュの値を保持するリストをクリア
        changeFaceIndex.Clear();
    }
}
