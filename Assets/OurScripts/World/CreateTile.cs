using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class CreateTile : MonoBehaviour
{

    public int posX = 0;
    public int posY = 0;

    // Use this for initialization
    void Start()
    {
        BuildMesh(posX, posY);
    }

    void BuildMesh(int posX, int posY)
    {
        Vector3[] vertices = new Vector3[4];
        int[] trinagles = new int[2 * 3];
        Vector3[] normals = new Vector3[4];
        Vector2[] uv = new Vector2[4];

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 0, -1);
        vertices[3] = new Vector3(1, 0, -1);

        trinagles[0] = 0;
        trinagles[1] = 3;
        trinagles[2] = 2;

        trinagles[3] = 0;
        trinagles[4] = 1;
        trinagles[5] = 3;

        normals[0] = Vector3.up;
        normals[1] = Vector3.up;
        normals[2] = Vector3.up;
        normals[3] = Vector3.up;

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = trinagles;
        mesh.normals = normals;
        mesh.uv = uv;

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;

        transform.position = new Vector3(posX, 0, posY);
    }
}
