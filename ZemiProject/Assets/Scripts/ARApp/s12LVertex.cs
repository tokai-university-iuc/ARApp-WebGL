using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s12LVertex : MonoBehaviour
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
        new Vector3(-0.37210275f, -0.37210283f, 0.85033943f),
        new Vector3(-0.37210275f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, -0.37210283f, 0.85033943f),
        new Vector3(-0.68440375f, -0.68440382f, 0.25135842f),
        new Vector3(-0.88671195f, 0.00000000f, 0.46232019f),
        new Vector3(-0.68440375f, 0.68440383f, 0.25135842f),
        new Vector3(0.00000008f, 0.88671203f, 0.46232019f),
        new Vector3(0.68440391f, 0.68440383f, 0.25135842f),
        new Vector3(0.88671211f, 0.00000000f, 0.46232019f),
        new Vector3(0.68440391f, -0.68440382f, 0.25135842f),
        new Vector3(0.00000008f, -0.88671202f, 0.46232019f),
        new Vector3(-0.74420553f, -0.48209560f, -0.46232023f),
        new Vector3(-0.94651373f, 0.20230822f, -0.25135846f),
        new Vector3(-0.48209559f, 0.74420581f, -0.46232023f),
        new Vector3(0.20230824f, 0.94651401f, -0.25135846f),
        new Vector3(0.74420550f, 0.48209553f, -0.46232032f),
        new Vector3(0.94651368f, -0.20230824f, -0.25135852f),
        new Vector3(0.48209586f, -0.74420614f, -0.46232018f),
        new Vector3(-0.20230823f, -0.94651406f, -0.25135841f),
        new Vector3(-0.51460872f, 0.10999275f, -0.85033925f),
        new Vector3(0.10999269f, 0.51461005f, -0.85033999f),
        new Vector3(0.51460748f, -0.10999294f, -0.85033865f),
        new Vector3(-0.10999264f, -0.51460945f, -0.85033947f)
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
