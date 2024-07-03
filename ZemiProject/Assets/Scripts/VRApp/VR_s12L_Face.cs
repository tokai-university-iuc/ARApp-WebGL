using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s12L_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.37210275f, -0.37210283f, 0.85033943f),
        new Vector3(-0.37210275f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, 0.37210284f, 0.85033943f),
        new Vector3(0.37210291f, -0.37210283f, 0.85033943f),
        new Vector3(-0.68440375f, -0.68440382f, 0.25135842f),
        new Vector3(-0.88671195f, 0.00000000f, 0.46232019f),
        new Vector3(-0.68440375f, 0.68440383f, 0.25135842f),
        new Vector3(0.00000008f, 0.88671203f, 0.46232019f),
        new Vector3(0.68440391f, 0.68440383f, 0.25135842f),
        new Vector3(0.88671211f, 0.00000000f, 0.46232019f),
        new Vector3(0.68440391f, -0.68440382f, 0.25135842f),
        new Vector3(0.00000008f, -0.88671202f, 0.46232019f),
        new Vector3(-0.74420553f, -0.48209560f, -0.46232023f),
        new Vector3(-0.94651373f, 0.20230822f, -0.25135846f),
        new Vector3(-0.48209559f, 0.74420581f, -0.46232023f),
        new Vector3(0.20230824f, 0.94651401f, -0.25135846f),
        new Vector3(0.74420550f, 0.48209553f, -0.46232032f),
        new Vector3(0.94651368f, -0.20230824f, -0.25135852f),
        new Vector3(0.48209586f, -0.74420614f, -0.46232018f),
        new Vector3(-0.20230823f, -0.94651406f, -0.25135841f),
        new Vector3(-0.51460872f, 0.10999275f, -0.85033925f),
        new Vector3(0.10999269f, 0.51461005f, -0.85033999f),
        new Vector3(0.51460748f, -0.10999294f, -0.85033865f),
        new Vector3(-0.10999264f, -0.51460945f, -0.85033947f)
    };

    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 5 },
        new int[] { 0, 5, 4 },
        new int[] { 0, 4, 11 },
        new int[] { 0, 11, 3 },
        new int[] { 0, 3, 2, 1 },
        new int[] { 1, 2, 7 },
        new int[] { 1, 7, 6 },
        new int[] { 1, 6, 5 },
        new int[] { 2, 3, 9 },
        new int[] { 2, 9, 8 },
        new int[] { 2, 8, 7 },
        new int[] { 3, 11, 10 },
        new int[] { 3, 10, 9 },
        new int[] { 4, 5, 13, 12 },
        new int[] { 4, 12, 19 },
        new int[] { 4, 19, 11 },
        new int[] { 5, 6, 13 },
        new int[] { 6, 7, 15, 14 },
        new int[] { 6, 14, 13 },
        new int[] { 7, 8, 15 },
        new int[] { 8, 16, 15 },
        new int[] { 8, 9, 17, 16 },
        new int[] { 9, 10, 17 },
        new int[] { 10, 11, 19, 18 },
        new int[] { 10, 18, 17 },
        new int[] { 12, 13, 20 },
        new int[] { 12, 20, 23 },
        new int[] { 12, 23, 19 },
        new int[] { 13, 14, 20 },
        new int[] { 14, 15, 21 },
        new int[] { 14, 21, 20 },
        new int[] { 15, 16, 21 },
        new int[] { 16, 22, 21 },
        new int[] { 16, 17, 22 },
        new int[] { 17, 18, 22 },
        new int[] { 18, 19, 23 },
        new int[] { 18, 23, 22 },
        new int[] { 20, 21, 22, 23 }
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
            else if (triangles[i].Length == 3)
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
            }
            else
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4]};
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