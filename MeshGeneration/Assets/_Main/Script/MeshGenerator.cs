using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int xSize = 20;
    public int zSize = 20;
    public float noiseX;
    public float noiseZ;
    public float noisemultiplier;
    public float offset;
    public float heightLimit;
    MeshCollider meshCollider;
    public Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uv;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = this.gameObject.AddComponent<MeshCollider>();
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * noiseX, z * noiseZ) * noisemultiplier + offset;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        uv = new Vector2[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }



        }
    }
    
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }

    public void DrawMesh(int index, float brushStrenght)
    {
        this.vertices[triangles[index * 3 + 0]] = new Vector3(vertices[triangles[index * 3 + 0]].x, vertices[triangles[index * 3 + 0]].y+ brushStrenght, vertices[triangles[index * 3 + 0]].z);
        this.vertices[triangles[index * 3 + 1]] = new Vector3(vertices[triangles[index * 3 + 1]].x, vertices[triangles[index * 3 + 1]].y + brushStrenght, vertices[triangles[index * 3 + 1]].z);
        this.vertices[triangles[index * 3 + 2]] = new Vector3(vertices[triangles[index * 3 + 2]].x, vertices[triangles[index * 3 + 2]].y + brushStrenght, vertices[triangles[index * 3 + 2]].z);
        if (this.vertices[triangles[index * 3 + 0]].y > heightLimit)
            this.vertices[triangles[index * 3 + 0]].y = heightLimit;
        if (this.vertices[triangles[index * 3 + 1]].y > heightLimit)
            this.vertices[triangles[index * 3 + 1]].y = heightLimit;
        if (this.vertices[triangles[index * 3 + 2]].y > heightLimit)
            this.vertices[triangles[index * 3 + 2]].y = heightLimit;

        UpdateMesh();
        Debug.Log("Updates");
    }


    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
    
}
