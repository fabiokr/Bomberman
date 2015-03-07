using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class TileMesh : MonoBehaviour {
	public bool processInEditMode = false;
	public bool save = false;
	public bool saveAs = false;
	Mesh mesh;
	string lastPath = "";
	string meshName = "New Mesh";

	public Rect tile = new Rect(0,0,1,1);

	Vector2 a,b,c,d;

	void Start()
	{
		CheckParameters();
	}
	
	void Update() {

		if (!CheckParameters())
			return;
		if (save)
		{
			save = false;
			Save();
		}
		if (saveAs)
		{
			saveAs = false;
			SaveAs();
		}
		if (processInEditMode)
		{
			UpdateTile(tile);
		}
		
	}
	
	
	void Save()
	{
		System.Collections.Generic.List<string> allPaths = new System.Collections.Generic.List<string>();
		allPaths.AddRange(AssetDatabase.GetAllAssetPaths());
		if (lastPath == "" || !allPaths.Contains(lastPath))
		{
			SaveAs();
			return;
		}
		SaveMesh(lastPath);
	}
	
	void SaveAs()
	{
		string path = EditorUtility.SaveFilePanelInProject(
			"Save Mesh in Assets",
			meshName,
			"asset", "Please enter a file name to save the Mesh to ");
		if (path.Length <= 0)
			return;
		meshName = path.Substring(path.LastIndexOf('/')+1, path.LastIndexOf(".") - path.LastIndexOf('/')-1);
		SaveMesh(path);
	}
	
	void SaveMesh(string path)
	{
		lastPath = path;
		if (mesh)
			mesh.name = meshName;
		else
			mesh = GetComponent<MeshFilter>().sharedMesh;
		Mesh m = Instantiate(mesh, Vector3.zero, Quaternion.identity) as Mesh;
		m.name = meshName;
		GetComponent<MeshFilter>().mesh = m;
		AssetDatabase.Refresh();
		
		if (path != "")
		{
			AssetDatabase.CreateAsset(m, path);
			AssetDatabase.SaveAssets();
		}
		AssetDatabase.Refresh();
	}
	
	void UpdateTile(Rect r){
		mesh = Instantiate(GetComponent<MeshFilter>().sharedMesh) as Mesh;
		mesh.name = meshName;
		a.x = r.x;
		a.y = r.y;
		b.x = r.x + r.width;
		b.y = r.y + r.height;
		c.x = r.x + r.width;
		c.y = r.y;
		d.x = r.x;
		d.y = r.y + r.height;
		Vector2[] uvs = {
			a ,
			b ,
			c ,
			d
		};
		mesh.uv = uvs;
		GetComponent<MeshFilter>().mesh = mesh;
	}

	bool CheckParameters()
	{
		if (GetComponent<MeshFilter>().sharedMesh == null || GetComponent<MeshFilter>().sharedMesh.uv.Length != 4)
		{
			Debug.LogWarning("TileMesh need a Quad mesh on MeshFilter to work properly!");
			save = false;
			saveAs = false;
			processInEditMode = false;
			return false;
		}
		return true;
	}

}
