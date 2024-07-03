using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s07_Vertex : MonoBehaviour
{
    [SerializeField] public GameObject sphere;
    public Material startMaterial;

    GameObject[] vertexObjects;

    public Text countText;

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