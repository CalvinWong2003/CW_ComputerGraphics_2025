using System;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline_Initials
{
    internal List<Vector3> vertices;
    List<Vector3Int> faces;
    private List<Vector3Int> texture_index_list;
    private List<Vector2> texture_coordinates;
    List<Vector3> normals;
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
        texture_index_list = new List<Vector3Int>();
        normals = new List<Vector3>();
        
        //Front face
        faces.Add(new Vector3Int(0, 6, 7)); texture_index_list.Add(new Vector3Int(23, 21, 22)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(0, 5, 6)); texture_index_list.Add(new Vector3Int(23, 20, 21)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(0, 1, 5)); texture_index_list.Add(new Vector3Int(23, 16, 20)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(1, 4, 5)); texture_index_list.Add(new Vector3Int(16, 19, 20)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(1, 2, 3)); texture_index_list.Add(new Vector3Int(16, 17, 18)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(1, 3, 4)); texture_index_list.Add(new Vector3Int(16, 18, 19)); normals.Add(new Vector3(0, 0, 1));
        //
        //new Vector3Int(30, 27, 26)
        //Back Face
        faces.Add(new Vector3Int(8, 15, 14)); texture_index_list.Add(new Vector3Int(30, 29, 28)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(8, 14, 13)); texture_index_list.Add(new Vector3Int(30, 28, 27)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(8, 13, 9)); texture_index_list.Add(new Vector3Int(26, 30, 27)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(9, 13, 12)); texture_index_list.Add(new Vector3Int(31, 27, 26)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(9, 11, 10)); texture_index_list.Add(new Vector3Int(31, 25, 24)); normals.Add(new Vector3(0, 0, -1));
        faces.Add(new Vector3Int(9, 12, 11)); texture_index_list.Add(new Vector3Int(31, 26, 25)); normals.Add(new Vector3(0, 0, -1));
        
        //Side Faces
        faces.Add(new Vector3Int(4, 13, 5)); texture_index_list.Add(new Vector3Int(12, 15, 14)); normals.Add(new Vector3(1, 0, 0));
        faces.Add(new Vector3Int(4, 12, 13)); texture_index_list.Add(new Vector3Int(12, 13, 15)); normals.Add(new Vector3(1, 0, 0));
        faces.Add(new Vector3Int(5, 14, 6)); texture_index_list.Add(new Vector3Int(40, 42, 41)); normals.Add(new Vector3(0, -1, 0));
        faces.Add(new Vector3Int(5, 13, 14)); texture_index_list.Add(new Vector3Int(40, 43, 42)); normals.Add(new Vector3(0, -1, 0));
        faces.Add(new Vector3Int(3, 12, 4)); texture_index_list.Add(new Vector3Int(38, 36, 39)); normals.Add(new Vector3(0, 1, 0));
        faces.Add(new Vector3Int(3, 11, 12)); texture_index_list.Add(new Vector3Int(38, 37, 36)); normals.Add(new Vector3(0, 1, 0));
        faces.Add(new Vector3Int(2, 11, 3)); texture_index_list.Add(new Vector3Int(47, 45, 44)); normals.Add((new Vector3(1, -1, 0).normalized));
        faces.Add(new Vector3Int(2, 10, 11)); texture_index_list.Add(new Vector3Int(47, 46, 45)); normals.Add((new Vector3(1, -1, 0).normalized));
        faces.Add(new Vector3Int(1, 9, 10)); texture_index_list.Add(new Vector3Int(0, 3, 2)); normals.Add(new Vector3(0, -1, 0));
        faces.Add(new Vector3Int(1, 10, 2)); texture_index_list.Add(new Vector3Int(0, 2, 1)); normals.Add(new Vector3(0, -1, 0));
        
        faces.Add(new Vector3Int(0, 8, 9)); texture_index_list.Add(new Vector3Int(7, 6, 4)); normals.Add(new Vector3(-1, 0, 0));
        faces.Add(new Vector3Int(0, 9, 1)); texture_index_list.Add(new Vector3Int(7, 4, 5)); normals.Add(new Vector3(-1, 0, 0));
        
        faces.Add(new Vector3Int(6, 14, 15)); texture_index_list.Add(new Vector3Int(35, 34, 33)); normals.Add((new Vector3(1, 1, 0).normalized));
        faces.Add(new Vector3Int(6, 15, 7)); texture_index_list.Add(new Vector3Int(35, 33, 32)); normals.Add((new Vector3(1, 1, 0).normalized));
        faces.Add(new Vector3Int(7, 15, 8)); texture_index_list.Add(new Vector3Int(11, 9, 8)); normals.Add(new Vector3(0, 1, 0));
        faces.Add(new Vector3Int(7, 8, 0)); texture_index_list.Add(new Vector3Int(11, 8, 10)); normals.Add(new Vector3(0, 1, 0));
    }

    void defineTextureVertices()
    {
        List<Vector2Int> textureVertexInt =  new List<Vector2Int>();
        
        textureVertexInt.Add(new Vector2Int(74, 330)); // 0
        textureVertexInt.Add(new Vector2Int(179, 330)); // 1
        textureVertexInt.Add(new Vector2Int(179, 304)); // 2
        textureVertexInt.Add(new Vector2Int(74, 304)); // 3
        
        textureVertexInt.Add(new Vector2Int(23, 290)); // 4
        textureVertexInt.Add(new Vector2Int(56, 290)); // 5
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
        
        textureVertexInt.Add(new Vector2Int(352, 292)); // 24
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
        
        textureVertexInt.Add(new Vector2Int(108, 40)); // 40
        textureVertexInt.Add(new Vector2Int(210, 40)); // 41
        textureVertexInt.Add(new Vector2Int(210, 73)); // 42
        textureVertexInt.Add(new Vector2Int(108, 73)); // 43
        
        textureVertexInt.Add(new Vector2Int(242, 287)); // 44
        textureVertexInt.Add(new Vector2Int(281, 287)); // 45
        textureVertexInt.Add(new Vector2Int(281, 322)); // 46
        textureVertexInt.Add(new Vector2Int(242, 322)); // 47
        
        texture_coordinates = ConvertToRelative(textureVertexInt);
    }

    private List<Vector2> ConvertToRelative(List<Vector2Int> textureVertexInt)
    {
        List<Vector2> textureVertex = new List<Vector2>();

        foreach (Vector2Int v in textureVertexInt)
        {
            textureVertex.Add(new Vector2(v.x / 511f, 1 - v.y / 511f));
        }
        return textureVertex;
    }

    public GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();
     
        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();

        for (int i = 0; i < faces.Count; i++)
        {
            Vector3 normal_for_face = normals[i];

            normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
