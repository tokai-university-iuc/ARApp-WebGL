using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s13L_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.23192831f, -0.31922195f, 0.91886098f),
        new Vector3(-0.37526789f, 0.12193192f, 0.91886098f),
        new Vector3(0.00000001f, 0.39458001f, 0.91886098f),
        new Vector3(0.37526790f, 0.12193192f, 0.91886098f),
        new Vector3(0.23192832f, -0.31922195f, 0.91886098f),
        new Vector3(-0.64379514f, -0.20918173f, 0.73605007f),
        new Vector3(-0.72920227f, 0.23693218f, 0.64197045f),
        new Vector3(-0.39788728f, 0.54764486f, 0.73605007f),
        new Vector3(0.00000001f, 0.76672864f, 0.64197045f),
        new Vector3(0.39788729f, 0.54764486f, 0.73605007f),
        new Vector3(0.72920229f, 0.23693218f, 0.64197045f),
        new Vector3(0.64379516f, -0.20918173f, 0.73605007f),
        new Vector3(0.45067180f, -0.62029652f, 0.64197045f),
        new Vector3(0.00000001f, -0.67692629f, 0.73605007f),
        new Vector3(-0.45067179f, -0.62029652f, 0.64197045f),
        new Vector3(-0.80031956f, -0.46310151f, 0.38082081f),
        new Vector3(-0.68774804f, 0.61804289f, 0.38082081f),
        new Vector3(0.37526790f, 0.84507302f, 0.38082081f),
        new Vector3(0.91967637f, -0.09575906f, 0.38082081f),
        new Vector3(0.19312336f, -0.90425538f, 0.38082081f),
        new Vector3(-0.53607892f, -0.81262648f, 0.22859680f),
        new Vector3(-0.80975412f, -0.58290795f, -0.06719747f),
        new Vector3(-0.98246410f, -0.17391866f, 0.06719745f),
        new Vector3(-0.93851120f, 0.25872595f, 0.22859680f),
        new Vector3(-0.80460617f, 0.58999346f, -0.06719747f),
        new Vector3(-0.46900456f, 0.88063506f, 0.06719744f),
        new Vector3(-0.04395289f, 0.97252791f, 0.22859680f),
        new Vector3(0.31248017f, 0.94754396f, -0.06719747f),
        new Vector3(0.69260335f, 0.71818105f, 0.06719745f),
        new Vector3(0.91134683f, 0.34232934f, 0.22859680f),
        new Vector3(0.99772955f, -0.00437911f, -0.06719747f),
        new Vector3(0.89705698f, -0.43677477f, 0.06719744f),
        new Vector3(0.60719621f, -0.76095675f, 0.22859680f),
        new Vector3(0.30415062f, -0.95025041f, -0.06719747f),
        new Vector3(-0.13819163f, -0.98812271f, 0.06719745f),
        new Vector3(-0.46900456f, -0.85309854f, -0.22859683f),
        new Vector3(-0.89001331f, -0.25070046f, -0.38082083f),
        new Vector3(-0.95627530f, 0.18242790f, -0.22859683f),
        new Vector3(-0.51345952f, 0.76898225f, -0.38082083f),
        new Vector3(-0.12200607f, 0.96584518f, -0.22859683f),
        new Vector3(0.57267787f, 0.72595761f, -0.38082083f),
        new Vector3(0.88087142f, 0.41449724f, -0.22859683f),
        new Vector3(0.86739393f, -0.32031578f, -0.38082083f),
        new Vector3(0.66641454f, -0.70967180f, -0.22859683f),
        new Vector3(-0.03659893f, -0.92392370f, -0.38082084f),
        new Vector3(-0.33866894f, -0.68787802f, -0.64197048f),
        new Vector3(-0.59886666f, -0.31557553f, -0.73605009f),
        new Vector3(-0.75886533f, 0.10952730f, -0.64197048f),
        new Vector3(-0.48519013f, 0.47203783f, -0.73605009f),
        new Vector3(-0.13033561f, 0.75556961f, -0.64197047f),
        new Vector3(0.29900267f, 0.60731094f, -0.73605008f),
        new Vector3(0.67831351f, 0.35744039f, -0.64197048f),
        new Vector3(0.66998396f, -0.09669904f, -0.73605009f),
        new Vector3(0.54955640f, -0.53465933f, -0.64197049f),
        new Vector3(0.11507018f, -0.66707429f, -0.73605010f),
        new Vector3(-0.17428848f, -0.35400123f, -0.91886092f),
        new Vector3(-0.39053340f, 0.05636580f, -0.91886108f),
        new Vector3(-0.06707427f, 0.38883710f, -0.91886080f),
        new Vector3(0.34907940f, 0.18394910f, -0.91886141f),
        new Vector3(0.28281632f, -0.27515023f, -0.91886006f)
    };

    // Start is called before the first frame update
    void Start()
    {
        vertexObjects = new GameObject[localVertexPositions.Length];

        //頂点オブジェクト生成
        for (int i = 0; i < localVertexPositions.Length; i++)
        {
            //ローカル座標からワールド座標に変換
            Vector3 worldPos = transform.TransformPoint(localVertexPositions[i]);
            vertexObjects[i] = Instantiate(sphere, worldPos, Quaternion.identity);
            vertexObjects[i].GetComponent<Crash>().SetUIText(countText);

            //ワールド座標からローカル座標に戻す
            Vector3 localPos = transform.InverseTransformPoint(vertexObjects[i].transform.position);

            //ローカル座標でsphereの位置を更新
            localVertexPositions[i] = localPos;

            //Sphereを子オブジェクトに設定
            vertexObjects[i].transform.SetParent(transform);

            Renderer startRenderer = vertexObjects[i].GetComponent<Renderer>();
            startRenderer.material = startMaterial;
        }
    }

    public void Vertex_Reset()
    {
        for (int i = 0; i < vertexObjects.Length; i++)
        {
            // 各頂点オブジェクトのCrashコンポーネントにFlagResetメソッドを追加
            vertexObjects[i].GetComponent<Crash>().FlagReset();

            Renderer reset_Rederer = vertexObjects[i].GetComponent<Renderer>();
            reset_Rederer.material = startMaterial;
        }
    }
}