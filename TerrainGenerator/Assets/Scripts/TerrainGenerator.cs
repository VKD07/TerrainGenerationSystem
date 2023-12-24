using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Animations;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    [SerializeField] Texture2D heightMap;
    [SerializeField] Vector3[] vertices;
    int[] triangles;

    [SerializeField] int rowX;
    [SerializeField] int columnZ;
    [SerializeField] float height;
    int texSize;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();    
        meshRenderer = GetComponent<MeshRenderer>();

        vertices = new Vector3[(rowX + 1) * (columnZ + 1)];
        texSize = heightMap.width * heightMap.height;

        int i = 0;
        for (int x = 0; x < rowX; x++)
        {
            for (int z = 0; z < columnZ; z++)
            {

                Color pixel = heightMap.GetPixel(Mathf.FloorToInt(z * (heightMap.width - 1) / rowX), Mathf.FloorToInt(x * (heightMap.height - 1) / columnZ));

                vertices[i] = new Vector3(z, pixel.r * height, x);
                i++;
            }
        }

        meshFilter.mesh.vertices = vertices;

        triangles = new int[rowX * columnZ * 6];
        int cv = 0;
        int tris = 0;
        for (int z = 0; z < columnZ; z++)
        {
            for (int x = 0; x < rowX; x++)
            {
                //Drawing first triangle of the square
                triangles[tris + 0] = cv; //first index should be in the 0 index of the vertices array
                triangles[tris + 1] = cv + columnZ; // 0 + the number of column 
                triangles[tris + 2] = cv + columnZ + 1; // the index next to the index above
                // second triangle of the square
                triangles[tris + 3] = cv + columnZ + 1; // using thesame index above
                triangles[tris + 4] = cv + 1; // index 1
                triangles[tris + 5] = cv; // current index
                cv++;
                tris += 6; //skipping the next 6 indeces in the triangle since we have already set 6 indeces above
            }
        }

        meshFilter.mesh.triangles = triangles;

    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;

        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Gizmos.DrawSphere(vertices[i], 0.1f);
        //}
    }
}
