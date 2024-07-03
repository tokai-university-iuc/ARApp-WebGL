using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_r04_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.35682209f, -0.49112347f, 0.79465447f),
        new Vector3(0.35682209f, -0.49112347f, 0.79465447f),
        new Vector3(0.57735027f, -0.79465447f, 0.18759247f),
        new Vector3(0.00000000f, -0.98224695f, -0.18759247f),
        new Vector3(-0.57735027f, -0.79465447f, 0.18759247f),
        new Vector3(-0.57735027f, 0.18759247f, 0.79465447f),
        new Vector3(0.57735027f, 0.18759247f, 0.79465447f),
        new Vector3(0.93417236f, -0.30353100f, -0.18759247f),
        new Vector3(0.00000000f, -0.60706200f, -0.79465447f),
        new Vector3(-0.93417236f, -0.30353100f, -0.18759247f),
        new Vector3(-0.93417236f, 0.30353100f, 0.18759247f),
        new Vector3(0.00000000f, 0.60706200f, 0.79465447f),
        new Vector3(0.93417236f, 0.30353100f, 0.18759247f),
        new Vector3(0.57735027f, -0.18759247f, -0.79465447f),
        new Vector3(-0.57735027f, -0.18759247f, -0.79465447f),
        new Vector3(-0.57735027f, 0.79465447f, -0.18759247f),
        new Vector3(0.00000000f, 0.98224695f, 0.18759247f),
        new Vector3(0.57735027f, 0.79465447f, -0.18759247f),
        new Vector3(0.35682209f, 0.49112347f, -0.79465447f),
        new Vector3(-0.35682209f, 0.49112347f, -0.79465447f)
    };

    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 6, 11, 5 },
        new int[]{ 0, 5, 10, 9, 4 },
        new int[]{ 0, 4, 3, 2, 1 },
        new int[]{ 1, 2, 7, 12, 6 },
        new int[]{ 2, 3, 8, 13, 7 },
        new int[]{ 3, 4, 9, 14, 8 },
        new int[]{ 5, 11, 16, 15, 10 },
        new int[]{ 6, 12, 17, 16, 11 },
        new int[]{ 7, 13, 18, 17, 12 },
        new int[]{ 8, 14, 19, 18, 13 },
        new int[]{ 9, 10, 15, 19, 14 },
        new int[]{ 15, 16, 17, 18, 19 }
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
            mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                        triangles[i][0], triangles[i][2], triangles[i][3],
                                        triangles[i][0], triangles[i][3], triangles[i][4] };
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