using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int m_Wight;

    [SerializeField] private int m_Hight;

    [SerializeField] private Tile m_TilePrefab;
    // Start is called before the first frame update
    
    void Start()
    {
        CreatGrids();
    }

    void CreatGrids()
    {
        for (int x = 0; x < m_Wight; x++)
        {
            for (int y = 0; y < m_Hight; y++)
            {
                var tile = Instantiate(m_TilePrefab, new Vector3(x, y), Quaternion.identity);
                tile.name = $"Tile {x}-{y}";
                tile.transform.parent = transform;
                tile.InitView((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0));
            }
        }

        var curCamera = Camera.main;
        curCamera.transform.position = new Vector3((float) m_Wight / 2 - 0.5f, (float) m_Hight / 2 - 0.5f, -10);
    }
}
