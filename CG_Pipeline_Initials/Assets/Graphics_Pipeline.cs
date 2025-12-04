using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Graphics_Pipeline : MonoBehaviour
{
    public GameObject screen;
    Renderer myRenderer;
    Texture2D texture;
    public Color lineColor = Color.black;
    public Texture2D texture_file;

    Pipeline_Initials myModel;
    Pipeline_Initials texture_index_list;

    private StreamWriter writer;
    string filename = "output.txt";
    private float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        writer = new StreamWriter(filename, false); // 'false' means overwrite the file if it exists
        myModel = new Pipeline_Initials();

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
    }

    private List<Vector2Int> convertToPixels(List<Vector2> viewportPts, int v1, int v2)
    {
       List<Vector2Int> output = new List<Vector2Int>();
        foreach (var p in viewportPts)
        {
            output.Add(convertToPixel(p, v1, v2));

            
        }
        return output;
    }

    private Vector2Int convertToPixel(Vector2 p, int width, int height)
    {
        return new Vector2Int((int)((p.x + 1) * (width - 1) / 2), (int)((p.y + 1) * (height - 1) / 2));
    }

    private List<Vector2> projection(List<Vector4> ptsProj)
    {
       List<Vector2> output = new List<Vector2>();
        foreach (var p in ptsProj)
            output.Add(new  Vector2(p.x/p.z, p.y/p.z));

        return output;
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

        if(dx < 0)
        {
            return bresenham(end,start);
        }

        int dy = end.y - start.y;

        if(dy < 0)
        {
            return NegY(bresenham(NegY(start), NegY(end)));
        }
        if(dy > dx)
        {
            return SwapXY(bresenham(SwapXY(start), SwapXY(end)));
        }
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

    private Vector2Int SwapXY(Vector2Int v)
    {
        return new Vector2Int(v.y, v.x);
    }

    private List<Vector2Int> SwapXY(List<Vector2Int> s)
    {
        List<Vector2Int> hold = new List<Vector2Int>();
        foreach (Vector2Int v in s)
        {
            hold.Add(SwapXY(v));
        }
        return hold;
    }

    private List<Vector2Int> NegY(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> hold = new List<Vector2Int>();
        foreach (Vector2Int v in vector2Ints)
        {
            hold.Add(NegY(v));
        }
        return hold;
    }

    private Vector2Int NegY(Vector2Int v)
    {
        return new Vector2Int(v.x, v.y * - 1);
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector4> verts = convertToHomg(myModel.vertices);
        angle += 1;
        Matrix4x4 rotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.up), Vector3.one);
        Matrix4x4 translation = Matrix4x4.TRS(new Vector3(0,0,5), Quaternion.identity, Vector3.one);
        Matrix4x4 worldTransforms = rotation * translation;

        Matrix4x4 viewingMatrix = Matrix4x4.LookAt(new Vector3(0, 0, 10), Vector3.zero, Vector3.up);

        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);

        Matrix4x4 superMatrix = projectionMatrix * viewingMatrix * worldTransforms;

        List<Vector4> ptsProj = applyTransformation(verts, superMatrix);

        List<Vector2> viewportPts = projection(ptsProj);

        // need to Clip???
        List<Vector2Int> pixelPts = convertToPixels(viewportPts, 512, 512);

        texture = new Texture2D(512, 512);
        foreach (Vector3Int face in myModel.faces)
        {
            Vector2 v1 = viewportPts[face.x], v2 = viewportPts[face.y], v3 = viewportPts[face.z];

            drawLine(v1, v2, texture, Color.blue);
            drawLine(v2, v3, texture, Color.blue);
            drawLine(v3, v1, texture, Color.blue);

        }

        //Applying 2D textures to each face of the initial.
        for (int i = 0; i < myModel.faces.Count; i++)
        {
            // --- FACE & UV INDEXING ---
            Vector3Int face = myModel.faces[i];
            Vector3Int texIndex = myModel.texture_index_list[i];

            Vector2 a_t = myModel.texture_coordinates[texIndex.x];
            Vector2 b_t = myModel.texture_coordinates[texIndex.y];
            Vector2 c_t = myModel.texture_coordinates[texIndex.z];

            // --- SCREEN COORDS (-1..+1) ---
            Vector2 a_vp = viewportPts[face.x];
            Vector2 b_vp = viewportPts[face.y];
            Vector2 c_vp = viewportPts[face.z];

            // --- PIXEL COORDS ---
            Vector2Int a2 = convertToPixel(a_vp, texture.width, texture.height);
            Vector2Int b2 = convertToPixel(b_vp, texture.width, texture.height);
            Vector2Int c2 = convertToPixel(c_vp, texture.width, texture.height);

            // --- TRIANGLE BOUNDS ---
            int minX = Mathf.Max(0, Mathf.Min(a2.x, Mathf.Min(b2.x, c2.x)));
            int maxX = Mathf.Min(texture.width - 1, Mathf.Max(a2.x, Mathf.Max(b2.x, c2.x)));
            int minY = Mathf.Max(0, Mathf.Min(a2.y, Mathf.Min(b2.y, c2.y)));
            int maxY = Mathf.Min(texture.height - 1, Mathf.Max(a2.y, Mathf.Max(b2.y, c2.y)));

            // --- PRECOMPUTE AREA (DENOM) ---
            float denom = (b2.x - a2.x) * (c2.y - a2.y) - (b2.y - a2.y) * (c2.x - a2.x);
            if (Mathf.Abs(denom) < 0.0001f)
                continue; // degenerate triangle


            int start = -1;
            int end = -1;

            // --- TRIANGLE RASTERIZATION ---
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    float px = x - a2.x;
                    float py = y - a2.y;

                    // barycentric r, s
                    float r = (px * (c2.y - a2.y) - py * (c2.x - a2.x)) / denom;
                    float s = ((b2.x - a2.x) * py - (b2.y - a2.y) * px) / denom;

                    if (r >= 0 && s >= 0 && (r + s) <= 1)
                    {

                        RangeX.AddPoint(px);
                        // UV interpolation (AFFINE)
                        Vector2 uv = a_t + r * (b_t - a_t) + s * (c_t - a_t);
                        uv *= 512; // your texture resolution

                        Color col = texture_file.GetPixel((int)uv.x, (int)uv.y);
                        texture.SetPixel(x, y, col);
                    }
                }
            }
        } //The result has slow rotation but shows the texture of the initials from the texture file
        
        myRenderer = screen.GetComponent<Renderer>();
        texture.Apply();
        myRenderer.material.mainTexture = texture;
    }

    private void drawLine(Vector2 v1, Vector2 v2, Texture2D texture, Color col)
    {
        // Clip the line
        Vector2 start = v1;
        Vector2 end = v2;

        if (LineClip(ref start,ref end))
        {
            List<Vector2Int> points = bresenham(convertToPixel (start, texture.width,texture.height), convertToPixel( end, texture.width,texture.height) );
            foreach (var p in points)
            {
                texture.SetPixel(p.x, p.y, col);
            }
        }
    }
}
