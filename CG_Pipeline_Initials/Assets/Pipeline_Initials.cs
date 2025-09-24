using System.Collections.Generic;
using UnityEngine;

public class Pipeline_Initials
{
    List<Vector3> vertices;
    List<Vector3Int> faces;

    public Pipeline_Initials()
    {
        defineVertices();
        defineFaces();
        defineTextureVertices();
        CreateUnityGameObject();
    }
    
    void defineVertices()
    {
        vertices = new List<Vector3>();
        
        //Front face
        vertices.Add(new Vector3( -4f, 8f, -2f )); // 0
        vertices.Add(new Vector3( -4f, -8f, -2f )); // 1
        vertices.Add(new Vector3( 1f, -8f, -2f )); // 2
        vertices.Add(new Vector3( 4f, -5f, -2f )); // 3
        vertices.Add(new Vector3( -2f, -5f, -2f )); // 4
        vertices.Add(new Vector3( -2f, 5f, -2f )); // 5
        vertices.Add(new Vector3( 4f, 5f, -2f )); // 6
        vertices.Add(new Vector3( 1f, 8f, -2f )); // 7
        
        //Back Face
        vertices.Add(new Vector3( -4f, 8f, 2f )); // 8
        vertices.Add(new Vector3( -4f, -8f, 2f )); // 9
        vertices.Add(new Vector3( 1f, -8f, 2f )); // 10
        vertices.Add(new Vector3( 4f, -5f, 2f )); // 11
        vertices.Add(new Vector3( -2f, -5f, 2f )); // 12
        vertices.Add(new Vector3( -2f, 5f, 2f )); // 13
        vertices.Add(new Vector3( 4f, 5f, 2f )); // 14
        vertices.Add(new Vector3( 1f, 8f, 2f )); // 15
    }

    void defineFaces()
    {
        faces = new List<Vector3Int>();
        
        //Front face
        faces.Add(new Vector3Int(0, 6, 7));
        faces.Add(new Vector3Int(0, 5, 6));
        faces.Add(new Vector3Int(0, 1, 5));
        faces.Add(new Vector3Int(1, 4, 5));
        faces.Add(new Vector3Int(1, 2, 3));
        faces.Add(new Vector3Int(1, 3, 4));
        
        //Back Face
        faces.Add(new Vector3Int(8, 15, 14));
        faces.Add(new Vector3Int(8, 14, 13));
        faces.Add(new Vector3Int(8, 13, 9));
        faces.Add(new Vector3Int(9, 13, 12));
        faces.Add(new Vector3Int(9, 11, 10));
        faces.Add(new Vector3Int(9, 12, 11));
        
        //Side Faces
        faces.Add(new Vector3Int(4, 13, 5));
        faces.Add(new Vector3Int(4, 12, 13));
        faces.Add(new Vector3Int(5, 14, 6));
        faces.Add(new Vector3Int(5, 13, 14));
        faces.Add(new Vector3Int(3, 12, 4));
        faces.Add(new Vector3Int(3, 11, 12));
        faces.Add(new Vector3Int(2, 11, 3));
        faces.Add(new Vector3Int(2, 10, 11));
        faces.Add(new Vector3Int(1, 9, 10));
        faces.Add(new Vector3Int(1, 10, 2));
        faces.Add(new Vector3Int(0, 8, 9));
        faces.Add(new Vector3Int(0, 9, 1));
        faces.Add(new Vector3Int(6, 14, 15));
        faces.Add(new Vector3Int(6, 15, 7));
        faces.Add(new Vector3Int(7, 15, 8));
        faces.Add(new Vector3Int(7, 8, 0));
    }

    void defineTextureVertices()
    {
        List<Vector2Int> textureVertexInt =  new List<Vector2Int>();
        
        textureVertexInt.Add(new Vector2Int(74, 330)); // 0
        textureVertexInt.Add(new Vector2Int(179, 330)); // 1
        textureVertexInt.Add(new Vector2Int(179, 304)); // 2
        textureVertexInt.Add(new Vector2Int(74, 304)); // 3
        
        textureVertexInt.Add(new Vector2Int(23, 290)); // 4
        textureVertexInt.Add(new Vector2Int(56, 304)); // 5
        textureVertexInt.Add(new Vector2Int(23, 140)); // 6
        textureVertexInt.Add(new Vector2Int(56, 140)); // 7
        
        textureVertexInt.Add(new Vector2Int(79, 103)); // 8
        textureVertexInt.Add(new Vector2Int(178, 103)); // 9
        textureVertexInt.Add(new Vector2Int(79, 133)); // 10
        textureVertexInt.Add(new Vector2Int(178, 133)); // 11
        
        textureVertexInt.Add(new Vector2Int(241, 264)); // 12
        textureVertexInt.Add(new Vector2Int(279, 264)); // 13
        textureVertexInt.Add(new Vector2Int(241, 177)); // 14
        textureVertexInt.Add(new Vector2Int(279, 177)); // 15
        
        textureVertexInt.Add(new Vector2Int(79, 290)); // 16
        textureVertexInt.Add(new Vector2Int(179, 290)); // 17
        textureVertexInt.Add(new Vector2Int(214, 263)); // 18
        textureVertexInt.Add(new Vector2Int(111, 263)); // 19
        textureVertexInt.Add(new Vector2Int(111, 177)); // 20
        textureVertexInt.Add(new Vector2Int(214, 177)); // 21
        textureVertexInt.Add(new Vector2Int(179, 144)); // 22
        textureVertexInt.Add(new Vector2Int(79, 144)); // 23
        
        textureVertexInt.Add(new Vector2Int(352, 291)); // 24
        textureVertexInt.Add(new Vector2Int(319, 265)); // 25
        textureVertexInt.Add(new Vector2Int(420, 265)); // 26
        textureVertexInt.Add(new Vector2Int(420, 178)); // 27
        textureVertexInt.Add(new Vector2Int(317, 178)); // 28
        textureVertexInt.Add(new Vector2Int(352, 146)); // 29
        textureVertexInt.Add(new Vector2Int(452, 146)); // 30
        textureVertexInt.Add(new Vector2Int(452, 292)); // 31
        
        textureVertexInt.Add(new Vector2Int(241, 125)); // 32
        textureVertexInt.Add(new Vector2Int(280, 125)); // 33
        textureVertexInt.Add(new Vector2Int(280, 162)); // 34
        textureVertexInt.Add(new Vector2Int(241, 162)); // 35
        
        textureVertexInt.Add(new Vector2Int(112, 353)); // 36
        textureVertexInt.Add(new Vector2Int(208, 353)); // 37
        textureVertexInt.Add(new Vector2Int(208, 380)); // 38
        textureVertexInt.Add(new Vector2Int(112, 380)); // 39
    }
    
    public GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();
     
        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        /*List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();*/

        for (int i = 0; i < faces.Count; i++)
        {
            //Vector3 normal_for_face = normals[i];

            //normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); //text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); //text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); //text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        /*mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();*/
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
