using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s02_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(-0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(-1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(-0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(-0.30901700f, 0.95105652f, 0.00000000f),
        new Vector3(0.30901699f, 0.95105652f, 0.00000000f),
        new Vector3(0.80901699f, 0.58778525f, 0.00000000f),
        new Vector3(1.00000000f, 0.00000000f, 0.00000000f),
        new Vector3(0.80901699f, -0.58778525f, 0.00000000f),
        new Vector3(0.30901699f, -0.95105652f, 0.00000000f),
        new Vector3(0.00000000f, -0.85065081f, -0.52573111f),
        new Vector3(-0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.50000000f, 0.68819096f, -0.52573111f),
        new Vector3(0.80901699f, -0.26286556f, -0.52573111f),
        new Vector3(-0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.00000000f, 0.52573111f, -0.85065081f),
        new Vector3(0.50000000f, 0.16245985f, -0.85065081f),
        new Vector3(0.30901699f, -0.42532540f, -0.85065081f),
        new Vector3(-0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.50000000f, -0.68819096f, 0.52573111f),
        new Vector3(0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, 0.85065081f, 0.52573111f),
        new Vector3(-0.80901699f, 0.26286556f, 0.52573111f),
        new Vector3(0.00000000f, -0.52573111f, 0.85065081f),
        new Vector3(0.50000000f, -0.16245985f, 0.85065081f),
        new Vector3(0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.30901699f, 0.42532540f, 0.85065081f),
        new Vector3(-0.50000000f, -0.16245985f, 0.85065081f)
    };

    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 11, 15, 10 },
        new int[]{ 0, 10, 9 },
        new int[]{ 0, 9, 21, 25, 20 },
        new int[]{ 0, 20, 1 },
        new int[]{ 1, 2, 11 },
        new int[]{ 1, 20, 29, 24, 2 },
        new int[]{ 2, 3, 12, 16, 11 },
        new int[]{ 2, 24, 3 },
        new int[]{ 3, 4, 12 },
        new int[]{ 3, 24, 28, 23, 4 },
        new int[]{ 4, 5, 13, 17, 12 },
        new int[]{ 4, 23, 5 },
        new int[]{ 5, 6, 13 },
        new int[]{ 5, 23, 27, 22, 6 },
        new int[]{ 6, 7, 14, 18, 13 },
        new int[]{ 6, 22, 7 },
        new int[]{ 7, 8, 14 },
        new int[]{ 7, 22, 26, 21, 8 },
        new int[]{ 8, 9, 10, 19, 14 },
        new int[]{ 8, 21, 9 },
        new int[]{ 10, 15, 19 },
        new int[]{ 11, 16, 15 },
        new int[]{ 12, 17, 16 },
        new int[]{ 13, 18, 17 },
        new int[]{ 14, 19, 18 },
        new int[]{ 15, 16, 17, 18, 19 },
        new int[]{ 20, 25, 29 },
        new int[]{ 21, 26, 25 },
        new int[]{ 22, 27, 26 },
        new int[]{ 23, 28, 27 },
        new int[]{ 24, 29, 28 },
        new int[]{ 25, 26, 27, 28, 29 }
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
            if(triangles[i].Length == 3)
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
            }
            else
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3],
                                            triangles[i][0], triangles[i][3], triangles[i][4] };
            }
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