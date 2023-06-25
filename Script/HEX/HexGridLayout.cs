using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    public float outerSize = 1f;

    public float innerSize = 0f;

    public float height = 1f;

    public bool isFlatTopped;

    public Material material;

    public Vector2Int gridSize;

    private List<GameObject> tiles = new List<GameObject>();

    private void OnEnable()
    {
        LayoutGrid();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            LayoutGrid();
        }
        
    }

    private void LayoutGrid()
    {
        if (tiles.Count == 0)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    GameObject tile = new GameObject($"Hex_{x}_{y}", typeof(HexRenderer));
                    HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                    hexRenderer.m_IsFlatTopped = isFlatTopped;
                    hexRenderer.m_OuterSize = outerSize;
                    hexRenderer.m_InnerSize = innerSize;
                    hexRenderer.m_Height = height;
                    hexRenderer.index = new Vector2Int(y, x);
                    hexRenderer.SetMaterial(material);
                    hexRenderer.DrawMesh();

                    tile.transform.SetParent(transform, true);
                    tile.transform.position = GetPositionHex(new Vector2Int(y, x));
                    tiles.Add(tile);
                }
            }
        }
        else
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                GameObject tile = tiles[i];
                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.m_IsFlatTopped = isFlatTopped;
                hexRenderer.m_OuterSize = outerSize;
                hexRenderer.m_InnerSize = innerSize;
                hexRenderer.m_Height = height;
                hexRenderer.DrawMesh();
                tile.transform.position = GetPositionHex(hexRenderer.index);
            }
        }
        
    }

    public Vector3 GetPositionHex(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPos;
        float yPos;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;
        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);
            offset = (shouldOffset) ? width / 2 : 0;

            xPos = (column * (horizontalDistance)) + offset;
            yPos = (row * verticalDistance);
        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3) * size;

            verticalDistance = width;
            horizontalDistance = height * (3f / 4f);
            offset = (shouldOffset) ? height / 2 : 0;

            xPos = (column * (horizontalDistance));
            yPos = (row * verticalDistance) - offset;
        }

        return new Vector3(xPos, 0, -yPos);
    }
}
