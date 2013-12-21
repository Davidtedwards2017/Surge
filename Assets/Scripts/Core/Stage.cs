using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

	//public members
	public static GameObject North{ get { return m_Instance.NorthBounds; } }
	public static GameObject East{ get { return m_Instance.EastBounds; } }
	public static GameObject South{ get { return m_Instance.SouthBounds; } }
	public static GameObject West{ get { return m_Instance.WestBounds; } }

	//private members
	private static Stage m_StageInstance;
	private static Stage m_Instance
	{
		get
		{
			if(!m_StageInstance)
			{
				GameObject StageObject = new GameObject ("Stage Instance");
				m_StageInstance = StageObject.AddComponent<Stage> ();
			}
		
			return m_StageInstance; 
		}
	}

	private GameObject m_NorthBounds;
	private GameObject m_EastBounds;
	private GameObject m_SouthBounds;
	private GameObject m_WestBounds;

	private GameObject NorthBounds
	{
		get
		{
			if(!m_NorthBounds)
				m_NorthBounds = GameObject.Find("NorthBounds");
			return m_NorthBounds;
		}
	}
	private GameObject EastBounds
	{
		get
		{
			if(!m_EastBounds)
				m_EastBounds = GameObject.Find("EastBounds");
			return m_EastBounds;
		}
	}
	private GameObject SouthBounds
	{
		get
		{
			if(!m_SouthBounds)
				m_SouthBounds = GameObject.Find("SouthBounds");
			return m_SouthBounds;
		}
	}
	private GameObject WestBounds
	{
		get
		{
			if(!m_WestBounds)
				m_WestBounds = GameObject.Find("WestBounds");
			return m_WestBounds;
		}
	}

	// Use this for initialization
	void Start () {

	}
	

	public static Vector3 GetRandomLocationInStage()
	{
		float x_max, x_min, z_max, z_min;

		x_max = North.transform.position.z;
		x_min = South.transform.position.z;
		z_max = East.transform.position.x;
		z_min = West.transform.position.x;

		Vector3 vec = new Vector3();
		vec.x = Random.Range(x_max,x_min);
		vec.z = Random.Range(z_max,z_min);
		vec.y = 0;

		return vec;
	}
}
