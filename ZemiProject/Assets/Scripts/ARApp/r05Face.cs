using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class r05Face : MonoBehaviour
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

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.52573111f, -0.72360680f, 0.44721360f),
        new Vector3(-0.85065081f, 0.27639320f, 0.44721360f),
        new Vector3(0.00000000f, 0.89442719f, 0.44721360f),
        new Vector3(0.85065081f, 0.27639320f, 0.447213607f),
        new Vector3(0.52573111f, -0.72360680f, 0.44721360f),
        new Vector3(0.00000000f, -0.89442719f, -0.44721360f),
        new Vector3(-0.85065081f, -0.27639320f, -0.44721360f),
        new Vector3(-0.52573111f, 0.72360680f, -0.44721360f),
        new Vector3(0.52573111f, 0.72360680f, -0.44721360f),
        new Vector3(0.85065081f, -0.27639320f, -0.44721360f),
        new Vector3(0.00000000f, 0.00000000f, 1.00000000f),
        new Vector3(0.00000000f, 0.00000000f, -1.00000000f)
    };

    //各面の頂点
    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 6 }, // １つ目の面
        new int[] { 0, 6, 5 }, // ２つ目の面
        new int[] { 0, 5, 4 }, // ３つ目の面
        new int[] { 0, 4, 10 }, // ４つ目の面
        new int[] { 0, 10, 1 }, // ５つ目の面
        new int[] { 1, 2, 7 }, // ６つ目の面
        new int[] { 1, 7, 6 }, // ７つ目の面
        new int[] { 1, 10, 2 },  // ８つ目の面
        new int[] { 2, 8, 7 },
        new int[] { 3, 2, 10 },
        new int[] { 3, 8, 2 },
        new int[] { 3, 4, 9 },
        new int[] { 3, 9, 8 },
        new int[] { 3, 10, 4 },
        new int[] { 4, 5, 9 },
        new int[] { 5, 6, 11 },
        new int[] { 5, 11, 9 },
        new int[] { 6, 7, 11 },
        new int[] { 7, 8, 11 },
        new int[] { 8, 9, 11 },
        new int[] { 8, 7, 2 }
    };

    void Start()
    {
        //カウントを表示するTextを取得する
        faceText = GameObject.Find("TextCanvas/FaceText").GetComponent<Text>();

        faceButton = GameObject.Find("ResetCanvas/FaceButton").GetComponent<Button>();
        faceButton.onClick.AddListener(ChangeFace);

        Mesh mesh = new Mesh { name = "ComiX Mesh" };
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // 各面のGameObjectを生成し、初期のマテリアルを設定
        faces = new GameObject[triangles.Length];
        for (int i = 0; i < faces.Length; i++)
        {
            GameObject face = new GameObject($"Face_{i}");
            face.transform.parent = transform; // 面をメッシュの子にする
            face.transform.localPosition = Vector3.zero;//ローカル座標をゼロに設定

            Mesh meshCopy = new Mesh(); //各面ごとに新しいメッシュを作成
            meshCopy.vertices = vertexPosition;
            meshCopy.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
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
            //最初のタッチを取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);
                RaycastHit hit;

                //タッチした位置からRayを飛ばし、facesに当たったら色を変更
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
    //ResetCanvas/FaceButtonを押したときに実行
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
