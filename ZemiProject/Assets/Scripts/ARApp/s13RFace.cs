using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s13RFace : MonoBehaviour
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
        new Vector3(-0.23192837f, -0.31922203f, 0.91886124f),
        new Vector3(0.23192838f, -0.31922203f, 0.91886124f),
        new Vector3(0.37526800f, 0.12193196f, 0.91886124f),
        new Vector3(0.00000000f, 0.39458012f, 0.91886124f),
        new Vector3(-0.37526799f, 0.12193196f, 0.91886124f),
        new Vector3(0.00000000f, -0.67692647f, 0.73605027f),
        new Vector3(0.45067192f, -0.62029668f, 0.64197064f),
        new Vector3(0.64379533f, -0.20918178f, 0.73605027f),
        new Vector3(0.72920248f, 0.23693225f, 0.64197064f),
        new Vector3(0.39788740f, 0.54764501f, 0.73605027f),
        new Vector3(0.00000000f, 0.76672886f, 0.64197064f),
        new Vector3(-0.39788739f, 0.54764501f, 0.73605027f),
        new Vector3(-0.72920248f, 0.23693225f, 0.64197064f),
        new Vector3(-0.64379532f, -0.20918178f, 0.73605027f),
        new Vector3(-0.45067191f, -0.62029668f, 0.64197064f),
        new Vector3(-0.19312341f, -0.90425562f, 0.38082092f),
        new Vector3(0.80031978f, -0.46310163f, 0.38082092f),
        new Vector3(0.68774824f, 0.61804307f, 0.38082092f),
        new Vector3(-0.37526799f, 0.84507325f, 0.38082092f),
        new Vector3(-0.91967661f, -0.09575908f, 0.38082092f),
        new Vector3(-0.60719637f, -0.76095695f, 0.22859687f),
        new Vector3(-0.30415069f, -0.95025066f, -0.06719748f),
        new Vector3(0.13819168f, -0.98812298f, 0.06719747f),
        new Vector3(0.53607907f, -0.81262670f, 0.22859687f),
        new Vector3(0.80975434f, -0.58290810f, -0.06719748f),
        new Vector3(0.98246437f, -0.17391870f, 0.06719747f),
        new Vector3(0.93851146f, 0.25872603f, 0.22859687f),
        new Vector3(0.80460641f, 0.58999364f, -0.06719748f),
        new Vector3(0.46900470f, 0.88063531f, 0.06719747f),
        new Vector3(0.04395291f, 0.97252818f, 0.22859687f),
        new Vector3(-0.31248024f, 0.94754422f, -0.06719748f),
        new Vector3(-0.69260352f, 0.71818125f, 0.06719747f),
        new Vector3(-0.91134706f, 0.34232943f, 0.22859687f),
        new Vector3(-0.99772981f, -0.00437910f, -0.06719748f),
        new Vector3(-0.89705721f, -0.43677489f, 0.06719747f),
        new Vector3(-0.66641472f, -0.70967199f, -0.22859688f),
        new Vector3(0.03659896f, -0.92392391f, -0.38082093f),
        new Vector3(0.46900470f, -0.85309878f, -0.22859688f),
        new Vector3(0.89001354f, -0.25070051f, -0.38082093f),
        new Vector3(0.95627558f, 0.18242795f, -0.22859688f),
        new Vector3(0.51345966f, 0.76898245f, -0.38082093f),
        new Vector3(0.12200611f, 0.96584547f, -0.22859688f),
        new Vector3(-0.57267800f, 0.72595780f, -0.38082093f),
        new Vector3(-0.88087165f, 0.41449736f, -0.22859688f),
        new Vector3(-0.86739415f, -0.32031586f, -0.38082093f),
        new Vector3(-0.54955655f, -0.53465945f, -0.64197064f),
        new Vector3(-0.11507021f, -0.66707442f, -0.73605028f),
        new Vector3(0.33866904f, -0.68787822f, -0.64197064f),
        new Vector3(0.59886681f, -0.31557561f, -0.73605027f),
        new Vector3(0.75886555f, 0.10952733f, -0.64197063f),
        new Vector3(0.48519026f, 0.47203794f, -0.73605027f),
        new Vector3(0.13033567f, 0.75556984f, -0.64197064f),
        new Vector3(-0.29900272f, 0.60731111f, -0.73605027f),
        new Vector3(-0.67831368f, 0.35744049f, -0.64197064f),
        new Vector3(-0.66998413f, -0.09669906f, -0.73605027f),
        new Vector3(-0.28281717f, -0.27515070f, -0.91886123f),
        new Vector3(0.17428857f, -0.35400135f, -0.91886120f),
        new Vector3(0.39053348f, 0.05636583f, -0.91886122f),
        new Vector3(0.06707433f, 0.38883729f, -0.91886104f),
        new Vector3(-0.34907938f, 0.18394906f, -0.91886143f)
    };

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[] { 0, 4, 13 },
        new int[] { 0, 13, 14 },
        new int[] { 0, 14, 5 },
        new int[] { 0, 5, 1 },
        new int[] { 0, 1, 2, 3, 4 },
        new int[] { 1, 5, 6 },
        new int[] { 1, 6, 7 },
        new int[] { 1, 7, 2 },
        new int[] { 2, 7, 8 },
        new int[] { 2, 8, 9 },
        new int[] { 2, 9, 3 },
        new int[] { 3, 9, 10 },
        new int[] { 3 , 10, 11 },
        new int[] { 3, 11, 4 },
        new int[] { 4, 11, 12 },
        new int[] { 4, 12, 13 },
        new int[] { 5, 14, 15 },
        new int[] { 5, 15, 22, 23, 6 },
        new int[] { 6, 23, 16 },
        new int[] { 6, 16, 7 },
        new int[] { 7, 16, 25, 26, 8 },
        new int[] { 8, 26, 17 },
        new int[] { 8, 17, 9 },
        new int[] { 9, 17, 28, 29, 10 },
        new int[] { 10, 29, 18 },
        new int[] { 10, 18, 11 },
        new int[] { 11, 18, 31, 32, 12 },
        new int[] { 12, 32, 19 },
        new int[] { 12, 19, 13 },
        new int[] { 13, 19, 34, 20, 14 },
        new int[] { 14, 20, 15 },
        new int[] { 15, 20, 21 },
        new int[] { 15, 21, 22 },
        new int[] { 16, 23, 24 },
        new int[] { 16, 24, 25 },
        new int[] { 17, 26, 27 },
        new int[] { 17, 27, 28 },
        new int[] { 18, 29, 30 },
        new int[] { 18, 30, 31 },
        new int[] { 19, 32, 33 },
        new int[] { 19, 33, 34 },
        new int[] { 20, 34, 35 },
        new int[] { 20, 35, 21 },
        new int[] { 21, 35, 45, 46, 36 },
        new int[] { 21 ,36, 22 },
        new int[] { 22, 36, 37 },
        new int[] { 22, 37, 23 },
        new int[] { 23, 37, 24 },
        new int[] { 24, 37, 47, 48, 38 },
        new int[] { 24, 38, 25 },
        new int[] { 25, 38, 39 },
        new int[] { 25, 39, 26 },
        new int[] { 26, 39, 27 },
        new int[] { 27, 40, 28 },
        new int[] { 27, 39, 49, 50, 40 },
        new int[] { 28, 41, 29 },
        new int[] { 28, 40, 41 },
        new int[] { 29, 41, 30 },
        new int[] { 30, 41, 51, 52, 42 },
        new int[] { 30, 42, 31 },
        new int[] { 31, 42, 43 },
        new int[] { 31, 43, 32 },
        new int[] { 32, 43, 33 },
        new int[] { 33, 43, 53, 54, 44 },
        new int[] { 33, 44, 34 },
        new int[] { 34, 44, 35 },
        new int[] { 35, 44, 45 },
        new int[] { 36, 46, 47 },
        new int[] { 36, 47, 37 },
        new int[] { 38, 48, 49 },
        new int[] { 38, 49, 39 },
        new int[] { 40, 51, 41 },
        new int[] { 40, 50, 51 },
        new int[] { 42, 52, 53 },
        new int[] { 42, 53, 43 },
        new int[] { 44, 54, 45 },
        new int[] { 45, 55, 46 },
        new int[] { 45, 54, 55 },
        new int[] { 46, 55, 56 },
        new int[] { 46, 56, 47 },
        new int[] { 47, 56, 48 },
        new int[] { 48, 57, 49 },
        new int[] { 48, 56, 57 },
        new int[] { 49, 57, 50 },
        new int[] { 50, 57, 58 },
        new int[] { 50, 58, 51 },
        new int[] { 51, 58, 52 },
        new int[] { 52, 58, 59 },
        new int[] { 52, 59, 53 },
        new int[] { 53, 59, 54 },
        new int[] { 54, 59, 55 },
        new int[] { 55, 59, 58, 57, 56 }
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
