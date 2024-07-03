using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWorldPos : MonoBehaviour
{
    public GameObject Object;

    private void Update()
    {
        Vector3 worldPos = this.transform.position;
        float x = worldPos.x;
        float y = worldPos.y;
        float z = worldPos.z;

        Debug.Log(this.gameObject.name + ", worldPos.x is " + x);
        Debug.Log(this.gameObject.name + ", worldPos.y is " + y);
        Debug.Log(this.gameObject.name + ", worldPos.z is " + z);
    }
}
