using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // 20�̃t�@�C�������i�[���郊�X�g
    public List<string> objFileNames; 

    // ��������I�u�W�F�N�g
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;

    // �e�����}�e���A��
    public Material vertex_Material;
    public Material edge_Material;
    public Material face_Material;

    // �ʂ̏Փ˔��莞�̃}�e���A��
    public Material faceCrashMaterial;

    // 20�̃{�^�����i�[���郊�X�g
    public List<Button> buttons;

    // �J�E���g��\������e�L�X�gUI�I�u�W�F�N�g
    public Text vertexCountText;
    public Text edgeCountText;
    public Text faceCountText;

    // 0bj�t�@�C������l���擾��, �e����ۑ�
    private List<Vector3> vertices = new List<Vector3>();
    private List<int[]> faces = new List<int[]>();
    private HashSet<(int, int)> edges = new HashSet<(int, int)>();

    // �ʂ��Ƃ̒��_�����i�[
    List<Vector3[]> faceVertexList = new List<Vector3[]>();
    
    // �����ς݊e�I�u�W�F�N�g��ۑ�
    private GameObject[] vertexObjects;
    private GameObject[] faceObjects;
    private GameObject[] edgeObjects;

    // �ǂݍ��񂾃I�u�W�F�N�g���i�[���郊�X�g
    private List<GameObject> loadedObjects = new List<GameObject>(); 

    void Start()
    {
        // ������Ԃōŏ��̃I�u�W�F�N�g�����[�h
        StartCoroutine(LoadAndDisplayOBJFile(objFileNames[0]));
    }

    // ���_�p���Z�b�g�{�^�������������Ɏ��s
    public void Vertex_Reset()
    {
        foreach(var vertex in vertexObjects)
        {
            vertex.GetComponent<Crash>().FlagReset();

            // �}�e���A����߂�
            vertex.GetComponent<Renderer>().material = vertex_Material;
        }
    }

    // �ӗp���Z�b�g�{�^�������������Ɏ��s
    public void Edge_Reset()
    {
        foreach(var edge in edgeObjects)
        {
            edge.GetComponent<Crash>().FlagReset();
            edge.GetComponent<Renderer>().material = edge_Material;
        }
    }

    // �ʗp���Z�b�g�{�^�������������Ɏ��s
    public void Face_Reset()
    {
        foreach(var face in faceObjects)
        {
            face.GetComponent<Crash>().FlagReset();
            face.GetComponent<Renderer>().material = face_Material;

            // Crash�R���|�[�l���g�̍Ēǉ�
            Crash re_CrashComponent = face.AddComponent<Crash>();
            re_CrashComponent.changeMaterial = faceCrashMaterial;
            re_CrashComponent.SetUIText(faceCountText);
        }
    }

    // �\���E��\���p�̃{�^�������������Ɏ��s
    public void OnButtonClick(int index)
    {
        StartCoroutine(LoadAndDisplayOBJFile(objFileNames[index]));
    }

    private IEnumerator LoadAndDisplayOBJFile(string fileName)
    {
        // ���ݕ\�����Ă���I�u�W�F�N�g���\���ɂ���
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

            // lines�̒��g�̊m�F
            for(int i = 0; i < lines.Length; i++)
            {
                Debug.Log("lines_" + i +"�̒��g�m�F: " + lines[i]);
            }

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                Debug.Log("trimmedLine�̒��g�m�F: " + trimmedLine);
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

        // matches�̒��g�̊m�F
        for(int i = 0; i < matches.Count; i++)
        {
            Debug.Log("matches_" + i + "�̒��g: " + matches[i]);
        }

        float x = float.Parse(matches[0].Value);
        float y = float.Parse(matches[1].Value);
        float z = float.Parse(matches[2].Value);

        Debug.Log("CreateVertex����line�̒l: " + line + "\n" + "(x, y, z) : " + "(" + x + ", " + y + ", " + z + ")");
        //Debug.Log("(x, y, z) : " + "(" + x + ", " + y + ", " + z + ")");
        
        Vector3 vertex = new Vector3(x, y, z);
        vertices.Add(vertex);
        Debug.Log("Added vertex: " + vertex);
    }

    void CreateFace(string line)
    {
        string[] parts = line.Split(' ');

        // parts�̒��g�m�F
        for(int i = 0; i < parts.Length; i++)
        {
            Debug.Log("parts_" + i + "�̒��g�m�F: " + parts[i]);
        }

        // parts�̒����m�F
        Debug.Log("parts.Length : " + parts.Length);

        int[] face = new int[parts.Length - 1];

        for (int i = 1; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out int index))
            {
                face[i - 1] = index - 1;

                // face[i - 1]�̒��g�m�F
                Debug.Log("face_" + (i - 1) + "�̒��g�m�F : " + face[i - 1]);

                Debug.Log($"Parsed vertex index from string '{parts[i]}' to int: {index - 1}");
                Debug.Log("Parse vertex index: " + (index - 1).GetType());
            }
            else
            {
                Debug.LogError($"Failed to parse vertex index from string '{parts[i]}'");
            }
        }
        faces.Add(face);

        // faces�̒��g�̐��l�m�F
        for(int i = 0; i < faces.Count; i++)
        {
            Debug.Log("faces_" + i + "�̐��l : " + string.Join(",", face));
        }
        Debug.Log("faces.Count : " + faces.Count);

        // face�̒����m�F
        Debug.Log("face.Length : " + face.Length);

        for (int i = 0; i < face.Length; i++)
        {
            AddEdge(face[i], face[(i + 1) % face.Length]);
        }
        Debug.Log("Added face: " + string.Join(",", face));

        // face�̌^���o��
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

            // ���_�̏���
            List<Vector3> meshVertices = new List<Vector3>();
            for (int j = 0; j < faces[i].Length; j++)
            {
                meshVertices.Add(vertices[faces[i][j]]);
            }

            // ���_����4�����̏ꍇ�A�G���[��h�����߂Ƀ_�~�[���_��ǉ�����
            while (meshVertices.Count < 4)
            {
                // �_�~�[�̃[���x�N�g����ǉ�
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

            // Crash�R���|�[�l���g��ǉ�
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
