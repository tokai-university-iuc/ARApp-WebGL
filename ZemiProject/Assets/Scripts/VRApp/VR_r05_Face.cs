using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_r05_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.52573111f, -0.72360680f, 0.44721360f),
        new Vector3(-0.85065081f, 0.27639320f, 0.44721360f),
        new Vector3(0.00000000f, 0.89442719f, 0.44721360f),
        new Vector3(0.85065081f, 0.27639320f, 0.447213607f),
        new Vector3(0.52573111f, -0.72360680f, 0.44721360f),
        new Vector3(0.00000000f, -0.89442719f, -0.44721360f),
        new Vector3(-0.85065081f, -0.27639320f, -0.44721360f),
        new Vector3(-0.52573111f, 0.72360680f, -0.44721360f),
        new Vector3(0.52573111f, 0.72360680f, -0.44721360f),
        new Vector3(0.85065081f, -0.27639320f, -0.44721360f),
        new Vector3(0.00000000f, 0.00000000f, 1.00000000f),
        new Vector3(0.00000000f, 0.00000000f, -1.00000000f)
    };

    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 6 },
        new int[] { 0, 6, 5 },
        new int[] { 0, 5, 4 },
        new int[] { 0, 4, 10 },
        new int[] { 0, 10, 1 },
        new int[] { 1, 2, 7 },
        new int[] { 1, 7, 6 },
        new int[] { 1, 10, 2 },
        new int[] { 2, 8, 7 },
        new int[] { 3, 2, 10 },
        new int[] { 3, 8, 2 },
        new int[] { 3, 4, 9 },
        new int[] { 3, 9, 8 },
        new int[] { 3, 10, 4 },
        new int[] { 4, 5, 9 },
        new int[] { 5, 6, 11 },
        new int[] { 5, 11, 9 },
        new int[] { 6, 7, 11 },
        new int[] { 7, 8, 11 },
        new int[] { 8, 9, 11 },
        new int[] { 8, 7, 2 }
    };
    // Start is called before the first frame update
    void Start()
    {
        //各面のGameObjectを生成し、初期のマテリアルを設定
        faceObjects = new GameObject[triangles.Length];
        for (int i = 0; i < faceObjects.Length; i++)
        {
            GameObject face = new GameObject();
            face.transform.parent = transform;
            face.transform.localPosition = Vector3.zero;

            Mesh mesh = new Mesh();
            mesh.vertices = vertexPosition;
            mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
            mesh.RecalculateNormals();

            face.AddComponent<MeshFilter>().mesh = mesh;
            face.AddComponent<MeshRenderer>().material = startFaceColor;

            MeshCollider meshCollider = face.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            meshCollider.isTrigger = true;

            Rigidbody rigidbody = face.AddComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = false;

            // Crashコンポーネントを追加
            Crash crashComponent = face.AddComponent<Crash>();
            crashComponent.changeMaterial = selectedMaterial;
            crashComponent.SetUIText(countText);

            faceObjects[i] = face;
        }
    }

    public void Face_Reset()
    {
        for (int i = 0; i < faceObjects.Length; i++)
        {
            faceObjects[i].AddComponent<Crash>().FlagReset();
            Renderer reset_Rederer = faceObjects[i].GetComponent<Renderer>();
            reset_Rederer.material = startFaceColor;

            // Crashコンポーネントの再追加
            Crash re_crashComponent = faceObjects[i].AddComponent<Crash>();
            re_crashComponent.changeMaterial = selectedMaterial;
            re_crashComponent.SetUIText(countText);
        }
    }
}