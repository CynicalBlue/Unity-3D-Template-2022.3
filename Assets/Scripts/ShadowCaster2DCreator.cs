using System.Linq;using System.Reflection;using UnityEditor;using UnityEngine;using UnityEngine.Rendering.Universal;using UnityEngine.Tilemaps;



#if UNITY_EDITOR
[RequireComponent(typeof(CompositeCollider2D))]public class ShadowCaster2DCreator : MonoBehaviour{	[SerializeField]	private bool selfShadows = true;

    public TileBase tileA1;
    public TileBase tileB1;
    public TileBase tileA2;
    public TileBase tileB2;
	public bool unit1IsTileA = true;
	public bool unit2IsTileA = true;
    public bool unit1CreateReady = true;
    public bool unit2CreateReady = true;

    private CompositeCollider2D tilemapCollider;	static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);	static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);	static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);	static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)									.Assembly									.GetType("UnityEngine.Rendering.Universal.ShadowUtility")									.GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);

    private void Update()
    {
        if (unit1CreateReady && unit2CreateReady)
		{
			Create();
			unit1CreateReady = false;
            unit2CreateReady = false;
        }
    }

    public void Create()	{		DestroyOldShadowCasters();		tilemapCollider = GetComponent<CompositeCollider2D>();		for (int i = 0; i < tilemapCollider.pathCount; i++)		{			Vector2[] pathVertices = new Vector2[tilemapCollider.GetPathPointCount(i)];			tilemapCollider.GetPath(i, pathVertices);			GameObject shadowCaster = new GameObject("shadow_caster_" + i);			shadowCaster.transform.parent = gameObject.transform;			ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();			UnitMain unitMainComponent = shadowCaster.AddComponent<UnitMain>();			unitMainComponent.selfNumber = i;
            if (i == 0)
            {
				unitMainComponent.tileA = tileA1;
                unitMainComponent.tileB = tileB1;
            }
            if (i == 1)
            {
                unitMainComponent.tileA = tileA2;
                unitMainComponent.tileB = tileB2;
            }
            shadowCasterComponent.selfShadows = this.selfShadows;			Vector3[] testPath = new Vector3[pathVertices.Length];			for (int j = 0; j < pathVertices.Length; j++)			{
				testPath[j] = pathVertices[j];
            }			shapePathField.SetValue(shadowCasterComponent, testPath);			shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));			meshField.SetValue(shadowCasterComponent, new Mesh());			generateShadowMeshMethod.Invoke(shadowCasterComponent,			new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent) });		}	}	public void DestroyOldShadowCasters()	{		var tempList = transform.Cast<Transform>().ToList();		foreach (var child in tempList)		{			DestroyImmediate(child.gameObject);		}	}}[CustomEditor(typeof(ShadowCaster2DCreator))]public class ShadowCaster2DTileMapEditor : Editor{	public override void OnInspectorGUI()	{		DrawDefaultInspector();		EditorGUILayout.BeginHorizontal();		if (GUILayout.Button("Create"))		{			var creator = (ShadowCaster2DCreator)target;			creator.Create();		}		if (GUILayout.Button("Remove Shadows"))		{			var creator = (ShadowCaster2DCreator)target;			creator.DestroyOldShadowCasters();		}		EditorGUILayout.EndHorizontal();	}}
#endif













































































