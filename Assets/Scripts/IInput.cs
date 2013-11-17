using UnityEngine;
using System.Collections;

public interface IInput {

	Vecter3 GetContactWorldLocation();
	bool GetPress(out Vector2 screenLoc);
}
