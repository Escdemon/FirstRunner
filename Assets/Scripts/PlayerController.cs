using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveSpeedSave;
	public float speedMultiplier;
	public float speedIncreaseMilestone;
	private float speedIncreaseMilestoneSave;
	private float speedMiletoneCount;
	private float speedMiletoneCountSave;
	public float jumpForce;

	public float jumpTime;
	private float jumpTimeCounter;

	private bool stoppedJumping;
	private bool canDoubleJump;

	private Rigidbody2D rb;

	public bool grounded, headed;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	//private Collider2D collider;

	private Animator animator;

	public GameManager gameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		//collider = GetComponent<Collider2D> ();
		animator = GetComponent<Animator> ();

		jumpTimeCounter = jumpTime;

		speedMiletoneCount = speedIncreaseMilestone;

		moveSpeedSave = moveSpeed;
		speedMiletoneCountSave = speedMiletoneCount;
		speedIncreaseMilestoneSave = speedIncreaseMilestone;

		stoppedJumping = true;
		canDoubleJump = true;
	}

	// Update is called once per frame
	void Update () {

		//grounded = Physics2D.IsTouchingLayers (collider, whatIsGround);
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (transform.position.x > speedMiletoneCount) {
			speedMiletoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
		}

		rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);


		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (grounded) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play();
			}
			if (canDoubleJump && !grounded) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play();
				canDoubleJump = false;
				jumpTimeCounter = jumpTime;
			}
		}

		if (Input.GetKey (KeyCode.Space)) {
			if (jumpTimeCounter > 0 && !stoppedJumping) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;
		}

		#elif UNITY_ANDROID
		/*
		if (Input.touchCount > 0) {
			if (grounded) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			}
		}*/

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			if (grounded) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play();
			}
			if (canDoubleJump && !grounded) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play();
				canDoubleJump = false;
				jumpTimeCounter = jumpTime;
			}
		}

		if (Input.touchCount > 0) {
			if (jumpTimeCounter > 0 && !stoppedJumping) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (Input.touchCount == 0) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;
		}
		#endif

		animator.SetFloat ("Speed", rb.velocity.x);
		animator.SetBool ("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other) {
		
		if (other.gameObject.CompareTag ("killBox")) {
			gameManager.RestartGame();
			moveSpeed = moveSpeedSave;
			speedMiletoneCount = speedMiletoneCountSave;
			speedIncreaseMilestone = speedIncreaseMilestoneSave;
			deathSound.Play ();
		}
	}
}
