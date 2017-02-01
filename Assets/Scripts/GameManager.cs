using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	GameObject[] block;
	int number = 0;
	public int width,height;
	public GameObject oneBlock;
	bool game = true;

	int[,] board; // 情報 

	GameObject[,] cube; 
	AudioSource audioSource;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);

		} else {
			Destroy (this.gameObject);
		}

		block = new GameObject[Resources.LoadAll ("Blocks").Length];
		for (int i = 0; i < block.Length; i++) {
			block [i] = (GameObject)Resources.LoadAll ("Blocks")[i];
		}
		audioSource = GetComponent<AudioSource> ();
			
	}

	// Use this for initialization
	void Start () {
		
		BoardStart ();
		NewBlock ();
	}

	// Update is called once per frame
	void Update () {
		Draw ();
	}

	void BoardStart(){
		board = new int[width,height+4];
		cube = new GameObject[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height+4; j++) {
				board [i, j] = 0;
				if (j < height) {
					cube [i, j] = Instantiate (oneBlock, new Vector3 (i, 0, j), Quaternion.identity)as GameObject;
				}

			}
		}
		game = true;
	}

	void Draw(){
		if (game) {
			for (int j = 0; j < height; j++) {
				for (int i = 0; i < width; i++) {
			
					if (board [i, j] >= 1) {
						cube [i, j].SetActive (true);
						cube [i, j].GetComponent<Renderer> ().material.color = Color.cyan;
					} else {
						cube [i, j].SetActive (false);
					}

				}
			}
		} else {
			for (int j = 0; j < height; j++) {
				for (int i = 0; i < width; i++) {

					if (board [i, j] >= 1) {
						cube [i, j].SetActive (true);
						cube [i, j].GetComponent<Renderer> ().material.color = Color.gray;
					} else {
						cube [i, j].SetActive (false);
					}

				}
			}
		}
	}

	public void NewBlock(){
		game = GameOver ();
		if (game) {
			int index = Random.Range (0, block.Length);
			GameObject obj = Instantiate (block [index], new Vector3 (width / 2, 0, height), Quaternion.identity)as GameObject;
			obj.GetComponent<BlockScript> ().SetId (number);
			number++;


		}

	}

	public void SetBlock(float x,float z){
		int i = (int)(x+0.1f);
		int j = (int)(z+0.1f);
		board [i, j] = 1;
		audioSource.Play ();
		//GameOver ();
	}

	public bool isBlock(float x,float z){
		int i = (int)(x+0.1f);
		int j = (int)(z+0.1f);
		return board [i, j] >= 1;
	}
	public void DeleteCheck(){
		for (int j = 0; j < height; j++) {
			bool ichiretu = true;
			for (int i = 0; i < width; i++) {
				if (board [i, j] >= 1) {

				} else {
					ichiretu = false;
				}
					

			}
			if (ichiretu) {
				Down (j);
				j--;
			}
		}
	}

	void Down(int input){
		for (int j = input; j < height-1; j++) {
			for (int i = 0; i < width; i++) {
				board [i, j] = board [i, j+1];
			}
		}
	}

	public bool GameOver(){
		bool check = false;
		for (int j = height; j < height+4; j++) {
			for (int i = 0; i < width; i++) {
				if (board [i, j] >= 1) {
					return false;
				}
			}
		}
		return true;
	}
}
