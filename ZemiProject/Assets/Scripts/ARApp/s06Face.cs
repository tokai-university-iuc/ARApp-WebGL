using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s06Face : MonoBehaviour
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
        new Vector3(-0.20177411f, -0.27771823f, 0.93923362f),
        new Vector3(-0.40354821f, -0.55543646f, 0.72707577f),
        new Vector3(-0.20177411f, -0.83315470f, 0.51491792f),
        new Vector3(0.20177411f, -0.83315470f, 0.51491792f),
        new Vector3(0.40354821f, -0.55543646f, 0.72707577f),
        new Vector3(0.20177411f, -0.27771823f, 0.93923362f),
        new Vector3(-0.32647736f, 0.10607893f, 0.93923362f),
        new Vector3(-0.65295472f, 0.21215785f, 0.72707577f),
        new Vector3(-0.85472883f, -0.06556038f, 0.51491792f),
        new Vector3(-0.73002557f, -0.44935754f, 0.51491792f),
        new Vector3(-0.73002557f, -0.66151539f, 0.17163931f),
        new Vector3(-0.40354821f, -0.89871508f, 0.17163931f),
        new Vector3(-0.20177411f, -0.96427546f, -0.17163931f),
        new Vector3(0.20177411f, -0.96427546f, -0.17163931f),
        new Vector3(0.40354821f, -0.89871508f, 0.17163931f),
        new Vector3(0.73002557f, -0.66151539f, 0.17163931f),
        new Vector3(0.73002557f, -0.44935754f, 0.51491792f),
        new Vector3(0.85472883f, -0.06556038f, 0.51491792f),
        new Vector3(0.65295472f, 0.21215785f, 0.72707577f),
        new Vector3(0.32647736f, 0.10607893f, 0.93923362f),
        new Vector3(-0.00000000f, 0.34327861f, 0.93923362f),
        new Vector3(-0.65295472f, 0.55543646f, 0.51491792f),
        new Vector3(-0.85472883f, 0.48987608f, 0.17163931f),
        new Vector3(-0.97943209f, 0.10607893f, 0.17163931f),
        new Vector3(-0.97943209f, -0.10607892f, -0.17163931f),
        new Vector3(-0.85472883f, -0.48987608f, -0.17163931f),
        new Vector3(-0.65295472f, -0.55543646f, -0.51491792f),
        new Vector3(-0.32647736f, -0.79263615f, -0.51491792f),
        new Vector3(-0.00000000f, -0.68655723f, -0.72707577f),
        new Vector3(0.32647736f, -0.79263615f, -0.51491792f),
        new Vector3(0.65295472f, -0.55543646f, -0.51491792f),
        new Vector3(0.85472883f, -0.48987608f, -0.17163931f),
        new Vector3(0.97943209f, -0.10607893f, -0.17163931f),
        new Vector3(0.97943209f, 0.10607893f, 0.17163931f),
        new Vector3(0.85472883f, 0.48987608f, 0.17163931f),
        new Vector3(0.65295472f, 0.55543646f, 0.51491792f),
        new Vector3(0.32647736f, 0.79263615f, 0.51491792f),
        new Vector3(0.00000000f, 0.68655723f, 0.72707577f),
        new Vector3(-0.32647736f, 0.79263615f, 0.51491792f),
        new Vector3(-0.73002557f, 0.66151539f, -0.17163931f),
        new Vector3(-0.73002557f, 0.44935754f, -0.51491792f),
        new Vector3(-0.85472883f, 0.06556038f, -0.51491792f),
        new Vector3(-0.65295472f, -0.21215785f, -0.72707577f),
        new Vector3(-0.32647736f, -0.10607892f, -0.93923362f),
        new Vector3(0.00000000f, -0.34327861f, -0.93923362f),
        new Vector3(0.32647736f, -0.10607892f, -0.93923362f),
        new Vector3(0.65295472f, -0.21215785f, -0.72707577f),
        new Vector3(0.85472883f, 0.06556038f, -0.51491792f),
        new Vector3(0.73002557f, 0.44935754f, -0.51491792f),
        new Vector3(0.73002557f, 0.66151539f, -0.17163931f),
        new Vector3(0.40354821f, 0.89871508f, -0.17163931f),
        new Vector3(0.20177411f, 0.96427546f, 0.17163931f),
        new Vector3(-0.20177411f, 0.96427546f, 0.17163931f),
        new Vector3(-0.40354821f, 0.89871508f, -0.17163931f),
        new Vector3(-0.20177411f, 0.83315469f, -0.51491792f),
        new Vector3(-0.40354821f, 0.55543646f, -0.72707577f),
        new Vector3(-0.20177411f, 0.27771823f, -0.93923362f),
        new Vector3(0.20177410f, 0.27771823f, -0.93923362f),
        new Vector3(0.40354821f, 0.55543646f, -0.72707577f),
        new Vector3(0.20177411f, 0.83315469f, -0.51491792f)
    };

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[]{ 0, 5, 19, 20, 6 },
        new int[]{ 0, 6, 7, 8, 9, 1 },
        new int[]{ 0, 1, 2, 3, 4, 5 },
        new int[]{ 1, 9, 10, 11, 2 },
        new int[]{ 2, 11, 12, 13, 14, 3 },
        new int[]{ 3, 14, 15, 16, 4 },
        new int[]{ 4, 16, 17, 18, 19, 5 },
        new int[]{ 6, 20, 37, 38, 21, 7 },
        new int[]{ 7, 21, 22, 23, 8 },
        new int[]{ 8, 23, 24, 25, 10, 9 },
        new int[]{ 10, 25, 26, 27, 12, 11 },
        new int[]{ 12, 27, 28, 29, 13 },
        new int[]{ 13, 29, 30, 31, 15, 14 },
        new int[]{ 15, 31, 32, 33, 17, 16 },
        new int[]{ 17, 33, 34, 35, 18 },
        new int[]{ 18, 35, 36, 37, 20, 19 },
        new int[]{ 21, 38, 52, 53, 39, 22 },
        new int[]{ 22, 39, 40, 41, 24, 23 },
        new int[]{ 24, 41, 42, 26, 25 },
        new int[]{ 26, 42, 43, 44, 28, 27 },
        new int[]{ 28, 44, 45, 46, 30, 29 },
        new int[]{ 30, 46, 47, 32, 31 },
        new int[]{ 32, 47, 48, 49, 34, 33 },
        new int[]{ 34, 49, 50, 51, 36, 35 },
        new int[]{ 36, 51, 52, 38, 37 },
        new int[]{ 39, 53, 54, 55, 40 },
        new int[]{ 40, 55, 56, 43, 42, 41 },
        new int[]{ 43, 56, 57, 45, 44 },
        new int[]{ 45, 57, 58, 48, 47, 46 },
        new int[]{ 48, 58, 59, 50, 49 },
        new int[]{ 50, 59, 54, 53, 52, 51},
        new int[]{ 54, 59, 58, 57, 56, 55 }
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

            if (triangles[i].Length == 5)
            {
                meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3],
                                            triangles[i][0], triangles[i][3], triangles[i][4] };
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
