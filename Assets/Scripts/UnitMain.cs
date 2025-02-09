using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitMain : MonoBehaviour
{
    public int selfNumber;

    public Tilemap tilemap; // Reference to the Tilemap containing the tiles
    public Vector3Int tilePosition; // Position of the tile to animate
    public TileBase tileA; // First tile
    public TileBase tileB; // Second tile
    public float resetMarker = 0.5f;

    private bool isTileA = true; // Tracks which tile is currently displayed
    private float timer = 0f;

    public ShadowCaster2DCreator shadowCaster2DCreatorHolder;

    private void Start()
    {
        shadowCaster2DCreatorHolder = GameObject.Find("Grid").GetComponentInChildren<ShadowCaster2DCreator>();
        tilemap = Tilemap.FindAnyObjectByType<Tilemap>(); // Find the Tilemap in the scene
        if (selfNumber == 0)
        {
            tilePosition = new Vector3Int(2, 1, 0);
            isTileA = shadowCaster2DCreatorHolder.unit1IsTileA;
        }
        if (selfNumber == 1)
        {
            tilePosition = new Vector3Int(-4, 1, 0);
            isTileA = shadowCaster2DCreatorHolder.unit2IsTileA;
        }
        // Ensure the tile starts with tileA
        if (tilemap != null && tilemap.HasTile(tilePosition))
        {
            if (!isTileA)
            {
                tilemap.SetTile(tilePosition, tileB);
            }
            else
            {
                tilemap.SetTile(tilePosition, tileA);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Swap tiles every 0.5 seconds
        if (timer >= resetMarker)
        {
            timer = 0f;
            isTileA = !isTileA; // Toggle between tileA and tileB

            // Update the tile at the specified position
            if (tilemap != null && tilemap.HasTile(tilePosition))
            {
                tilemap.SetTile(tilePosition, isTileA ? tileA : tileB);
                if (selfNumber == 0)
                {
                    shadowCaster2DCreatorHolder.unit1IsTileA = isTileA;
                    shadowCaster2DCreatorHolder.unit1CreateReady = true;
                }
                if (selfNumber == 1)
                {
                    shadowCaster2DCreatorHolder.unit2IsTileA = isTileA;
                    shadowCaster2DCreatorHolder.unit2CreateReady = true;
                }
            }
        }
    }
}