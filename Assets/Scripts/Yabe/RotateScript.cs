using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (new Vector3(0,0,Time.deltaTime*-30));
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (new Vector3(0,0,Time.deltaTime*30));
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Rotate (new Vector3(Time.deltaTime*30,0,0));
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Rotate (new Vector3(Time.deltaTime*-30,0,0));
		}
	}
}
