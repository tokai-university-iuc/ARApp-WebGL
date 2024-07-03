using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s08_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

    Vector3[] localVertexPositions = new Vector3[]
    {
        new Vector3(-0.35740674f, -0.86285621f, 0.35740674f),
        new Vector3(-0.86285621f, -0.35740674f, 0.35740674f),
        new Vector3(-0.86285621f, 0.35740674f, 0.35740674f),
        new Vector3(-0.35740674f, 0.86285621f, 0.35740674f),
        new Vector3(0.35740674f, 0.86285621f, 0.35740674f),
        new Vector3(0.86285621f, 0.35740674f, 0.35740674f),
        new Vector3(0.86285621f, -0.35740674f, 0.35740674f),
        new Vector3(0.35740674f, -0.86285621f, 0.35740674f),
        new Vector3(-0.35740674f, -0.86285621f, -0.35740674f),
        new Vector3(-0.86285621f, -0.35740674f, -0.35740674f),
        new Vector3(-0.86285621f, 0.35740674f, -0.35740674f),
        new Vector3(-0.35740674f, 0.86285621f, -0.35740674f),
        new Vector3(0.35740674f, 0.86285621f, -0.35740674f),
        new Vector3(0.86285621f, 0.35740674f, -0.35740674f),
        new Vector3(0.86285621f, -0.35740674f, -0.35740674f),
        new Vector3(0.35740674f, -0.86285621f, -0.35740674f),
        new Vector3(-0.35740674f, -0.35740674f, 0.86285621f),
        new Vector3(0.35740674f, -0.35740674f, 0.86285621f),
        new Vector3(0.35740674f, 0.35740674f, 0.86285621f),
        new Vector3(-0.35740674f, 0.35740674f, 0.86285621f),
        new Vector3(-0.35740674f, -0.35740674f, -0.86285621f),
        new Vector3(-0.35740674f, 0.35740674f, -0.86285621f),
        new Vector3(0.35740674f, 0.35740674f, -0.86285621f),
        new Vector3(0.35740674f, -0.35740674f, -0.86285621f)
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