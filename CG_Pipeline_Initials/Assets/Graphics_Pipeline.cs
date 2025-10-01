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

        writeVectorstoFile(myModel.vertices, "Vertices of my model", " ----- ");

        Vector3 axis = new Vector3(19, -3, -3).normalized;
        Matrix4x4 rotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-41f, axis), Vector3.one);

        writeMatrixtoFile(rotation, "rotationMatrix", " ----- ");
        
        //List<Vector3> imageAfterRotation = 
        
        
        
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

    private void writeVectorstoFile(List<Vector3> verts, string before, string after)
    {
       
        writer.WriteLine(before);
        foreach (Vector3 v in verts)
        {
            writer.WriteLine("("+ v.x + "," + v.y + "," + v.z + ")");
        }
        writer.WriteLine(after);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
