using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	public GameObject left,right,down,camera1;

	// Use this for initialization
	void Start () {
		int width = GameManager.instance.width;
		int height = GameManager.instance.height;

		left.transform.localScale = new Vector3 (1,1,height);
		right.transform.localScale = new Vector3 (1,1,height);
		down.transform.localScale = new Vector3 (width,1,1);

		left.transform.position = new Vector3 (-1,0,(height-1.0f)/2.0f);
		right.transform.position = new Vector3 (width,0,(height-1.0f)/2.0f);
		down.transform.position = new Vector3 ((width-1.0f)/2.0f,0,-1);
		camera1.transform.position = new Vector3 ((width-1.0f)/2.0f,30,height/2.0f);


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
