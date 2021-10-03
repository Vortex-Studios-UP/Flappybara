using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMovement : MonoBehaviour
{
	[SerializeField]
	private float gravityGoingDown; // Gravity as the y velocity is negative

	[SerializeField]
	private float gravityGoingUp; // Gravity as the y velocity is positive

	[SerializeField]
	private float jumpHeight; // Desired jump height

	// Jump speed is dependent on gravity and desired jump height
	// If jump speed is modified directly, change the jump height only
	public float jumpSpeed
    {
		get => Mathf.Sqrt(2 * jumpHeight * gravityGoingUp);
        set => jumpHeight = 0.5f * jumpSpeed * jumpSpeed / gravityGoingUp;
	}

	[SerializeField]
	private float maxFallSpeed; // Seems to feel best when it is about the same as jumpSpeed

	// Can be read publicly but only modified privately
	private Vector3 _speed;
	public Vector3 Speed
	{
		get => _speed;
		private set => _speed = value;
	}

	// To store on Stop() and restore on UnStop()
	private Vector3 storedSpeed;

	// Should the player character be moving or not?
	private bool stop = false;

	// Start is called before the first frame update
	void Start()
	{
		_speed = Vector3.zero;
		storedSpeed = Speed;
		Stop(); // We want the player to be still at startup and move once they begin jumping
	}

	// Update is called once per frame
	void Update()
	{
		Gravity();

		if(!stop)
			transform.Translate(_speed * Time.deltaTime);
	}

	public void Gravity()
	{
		if(_speed.y > 0)
			_speed.y = Mathf.Clamp(_speed.y - (gravityGoingUp * Time.deltaTime), -maxFallSpeed, float.PositiveInfinity);
		else
			_speed.y = Mathf.Clamp(_speed.y - (gravityGoingDown * Time.deltaTime), -maxFallSpeed, float.PositiveInfinity);
	}

	// To be used by input management
	public void Jump()
	{
		UnStop();
		_speed.y = jumpSpeed;
	}

	// To be used by input management and on lose condition
	public void Stop()
	{
		if(!stop)
			storedSpeed = _speed;

		stop = true;
	}

	// To be used by input management
	public void UnStop()
	{
		if(stop)
			_speed = storedSpeed;

		stop = false;
	}
}
