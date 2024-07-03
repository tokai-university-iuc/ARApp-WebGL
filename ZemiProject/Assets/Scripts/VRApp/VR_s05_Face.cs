using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s05_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.28108464f, -0.67859834f, 0.67859834f),
        new Vector3(-0.67859834f, -0.28108464f, 0.67859834f),
        new Vector3(-0.67859834f, 0.28108464f, 0.67859834f),
        new Vector3(-0.28108464f, 0.67859834f, 0.67859834f),
        new Vector3(0.28108464f, 0.67859834f, 0.67859834f),
        new Vector3(0.67859834f, 0.28108464f, 0.67859834f),
        new Vector3(0.67859834f, -0.28108464f, 0.67859834f),
        new Vector3(0.28108464f, -0.67859834f, 0.67859834f),
        new Vector3(0.00000000f, -0.95968298f, 0.28108464f),
        new Vector3(-0.95968298f, 0.00000000f, 0.28108464f),
        new Vector3(0.00000000f, 0.95968298f, 0.28108464f),
        new Vector3(0.95968298f, -0.00000000f, 0.28108464f),
        new Vector3(-0.00000000f, -0.95968298f, -0.28108464f),
        new Vector3(-0.95968298f, 0.00000000f, -0.28108464f),
        new Vector3(0.00000000f, 0.95968298f, -0.28108464f),
        new Vector3(0.95968298f, -0.00000000f, -0.28108464f),
        new Vector3(-0.28108464f, -0.67859834f, -0.67859834f),
        new Vector3(-0.67859834f, -0.28108464f, -0.67859834f),
        new Vector3(-0.67859834f, 0.28108464f, -0.67859834f),
        new Vector3(-0.28108464f, 0.67859834f, -0.67859834f),
        new Vector3(0.28108464f, 0.67859834f, -0.67859834f),
        new Vector3(0.67859834f, 0.28108464f, -0.67859834f),
        new Vector3(0.67859834f, -0.28108464f, -0.67859834f),
        new Vector3(0.28108464f, -0.67859834f, -0.67859834f)
    };

    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 3 },
        new int[] { 0, 3, 2 },
        new int[] { 0, 2, 1 },
        new int[] { 1, 2, 3 }
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
                                            triangles[i][0], triangles[i][3], triangles[i][4],
                                            triangles[i][0], triangles[i][4], triangles[i][5],
                                            triangles[i][0], triangles[i][5], triangles[i][6],
                                            triangles[i][0], triangles[i][6], triangles[i][7] };
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