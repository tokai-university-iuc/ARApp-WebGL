using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s04Face : MonoBehaviour
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
        new Vector3(-0.31622777f, -0.54772256f, 0.77459667f),
        new Vector3(-0.63245553f, 0.00000000f, 0.77459667f),
        new Vector3(-0.31622777f, 0.54772256f, 0.77459667f),
        new Vector3(0.31622777f, 0.54772256f, 0.77459667f),
        new Vector3(0.63245553f, -0.00000000f, 0.77459667f),
        new Vector3(0.31622777f, -0.54772256f, 0.77459667f),
        new Vector3(-0.31622777f, -0.91287093f, 0.25819889f),
        new Vector3(-0.94868330f, 0.18257419f, 0.25819889f),
        new Vector3(-0.63245553f, 0.73029674f, 0.25819889f),
        new Vector3(0.63245553f, 0.73029674f, 0.25819889f),
        new Vector3(0.94868330f, 0.18257419f, 0.25819889f),
        new Vector3(0.31622777f, -0.91287093f, 0.25819889f),
        new Vector3(-0.63245553f, -0.73029674f, -0.25819889f),
        new Vector3(-0.94868330f, -0.18257419f, -0.25819889f),
        new Vector3(-0.31622777f, 0.91287093f, -0.25819889f),
        new Vector3(0.31622777f, 0.91287093f, -0.25819889f),
        new Vector3(0.94868330f, -0.18257419f, -0.25819889f),
        new Vector3(0.63245553f, -0.73029674f, -0.25819889f),
        new Vector3(-0.31622777f, -0.54772256f, -0.77459667f),
        new Vector3(-0.63245553f, 0.00000000f, -0.77459667f),
        new Vector3(-0.31622777f, 0.54772256f, -0.77459667f),
        new Vector3(0.31622777f, 0.54772256f, -0.77459667f),
        new Vector3(0.63245553f, -0.00000000f, -0.77459667f),
        new Vector3(0.31622777f, -0.54772256f, -0.77459667f)
    };

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[]{ 0, 6, 11, 5 },
        new int[]{ 0, 5, 4, 3, 2, 1 },
        new int[]{ 0, 1, 7, 13, 12, 6 },
        new int[]{ 1, 2, 8, 7 },
        new int[]{ 2, 3, 9, 15, 14, 8 },
        new int[]{ 3, 4, 10, 9 },
        new int[]{ 4, 5, 11, 17, 16, 10 },
        new int[]{ 6, 12, 18, 23, 17, 11 },
        new int[]{ 7, 8, 14, 20, 19, 13 },
        new int[]{ 9, 10, 16, 22, 21, 15 },
        new int[]{ 12, 13, 19, 18 },
        new int[]{ 14, 15, 21, 20 },
        new int[]{ 16, 17, 23, 22 },
        new int[]{ 18, 19, 20, 21, 22, 23 }
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
            else
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3],
                                            triangles[i][0], triangles[i][3], triangles[i][4],
                                            triangles[i][0], triangles[i][4], triangles[i][5] };
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
