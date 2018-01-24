using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text timerText;
	private Rigidbody rb;

	private int count;
	private float startTime;
	private bool stopTimer = false;
	private bool gameOver = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
		startTime = Time.time;
	}

/*	private void StartNewGame()
	{
		SetCountText ();
		winText.text = "";
		startTime = Time.time;
		gameOver = false;
		stopTimer = false;
	}*/

	void FixedUpdate()
	{
		

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);

	}

	void Update()
	{
		if (stopTimer) {
			return;
		}
		float t = Time.time - startTime;
		float time = 60;
		t = time - t;

		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");
		timerText.text = minutes + ":" + seconds;

		if (seconds == "0" && minutes == "0") {
			winText.text = "You Lost!";
			gameOver = true;
			stopTimer = true;
		}

		if (Input.GetKeyDown (KeyCode.N)) {
			StartNewGame ();
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (gameOver) {
		} else {
			if (other.gameObject.CompareTag ("Pickup")) {
				other.gameObject.SetActive (false);
				count = count + 1;
				SetCountText ();
			}
			if (other.gameObject.CompareTag ("UniquePickup")) {
				other.gameObject.SetActive (false);
				count = count + 5;
				SetCountText ();
			}	
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 16) {
			winText.text = "You Win!";
			stopTimer = true;

		}

	}
}

