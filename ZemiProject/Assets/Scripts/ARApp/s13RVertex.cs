using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s13RVertex : MonoBehaviour
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
