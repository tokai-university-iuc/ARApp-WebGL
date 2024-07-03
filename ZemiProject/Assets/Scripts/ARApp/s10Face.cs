using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s10Face : MonoBehaviour
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

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 9, 17, 16, 8 },
        new int[] { 0, 8, 15, 7 },
        new int[] { 0, 7, 6, 5, 4, 3, 2, 1 },
        new int[] { 1, 2, 10, 9 },
        new int[] { 2, 3, 11, 19, 18, 10 },
        new int[] { 3, 4, 12, 11 },
        new int[] { 4, 5, 13, 21, 20, 12 },
        new int[] { 5, 6, 14, 13 },
        new int[] { 6, 7, 15, 23, 22, 14 },
        new int[] { 8, 16, 27, 26, 25, 24, 23, 15 },
        new int[] { 9, 10, 18, 31, 30, 29, 28, 17 },
        new int[] { 11, 12, 20, 35, 34, 33, 32, 19 },
        new int[] { 13, 14, 22, 39, 38, 37, 36, 21 },
        new int[] { 16, 17, 28, 27 },
        new int[] { 18, 19, 32, 31 },
        new int[] { 20, 21, 36, 35 },
        new int[] { 22, 23, 24, 39 },
        new int[] { 24, 25, 47, 40, 38, 39 },
        new int[] { 25, 26, 46, 47 },
        new int[] { 26, 27, 28, 29, 45, 46 },
        new int[] { 29, 30, 44, 45 },
        new int[] { 30, 31, 32, 33, 43, 44 },
        new int[] { 33, 34, 42, 43 },
        new int[] { 34, 35, 36, 37, 41, 42 },
        new int[] { 37, 38, 40, 41 },
        new int[] { 40, 47, 46, 45, 44, 43, 42, 41 }
    };

    void Start()
    {
        //カウントを表示するTextを取得する
        faceText = GameObject.Find("TextCanvas/FaceText").GetComponent<Text>();

        faceButton = GameObject.Find("ResetCanvas/FaceButton").GetComponent<Button>();
        faceButton.onClick.AddListener(ChangeFace);

        Mesh mesh = new Mesh { name = "s01 Face Mesh" };
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

            if (triangles[i].Length == 4)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3] };
            }
            else if (triangles[i].Length == 6)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4],
                                                triangles[i][0], triangles[i][4], triangles[i][5] };
            }
            else
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4],
                                                triangles[i][0], triangles[i][4], triangles[i][5],
                                                triangles[i][0], triangles[i][5], triangles[i][6],
                                                triangles[i][0], triangles[i][6], triangles[i][7]};
            }

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
