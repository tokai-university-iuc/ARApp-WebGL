using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s07Vertex : MonoBehaviour
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
