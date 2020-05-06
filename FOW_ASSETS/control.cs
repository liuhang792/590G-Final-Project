using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class control : MonoBehaviour {

	public GameObject m_fogOfWarPlane;
	public Transform m_player;
	public LayerMask m_fogLayer;
	public float m_radius = 50.0f;
	private float m_radiusSqr { get { return m_radius*m_radius; }}
	
	private Mesh m_mesh;
	private Vector3[] m_vertices;
	private Color[] m_colors;

	private FileStream stream = null;
    private StreamWriter writer = null;
	private string filename = @"output.txt"; 
	
	// Use this for initialization
	void Start () {
		Initialize();
		// openFile();

		// Debug.Log(m_radiusSqr);
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log(m_player.position);
		Ray r = new Ray(transform.position, m_player.position - transform.position);
		RaycastHit hit;
		if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide)) {
			// Debug.Log(hit.point);
			for (int i=0; i< m_vertices.Length; i++) {
				Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
				// Debug.Log("iter: " + i.ToString() + ", " + v.ToString());
				float dist = Vector3.SqrMagnitude(v - hit.point);
				// writer.WriteLine("iter: " + i.ToString() + ", v: " + v.ToString() + ", player: " + hit.point.ToString() + ", dist: " + dist.ToString());
				if (dist < m_radiusSqr) {
					// Debug.Log("Changing.....: " + dist.ToString());
					float alpha = Mathf.Min(m_colors[i].a, dist/m_radiusSqr);
					// Debug.Log(alpha);
					m_colors[i].a = alpha;
				}
			}
			UpdateColor();
		}
	}
	
	void Initialize() {
		m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		// BuildMesh(m_vertices[0], m_vertices[m_vertices.Length - 1]);
		m_vertices = m_mesh.vertices;
		m_colors = new Color[m_vertices.Length];
		// Debug.Log(m_vertices.Length);
		for (int i=0; i < m_colors.Length; i++) {
			m_colors[i] = Color.black;
		}
		UpdateColor();
		// Debug.Log(m_mesh.colors[0]);
	}

	void BuildMesh(Vector3 start, Vector3 end) {
		List<Vector3> vertices = new List<Vector3>();
		List<Vector3> triangles = new List<Vector3>();

		float step = 0.1f;

		for (float x = start[0] ; x >= end[0] ; x -= step) {
			for (float z = start[2] ; z >= end[2] ; z -= step) {
				// Debug.Log(x.ToString() + ", " + z.ToString());
				vertices.Add(new Vector3(x, start[1], z));
			}
		}

		// Vector3[] verts = new Vector3[vertices.Count];

		// for (int i = 0 ; i < vertices.Count ; i++) {
		// 	verts[i] = vertices[i];
		// }
		m_mesh.Clear();
		m_mesh.SetVertices(vertices);
		m_fogOfWarPlane.GetComponent<MeshFilter>().mesh = m_mesh;
	}
	
	void UpdateColor() {
		m_mesh.colors = m_colors;
		// m_fogOfWarPlane.active = false;
		// m_fogOfWarPlane.GetComponent<MeshFilter>().mesh = m_mesh;
	}

	void openFile(bool trunc = true) {
		if (!File.Exists(filename)) {
			stream = new FileStream(filename, FileMode.CreateNew);
		}
		else {
			stream = new FileStream(filename, (trunc) ? FileMode.Truncate : FileMode.Append);
		}
		
		writer = new StreamWriter(stream);
	}

	void closeFile() {
        writer.Close();
        stream.Close();

        writer = null;
        stream = null;
    }

	void OnDestroy() {
		Debug.Log("Closed");
		// closeFile();
	}
}
