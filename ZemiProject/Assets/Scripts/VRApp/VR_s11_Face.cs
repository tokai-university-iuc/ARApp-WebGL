using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s11_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.13149606f, -0.40470333f, 0.90494412f),
        new Vector3(-0.34426119f, -0.25012042f, 0.90494412f),
        new Vector3(-0.42553024f, -0.00000001f, 0.90494412f),
        new Vector3(-0.34426119f, 0.25012040f, 0.90494412f),
        new Vector3(-0.13149606f, 0.40470332f, 0.90494412f),
        new Vector3(0.13149611f, 0.40470332f, 0.90494412f),
        new Vector3(0.34426124f, 0.25012040f, 0.90494412f),
        new Vector3(0.42553028f, -0.00000001f, 0.90494412f),
        new Vector3(0.34426124f, -0.25012042f, 0.90494412f),
        new Vector3(0.13149611f, -0.40470333f, 0.90494412f),
        new Vector3(-0.13149606f, -0.62841783f, 0.76668096f),
        new Vector3(-0.34426119f, -0.69754941f, 0.62841780f),
        new Vector3(-0.42553024f, -0.80940666f, 0.40470330f),
        new Vector3(-0.34426119f, -0.92126391f, 0.18098881f),
        new Vector3(-0.13149606f, -0.99039549f, 0.04272564f),
        new Vector3(0.13149611f, -0.99039549f, 0.04272564f),
        new Vector3(0.34426124f, -0.92126391f, 0.18098881f),
        new Vector3(0.42553028f, -0.80940666f, 0.40470330f),
        new Vector3(0.34426124f, -0.69754941f, 0.62841780f),
        new Vector3(0.13149611f, -0.62841783f, 0.76668096f),
        new Vector3(-0.55702632f, -0.54296650f, 0.62841780f),
        new Vector3(-0.55702632f, -0.31925200f, 0.76668096f),
        new Vector3(-0.63829537f, -0.06913159f, 0.76668096f),
        new Vector3(-0.76979145f, 0.11185724f, 0.62841780f),
        new Vector3(-0.90128753f, 0.15458291f, 0.40470330f),
        new Vector3(-0.98255658f, 0.04272566f, 0.18098880f),
        new Vector3(-0.98255658f, -0.18098884f, 0.04272564f),
        new Vector3(-0.90128753f, -0.43110925f, 0.04272564f),
        new Vector3(-0.76979145f, -0.61209808f, 0.18098881f),
        new Vector3(-0.63829537f, -0.65482375f, 0.40470330f),
        new Vector3(-0.82001848f, 0.54296648f, 0.18098881f),
        new Vector3(-0.82001848f, 0.40470332f, 0.40470330f),
        new Vector3(-0.68852240f, 0.36197765f, 0.62841780f),
        new Vector3(-0.47575727f, 0.43110923f, 0.76668096f),
        new Vector3(-0.26299214f, 0.58569215f, 0.76668096f),
        new Vector3(-0.13149606f, 0.76668098f, 0.62841780f),
        new Vector3(-0.13149606f, 0.90494414f, 0.40470330f),
        new Vector3(-0.26299214f, 0.94766981f, 0.18098881f),
        new Vector3(-0.47575727f, 0.87853823f, 0.04272564f),
        new Vector3(-0.68852240f, 0.72395531f, 0.04272564f),
        new Vector3(0.47575732f, 0.87853821f, 0.04272564f),
        new Vector3(0.26299219f, 0.94766979f, 0.18098880f),
        new Vector3(0.13149611f, 0.90494413f, 0.40470330f),
        new Vector3(0.13149611f, 0.76668098f, 0.62841780f),
        new Vector3(0.26299219f, 0.58569215f, 0.76668096f),
        new Vector3(0.47575732f, 0.43110923f, 0.76668096f),
        new Vector3(0.68852245f, 0.36197765f, 0.62841780f),
        new Vector3(0.82001853f, 0.40470330f, 0.40470331f),
        new Vector3(0.82001853f, 0.54296646f, 0.18098881f),
        new Vector3(0.68852245f, 0.72395529f, 0.04272564f),
        new Vector3(0.63829541f, -0.65482375f, 0.40470330f),
        new Vector3(0.76979150f, -0.61209808f, 0.18098881f),
        new Vector3(0.90128758f, -0.43110925f, 0.04272564f),
        new Vector3(0.98255663f, -0.18098884f, 0.04272564f),
        new Vector3(0.98255647f, 0.04272561f, 0.18098879f),
        new Vector3(0.90128750f, 0.15458288f, 0.40470331f),
        new Vector3(0.76979150f, 0.11185724f, 0.62841780f),
        new Vector3(0.63829541f, -0.06913159f, 0.76668096f),
        new Vector3(0.55702637f, -0.31925200f, 0.76668096f),
        new Vector3(0.55702637f, -0.54296650f, 0.62841780f),
        new Vector3(-0.47575727f, -0.87853824f, -0.04272569f),
        new Vector3(-0.68852240f, -0.72395533f, -0.04272569f),
        new Vector3(-0.82001848f, -0.54296650f, -0.18098886f),
        new Vector3(-0.82001848f, -0.40470333f, -0.40470335f),
        new Vector3(-0.68852240f, -0.36197767f, -0.62841785f),
        new Vector3(-0.47575727f, -0.43110925f, -0.76668101f),
        new Vector3(-0.26299214f, -0.58569216f, -0.76668101f),
        new Vector3(-0.13149605f, -0.76668099f, -0.62841784f),
        new Vector3(-0.13149606f, -0.90494416f, -0.40470335f),
        new Vector3(-0.26299214f, -0.94766982f, -0.18098885f),
        new Vector3(-0.90128753f, -0.15458292f, -0.40470335f),
        new Vector3(-0.98255658f, -0.04272567f, -0.18098885f),
        new Vector3(-0.98255658f, 0.18098882f, -0.04272569f),
        new Vector3(-0.90128753f, 0.43110923f, -0.04272569f),
        new Vector3(-0.76979145f, 0.61209806f, -0.18098885f),
        new Vector3(-0.63829536f, 0.65482373f, -0.40470335f),
        new Vector3(-0.55702632f, 0.54296648f, -0.62841785f),
        new Vector3(-0.55702632f, 0.31925199f, -0.76668101f),
        new Vector3(-0.63829537f, 0.06913157f, -0.76668101f),
        new Vector3(-0.76979145f, -0.11185726f, -0.62841785f),
        new Vector3(-0.13149606f, 0.62841781f, -0.76668101f),
        new Vector3(-0.34426119f, 0.69754940f, -0.62841785f),
        new Vector3(-0.42553023f, 0.80940665f, -0.40470335f),
        new Vector3(-0.34426119f, 0.92126389f, -0.18098885f),
        new Vector3(-0.13149606f, 0.99039547f, -0.04272569f),
        new Vector3(0.13149611f, 0.99039546f, -0.04272569f),
        new Vector3(0.34426124f, 0.92126387f, -0.18098886f),
        new Vector3(0.42553028f, 0.80940661f, -0.40470335f),
        new Vector3(0.34426124f, 0.69754939f, -0.62841785f),
        new Vector3(0.13149611f, 0.62841781f, -0.76668101f),
        new Vector3(0.76979153f, -0.11185725f, -0.62841782f),
        new Vector3(0.63829541f, 0.06913155f, -0.76668097f),
        new Vector3(0.55702634f, 0.31925197f, -0.76668097f),
        new Vector3(0.55702635f, 0.54296649f, -0.62841782f),
        new Vector3(0.63829541f, 0.65482370f, -0.40470335f),
        new Vector3(0.76979149f, 0.61209803f, -0.18098885f),
        new Vector3(0.90128750f, 0.43110919f, -0.04272571f),
        new Vector3(0.98255648f, 0.18098877f, -0.04272572f),
        new Vector3(0.98255640f, -0.04272561f, -0.18098874f),
        new Vector3(0.90128711f, -0.15458281f, -0.40470320f),
        new Vector3(0.26299219f, -0.94766982f, -0.18098885f),
        new Vector3(0.13149611f, -0.90494416f, -0.40470335f),
        new Vector3(0.13149611f, -0.76668098f, -0.62841784f),
        new Vector3(0.26299219f, -0.58569215f, -0.76668099f),
        new Vector3(0.47575682f, -0.43110886f, -0.76668009f),
        new Vector3(0.68852237f, -0.36197738f, -0.62841730f),
        new Vector3(0.82001805f, -0.40470325f, -0.40470322f),
        new Vector3(0.82001853f, -0.54296650f, -0.18098885f),
        new Vector3(0.68852245f, -0.72395533f, -0.04272569f),
        new Vector3(0.47575732f, -0.87853824f, -0.04272569f),
        new Vector3(-0.13149606f, -0.40470333f, -0.90494417f),
        new Vector3(-0.34426119f, -0.25012042f, -0.90494417f),
        new Vector3(-0.42553024f, -0.00000000f, -0.90494417f),
        new Vector3(-0.34426119f, 0.25012040f, -0.90494417f),
        new Vector3(-0.13149606f, 0.40470332f, -0.90494417f),
        new Vector3(0.13149611f, 0.40470332f, -0.90494417f),
        new Vector3(0.34426120f, 0.25012038f, -0.90494414f),
        new Vector3(0.42553027f, -0.00000003f, -0.90494414f),
        new Vector3(0.34426060f, -0.25012001f, -0.90494332f),
        new Vector3(0.13149611f, -0.40470332f, -0.90494416f)
    };

    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 21, 20, 11, 10 },
        new int[] { 0, 10, 19, 9 },
        new int[] { 0, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
        new int[] { 1, 2, 22, 21 },
        new int[] { 2, 3, 33, 32, 23, 22 },
        new int[] { 3, 4, 34, 33 },
        new int[] { 4, 5, 44, 43, 35, 34 },
        new int[] { 5, 6, 45, 44 },
        new int[] { 6, 7, 57, 56, 46, 45 },
        new int[] { 7, 8, 58, 57 },
        new int[] { 8, 9, 19, 18, 59, 58 },
        new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 },
        new int[] { 11, 20, 29, 12 },
        new int[] { 12, 29, 28, 61, 60, 13 },
        new int[] { 13, 60, 69, 14 },
        new int[] { 14, 69, 68, 101, 100, 15 },
        new int[] { 15, 100, 109, 16 },
        new int[] { 16, 109, 108, 51, 50, 17 },
        new int[] { 17, 50, 59, 18 },
        new int[] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 },
        new int[] { 23, 32, 31, 24 },
        new int[] { 24, 31, 30, 73, 72, 25 },
        new int[] { 25, 72, 71, 26 },
        new int[] { 26, 71, 70, 63, 62, 27 },
        new int[] { 27, 62, 61, 28 },
        new int[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 },
        new int[] { 30, 39, 74, 73 },
        new int[] { 35, 43, 42, 36 },
        new int[] { 36, 42, 41, 85, 84, 37 },
        new int[] { 37, 84, 83, 38 },
        new int[] { 38, 83, 82, 75, 74, 39 },
        new int[] { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 },
        new int[] { 40, 49, 95, 94, 87, 86 },
        new int[] { 40, 86, 85, 41 },
        new int[] { 46, 56, 55, 47 },
        new int[] { 47, 55, 54, 97, 96, 48 },
        new int[] { 48, 96, 95, 49 },
        new int[] { 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 },
        new int[] { 51, 108, 107, 52 },
        new int[] { 52, 107, 106, 99, 98, 53 },
        new int[] { 53, 98, 97, 54 },
        new int[] { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 },
        new int[] { 63, 70, 79, 64 },
        new int[] { 64, 79, 78, 112, 111, 65 },
        new int[] { 65, 111, 110, 66 },
        new int[] { 66, 110, 119, 103, 102, 67 },
        new int[] { 67, 102, 101, 68 },
        new int[] { 70, 71, 72, 73, 74, 75, 76, 77, 78, 79 },
        new int[] { 75, 82, 81, 76 },
        new int[] { 76, 81, 80, 114, 113, 77 },
        new int[] { 77, 113, 112, 78 },
        new int[] { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 },
        new int[] { 80, 89, 115, 114 },
        new int[] { 87, 94, 93, 88 },
        new int[] { 88, 93, 92, 116, 115, 89 },
        new int[] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 },
        new int[] { 90, 99, 106, 105 },
        new int[] { 90, 105, 104, 118, 117, 91 },
        new int[] { 91, 117, 116, 92 },
        new int[] { 100, 101, 102, 103, 104, 105, 106, 107, 108, 109 },
        new int[] { 103, 119, 118, 104 },
        new int[] { 110, 111, 112,113, 114, 115, 116, 117, 118, 119 }
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
                                                triangles[i][0], triangles[i][6], triangles[i][7],
                                                triangles[i][0], triangles[i][7], triangles[i][8],
                                                triangles[i][0], triangles[i][8], triangles[i][9] };
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