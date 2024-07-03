using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s02Vertex : MonoBehaviour
{
    [SerializeField] public GameObject spherePrefab;

    public Material StartMaterial;

    //青色のMaterialをアタッチ
    public Material blueMaterial;
    // 頂点ごとのSphereオブジェクト
    GameObject[] spheres;

    Button vertexButton;

    //マテリアルが変更された回数をカウントする変数
    int materialChangeCount = 0;

    //カウントを表示するテキスト
    Text vertexText;

    //変更されたSphereの値を保持するリスト
    List<int> changedSphereIndex = new List<int>();

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(-0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(-1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(-0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(-0.30901700f, 0.95105652f, 0.00000000f),
        new Vector3(0.30901699f, 0.95105652f, 0.00000000f),
        new Vector3(0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(0.00000000f, -0.85065081f, -0.52573111f),
        new Vector3(-0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.00000000f, 0.52573111f, -0.85065081f),
        new Vector3(0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, 0.85065081f, 0.52573111f),
        new Vector3(-0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, -0.52573111f, 0.85065081f),
        new Vector3(0.50000000f, -0.16245985f, 0.85065081f),
        new Vector3(0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.50000000f, -0.16245985f, 0.85065081f)
    };

    // Start is called before the first frame update
    void Start()
    {
        //カウントを表示するTextを取得
        vertexText = GameObject.Find("TextCanvas/VertexText").GetComponent<Text>();

        vertexButton = GameObject.Find("ResetCanvas/VertexButton").GetComponent<Button>();
        vertexButton.onClick.AddListener(ChangeSphere);

        spheres = new GameObject[localVertexPositions.Length];

        for (int i = 0; i < localVertexPositions.Length; i++)
        {
            // ローカル座標をワールド座標に変換してからSphereを配置
            Vector3 worldPosition = transform.TransformPoint(localVertexPositions[i]);
            spheres[i] = Instantiate(spherePrefab, worldPosition, Quaternion.identity);
        }

        //Sphereを配置した後、ローカル座標に戻す
        for (int i = 0; i < spheres.Length; i++)
        {
            Vector3 localPosition = transform.InverseTransformPoint(spheres[i].transform.position);

            //ローカル座標でSphereの位置を更新
            localVertexPositions[i] = localPosition;

            //SphereをVertexGeneratorの子オブジェクトに設定
            spheres[i].transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //タッチ入力の処理
        if (Input.touchCount > 0)
        {
            //最初のタッチを取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);

                RaycastHit hit;

                //タッチした位置からRayを飛ばし、Sphereに当たったら色を変更
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedSphere = hit.transform.gameObject;
                    int sphereIndex = System.Array.IndexOf(spheres, touchedSphere);

                    //Sphereがまだ変更されていない場合のみ処理をおこなう
                    if (sphereIndex >= 0 && !changedSphereIndex.Contains(sphereIndex))
                    {
                        Renderer sphereRenderer = spheres[sphereIndex].GetComponent<Renderer>();

                        sphereRenderer.material = blueMaterial;

                        //変更されたSphereの値をリストに追加
                        changedSphereIndex.Add(sphereIndex);

                        //マテリアルが変更された回数を増やす
                        materialChangeCount++;

                        //カウントを表示するテキストを更新
                        vertexText.text = "頂点：" + materialChangeCount + "個";
                    }
                }
            }
        }
    }

    //ResetCanvas/VertexButtonが押されたときに実行
    void ChangeSphere()
    {
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].GetComponent<Renderer>().material = StartMaterial;
        }

        //materialChangeCountを0にリセット
        materialChangeCount = 0;

        //カウントを表示するテキストを更新
        vertexText.text = "頂点：" + materialChangeCount + "個";

        //更新されたSphereの値を保持するリストもクリア
        changedSphereIndex.Clear();
    }
}
