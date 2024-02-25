using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("General Movement Settings:")]
		[Tooltip("Move speed of the character in m/s")]
		[SerializeField] private float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		[SerializeField] private float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		[SerializeField] private float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		[SerializeField] private float SpeedChangeRate = 10.0f;

		[Header("Detection:")]
		[Tooltip("The layer mask for the maze enemy.")]
		[SerializeField] private LayerMask enemyMask;
		[Tooltip("The layer mask for raycast obstacles.")]
		[SerializeField] private LayerMask obstacleMask;
		[Tooltip("The view radius of the player.")]
		[SerializeField] private float viewRadius = 20f;
		[Tooltip("The view angle of the player.")]
		[SerializeField] private float viewAngle = 90f;


		[Header("Jump Settings:")]
		[Tooltip("The height the player can jump")]
		[SerializeField] private float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		[SerializeField] private float Gravity = -15.0f;
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		[SerializeField] private float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		[SerializeField] private float FallTimeout = 0.15f;

		[Header("Player Grounded:")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		[SerializeField] private bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		[SerializeField] private float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		[SerializeField] private float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		[SerializeField] private LayerMask GroundLayers;

		[Header("Cinemachine:")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		[SerializeField] private GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		[SerializeField] private float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		[SerializeField] private float BottomClamp = -90.0f;

		[HideInInspector] public float maxStamina = 100f;
		[HideInInspector] public float currentStamina = 100f;
		[HideInInspector] public float staminaDrainAmount = 0.5f;
		[HideInInspector] public float staminaRegenAmount = 0.2f;

		private float previousMoveSpeed;
		private float previousSprintSpeed;
		private float previousRotationSpeed;
		private bool isLooking = true;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		[HideInInspector] public StaminaSystem _staminaSystem; // Create reference to stamina system

	
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_staminaSystem = GetComponent<StaminaSystem>(); // Get stamina system
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// Reset timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
		}

		private void ScanEnvironment()
		{
			DrawDebugVisionArc();

			Collider[] enemyInRange = Physics.OverlapSphere(transform.position, viewRadius, enemyMask);  // Collider array of enemy colliders that are in range

			for (int i = 0; i < enemyInRange.Length; i++)
			{
				Transform enemyTransform = enemyInRange[i].transform; // Set enemy transform variable
				Vector3 enemyDirection = (enemyTransform.position - transform.position).normalized; // Set enemy direction variable
				if (Vector3.Angle(transform.forward, enemyDirection) < viewAngle / 2) // If enemy is spotted...
				{
					float enemyDistance = Vector3.Distance(transform.position, enemyTransform.position); // Set enemy distance variable
					if (!Physics.Raycast(transform.position, enemyDirection, enemyDistance, obstacleMask)) // Check for any obstacles in the way of raycast
					{
						Debug.DrawRay(this.transform.position, enemyDirection, Color.green, enemyDistance);
						if (enemyInRange[i].CompareTag("TeleportingEnemy"))
						{
							Teleportation.Instance.PlayerHasLooked();
						}
						isLooking = true;
						//Debug.Log(isLooking);
						return;
					}
					else
					{
						//Teleportation.Instance.PlayerHasNotLooked();// Player not looking
						isLooking = false;
						//Debug.Log(isLooking);
					}
				}
			}
		}
		private void DrawDebugVisionArc()
		{
			Vector3 forward = transform.forward; // Get forward vector
			float rightAngle = (viewAngle / 2); // Set angle of right boundary to half of the view angle
			float leftAngle = -rightAngle; // Set angle of left boundary to opposite of the right view angle
			Vector3 rightBoundary = Quaternion.Euler(0, rightAngle, 0) * forward; // Create forward vector in direction of right angle to define right vision boundary
			Vector3 leftBoundary = Quaternion.Euler(0, leftAngle, 0) * forward; // Create forward vector in direction of left angle to define left vision boundary

			Debug.DrawLine(transform.position, transform.position + rightBoundary * viewRadius, Color.green); // Draw right boundary line
			Debug.DrawLine(transform.position, transform.position + leftBoundary * viewRadius, Color.green); // Draw left boundary line
			Debug.DrawLine(transform.position + rightBoundary * viewRadius, transform.position + leftBoundary * viewRadius, Color.green); // Draw a line between the two boundary end points to show view radius
		}

		public void SetSprintSpeed(float newSprintSpeed)
        {
			SprintSpeed = newSprintSpeed; // Change sprint speed
        }
		public void SetMoveSpeed(float newMoveSpeed)
		{
		MoveSpeed = newMoveSpeed; // Change walk speed
		}

		public void StopMovement()
        {
			previousMoveSpeed = MoveSpeed;
			previousSprintSpeed = SprintSpeed;
			MoveSpeed = 0;
			SprintSpeed = 0;
			_speed = 0; // Make it so player doesn't jut forward when entering maze
			_rotationVelocity = 0; // Make it so player doesn't rotate uncontrollably when entering maze
		}

		public void ResumeMovement()
		{
			MoveSpeed = previousMoveSpeed;
			SprintSpeed = previousSprintSpeed;
		}

		public void FreezeRotation()
        {
			previousRotationSpeed = RotationSpeed;
			RotationSpeed = 0;
        }

		public void UnfreezeRotation()
        {
			RotationSpeed = previousRotationSpeed;
        }

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();

			if (_input.sprint)
            {

            }

		}

		private void LateUpdate()
		{
			CameraRotation();
			ScanEnvironment();
		}

		public void MoveToPosition(Vector3 position)
        {
			gameObject.transform.position = position;
			Debug.Log(gameObject.transform.position);
        }


		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			if(!_input.sprint)
            {
				_staminaSystem.isCurrentlySprinting = false;
            }

			if (_input.sprint && _controller.velocity.sqrMagnitude > 0)
			{
				if (_staminaSystem.currentStamina > 0)
                {
					_staminaSystem.isCurrentlySprinting = true;
					_staminaSystem.Sprinting();
                }
				else
                {
					_input.sprint = false;
                }
			}

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}
			if (!SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(0)) ||
			   !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(1)) ||
			   !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(2)))
			{
				// move the player
				_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			}

		}

		public void PlayerJump()
        {
			_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity); // The square root of H * -2 * G = how much velocity needed to reach desired height
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				_fallTimeoutDelta = FallTimeout; // Reset the fall timeout timer

				
				if (_verticalVelocity < 0.0f) // Stop our velocity dropping infinitely when grounded
				{
					_verticalVelocity = -2f; // Set max falling velocity
				}

		
				if (_input.jump && _jumpTimeoutDelta <= 0.0f) // Check if jump input is pressed and if jump timeout timer has elapsed
				{
					_staminaSystem.StaminaJump(); // Use stamina jump method to check if player has enough stamina to do so
					_jumpTimeoutDelta = JumpTimeout;
				}

				if (_jumpTimeoutDelta >= 0.0f) { _jumpTimeoutDelta -= Time.deltaTime; } // Jump timeout
			}
			else
			{
				_jumpTimeoutDelta = JumpTimeout; // Reset the jump timeout timer

				if (_fallTimeoutDelta >= 0.0f) // Fall timeout
				{
					_fallTimeoutDelta -= Time.deltaTime; // Increment fall timeout timer
				}

				
				_input.jump = false; // If we are not grounded, do not jump
			}

			
			if (_verticalVelocity < _terminalVelocity) // Apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			{
				_verticalVelocity += Gravity * Time.deltaTime; // Apply gravity over time using algorithmD
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}