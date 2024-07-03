using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s12R_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

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