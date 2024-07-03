using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_s13L_Face : MonoBehaviour
{
    public Material startFaceColor;
    public Material selectedMaterial;

    GameObject[] faceObjects;

    public Text countText;

    Vector3[] vertexPosition = new Vector3[]
    {
        new Vector3(-0.23192831f, -0.31922195f, 0.91886098f),
        new Vector3(-0.37526789f, 0.12193192f, 0.91886098f),
        new Vector3(0.00000001f, 0.39458001f, 0.91886098f),
        new Vector3(0.37526790f, 0.12193192f, 0.91886098f),
        new Vector3(0.23192832f, -0.31922195f, 0.91886098f),
        new Vector3(-0.64379514f, -0.20918173f, 0.73605007f),
        new Vector3(-0.72920227f, 0.23693218f, 0.64197045f),
        new Vector3(-0.39788728f, 0.54764486f, 0.73605007f),
        new Vector3(0.00000001f, 0.76672864f, 0.64197045f),
        new Vector3(0.39788729f, 0.54764486f, 0.73605007f),
        new Vector3(0.72920229f, 0.23693218f, 0.64197045f),
        new Vector3(0.64379516f, -0.20918173f, 0.73605007f),
        new Vector3(0.45067180f, -0.62029652f, 0.64197045f),
        new Vector3(0.00000001f, -0.67692629f, 0.73605007f),
        new Vector3(-0.45067179f, -0.62029652f, 0.64197045f),
        new Vector3(-0.80031956f, -0.46310151f, 0.38082081f),
        new Vector3(-0.68774804f, 0.61804289f, 0.38082081f),
        new Vector3(0.37526790f, 0.84507302f, 0.38082081f),
        new Vector3(0.91967637f, -0.09575906f, 0.38082081f),
        new Vector3(0.19312336f, -0.90425538f, 0.38082081f),
        new Vector3(-0.53607892f, -0.81262648f, 0.22859680f),
        new Vector3(-0.80975412f, -0.58290795f, -0.06719747f),
        new Vector3(-0.98246410f, -0.17391866f, 0.06719745f),
        new Vector3(-0.93851120f, 0.25872595f, 0.22859680f),
        new Vector3(-0.80460617f, 0.58999346f, -0.06719747f),
        new Vector3(-0.46900456f, 0.88063506f, 0.06719744f),
        new Vector3(-0.04395289f, 0.97252791f, 0.22859680f),
        new Vector3(0.31248017f, 0.94754396f, -0.06719747f),
        new Vector3(0.69260335f, 0.71818105f, 0.06719745f),
        new Vector3(0.91134683f, 0.34232934f, 0.22859680f),
        new Vector3(0.99772955f, -0.00437911f, -0.06719747f),
        new Vector3(0.89705698f, -0.43677477f, 0.06719744f),
        new Vector3(0.60719621f, -0.76095675f, 0.22859680f),
        new Vector3(0.30415062f, -0.95025041f, -0.06719747f),
        new Vector3(-0.13819163f, -0.98812271f, 0.06719745f),
        new Vector3(-0.46900456f, -0.85309854f, -0.22859683f),
        new Vector3(-0.89001331f, -0.25070046f, -0.38082083f),
        new Vector3(-0.95627530f, 0.18242790f, -0.22859683f),
        new Vector3(-0.51345952f, 0.76898225f, -0.38082083f),
        new Vector3(-0.12200607f, 0.96584518f, -0.22859683f),
        new Vector3(0.57267787f, 0.72595761f, -0.38082083f),
        new Vector3(0.88087142f, 0.41449724f, -0.22859683f),
        new Vector3(0.86739393f, -0.32031578f, -0.38082083f),
        new Vector3(0.66641454f, -0.70967180f, -0.22859683f),
        new Vector3(-0.03659893f, -0.92392370f, -0.38082084f),
        new Vector3(-0.33866894f, -0.68787802f, -0.64197048f),
        new Vector3(-0.59886666f, -0.31557553f, -0.73605009f),
        new Vector3(-0.75886533f, 0.10952730f, -0.64197048f),
        new Vector3(-0.48519013f, 0.47203783f, -0.73605009f),
        new Vector3(-0.13033561f, 0.75556961f, -0.64197047f),
        new Vector3(0.29900267f, 0.60731094f, -0.73605008f),
        new Vector3(0.67831351f, 0.35744039f, -0.64197048f),
        new Vector3(0.66998396f, -0.09669904f, -0.73605009f),
        new Vector3(0.54955640f, -0.53465933f, -0.64197049f),
        new Vector3(0.11507018f, -0.66707429f, -0.73605010f),
        new Vector3(-0.17428848f, -0.35400123f, -0.91886092f),
        new Vector3(-0.39053340f, 0.05636580f, -0.91886108f),
        new Vector3(-0.06707427f, 0.38883710f, -0.91886080f),
        new Vector3(0.34907940f, 0.18394910f, -0.91886141f),
        new Vector3(0.28281632f, -0.27515023f, -0.91886006f)
    };

    int[][] triangles = new int[][]
    {
        new int[] { 0, 1, 5 },
        new int[] { 0, 5, 14 },
        new int[] { 0, 14, 13 },
        new int[] { 0, 13, 4 },
        new int[] { 0, 4, 3, 2, 1 },
        new int[] { 1, 2, 7 },
        new int[] { 1, 7, 6 },
        new int[] { 1, 6, 5 },
        new int[] { 2, 3, 9 },
        new int[] { 2, 9, 8 },
        new int[] { 2, 8, 7 },
        new int[] { 3, 4, 11 },
        new int[] { 3, 11, 10 },
        new int[] { 3, 10, 9 },
        new int[] { 4, 13, 12 },
        new int[] { 4, 12, 11 },
        new int[] { 5, 6, 23, 22, 15 },
        new int[] { 5, 15, 14 },
        new int[] { 6, 7, 16 },
        new int[] { 6, 16, 23 },
        new int[] { 7, 8, 26, 25, 16 },
        new int[] { 8, 9, 17 },
        new int[] { 8, 17, 26 },
        new int[] { 9, 10, 29, 28, 17 },
        new int[] { 10, 11, 18 },
        new int[] { 10, 18, 29 },
        new int[] { 11, 12, 32, 31, 18 },
        new int[] { 12, 13, 19 },
        new int[] { 12, 19, 32 },
        new int[] { 13, 14, 20, 34, 19 },
        new int[] { 14, 15, 20 },
        new int[] { 15, 22, 21 },
        new int[] { 15, 21, 20 },
        new int[] { 16, 25, 24 },
        new int[] { 16, 24, 23 },
        new int[] { 17, 28, 27 },
        new int[] { 17, 27, 26 },
        new int[] { 18, 31, 30 },
        new int[] { 18, 30, 29 },
        new int[] { 19, 34, 33 },
        new int[] { 19, 33, 32 },
        new int[] { 20, 21, 35 },
        new int[] { 20, 35, 34 },
        new int[] { 21, 22, 36 },
        new int[] { 21, 36, 46, 45, 35 },
        new int[] { 22, 23, 37 },
        new int[] { 22, 37, 36 },
        new int[] { 23, 24, 37 },
        new int[] { 24, 25, 38 },
        new int[] { 24, 38, 48, 47, 37 },
        new int[] { 25, 26, 39 },
        new int[] { 25, 39, 38 },
        new int[] { 26, 27, 39 },
        new int[] { 27, 28, 40 },
        new int[] { 27, 40, 50, 49, 39 },
        new int[] { 28, 29, 41 },
        new int[] { 28, 41, 40 },
        new int[] { 29, 30, 41 },
        new int[] { 30, 42, 52, 51, 41 },
        new int[] { 30, 31, 42 },
        new int[] { 31, 43, 42 },
        new int[] { 31, 32, 43 },
        new int[] { 32, 33, 43 },
        new int[] { 33, 34, 44 },
        new int[] { 33, 44, 54, 53, 43 },
        new int[] { 34, 35, 44 },
        new int[] { 35, 45, 44 },
        new int[] { 36, 37, 47 },
        new int[] { 36, 47, 46 },
        new int[] { 38, 39, 49 },
        new int[] { 38, 49, 48 },
        new int[] { 40, 41, 51 },
        new int[] { 40, 51, 50 },
        new int[] { 42, 43, 53 },
        new int[] { 42, 53, 52 },
        new int[] { 44, 45, 54 },
        new int[] { 45, 46, 55 },
        new int[] { 45, 55, 54 },
        new int[] { 46, 47, 56 },
        new int[] { 46, 56, 55 },
        new int[] { 47, 48, 56 },
        new int[] { 48, 49, 57 },
        new int[] { 48, 57, 56 },
        new int[] { 49, 50, 57 },
        new int[] { 50, 58, 57 },
        new int[] { 50, 51, 58 },
        new int[] { 51, 52, 58 },
        new int[] { 52, 53, 59 },
        new int[] { 52, 59, 58 },
        new int[] { 53, 54, 59 },
        new int[] { 54, 55, 59 },
        new int[] { 55, 56, 57, 58, 59 },
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
            if (triangles[i].Length == 5)
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2],
                                                triangles[i][0], triangles[i][2], triangles[i][3],
                                                triangles[i][0], triangles[i][3], triangles[i][4] };
            }
            else
            {
                mesh.triangles = new int[] { triangles[i][0], triangles[i][1], triangles[i][2] };
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