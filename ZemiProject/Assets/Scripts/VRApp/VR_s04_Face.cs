using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s04_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.31622777f, -0.54772256f, 0.77459667f),
        new Vector3(-0.63245553f, 0.00000000f, 0.77459667f),
        new Vector3(-0.31622777f, 0.54772256f, 0.77459667f),
        new Vector3(0.31622777f, 0.54772256f, 0.77459667f),
        new Vector3(0.63245553f, -0.00000000f, 0.77459667f),
        new Vector3(0.31622777f, -0.54772256f, 0.77459667f),
        new Vector3(-0.31622777f, -0.91287093f, 0.25819889f),
        new Vector3(-0.94868330f, 0.18257419f, 0.25819889f),
        new Vector3(-0.63245553f, 0.73029674f, 0.25819889f),
        new Vector3(0.63245553f, 0.73029674f, 0.25819889f),
        new Vector3(0.94868330f, 0.18257419f, 0.25819889f),
        new Vector3(0.31622777f, -0.91287093f, 0.25819889f),
        new Vector3(-0.63245553f, -0.73029674f, -0.25819889f),
        new Vector3(-0.94868330f, -0.18257419f, -0.25819889f),
        new Vector3(-0.31622777f, 0.91287093f, -0.25819889f),
        new Vector3(0.31622777f, 0.91287093f, -0.25819889f),
        new Vector3(0.94868330f, -0.18257419f, -0.25819889f),
        new Vector3(0.63245553f, -0.73029674f, -0.25819889f),
        new Vector3(-0.31622777f, -0.54772256f, -0.77459667f),
        new Vector3(-0.63245553f, 0.00000000f, -0.77459667f),
        new Vector3(-0.31622777f, 0.54772256f, -0.77459667f),
        new Vector3(0.31622777f, 0.54772256f, -0.77459667f),
        new Vector3(0.63245553f, -0.00000000f, -0.77459667f),
        new Vector3(0.31622777f, -0.54772256f, -0.77459667f)
    };

    int[][] triangles = new int[][]
    {
        new int[]{ 0, 6, 11, 5 },
        new int[]{ 0, 5, 4, 3, 2, 1 },
        new int[]{ 0, 1, 7, 13, 12, 6 },
        new int[]{ 1, 2, 8, 7 },
        new int[]{ 2, 3, 9, 15, 14, 8 },
        new int[]{ 3, 4, 10, 9 },
        new int[]{ 4, 5, 11, 17, 16, 10 },
        new int[]{ 6, 12, 18, 23, 17, 11 },
        new int[]{ 7, 8, 14, 20, 19, 13 },
        new int[]{ 9, 10, 16, 22, 21, 15 },
        new int[]{ 12, 13, 19, 18 },
        new int[]{ 14, 15, 21, 20 },
        new int[]{ 16, 17, 23, 22 },
        new int[]{ 18, 19, 20, 21, 22, 23 }
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
            if(triangles[i].Length == 4)
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                            triangles[i][0], triangles[i][2], triangles[i][3] };
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