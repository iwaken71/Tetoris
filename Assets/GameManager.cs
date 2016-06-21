using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject block;
	int number = 0;
	public int width,height;
	public GameObject oneBlock;

	int[,] board; // 情報 

	GameObject[,] cube; 

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);

		} else {
			Destroy (this.gameObject);
		}

	}

	// Use this for initialization
	void Start () {

		width = 10;
		height = 20;
		BoardStart ();
		NewBlock ();
	}

	void BoardStart(){
		board = new int[width,height];
		cube = new GameObject[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				board [i, j] = 0;
				cube [i, j] = Instantiate(oneBlock,new Vector3 (i,0,j),Quaternion.identity)as GameObject;

			}
		}
	}

	// Update is called once per frame
	void Update () {
		Draw ();
	}

	void Draw(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (board [i, j] >= 1) {
					cube [i, j].SetActive (true);
				} else {
					cube [i, j].SetActive (false);
				}

			}
		}
	}

	public void NewBlock(){
		GameObject obj = Instantiate (block, new Vector3 (5, 0, height-4), Quaternion.identity)as GameObject;
		obj.GetComponent<BlockScript> ().SetId (number);
		number++;
	}

	public void SetBlock(float x,float z){
		int i = (int)(x+0.1f);
		int j = (int)(z+0.1f);
		board [i, j] = 1;
	}

	public bool isBlock(float x,float z){
		int i = (int)(x+0.1f);
		int j = (int)(z+0.1f);
		return board [i, j] >= 1;

	}
	void DeleteCheck(){
		for (int j = 0; j < height; j++) {
			bool ichiretu = false;
			for (int i = 0; i < width; i++) {
				if (board [i, j] >= 1) {

				} else {
					ichiretu = false;
				}
					

			}
			if (ichiretu) {
				for (int i = 0; i < width; i++) {
					board [i, j] = 0;
				}
			}
		}
	}
}
