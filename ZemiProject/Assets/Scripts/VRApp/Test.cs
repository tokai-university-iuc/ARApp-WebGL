using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // 20個のファイル名を格納するリスト
    public List<string> objFileNames; 

    // 生成するオブジェクト
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;

    // 各初期マテリアル
    public Material vertex_Material;
    public Material edge_Material;
    public Material face_Material;

    // 面の衝突判定時のマテリアル
    public Material faceCrashMaterial;

    // 20個のボタンを格納するリスト
    public List<Button> buttons;

    // カウントを表示するテキストUIオブジェクト
    public Text vertexCountText;
    public Text edgeCountText;
    public Text faceCountText;

    // 0bjファイルから値を取得し, 各情報を保存
    private List<Vector3> vertices = new List<Vector3>();
    private List<int[]> faces = new List<int[]>();
    private HashSet<(int, int)> edges = new HashSet<(int, int)>();

    // 面ごとの頂点情報を格納
    List<Vector3[]> faceVertexList = new List<Vector3[]>();
    
    // 生成済み各オブジェクトを保存
    private GameObject[] vertexObjects;
    private GameObject[] faceObjects;
    private GameObject[] edgeObjects;

    // 読み込んだオブジェクトを格納するリスト
    private List<GameObject> loadedObjects = new List<GameObject>(); 

    void Start()
    {
        // 初期状態で最初のオブジェクトをロード
        StartCoroutine(LoadAndDisplayOBJFile(objFileNames[0]));
    }

    // 頂点用リセットボタンを押した時に実行
    public void Vertex_Reset()
    {
        foreach(var vertex in vertexObjects)
        {
            vertex.GetComponent<Crash>().FlagReset();

            // マテリアルを戻す
            vertex.GetComponent<Renderer>().material = vertex_Material;
        }
    }

    // 辺用リセットボタンを押した時に実行
    public void Edge_Reset()
    {
        foreach(var edge in edgeObjects)
        {
            edge.GetComponent<Crash>().FlagReset();
            edge.GetComponent<Renderer>().material = edge_Material;
        }
    }

    // 面用リセットボタンを押した時に実行
    public void Face_Reset()
    {
        foreach(var face in faceObjects)
        {
            face.GetComponent<Crash>().FlagReset();
            face.GetComponent<Renderer>().material = face_Material;

            // Crashコンポーネントの再追加
            Crash re_CrashComponent = face.AddComponent<Crash>();
            re_CrashComponent.changeMaterial = faceCrashMaterial;
            re_CrashComponent.SetUIText(faceCountText);
        }
    }

    // 表示・非表示用のボタンを押した時に実行
    public void OnButtonClick(int index)
    {
        StartCoroutine(LoadAndDisplayOBJFile(objFileNames[index]));
    }

    private IEnumerator LoadAndDisplayOBJFile(string fileName)
    {
        // 現在表示しているオブジェクトを非表示にする
        foreach (var obj_show in loadedObjects)
        {
            obj_show.SetActive(false);
        }

        string filePath = Path.Combine(Application.streamingAssetsPath, "polyhedrons_obj", fileName);
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
            vertices.Clear();
            faces.Clear();
            edges.Clear();

            // linesの中身の確認
            for(int i = 0; i < lines.Length; i++)
            {
                Debug.Log("lines_" + i +"の中身確認: " + lines[i]);
            }

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                Debug.Log("trimmedLineの中身確認: " + trimmedLine);
                if (string.IsNullOrEmpty(trimmedLine)) continue;

                if (trimmedLine.StartsWith("v "))
                {
                    CreateVertex(trimmedLine);
                }
                else if (trimmedLine.StartsWith("f "))
                {
                    CreateFace(trimmedLine);
                }
            }

            Debug.Log("Finished loading OBJ file. Total vertices: " + vertices.Count + ", Total faces: " + faces.Count);
        }

        GameObject obj = new GameObject(fileName);
        obj.transform.parent = transform;
        loadedObjects.Add(obj);

        CreateVertices(obj);
        CreateEdges(obj);
        CreateMesh(obj);
    }
    void CreateVertex(string line)
    {
        MatchCollection matches = Regex.Matches(line, @"-?\d+\.\d+");

        // matchesの中身の確認
        for(int i = 0; i < matches.Count; i++)
        {
            Debug.Log("matches_" + i + "の中身: " + matches[i]);
        }

        float x = float.Parse(matches[0].Value);
        float y = float.Parse(matches[1].Value);
        float z = float.Parse(matches[2].Value);

        Debug.Log("CreateVertex内のlineの値: " + line + "\n" + "(x, y, z) : " + "(" + x + ", " + y + ", " + z + ")");
        //Debug.Log("(x, y, z) : " + "(" + x + ", " + y + ", " + z + ")");
        
        Vector3 vertex = new Vector3(x, y, z);
        vertices.Add(vertex);
        Debug.Log("Added vertex: " + vertex);
    }

    void CreateFace(string line)
    {
        string[] parts = line.Split(' ');

        // partsの中身確認
        for(int i = 0; i < parts.Length; i++)
        {
            Debug.Log("parts_" + i + "の中身確認: " + parts[i]);
        }

        // partsの長さ確認
        Debug.Log("parts.Length : " + parts.Length);

        int[] face = new int[parts.Length - 1];

        for (int i = 1; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out int index))
            {
                face[i - 1] = index - 1;

                // face[i - 1]の中身確認
                Debug.Log("face_" + (i - 1) + "の中身確認 : " + face[i - 1]);

                Debug.Log($"Parsed vertex index from string '{parts[i]}' to int: {index - 1}");
                Debug.Log("Parse vertex index: " + (index - 1).GetType());
            }
            else
            {
                Debug.LogError($"Failed to parse vertex index from string '{parts[i]}'");
            }
        }
        faces.Add(face);

        // facesの中身の数値確認
        for(int i = 0; i < faces.Count; i++)
        {
            Debug.Log("faces_" + i + "の数値 : " + string.Join(",", face));
        }
        Debug.Log("faces.Count : " + faces.Count);

        // faceの長さ確認
        Debug.Log("face.Length : " + face.Length);

        for (int i = 0; i < face.Length; i++)
        {
            AddEdge(face[i], face[(i + 1) % face.Length]);
        }
        Debug.Log("Added face: " + string.Join(",", face));

        // faceの型を出力
        Debug.Log("Face array type: " + face.GetType());
    }

    void AddEdge(int v1, int v2)
    {
        var edge = (Mathf.Min(v1, v2), Mathf.Max(v1, v2));
        edges.Add(edge);
    }

    void CreateVertices(GameObject parentObject)
    {
        vertexObjects = new GameObject[vertices.Count];
        int index = 0;
        
        foreach (var vertex in vertices)
        {
            GameObject sphere = Instantiate(spherePrefab, parentObject.transform.TransformPoint(vertex), Quaternion.identity);
            sphere.GetComponent<Crash>().SetUIText(vertexCountText);
            sphere.transform.localPosition = parentObject.transform.InverseTransformPoint(sphere.transform.position);
            sphere.GetComponent<Renderer>().material = vertex_Material;
            sphere.transform.parent = parentObject.transform;

            vertexObjects[index] = sphere;
            index++;
        }
    }

    void CreateEdges(GameObject parentObject)
    {
        edgeObjects = new GameObject[edges.Count];
        int index = 0;

        foreach (var edge in edges)
        {
            int vertex1 = edge.Item1;
            int vertex2 = edge.Item2;

            Vector3 worldPos1 = parentObject.transform.TransformPoint(vertices[vertex1]);
            Vector3 worldPos2 = parentObject.transform.TransformPoint(vertices[vertex2]);
            Vector3 midPoint = (worldPos1 + worldPos2) / 2;
            float distance = Vector3.Distance(worldPos1, worldPos2);

            GameObject cylinder = Instantiate(cylinderPrefab, midPoint, Quaternion.identity);
            cylinder.GetComponent<Crash>().SetUIText(edgeCountText);
            cylinder.transform.up = worldPos2 - worldPos1;
            cylinder.transform.localScale = new Vector3(0.1f, distance / 2, 0.1f);
            cylinder.transform.localPosition = parentObject.transform.InverseTransformPoint(cylinder.transform.position);
            cylinder.transform.SetParent(parentObject.transform);
            cylinder.GetComponent<Renderer>().material = edge_Material;

            edgeObjects[index] = cylinder;
            index++;
        }
    }

    void CreateMesh(GameObject parentObject)
    {
        faceObjects = new GameObject[faces.Count];
        for (int i = 0; i < faces.Count; i++)
        {
            GameObject face = new GameObject("Face_" + i);
            face.transform.parent = parentObject.transform;
            face.transform.localPosition = Vector3.zero;

            Mesh mesh = new Mesh();

            // 頂点の準備
            List<Vector3> meshVertices = new List<Vector3>();
            for (int j = 0; j < faces[i].Length; j++)
            {
                meshVertices.Add(vertices[faces[i][j]]);
            }

            // 頂点数が4未満の場合、エラーを防ぐためにダミー頂点を追加する
            while (meshVertices.Count < 4)
            {
                // ダミーのゼロベクトルを追加
                meshVertices.Add(Vector3.zero); 
            }

            mesh.SetVertices(meshVertices);

            int[] triangles = GenerateTriangles(faces[i].Length);
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            face.AddComponent<MeshFilter>().mesh = mesh;
            face.AddComponent<MeshRenderer>().material = face_Material;

            MeshCollider meshCollider = face.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            meshCollider.isTrigger = true;

            Rigidbody rigidbody = face.AddComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = false;

            // Crashコンポーネントを追加
            Crash crashComponent = face.AddComponent<Crash>();
            crashComponent.changeMaterial = faceCrashMaterial;
            crashComponent.SetUIText(faceCountText);

            faceObjects[i] = face;
        }
    }

    int[] GenerateTriangles(int vertexCount)
    {
        List<int> triangles = new List<int>();

        for (int i = 1; i < vertexCount - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }
        return triangles.ToArray();
    }
}
