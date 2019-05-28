using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshObject : MonoBehaviour
{

    MeshCollider meshCollider;
    public Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uv;
    public float heightLimit;
    public Material material;
    public Color color;

    void Start()
    {
        //GetComponent<MeshFilter>().mesh = GetComponent<MeshRenderer>().;
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        meshCollider = this.gameObject.AddComponent<MeshCollider>();
        uv = new Vector2[vertices.Length];
        material = GetComponent<MeshRenderer>().materials[0];
        color = material.color;
       // UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.uv = uv;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }

    public void DrawMesh(int index, float brushStrenght)
    {
        this.vertices[triangles[index * 3 + 0]] = new Vector3(vertices[triangles[index * 3 + 0]].x, vertices[triangles[index * 3 + 0]].y + brushStrenght, vertices[triangles[index * 3 + 0]].z);
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

}
