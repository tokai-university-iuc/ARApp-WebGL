using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTest : MonoBehaviour
{
    public GameObject Sphere;
    public Material StartMaterial;
    public Material VertexMaterial;

    //Sphereが衝突したオブジェクト名を出力
    string objName;

    // Start is called before the first frame update
    void Start()
    {
        Renderer StartRenderer = Sphere.GetComponent<Renderer>();
        StartRenderer.material = StartMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "MRHand")
        {
            objName = other.gameObject.name;
            Debug.Log("Sphere is touched by " + objName);
            Renderer SphereRenderer = Sphere.GetComponent<Renderer>();
            SphereRenderer.material = VertexMaterial;
        }
    }
}
