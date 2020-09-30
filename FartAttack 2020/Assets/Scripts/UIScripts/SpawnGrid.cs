using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    public Vector3 topLeftPos;
    public int row;
    public int column;
    public GameObject gridPiece;

    Vector3 spawnPos;

    void Start()
    {
        GridSpawn();
    }

    void GridSpawn()
    {
        spawnPos = topLeftPos;
        for(int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Instantiate(gridPiece, spawnPos, gridPiece.transform.rotation);
                spawnPos.x += 1;
            }
            spawnPos.x = topLeftPos.x;
            spawnPos.z -= 1;
        }
    }
}
