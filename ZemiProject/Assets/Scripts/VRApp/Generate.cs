/*
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class Generate : MonoBehaviour
{
    // 読み込みたいobjファイルの名前
    public string objFile_name = null;

    // 読み込みたいobjファイルのフォルダー
    private string objFolder_name = "polyhedrons_obj";

    private string filePath;
    
    // SphereのPrefab
    public GameObject spherePrefab;

    // CylinderのPrefab
    public GameObject cylinderPrefab;

    // 生成したオブジェクトに設定するマテリアル
    public Material vertex_Material;
    public Material edge_Material;
    public Material face_Material;

    //public Material selectedMaterial; // Crashコンポーネントに設定する選択時のマテリアル
    //public TMPro.TextMeshProUGUI countText; // Crashコンポーネントに設定するテキスト

    private List<Vector3> vertices = new List<Vector3>();
    private List<int[]> faces = new List<int[]>();
    private GameObject[] faceObjects;
    private List<int[]> edges = new List<int[]>();
    private GameObject[] edgeObjects;

    void Start()
    {
        StartCoroutine(LoadOBJFile(objFile_name));
    }

    private IEnumerator LoadOBJFile(string fileNameWithExtension)
    {
        filePath = Application.streamingAssetsPath + "/" + objFolder_name + "/" + objFile_name; ;
        Debug.Log("Start loading OBJ file: " + filePath);

        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error while reading file: " + filePath + " : " + www.error);
                yield break;
            }

            string[] lines = www.downloadHandler.text.Split('\n');
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine)) continue;

                string firstChar = trimmedLine.Substring(0, 1);

                if (firstChar == "v")
                {
                    CreateVertex(trimmedLine);
                }
                else if (firstChar == "f")
                {
                    CreateFace(trimmedLine);
                }
            }

            Debug.Log("Finished loading OBJ file. Total vertices: " + vertices.Count + ", Total faces: " + faces.Count);
        }

        CreateVertices();
        CreateEdges();
        CreateMesh();
    }

    void CreateVertex(string line)
    {
        MatchCollection matches = Regex.Matches(line, @"-?\d+\.\d+");
        float x = float.Parse(matches[0].Value);
        float y = float.Parse(matches[1].Value);
        float z = float.Parse(matches[2].Value);
        Vector3 vertex = new Vector3(x, y, z);
        vertices.Add(vertex);
        Debug.Log("Added vertex: " + vertex);
    }

    void CreateFace(string line)
    {
        MatchCollection matches = Regex.Matches(line, @"\d+");
        int[] face = new int[matches.Count];
        for(int i = 0; i < matches.Count; i++)
        {
            face[i] = int.Parse(matches[i].Value) - 1;
        }
        faces.Add(face);

        for(int i = 0; i < face.Length; i++)
        {
            AddEdge(face[i], face[(i + 1) % face.Length]);
        }
        Debug.Log("Added face: " + string.Join(",", face));
    }

    void AddEdge(int v1, int v2)
    {
        int[] edge = new int[] { Mathf.Min(v1, v2), Mathf.Max(v1, v2) };
        foreach (var e in edges)
        {
            if (e[0] == edge[0] && e[1] == edge[1])
            {
                return; // 重複する辺が既に存在する場合は追加しない
            }
        }
        edges.Add(edge);
    }

    void CreateVertices()
    {
        foreach (var vertex in vertices)
        {
            // ワールド座標に変換してからPrefabを生成
            GameObject sphere = Instantiate(spherePrefab, transform.TransformPoint(vertex), Quaternion.identity);

            // 生成したオブジェクトの座標をローカル座標に戻す
            Vector3 localPosition = transform.InverseTransformPoint(sphere.transform.position);
            sphere.transform.localPosition = localPosition;

            // 生成したオブジェクトのマテリアルを設定
            sphere.GetComponent<Renderer>().material = vertex_Material;

            // 生成したオブジェクトをこのオブジェクトの子オブジェクトに設定
            sphere.transform.parent = transform;
        }
    }

    void CreateEdges()
    {
        edgeObjects = new GameObject[edges.Count];
        //localCylinderPos = new Vector3[edges.Count];

        for (int i = 0; i < edges.Count; i++)
        {
            int vertex1 = edges[i][0];
            int vertex2 = edges[i][1];

            Vector3 worldPos1 = transform.TransformPoint(vertices[vertex1]);
            Vector3 worldPos2 = transform.TransformPoint(vertices[vertex2]);
            Vector3 midPoint = (worldPos1 + worldPos2) / 2;
            float distance = Vector3.Distance(worldPos1, worldPos2);

            edgeObjects[i] = Instantiate(cylinderPrefab, midPoint, Quaternion.identity);
            //edgeObjects[i].GetComponent<Crash>().SetUIText(countText);

            // cylinderの向き
            Vector3 direction = worldPos2 - worldPos1;
            edgeObjects[i].transform.up = direction;

            // スケール設定
            edgeObjects[i].transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);

            // ワールド座標からローカル座標に戻す
            Vector3 localPos = transform.InverseTransformPoint(edgeObjects[i].transform.position);
            edgeObjects[i].transform.localPosition = localPos;

            // 子オブジェクトに設定
            edgeObjects[i].transform.SetParent(transform);

            // マテリアル設定
            Renderer cylinderRenderer = edgeObjects[i].GetComponent<Renderer>();
            cylinderRenderer.material = edge_Material;
        }
    }

    void CreateMesh()
    {
        faceObjects = new GameObject[faces.Count];
        for (int i = 0; i < faceObjects.Length; i++)
        {
            GameObject face = new GameObject("Face_" + i);
            face.transform.parent = transform;
            face.transform.localPosition = Vector3.zero;

            Mesh mesh = new Mesh();
            Vector3[] faceVertices = new Vector3[faces[i].Length];
            for (int j = 0; j < faces[i].Length; j++)
            {
                faceVertices[j] = transform.InverseTransformPoint(vertices[faces[i][j]]);
            }
            mesh.vertices = faceVertices;

            if(faces[i].Length == 3) { mesh.triangles = new int[] { 0, 1, 2 }; }
            if(faces[i].Length == 4) { mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 }; }
            if(faces[i].Length == 5) { mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4}; }
            if(faces[i].Length == 6) { mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5 }; }
            if(faces[i].Length == 8) { mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 7 }; }
            if(faces[i].Length == 10) { mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 7, 0, 7, 8, 0, 8, 9 }; }
            mesh.RecalculateNormals(); 

            face.AddComponent<MeshFilter>().mesh = mesh;
            face.AddComponent<MeshRenderer>().material = face_Material;

            MeshCollider meshCollider = face.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            meshCollider.isTrigger = true;

            Rigidbody rigidbody = face.AddComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = false;

            Crash crashComponent = face.AddComponent<Crash>();
            //crashComponent.changeMaterial = selectedMaterial;
            //crashComponent.SetUIText(countText);

            faceObjects[i] = face;
        }
    }
}
*/