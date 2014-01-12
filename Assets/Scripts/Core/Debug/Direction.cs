using UnityEngine;
using System.Collections;

public class Direction : MonoBehaviour {

    public LineRenderer ForwardLine;
    public float lineDistance = 15;

	void Awake()
    {
        ForwardLine = gameObject.AddComponent<LineRenderer>();
        ForwardLine.SetVertexCount(2);
        ForwardLine.SetColors( Color.blue, Color.blue);
        ForwardLine.SetWidth(0.5f, 0.5f);

    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ForwardLine.SetPosition(0,transform.position);
        ForwardLine.SetPosition(1,transform.forward * lineDistance);
	}
}
