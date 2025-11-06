using System;
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
        
        Matrix4x4 translationMatrix =  Matrix4x4.TRS(new Vector3(4, -5, -2), Quaternion.identity, Vector3.one);
        writeMatrixtoFile(translationMatrix, "translationMatrix", " ----- ");
        
        List<Vector4> imageAfterTranslation = applyTransformation(imageAfterScale, translationMatrix);
        writeVectorstoFile(imageAfterTranslation, "imageAfterTranslationMatrix", " ----- ");

        Matrix4x4 Mot = translationMatrix * scaleMatrix * rotation;
        writeMatrixtoFile(Mot, "Single Matrix of transformation", " ----- ");
        
        List<Vector4> imageAfterMot = applyTransformation(verts, Mot);
        writeVectorstoFile(imageAfterMot, "imageAfterTransformation", " ----- ");
        
        Vector3 cameraPosition = new Vector3(21, 0, 47);
        Vector3 lookAtCoord = new Vector3(-3, 19, 3).normalized;
        Vector3 cameraUpPosition = new Vector3(-2, -3, 19).normalized;
        Matrix4x4 viewingMatrix = Matrix4x4.LookAt(cameraPosition, lookAtCoord, cameraUpPosition);
        writeMatrixtoFile(viewingMatrix, "viewingMatrix", " ----- ");
        
        List<Vector4> imageAfterViewing = applyTransformation(imageAfterTranslation, viewingMatrix);
        writeVectorstoFile(imageAfterViewing, "imageAfterViewing", " ----- ");
        
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);
        writeMatrixtoFile(projectionMatrix, "projectionMatrix", " ----- ");

        List<Vector4> imageAfterProjection = applyTransformation(imageAfterViewing, projectionMatrix);
        writeVectorstoFile(imageAfterProjection, "imageAfterProjection", " ----- ");

        Matrix4x4 MoE = projectionMatrix * viewingMatrix * Mot;
        writeMatrixtoFile(MoE, "Single Matrix of Everything", " ----- ");

        List<Vector4> imageAfterMoE = applyTransformation(verts, MoE);
        writeVectorstoFile(imageAfterMoE, "Image after MoE", " ----- ");
        
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

    private List<Vector4> applyTransformation(List<Vector4> verts, Matrix4x4 m)
    {
        List<Vector4> result = new List<Vector4>();
        foreach (Vector4 v in verts)
        {
            result.Add(m * v);
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

    public static bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        OutCode startoc = new OutCode(start);
        OutCode endoc = new OutCode(end);
        OutCode inViewport = new OutCode();

        //Test for Trivial Acceptance is true if both points in viewport i.e. have 0000 as Outcode
        if ((startoc + endoc) == inViewport)
        {
            return true;
        }

        //Test for Trivial Rejection is false if both points are not in viewport i.e. have 0100 or 0010 as Outcode
        if ((startoc * endoc) != inViewport)
        {
            return false;
        }
        return false;
    }

    Vector2 Intercept(Vector2 start, Vector2 end, int edgeIndex)
    {
        if (end.x != start.x)
        {
            float m = (end.y - start.y) / (end.x - start.x);

            switch (edgeIndex)
            {
                case 0: //Top edge y = 1 what's x
                    //x = x1 + (1/m) * (y - y1)
                    break;
                case 1: //Bottom edge y = -1 what's x
                    break;
                case 2: //Left edge x = 1 what's y
                    break;
                default: //Right edge x = -1 what's y
                    float y = (start.y + m) * (1 - start.x);
                    return new Vector2(1, y);
                    break;
            }
        }
        else
        {

        }
        return Intercept(start, end, edgeIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
