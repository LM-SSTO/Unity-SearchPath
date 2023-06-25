using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Face
{
    public List<Vector3> vertices
    {
        get;
        private set;
    }
    
    public List<int> triangles { get; private set; }
    
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.uvs = uvs;
        this.vertices = vertices;
        this.triangles = triangles;
    }
}



[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh m_Mesh;
    public MeshFilter m_MeshFilter;
    private MeshRenderer m_MeshRenderer;
    public Material m_Material;
    [SerializeField] public float m_InnerSize;
    [SerializeField] public float m_OuterSize;
    [SerializeField] public float m_Height;
    [SerializeField] public bool m_IsFlatTopped;
    public Vector2Int index;
    private List<Face> m_Faces;

    private void Awake()
    {
        m_Mesh = new Mesh();
        m_Mesh.name = "Hex";
        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_MeshFilter.mesh = m_Mesh;
        m_MeshRenderer.material = m_Material;
    }

    public void SetMaterial(Material material)
    {
        m_Material = material;
        m_MeshRenderer.material = m_Material;
    }
    private void OnEnable()
    {
        DrawMesh();
    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    public void OnValidate()
    {
        if (Application.isPlaying)
        {
            DrawMesh();
        }
    }

    private void DrawFaces()
    {
        m_Faces = new List<Face>();
        for (int point = 0; point < 6; point++)
        {
            m_Faces.Add(CreateFace(m_InnerSize, m_OuterSize, m_Height/2f, m_Height/2f, point));
        }
        
        for (int point = 0; point < 6; point++)
        {
            m_Faces.Add(CreateFace(m_InnerSize, m_OuterSize, -m_Height/2f, -m_Height/2f, point, true));
        }
        
        for (int point = 0; point < 6; point++)
        {
            m_Faces.Add(CreateFace(m_InnerSize, m_InnerSize, m_Height/2f, -m_Height/2f, point));
        }
        
        for (int point = 0; point < 6; point++)
        {
            m_Faces.Add(CreateFace(m_OuterSize, m_OuterSize, m_Height/2f, -m_Height/2f, point, true));
        }
    }

    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point,
        bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, point < 5 ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, point < 5 ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() {pointA, pointB, pointC, pointD};
        List<int> triangles = new List<int>() {0, 1, 2, 2, 3, 0};
        List<Vector2> uvs = new List<Vector2>()
            {new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};
        if (reverse)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);

    }

    private Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = m_IsFlatTopped ? 60 * index: 60 * index - 30;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3((size * Mathf.Cos(angle_rad)), height, size * Mathf.Sin(angle_rad));
    }
    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < m_Faces.Count; i++)
        {
            var face = m_Faces[i];
            vertices.AddRange(face.vertices);
            uvs.AddRange(face.uvs);
            int offset = (4 * i);
            foreach (int triangle in face.triangles)
            {
                tris.Add(triangle + offset);
            }
        }
        m_Mesh.vertices = vertices.ToArray();
        m_Mesh.triangles = tris.ToArray();
        m_Mesh.uv = uvs.ToArray();
        m_Mesh.RecalculateNormals();
    }

    
}
