using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s10_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.21573941f, -0.52084100f, 0.82594259f),
        new Vector3(-0.52084100f, -0.21573941f, 0.82594259f),
        new Vector3(-0.52084100f, 0.21573941f, 0.82594259f),
        new Vector3(-0.21573941f, 0.52084100f, 0.82594259f),
        new Vector3(0.21573941f, 0.52084100f, 0.82594259f),
        new Vector3(0.52084100f, 0.21573941f, 0.82594259f),
        new Vector3(0.52084100f, -0.21573941f, 0.82594259f),
        new Vector3(0.21573941f, -0.52084100f, 0.82594259f),
        new Vector3(-0.21573941f, -0.82594259f, 0.52084100f),
        new Vector3(-0.82594259f, -0.21573941f, 0.52084100f),
        new Vector3(-0.82594259f, 0.21573941f, 0.52084100f),
        new Vector3(-0.21573941f, 0.82594259f, 0.52084100f),
        new Vector3(0.21573941f, 0.82594259f, 0.52084100f),
        new Vector3(0.82594259f, 0.21573941f, 0.52084100f),
        new Vector3(0.82594259f, -0.21573941f, 0.52084100f),
        new Vector3(0.21573941f, -0.82594259f, 0.52084100f),
        new Vector3(-0.52084100f, -0.82594259f, 0.21573941f),
        new Vector3(-0.82594259f, -0.52084100f, 0.21573941f),
        new Vector3(-0.82594259f, 0.52084100f, 0.21573941f),
        new Vector3(-0.52084100f, 0.82594259f, 0.21573941f),
        new Vector3(0.52084100f, 0.82594259f, 0.21573941f),
        new Vector3(0.82594259f, 0.52084100f, 0.21573941f),
        new Vector3(0.82594259f, -0.52084100f, 0.21573941f),
        new Vector3(0.52084100f, -0.82594259f, 0.21573941f),
        new Vector3(0.52084100f, -0.82594259f, -0.21573941f),
        new Vector3(0.21573940f, -0.82594259f, -0.52084100f),
        new Vector3(-0.21573941f, -0.82594259f, -0.52084100f),
        new Vector3(-0.52084100f, -0.82594259f, -0.21573941f),
        new Vector3(-0.82594259f, -0.52084100f, -0.21573941f),
        new Vector3(-0.82594259f, -0.21573941f, -0.52084100f),
        new Vector3(-0.82594259f, 0.21573941f, -0.52084100f),
        new Vector3(-0.82594259f, 0.52084100f, -0.21573941f),
        new Vector3(-0.52084100f, 0.82594259f, -0.21573941f),
        new Vector3(-0.21573941f, 0.82594259f, -0.52084100f),
        new Vector3(0.21573941f, 0.82594259f, -0.52084100f),
        new Vector3(0.52084100f, 0.82594259f, -0.21573941f),
        new Vector3(0.82594259f, 0.52084100f, -0.21573941f),
        new Vector3(0.82594259f, 0.21573941f, -0.52084100f),
        new Vector3(0.82594259f, -0.21573941f, -0.52084100f),
        new Vector3(0.82594259f, -0.52084100f, -0.21573941f),
        new Vector3(0.52084100f, -0.21573941f, -0.82594259f),
        new Vector3(0.52084100f, 0.21573941f, -0.82594259f),
        new Vector3(0.21573941f, 0.52084100f, -0.82594259f),
        new Vector3(-0.21573941f, 0.52084100f, -0.82594259f),
        new Vector3(-0.52084100f, 0.21573941f, -0.82594259f),
        new Vector3(-0.52084100f, -0.21573941f, -0.82594259f),
        new Vector3(-0.21573941f, -0.52084100f, -0.82594259f),
        new Vector3(0.21573940f, -0.52084100f, -0.82594259f)
    };

    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 9, 17, 16, 8 },
        new int[] { 0, 8, 15, 7 },
        new int[] { 0, 7, 6, 5, 4, 3, 2, 1 },
        new int[] { 1, 2, 10, 9 },
        new int[] { 2, 3, 11, 19, 18, 10 },
        new int[] { 3, 4, 12, 11 },
        new int[] { 4, 5, 13, 21, 20, 12 },
        new int[] { 5, 6, 14, 13 },
        new int[] { 6, 7, 15, 23, 22, 14 },
        new int[] { 8, 16, 27, 26, 25, 24, 23, 15 },
        new int[] { 9, 10, 18, 31, 30, 29, 28, 17 },
        new int[] { 11, 12, 20, 35, 34, 33, 32, 19 },
        new int[] { 13, 14, 22, 39, 38, 37, 36, 21 },
        new int[] { 16, 17, 28, 27 },
        new int[] { 18, 19, 32, 31 },
        new int[] { 20, 21, 36, 35 },
        new int[] { 22, 23, 24, 39 },
        new int[] { 24, 25, 47, 40, 38, 39 },
        new int[] { 25, 26, 46, 47 },
        new int[] { 26, 27, 28, 29, 45, 46 },
        new int[] { 29, 30, 44, 45 },
        new int[] { 30, 31, 32, 33, 43, 44 },
        new int[] { 33, 34, 42, 43 },
        new int[] { 34, 35, 36, 37, 41, 42 },
        new int[] { 37, 38, 40, 41 },
        new int[] { 40, 47, 46, 45, 44, 43, 42, 41 }
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
            if (triangles[i].Length == 4)
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3] };
            }
            else if (triangles[i].Length == 6)
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4],
                                                triangles[i][0], triangles[i][4], triangles[i][5] };
            }
            else
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4],
                                                triangles[i][0], triangles[i][4], triangles[i][5],
                                                triangles[i][0], triangles[i][5], triangles[i][6],
                                                triangles[i][0], triangles[i][6], triangles[i][7]};
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