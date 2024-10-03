using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class CreateTriangleMesh : MonoBehaviour
{
    private void Start()
    {
        // Create the mesh
        Mesh mesh = new Mesh();
        mesh.name = "TriangleMesh"; // Optionally name the mesh
        GetComponent<MeshFilter>().mesh = mesh;

        // Define the vertices of the triangle
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0.5f, Mathf.Sqrt(0.75f), 0)
        };

        // Define the single triangle
        int[] triangles = new int[]
        {
            0, 1, 2
        };

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals for correct lighting
        mesh.RecalculateNormals();

        // Assign the generated mesh to the MeshFilter
        GetComponent<MeshFilter>().mesh = mesh;

        // Assign the generated mesh to the MeshCollider
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
