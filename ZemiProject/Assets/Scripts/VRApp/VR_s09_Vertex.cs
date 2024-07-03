using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s09_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.22391898f, -0.68915176f, 0.68915176f),
        new Vector3(-0.5862275f, -0.42591921f, 0.68915176f),
        new Vector3(-0.72461704f, 0.0f, 0.68915176f),
        new Vector3(-0.5862275f, 0.42591921f, 0.68915176f),
        new Vector3(-0.22391898f, 0.68915176f, 0.68915176f),
        new Vector3(0.22391898f, 0.68915176f, 0.68915176f),
        new Vector3(0.5862275f, 0.42591921f, 0.68915176f),
        new Vector3(0.72461704f, 0.0f, 0.68915176f),
        new Vector3(0.5862275f, -0.42591921f, 0.68915176f),
        new Vector3(0.22391898f, -0.68915176f, 0.68915176f),
        new Vector3(-0.22391898f, -0.92459411f, 0.30819804f),
        new Vector3(-0.81014648f, -0.4986749f, 0.30819804f),
        new Vector3(-0.94853602f, -0.07275569f, 0.30819804f),
        new Vector3(-0.72461704f, 0.61639607f, 0.30819804f),
        new Vector3(-0.36230852f, 0.87962862f, 0.30819804f),
        new Vector3(0.36230852f, 0.87962862f, 0.30819804f),
        new Vector3(0.72461704f, 0.61639607f, 0.30819804f),
        new Vector3(0.94853602f, -0.07275569f, 0.30819804f),
        new Vector3(0.81014648f, -0.4986749f, 0.30819804f),
        new Vector3(0.22391898f, -0.92459411f, 0.30819804f),
        new Vector3(0.0f, -0.99734979f, -0.07275569f),
        new Vector3(-0.5862275f, -0.80687293f, 0.07275569f),
        new Vector3(-0.94853602f, 0.30819804f, 0.07275569f),
        new Vector3(0.0f, 0.99734979f, 0.07275569f),
        new Vector3(0.94853602f, 0.30819803f, 0.07275569f),
        new Vector3(0.5862275f, -0.80687293f, 0.07275569f),
        new Vector3(-0.36230852f, -0.87962862f, -0.30819804f),
        new Vector3(-0.22391898f, -0.68915176f, -0.68915176f),
        new Vector3(-0.22391898f, -0.30819804f, -0.92459411f),
        new Vector3(-0.36230852f, 0.11772117f, -0.92459411f),
        new Vector3(-0.5862275f, 0.42591921f, -0.68915176f),
        new Vector3(-0.81014648f, 0.4986749f, -0.30819804f),
        new Vector3(0.36230852f, -0.87962862f, -0.30819804f),
        new Vector3(0.22391898f, -0.68915176f, -0.68915176f),
        new Vector3(0.22391898f, -0.30819803f, -0.92459411f),
        new Vector3(0.36230852f, 0.11772117f, -0.92459411f),
        new Vector3(0.5862275f, 0.42591921f, -0.68915176f),
        new Vector3(0.81014648f, 0.4986749f, -0.30819804f),
        new Vector3(0.0f, 0.38095372f, -0.9245941f),
        new Vector3(-0.22391898f, 0.68915176f, -0.68915176f),
        new Vector3(0.22391898f, 0.68915176f, -0.68915176f),
        new Vector3(-0.22391898f, 0.92459411f, -0.30819804f),
        new Vector3(0.22391898f, 0.92459411f, -0.30819804f),
        new Vector3(0.5862275f, 0.80687293f, -0.07275569f),
        new Vector3(-0.5862275f, 0.80687293f, -0.07275569f),
        new Vector3(0.0f, -0.38095372f, 0.92459411f),
        new Vector3(0.36230852f, -0.11772117f, 0.92459411f),
        new Vector3(0.22391898f, 0.30819804f, 0.92459411f),
        new Vector3(-0.22391898f, 0.30819804f, 0.92459411f),
        new Vector3(-0.36230852f, -0.11772117f, 0.92459411f),
        new Vector3(-0.94853602f, -0.30819804f, -0.07275569f),
        new Vector3(-0.94853602f, 0.07275569f, -0.30819804f),
        new Vector3(-0.72461704f, 0.0f, -0.68915176f),
        new Vector3(-0.5862275f, -0.42591921f, -0.68915176f),
        new Vector3(-0.72461704f, -0.61639607f, -0.30819804f),
        new Vector3(0.94853602f, -0.30819804f, -0.07275569f),
        new Vector3(0.72461704f, -0.61639607f, -0.30819804f),
        new Vector3(0.5862275f, -0.42591921f, -0.68915176f),
        new Vector3(0.72461704f, 0.0f, -0.68915176f),
        new Vector3(0.94853602f, 0.07275569f, -0.30819804f)
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