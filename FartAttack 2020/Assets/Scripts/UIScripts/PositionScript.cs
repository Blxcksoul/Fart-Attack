using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScript : MonoBehaviour
{
    public Transform Player; //stores the transform of the player character
    public Vector2 playerGridPos; //the grid space the player is currently on

    private float gridOffsetX = 0f; //how much the grid is offset from the origin point in the X axis
    private float gridOffsetZ = 0f; //how much the grid is offset from the origin point in the Z axis
    private float gridWidth = 1f; //width of the grid in units
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetGridPosition(Player);
    }

    void GetGridPosition(Transform player)
    {
        float playerGridX = Mathf.Floor((player.position.x + gridOffsetX) / gridWidth);
        float playerGridZ = Mathf.Floor((player.position.z + gridOffsetZ) / gridWidth);

        playerGridPos = new Vector2(playerGridX, playerGridZ);
    }
}
