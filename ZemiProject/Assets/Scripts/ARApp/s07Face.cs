using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s07Face : MonoBehaviour
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

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 23, 22, 21, 20, 19, 18, 11, 10 },
        new int[]{ 0, 10, 9 },
        new int[]{ 0, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
        new int[]{ 1, 2, 23 },
        new int[]{ 2, 3, 29, 28, 27, 26, 25, 24, 22, 23 },
        new int[]{ 3, 4, 29 },
        new int[]{ 4, 5, 35, 34, 33, 32, 31, 30, 28, 29 },
        new int[]{ 5, 6, 35 },
        new int[]{ 6, 7, 17, 16, 39, 38, 37, 36, 34, 35 },
        new int[]{ 7, 8, 17 },
        new int[]{ 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 },
        new int[]{ 11, 18, 12 },
        new int[]{ 12, 18, 19, 45, 44, 43, 42, 41, 40, 13 },
        new int[]{ 13, 40, 14 },
        new int[]{ 14, 40, 41, 59, 58, 56, 57, 38, 39, 15 },
        new int[]{ 14, 39, 16 },
        new int[]{ 19, 20, 45 },
        new int[]{ 20, 21, 24, 25, 49, 48, 47, 46, 44, 45 },
        new int[]{ 21, 22, 24 },
        new int[]{ 25, 26, 49 },
        new int[]{ 27, 27, 30, 31, 53, 52, 51, 50, 48, 49 },
        new int[]{ 27, 28, 30 },
        new int[]{ 31, 32, 53 },
        new int[]{ 32, 33, 36, 37, 57, 56, 55, 54, 52, 53 },
        new int[]{ 33, 34, 36 },
        new int[]{ 37, 38, 57 },
        new int[]{ 41, 42, 59 },
        new int[]{ 42, 43, 46, 47, 50, 51, 54, 55, 58, 59 },
        new int[]{ 43, 44, 46 },
        new int[]{ 47, 48, 50 },
        new int[]{ 51, 52, 54 },
        new int[]{ 55, 56, 58 }
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

            if (triangles[i].Length == 10)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3],
                                            triangles[i][0], triangles[i][3], triangles[i][4],
                                            triangles[i][0], triangles[i][4], triangles[i][5],
                                            triangles[i][0], triangles[i][5], triangles[i][6],
                                            triangles[i][0], triangles[i][6], triangles[i][7],
                                            triangles[i][0], triangles[i][7], triangles[i][8],
                                            triangles[i][0], triangles[i][8], triangles[i][9]
                };
            }
            else
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
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
