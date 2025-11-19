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
    { /*
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
        */
        Vector2 s = new Vector2(-1.43f, 0.12f);
        Vector2 e = new Vector2(0.92f, 1.28f);

        if(LineClip(ref s, ref e))
        {
            print(s.ToString() + e.ToString());
        }
        else
        {
            print("Line Rejected");
        }

        bresenham(new Vector2Int(10, 12), new Vector2Int(17, 22));
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

    public bool LineClip(ref Vector2 start, ref Vector2 end)
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

        if(startoc == inViewport)
        {
            return LineClip(ref end, ref start);
        }
        //Work to do
        if (startoc.up)
        {
            start = Intercept(start, end, 0);
            return LineClip(ref start, ref end);
        }
        if (startoc.down)
        {
            start = Intercept(start, end, 1);
            return LineClip(ref start, ref end);
        }
        if (startoc.left)
        {
            start = Intercept(start, end, 2);
            return LineClip(ref start, ref end);
        }
        if (startoc.right)
        {
            start = Intercept(start, end, 3);
            return LineClip(ref start, ref end);
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
                    if(m != 0)
                    {
                        float x = start.x + (1 / m) * (1 - start.y);
                        return new Vector2(x, 1);
                    }
                    else
                    {
                        if(start.y == 1)
                        {
                            return new Vector2(1, 1);
                        }
                        else
                        {
                            print("No answer");
                            return new Vector2(float.NaN, float.NaN);
                        }
                    }
                case 1: //Bottom edge y = -1 what's x
                    if (m != 0)
                    {
                        float x1 = start.x + (1 / m) * (-1 - start.y);
                        return new Vector2(x1, -1);
                    }
                    else
                    {
                        if (start.y == -1)
                        {
                            return new Vector2(1, -1);
                        }
                        else
                        {
                            print("No answer");
                            return new Vector2(float.NaN, float.NaN);
                        }
                    }
                case 2: //Left edge x = -1 what's y
                    float y = start.y + m * (-1 - start.x);
                    return new Vector2(-1, y);
                default: //Right edge x = +1 what's y
                    float y1 = start.y + m * (1 - start.x);
                    return new Vector2(1, y1);
            }
        }
        else
        {
            switch (edgeIndex)
            {
                case 0: //Top edge y = 1 what's x
                    return new Vector2(start.x, 1);
                case 1: //Bottom edge y = -1 what's x
                    return new Vector2(start.x, -1);
                case 2: //Left edge x = -1 what's y
                    if(start.x == -1)
                    {
                        return new Vector2(-1, 1);
                    }
                    else
                    {
                        print("No answer");
                        return new Vector2(float.NaN, float.NaN);
                    }
                default: //Right edge x = +1 what's y
                    if (start.x == 1)
                    {
                        return new Vector2(1, 1);
                    }
                    else
                    {
                        print("No answer");
                        return new Vector2(float.NaN, float.NaN);
                    }
            }
        }
      
    }


    List<Vector2Int> bresenham(Vector2Int start, Vector2Int end)
    {
        int dx = end.x - start.x;
        int dy = end.y - start.y;
        int dyminusdx = dy - dx;
        int neg = 2 * dyminusdx;
        int pos = 2 * dy;
        int p = 2 * dy - dx;

        List<Vector2Int> linePoints = new List<Vector2Int>();
        int y = start.y;

        linePoints.Add(start);
        for(int x = start.x + 1; x <= end.x; x++)
        {
            if(p < 0)
            {
                //y stays the same
                p += pos;

            }
            else
            {
                y += 1;
                p += neg;
            }
            linePoints.Add(new Vector2Int(x,y));
        }

        return linePoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
