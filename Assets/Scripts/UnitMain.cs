using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class UnitMain : MonoBehaviour
{
    public int selfNumber;

    // 
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
        tilemap = Tilemap.FindAnyObjectByType<Tilemap>();
        if (selfNumber == 0)
        {
            tilePosition = new Vector3Int(2, 1, 0); // Tiles on tilemaps have their own positions, so their Vector3 is different than the equivalent for GameObjects in the scene
            isTileA = shadowCaster2DCreatorHolder.unit1IsTileA; // This pulls back the stored tile state from ShadowCaster2DCreator
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
                    shadowCaster2DCreatorHolder.unit1IsTileA = isTileA; // Stores their tile state to ShadowCaster2DCreator, as this object is destroyed by Create()
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

// ----- AI Assistant - Deepseek - Query was:

// Hello.
// I want a tile placed by the tilemap in the scene to change to a different tile every .5 seconds as if it was an animation.
// How would I go about this?
// This is for Unity.

// ----- AI Assistant - Deepseek - Response was:
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class AnimatedTileScript : MonoBehaviour
//{
//    public Tilemap tilemap;
//    public Vector3Int tilePosition;
//    public TileBase[] tiles;
//    public float frameRate = 0.5f;

//    private int currentIndex = 0;
//    private float timer = 0f;

//    void Update()
//    {
//        timer += Time.deltaTime;

//        if (timer >= frameRate)
//        {
//            timer = 0f;
//            currentIndex = (currentIndex + 1) % tiles.Length;
//            tilemap.SetTile(tilePosition, tiles[currentIndex]);
//        }
//    }
//}



// ----- AI Assistant - Deepseek - Query was:

//Here is the script that I've somewhat implemented some of the code into:
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class UnitMain : MonoBehaviour
//{
//    public int unitHealth = 100;
//    public int baseHealth = 100;
//    public int newCurrentHealth;
//    public int selfNumber;

//    public Tilemap tilemap;
//    public Vector3Int tilePosition;
//    public TileBase[] tile1;
//    public TileBase[] tile2;
//    public float resetMarker = 0.5f;
//    public int currentIndex = 0;
//    public float timer = 0f;

//    public ShadowCaster2DCreator shadowCaster2DCreatorHolder;

//    private void Start()
//    {
//        shadowCaster2DCreatorHolder = GameObject.Find("Grid").GetComponentInChildren<ShadowCaster2DCreator>();
//        tilemap = Tilemap.FindAnyObjectByType<Tilemap>();
//        newCurrentHealth = unitHealth;
//    }

//    private void Update()
//    {
//        timer += Time.deltaTime;

//        if (timer >= resetMarker)
//        {
//            timer = 0f;
//            currentIndex = (currentIndex + 1) % tile1.Length;
//            tilemap.SetTile(tilePosition, tile1[currentIndex]);
//        }
//        if (unitHealth != newCurrentHealth)
//        {
//            if (selfNumber == 0)
//            {
//                shadowCaster2DCreatorHolder.unitHealth1 = unitHealth;
//                shadowCaster2DCreatorHolder.unitBaseHealth1 = baseHealth;
//                shadowCaster2DCreatorHolder.Create();
//            }
//            if (selfNumber == 1)
//            {
//                shadowCaster2DCreatorHolder.unitHealth2 = unitHealth;
//                shadowCaster2DCreatorHolder.unitBaseHealth2 = baseHealth;
//                shadowCaster2DCreatorHolder.Create();
//            }
//        }
//    }
//}

//I need the tile to specifically change to one other specific tile over and over every .5 seconds


// ----- AI Assistant - Deepseek - Response was:
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class UnitMain : MonoBehaviour
//{
//    public int unitHealth = 100;
//    public int baseHealth = 100;
//    public int newCurrentHealth;
//    public int selfNumber;

//    public Tilemap tilemap;
//    public Vector3Int tilePosition;
//    public TileBase tileA; // First tile
//    public TileBase tileB; // Second tile
//    public float resetMarker = 0.5f;

//    private bool isTileA = true; // Tracks which tile is currently displayed
//    private float timer = 0f;

//    public ShadowCaster2DCreator shadowCaster2DCreatorHolder;

//    private void Start()
//    {
//        shadowCaster2DCreatorHolder = GameObject.Find("Grid").GetComponentInChildren<ShadowCaster2DCreator>();
//        tilemap = Tilemap.FindAnyObjectByType<Tilemap>();
//        newCurrentHealth = unitHealth;

//        // Initialize the tile to the first tile (tileA)
//        tilemap.SetTile(tilePosition, tileA);
//    }

//    private void Update()
//    {
//        timer += Time.deltaTime;

//        // Swap tiles every 0.5 seconds
//        if (timer >= resetMarker)
//        {
//            timer = 0f;
//            isTileA = !isTileA; // Toggle between tileA and tileB
//            tilemap.SetTile(tilePosition, isTileA ? tileA : tileB);
//        }

//        // Handle health updates
//        if (unitHealth != newCurrentHealth)
//        {
//            if (selfNumber == 0)
//            {
//                shadowCaster2DCreatorHolder.unitHealth1 = unitHealth;
//                shadowCaster2DCreatorHolder.unitBaseHealth1 = baseHealth;
//                shadowCaster2DCreatorHolder.Create();
//            }
//            if (selfNumber == 1)
//            {
//                shadowCaster2DCreatorHolder.unitHealth2 = unitHealth;
//                shadowCaster2DCreatorHolder.unitBaseHealth2 = baseHealth;
//                shadowCaster2DCreatorHolder.Create();
//            }
//        }
//    }
//}



// ----- AI Assistant - Deepseek - Query was:

//It's important to mention that the other script I have that interacts with the one I gave you is this:
//using System.Linq;
//using System.Reflection;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.Rendering.Universal;



//#if UNITY_EDITOR

//[RequireComponent(typeof(CompositeCollider2D))]
//public class ShadowCaster2DCreator : MonoBehaviour
//{
//    [SerializeField]
//    private bool selfShadows = true;
//    public int unitHealth1;
//    public int unitBaseHealth1;
//    public int unitHealth2;
//    public int unitBaseHealth2;

//    private CompositeCollider2D tilemapCollider;

//    static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
//    static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
//    static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
//    static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)
//                                    .Assembly
//                                    .GetType("UnityEngine.Rendering.Universal.ShadowUtility")
//                                    .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);

//    public void Create()
//    {
//        DestroyOldShadowCasters();
//        tilemapCollider = GetComponent<CompositeCollider2D>();

//        for (int i = 0; i < tilemapCollider.pathCount; i++)
//        {
//            Vector2[] pathVertices = new Vector2[tilemapCollider.GetPathPointCount(i)];
//            tilemapCollider.GetPath(i, pathVertices);
//            GameObject shadowCaster = new GameObject("shadow_caster_" + i);
//            shadowCaster.transform.parent = gameObject.transform;
//            ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
//            UnitMain unitMainComponent = shadowCaster.AddComponent<UnitMain>();
//            unitMainComponent.selfNumber = i;
//            if (i == 0 && unitBaseHealth1 != 0)
//            {
//                unitMainComponent.unitHealth = unitHealth1;
//                unitMainComponent.baseHealth = unitBaseHealth1;
//            }
//            if (i == 1 && unitBaseHealth2 != 0)
//            {
//                unitMainComponent.unitHealth = unitHealth2;
//                unitMainComponent.baseHealth = unitBaseHealth2;
//            }
//            shadowCasterComponent.selfShadows = this.selfShadows;


//            Vector3[] testPath = new Vector3[pathVertices.Length];
//            for (int j = 0; j < pathVertices.Length; j++)
//            {
//                // Make width of shadow multiplied by .5% for every 1% of health lost of the unit
//                if (j == 0 && unitBaseHealth1 != 0)
//                {
//                    testPath[j] = pathVertices[j] * ((((unitBaseHealth1 - unitHealth1) / unitBaseHealth1) / 2) + 1);
//                    Debug.Log("Got Here" + pathVertices[j] * ((((unitBaseHealth1 - unitHealth1) / unitBaseHealth1) / 2) + 1));
//                }
//                if (j == 1 && unitBaseHealth2 != 0)
//                {
//                    testPath[j] = pathVertices[j] * ((((unitBaseHealth2 - unitHealth2) / unitBaseHealth2) / 2) + 1);
//                }
//                else
//                {
//                    testPath[j] = pathVertices[j] * ((((unitMainComponent.baseHealth - unitMainComponent.unitHealth) / unitMainComponent.baseHealth) / 2) + 1);
//                }
//            }

//            shapePathField.SetValue(shadowCasterComponent, testPath);
//            shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));
//            meshField.SetValue(shadowCasterComponent, new Mesh());
//            generateShadowMeshMethod.Invoke(shadowCasterComponent,
//            new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent) });
//        }
//    }
//    public void DestroyOldShadowCasters()
//    {

//        var tempList = transform.Cast<Transform>().ToList();
//        foreach (var child in tempList)
//        {
//            DestroyImmediate(child.gameObject);
//        }
//    }
//}

//[CustomEditor(typeof(ShadowCaster2DCreator))]
//public class ShadowCaster2DTileMapEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        EditorGUILayout.BeginHorizontal();
//        if (GUILayout.Button("Create"))
//        {
//            var creator = (ShadowCaster2DCreator)target;
//            creator.Create();
//        }

//        if (GUILayout.Button("Remove Shadows"))
//        {
//            var creator = (ShadowCaster2DCreator)target;
//            creator.DestroyOldShadowCasters();
//        }
//        EditorGUILayout.EndHorizontal();
//    }

//}

//#endif


//The tiles that I want to animate already exist in the scene, so they specifically need to be animated


// ----- AI Assistant - Deepseek - Response was:
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class UnitMain : MonoBehaviour
//{
//    public int unitHealth = 100;
//    public int baseHealth = 100;
//    public int newCurrentHealth;
//    public int selfNumber;

//    public Tilemap tilemap; // Reference to the Tilemap containing the tiles
//    public Vector3Int tilePosition; // Position of the tile to animate
//    public TileBase tileA; // First tile
//    public TileBase tileB; // Second tile
//    public float resetMarker = 0.5f;

//    private bool isTileA = true; // Tracks which tile is currently displayed
//    private float timer = 0f;

//    public ShadowCaster2DCreator shadowCaster2DCreatorHolder;

//    private void Start()
//    {
//        shadowCaster2DCreatorHolder = GameObject.Find("Grid").GetComponentInChildren<ShadowCaster2DCreator>();
//        tilemap = Tilemap.FindAnyObjectByType<Tilemap>(); // Find the Tilemap in the scene
//        newCurrentHealth = unitHealth;

//        // Ensure the tile starts with tileA
//        if (tilemap != null && tilemap.HasTile(tilePosition))
//        {
//            tilemap.SetTile(tilePosition, tileA);
//        }
//    }

//    private void Update()
//    {
//        timer += Time.deltaTime;

//        // Swap tiles every 0.5 seconds
//        if (timer >= resetMarker)
//        {
//            timer = 0f;
//            isTileA = !isTileA; // Toggle between tileA and tileB

//            // Update the tile at the specified position
//            if (tilemap != null && tilemap.HasTile(tilePosition))
//            {
//                tilemap.SetTile(tilePosition, isTileA ? tileA : tileB);
//            }
//        }

//        // Handle health updates
//        if (unitHealth != newCurrentHealth)
//        {
//            if (selfNumber == 0)
//            {
//                shadowCaster2DCreatorHolder.unitHealth1 = unitHealth;
//                shadowCaster2DCreatorHolder.unitBaseHealth1 = baseHealth;
//                shadowCaster2DCreatorHolder.Create();
//            }
//            if (selfNumber == 1)
//            {
//                shadowCaster2DCreatorHolder.unitHealth2 = unitHealth;
//                shadowCaster2DCreatorHolder.unitBaseHealth2 = baseHealth;
//                shadowCaster2DCreatorHolder.Create();
//            }
//        }
//    }
//}



// This details all 3 queries and the 3 respective responses I got from Deekseep for this script.
// It's a bit messy looking, but I don't know how else to format the larger queries without
// Going outside of the format outlined in the syllabus.
// As you can tell by the queries, though, I fed it code I already made in order for it
// To spit out an edited version of UnitMain until I got mostly what I wanted from it.
// Mostly being the keyword, as you can tell that my finished UnitMain.cs script is
// Still different from the final response I got from Deepseek.
// It was easier to do the remaining parts myself because I already had an idea
// Of how to do it, and using Deepseek further would just have a higher liklihood
// Of it messing one or more things up.