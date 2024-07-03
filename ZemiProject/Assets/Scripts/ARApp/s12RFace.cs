using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s12RFace : MonoBehaviour
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
        new Vector3(-0.37210283f, -0.37210275f, 0.85033943f),
        new Vector3(0.37210284f, -0.37210275f, 0.85033943f),
        new Vector3(0.37210284f, 0.37210291f, 0.85033943f),
        new Vector3(-0.37210283f, 0.37210291f, 0.85033943f),
        new Vector3(-0.68440382f, -0.68440374f, 0.25135842f),
        new Vector3(0.00000000f, -0.88671194f, 0.46232019f),
        new Vector3(0.68440383f, -0.68440374f, 0.25135842f),
        new Vector3(0.88671203f, 0.00000008f, 0.46232019f),
        new Vector3(0.68440383f, 0.68440391f, 0.25135842f),
        new Vector3(0.00000001f, 0.88671211f, 0.46232019f),
        new Vector3(-0.68440382f, 0.68440391f, 0.25135842f),
        new Vector3(-0.88671202f, 0.00000008f, 0.46232019f),
        new Vector3(-0.48209560f, -0.74420553f, -0.46232022f),
        new Vector3(0.20230822f, -0.94651373f, -0.25135846f),
        new Vector3(0.74420581f, -0.48209559f, -0.46232022f),
        new Vector3(0.94651401f, 0.20230824f, -0.25135846f),
        new Vector3(0.48209553f, 0.74420550f, -0.46232032f),
        new Vector3(-0.20230824f, 0.94651368f, -0.25135852f),
        new Vector3(-0.74420614f, 0.48209586f, -0.46232018f),
        new Vector3(-0.94651406f, -0.20230823f, -0.25135841f),
        new Vector3(0.10999275f, -0.51460872f, -0.85033925f),
        new Vector3(0.51461005f, 0.10999269f, -0.85033999f),
        new Vector3(-0.10999294f, 0.51460747f, -0.85033864f),
        new Vector3(-0.51460944f, -0.10999264f, -0.85033947f)
    };

    // 各面の頂点
    int[][] triangles = new int[][]
    {
        new int[] { 0, 3, 11 },
        new int[] { 0, 11, 4 },
        new int[] { 0, 4, 5 },
        new int[] { 0, 5, 1 },
        new int[] { 0, 1, 2, 3 },
        new int[] { 1, 5, 6 },
        new int[] { 1, 6, 7 },
        new int[] { 1, 7, 2 },
        new int[] { 2, 7, 8 },
        new int[] { 2, 8, 9 },
        new int[] { 2, 9, 3 },
        new int[] { 3, 9, 10 },
        new int[] { 3, 10, 11 },
        new int[] { 4, 11, 19 },
        new int[] { 4, 19, 12 },
        new int[] { 4, 12, 13, 5 },
        new int[] { 5, 13, 6 },
        new int[] { 6, 13, 14 },
        new int[] { 6, 14, 15, 7 },
        new int[] { 7, 15, 8 },
        new int[] { 8, 15, 16 },
        new int[] { 8, 16, 17, 9 },
        new int[] { 9, 17, 10 },
        new int[] { 10, 17, 18 },
        new int[] { 10, 18, 19, 11 },
        new int[] { 12, 19, 23 },
        new int[] { 12, 23, 20 },
        new int[] { 12, 20, 13 },
        new int[] { 13, 20, 14 },
        new int[] { 14, 20, 21 },
        new int[] { 14, 21, 15 },
        new int[] { 15, 21, 16 },
        new int[] { 16, 21, 22 },
        new int[] { 16, 22, 17 },
        new int[] { 17, 22, 18 },
        new int[] { 18, 22, 23 },
        new int[] { 18, 23, 19 },
        new int[] { 20, 23, 22, 21 }
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
