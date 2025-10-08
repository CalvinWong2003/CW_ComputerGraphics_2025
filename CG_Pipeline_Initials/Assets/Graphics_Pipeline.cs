using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Graphics_Pipeline : MonoBehaviour
{
    private StreamWriter writer;
    string filename = "output.txt";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        writer = new StreamWriter(filename, false); // 'false' means overwrite the file if it exists
        Pipeline_Initials myModel = new Pipeline_Initials();
        List<Vector3> verts3 = myModel.vertices;
        List<Vector4> verts = convertToHomg(verts3);

        writeVectorstoFile(verts, "Vertices of my model", " ----- ");

        Vector3 axis = new Vector3(19, -3, -3).normalized;
        Matrix4x4 rotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-41f, axis), Vector3.one);

        writeMatrixtoFile(rotation, "rotationMatrix", " ----- ");

        List<Vector4> imageAfterRotation = applyTransformation(verts, rotation);
        writeVectorstoFile(imageAfterRotation, "imageAfterRotationMatrix", " ----- ");
        
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(2, 2, 1));
        writeMatrixtoFile(scaleMatrix, "scaleMatrix", " ----- ");
        
        List<Vector4> imageAfterScale = applyTransformation(imageAfterRotation, scaleMatrix);
        writeVectorstoFile(imageAfterScale, "imageAfterScaleMatrix", " ----- ");
        
        Matrix4x4 viewingMatrix = Matrix4x4.LookAt(Vector3.zero, Vector3.zero, Vector3.up);
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);
        
        writer.Close(); // Close the writer to release the file
    }

    private void writeMatrixtoFile(Matrix4x4 m, string before, string after)
    {
        writer.WriteLine(before);
        for (int i = 0; i < 4; i++)
        {
            Vector4 r = m.GetRow(i);
            writer.WriteLine("("+ r.x + "," + r.y + "," + r.z + "," + r.w + ")");
        }
        writer.WriteLine(after);
    }

    private void writeVectorstoFile(List<Vector4> verts, string before, string after)
    {
        writer.WriteLine(before);
        foreach (Vector4 v in verts)
        {
            writer.WriteLine("("+ v.x + "," + v.y + "," + v.z + "," + v.w + ")");
        }
        writer.WriteLine(after);
    }

    private List<Vector4> applyTransformation(List<Vector4> verts, Matrix4x4 rotation)
    {
        List<Vector4> result = new List<Vector4>();
        foreach (Vector4 v in verts)
        {
            result.Add(rotation * v);
        }
        return result;
    }

    private List<Vector4> convertToHomg(List<Vector3> verts)
    {
        List<Vector4> result = new List<Vector4>();
        foreach (Vector3 v in verts)
        {
            result.Add(new Vector4(v.x, v.y, v.z, 1.0f));
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
