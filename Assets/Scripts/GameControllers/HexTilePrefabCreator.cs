using UnityEngine;

// This is a utility script to help create a hex tile prefab
// It can be attached to an empty GameObject in the Editor to generate a hex mesh

public class HexTilePrefabCreator : MonoBehaviour
{
    [SerializeField] private float size = 1.0f;
    [SerializeField] private float height = 0.2f;
    [SerializeField] private Material material;

    // These vectors define the 6 corners of a pointy-top hexagon with size 1
    private static readonly Vector3[] HexCorners = new Vector3[]
    {
        new Vector3(0f, 0f, 1f),               // Top
        new Vector3(0.866f, 0f, 0.5f),         // Top-right
        new Vector3(0.866f, 0f, -0.5f),        // Bottom-right
        new Vector3(0f, 0f, -1f),              // Bottom
        new Vector3(-0.866f, 0f, -0.5f),       // Bottom-left
        new Vector3(-0.866f, 0f, 0.5f)         // Top-left
    };

    [ContextMenu("Create Hex Mesh")]
    public void CreateHexMesh()
    {
        // Create a new GameObject for the hex
        GameObject hexObj = new GameObject("HexTile");
        hexObj.transform.SetParent(transform);
        hexObj.transform.localPosition = Vector3.zero;

        // Add a MeshFilter and MeshRenderer
        MeshFilter meshFilter = hexObj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = hexObj.AddComponent<MeshRenderer>();

        // Create the mesh
        Mesh hexMesh = GenerateHexMesh();
        meshFilter.mesh = hexMesh;

        // Set the material
        if (material != null)
        {
            meshRenderer.material = material;
        }

        // Add a collider for interaction
        MeshCollider collider = hexObj.AddComponent<MeshCollider>();
        collider.sharedMesh = hexMesh;

        Debug.Log("Hex tile created. You can now create a prefab from this GameObject.");
    }

    private Mesh GenerateHexMesh()
    {
        Mesh mesh = new Mesh();

        // We'll create a mesh with:
        // - 7 vertices on top (6 corners + center)
        // - 7 vertices on bottom (6 corners + center)
        Vector3[] vertices = new Vector3[14];

        // Center vertices
        vertices[0] = new Vector3(0, height / 2, 0);  // Top center
        vertices[7] = new Vector3(0, -height / 2, 0); // Bottom center

        // Create the 6 corner vertices for top and bottom
        for (int i = 0; i < 6; i++)
        {
            // Scale the corner by the size
            Vector3 scaledCorner = HexCorners[i] * size;

            // Top face vertex (offset upward by half height)
            vertices[i + 1] = new Vector3(scaledCorner.x, height / 2, scaledCorner.z);

            // Bottom face vertex (offset downward by half height)
            vertices[i + 8] = new Vector3(scaledCorner.x, -height / 2, scaledCorner.z);
        }

        // Create triangles
        int[] triangles = new int[3 * 6 + 6 * 6 + 3 * 6]; // Top (6 tris) + Sides (12 tris) + Bottom (6 tris)
        int triIndex = 0;

        // Top face (6 triangles forming a hexagon)
        for (int i = 0; i < 6; i++)
        {
            // Connect center to each edge (going clockwise)
            triangles[triIndex++] = 0; // Center top
            triangles[triIndex++] = i + 1; // Current vertex
            triangles[triIndex++] = i < 5 ? i + 2 : 1; // Next vertex (or wrap to first)
        }

        // Side faces (12 triangles forming the 6 sides)
        for (int i = 0; i < 6; i++)
        {
            int topCurrent = i + 1;
            int topNext = i < 5 ? i + 2 : 1;
            int bottomCurrent = i + 8;
            int bottomNext = i < 5 ? i + 9 : 8;

            // First triangle of the side
            triangles[triIndex++] = topCurrent;
            triangles[triIndex++] = bottomCurrent;
            triangles[triIndex++] = topNext;

            // Second triangle of the side
            triangles[triIndex++] = topNext;
            triangles[triIndex++] = bottomCurrent;
            triangles[triIndex++] = bottomNext;
        }

        // Bottom face (6 triangles forming a hexagon, but with reversed winding)
        for (int i = 0; i < 6; i++)
        {
            // Connect center to each edge (going counter-clockwise for correct normal)
            triangles[triIndex++] = 7; // Center bottom
            triangles[triIndex++] = i < 5 ? i + 9 : 8; // Next vertex (or wrap to first)
            triangles[triIndex++] = i + 8; // Current vertex
        }

        // Calculate normals (pointing outward)
        Vector3[] normals = new Vector3[vertices.Length];
        for (int i = 0; i <= 6; i++)
        {
            normals[i] = Vector3.up; // Top vertices
        }
        for (int i = 7; i <= 13; i++)
        {
            normals[i] = Vector3.down; // Bottom vertices
        }

        // Calculate UVs (simple mapping)
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(
                (vertices[i].x / size + 1f) * 0.5f,
                (vertices[i].z / size + 1f) * 0.5f
            );
        }

        // Assign everything to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uvs;

        return mesh;
    }

    // Debug visualization in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Draw the outline of the hexagon
        for (int i = 0; i < 6; i++)
        {
            Vector3 current = transform.position + HexCorners[i] * size;
            Vector3 next = transform.position + HexCorners[(i + 1) % 6] * size;
            Gizmos.DrawLine(current, next);
        }

        // Draw the center
        Gizmos.DrawSphere(transform.position, 0.1f * size);
    }
}