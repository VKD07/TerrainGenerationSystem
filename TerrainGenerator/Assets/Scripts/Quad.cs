using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh.vertices = new Vector3[]{
            new Vector3(-1,1, 0),
            new Vector3(1,1,0),
            new Vector3(1,-1,0),
            new Vector3(-1,-1,0)
        };

        meshFilter.mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0};
    }
}
