using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s06_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

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