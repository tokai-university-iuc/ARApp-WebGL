using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s03_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.42640143f, -0.73854895f, 0.52223297f),
        new Vector3(-0.85280287f, 0.00000000f, 0.52223297f),
        new Vector3(-0.42640143f, 0.73854895f, 0.52223297f),
        new Vector3(0.42640143f, 0.73854895f, 0.52223297f),
        new Vector3(0.85280287f, -0.00000000f, 0.52223297f),
        new Vector3(0.42640143f, -0.73854895f, 0.52223297f),
        new Vector3(-0.85280287f, -0.49236596f, -0.17407766f),
        new Vector3(0.00000000f, 0.98473193f, -0.17407766f),
        new Vector3(0.85280287f, -0.49236596f, -0.17407766f),
        new Vector3(-0.42640143f, -0.24618298f, -0.87038828f),
        new Vector3(0.00000000f, 0.49236596f, -0.87038828f),
        new Vector3(0.42640143f, -0.24618298f, -0.87038828f)
    };

    int[][] triangles = new int[][]
    {
        new int[]{ 0, 1, 6 },
        new int[]{ 0, 6, 9, 11, 8, 5 },
        new int[]{ 0, 5, 4, 3, 2, 1 },
        new int[]{ 1, 2, 7, 10, 9, 6 },
        new int[]{ 2, 3, 7 },
        new int[]{ 3, 4, 8, 11, 10, 7 },
        new int[]{ 4, 5, 8 },
        new int[]{ 9, 10, 11 }
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
                                            triangles[i][0], triangles[i][4], triangles[i][5] };
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