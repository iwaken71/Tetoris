using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	bool move,left,right,down;
	GameObject[] child;
	public int id;

	// Use this for initialization
	void Start () {
		move = true;
		left = true;
		right = true;
		down = true;
		child = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			child [i] = transform.GetChild (i).gameObject;
		}
	}



	// Update is called once per frame
	void Update () {
		LeftCheck ();
		RightCheck ();
		DownCheck ();
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (move) {
				if (down) {
					DownMove ();
				} else {
					move = false;
					GameManager.instance.NewBlock ();
					for (int i = 0; i < transform.childCount; i++) {
						Vector3 vec = child [i].transform.position;
						GameManager.instance.SetBlock (vec.x,vec.z);
					}
					GameManager.instance.DeleteCheck ();
					Destroy (this.gameObject);

				}
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (move && left) {
				LeftMove ();

			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (move && right) {
				RightMove ();
			}
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			if (move) {
				Vector3 tmp1 = transform.position;
				Quaternion tmp2 = transform.rotation;
				transform.Rotate (new Vector3(0,90,0));
				Fix ();
				bool check = true;
				for (int i = 0; i < transform.childCount; i++) {
					Vector3 vec = child [i].transform.position;
					if (GameManager.instance.isBlock (vec.x, vec.z)) {
						check = false;
						break;
					}
				}
				if (!check) {
					transform.position = tmp1;
					transform.rotation = tmp2;
				}
			}
		}
	}


	void LeftCheck(){

		bool check = true;
		for (int i = 0; i < transform.childCount; i++) {
			Ray ray = new Ray (child [i].transform.position, Vector3.left);
			RaycastHit hit;
			float distance = child [i].transform.localScale.x / 2+ 0.05f;
			if (Physics.Raycast (ray, out hit, distance)) {
				if (hit.collider.tag == "Block") {
					int tmp = hit.collider.GetComponent<BlockScript> ().id;
					if (id != tmp) {
						check = false;
						break;
					}
				} else {
					check = false;
					break;
				}
			}
		}
		left = check;
	}
	void RightCheck(){

		bool check = true;
		for (int i = 0; i < transform.childCount; i++) {
			Ray ray = new Ray (child [i].transform.position, Vector3.right);
			RaycastHit hit;
			float distance = child [i].transform.localScale.x / 2+ 0.05f;
			if (Physics.Raycast (ray, out hit, distance)) {
				if (hit.collider.tag == "Block") {
					int tmp = hit.collider.GetComponent<BlockScript> ().id;
					if (id != tmp) {
						check = false;
						break;
					}
				} else {
					check = false;
					break;
				}
			}
		}
		right = check;
	}

	void Fix(){
		for (int i = 0; i < transform.childCount; i++) {
			if (child [i].transform.position.x >= GameManager.instance.width) {
				transform.position += Vector3.left;
				Fix ();
			}
		}
		for (int i = 0; i < transform.childCount; i++) {
			if (child [i].transform.position.x <= -1) {
				transform.position += Vector3.right;
				Fix ();
			}
		}
		for (int i = 0; i < transform.childCount; i++) {
			if (child [i].transform.position.y <= -1) {
				transform.position += Vector3.forward;
				Fix ();
			}
		}

	}

	void DownCheck(){
		if (move) {
			bool check = true;
			for (int i = 0; i < transform.childCount; i++) {
				Ray ray = new Ray (child [i].transform.position, Vector3.back);
				RaycastHit hit;
				float distance = child [i].transform.localScale.z / 2 + 0.05f;
				if (Physics.Raycast (ray, out hit, distance)) {
					if (hit.collider.tag == "Block") {
						int tmp = hit.collider.GetComponent<BlockScript> ().id;
						if (id != tmp) {
							check = false;
							break;

						}
					} else {
						check = false;
						break;
					}
				}
			}
			down = check;
		}

	}

	public void SetId(int input){
		id = input;
	}

	void LeftMove(){
		transform.position += Vector3.left;
		Fix ();
	}
	void RightMove(){
		transform.position += Vector3.right;
		Fix ();
	}
	void DownMove(){
		transform.position += Vector3.back;
		Fix ();
	} 


}
