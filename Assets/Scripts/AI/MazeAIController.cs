using UnityEngine;
using UnityEngine.AI;

public class MazeAIController : MonoBehaviour
{
    [Header("Movement Settings:")]
    [Tooltip("The nav mesh agent component attached to the maze enemy.")]
    [SerializeField] private NavMeshAgent navMeshAgent; // Nav Mesh Agent component of game object
    [Tooltip("The walk speed of the maze enemy.")]
    [SerializeField] private float walkSpeed = 4f; // Walking speed of the AI
    [Tooltip("The sprint speed of the maze enemy.")]
    [SerializeField] private float sprintSpeed = 5f; // Sprinting speed of the AI
    [Tooltip("The length of time the maze enemy pauses between patrol points.")]
    [SerializeField] private float waitTime = 3f; // Time to wait between actions

    [Header("Detection Settings:")]
    [Tooltip("The amount of time before the maze enemy detects the player based on close proximity.")]
    [SerializeField] private float detectionTime = 6f; // Time until the AI rotates towards the player due to proximity
    [Tooltip("The view radius of the maze enemy.")]
    [SerializeField] private float viewRadius = 20f; // Distance AI can see
    [Tooltip("The cone of vision of the maze enemy.")]
    [SerializeField] private float viewAngle = 90f; // AI cone of vision
    [Tooltip("The layer mask for the player.")]
    [SerializeField] private LayerMask playerMask; // Used with raycast to detect player
    [Tooltip("The layer mask for raycast obstacles.")]
    [SerializeField] private LayerMask obstacleMask; // Used with raycast to detect obstacles

    [Header("Patrol Settings:")]
    [Tooltip("The amount of time the maze enemy waits to try moving to a patrol point before timing out and switching to the next.")]
    [SerializeField] private float waitTimeout = 5.0f; // Amount of time to wait until timeout to next patrol point
    [Tooltip("The list of patrol points that the maze enemy will choose from to move to.")]
    [SerializeField] private Transform[] patrolPoints; // An array containing points the AI patrols

    [Header("Animation Settings:")]
    [Tooltip("The animator component for the enemy's rigged 3D model.")]
    [SerializeField] private Animator animator; // The animator for the AI
    [Tooltip("Whether this is a Jelly enemy.")]
    [SerializeField] private bool isJelly = false;

    public bool _isPatrolling; // True if AI is patrolling
    public bool _isChasing; // True if player is within range of visibility and being chased
    public bool _isDetectingPlayer; // True if player is nearby and in the process of being detected
    public bool _playerCaught; // True if player has been caught
    public bool _bellCooldownComplete;

    [HideInInspector] public float raycastMeshResolution = 1.0f; // Amount of rays that are cast per degree to increase mesh filter resolution
    [HideInInspector] public int raycastEdgeIterations = 4; // Number of times raycast will iterate to increase mesh filter performance
    [HideInInspector] public float raycastEdgeDistance = 0.5f; // Maximum distance used to calculate the minimum and maximimum when ray cast hits

    private float _detectionTime; // Detection rotate time variable for value tracking
    private float _lastMoveTime; // Amount of time since AI last moved
    private float _waitTime; // Wait time delay variable for value tracking
    private Vector3 _lastSeenLocation; // The location where the player was last seen
    private Vector3 _lastDetectedLocation = Vector3.zero; // The location where the player was last detected
    private int _currentPatrolPointIndex; // The patrol point that the AI is currently moving to
    private float _bellCooldownTimer = 0f;

    private void Start()
    {
        _waitTime = waitTime; // Set initial wait time
        _detectionTime = detectionTime; // Set initial detection time
        _isPatrolling = true; // AI is currently patrolling
        _isChasing = false; // AI is not currently chasing player
        _isDetectingPlayer = false; // AI is not currently detecting player
        _playerCaught = false; // AI has not caught player
        _bellCooldownComplete = true;
        _bellCooldownTimer = 0f;

        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the Nav Mesh Agent component of the AI game object
        navMeshAgent.isStopped = false; // Make it so that the AI is no longer stopped
        navMeshAgent.speed = walkSpeed; // Set AI move speed to walk speed
        _currentPatrolPointIndex = Random.Range(0, patrolPoints.Length - 1); // Set random first patrol point
        navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Walk to next patrol point
    }

    private void Update()
    {
        if (!GameManager.Instance.GetPauseMenuCanvas().activeSelf)
        {
            ScanEnvironment(); // Scan environment for player

            if (_isChasing) // If AI set to chase then chase player, else patrol
            {
                Chase(); // Chase player
                if (GetComponentInChildren<MusicBox>() != null)
                {
                    MusicBox.Instance.PlayMusic();
                }

                if (GetComponentInChildren<Teleportation>() != null)
                {
                    Teleportation.Instance.TeleportIn();
                }

                if (GetComponentInChildren<PenguinBell>() != null)
                {
                    //Debug.Log("Chasing and has PenguinBell");

                    if (_bellCooldownComplete)
                    {
                        //Debug.Log(_bellCooldownComplete);

                        PenguinBell.Instance.SoundAlarmBell();
                        MoveEnemiesToAlarmSound();
                        _bellCooldownComplete = false;
                        _bellCooldownTimer = 0.0f;
                    }
                }

                if (!_bellCooldownComplete)
                {
                    _bellCooldownTimer += Time.deltaTime;
                    if (_bellCooldownTimer >= PenguinBell.Instance.GetPenguinCoolDownTime())
                    {
                        _bellCooldownComplete = true;
                    }
                }
            }

            else
            {
                Patrol(); // Patrol maze
            }

            if (navMeshAgent.velocity.magnitude <= navMeshAgent.stoppingDistance && // Check if AI is within stopping distance
                navMeshAgent.velocity.magnitude < 0.1f) // Check if AI is stopped
            {
                _lastMoveTime += Time.deltaTime; // Increment last moved timer
            }
            else // If not within stopping distance or moving
            {
                _lastMoveTime = 0; // Reset last moved timer
            }

            if (_lastMoveTime > waitTimeout && !_isChasing) // If last moved timer has reached timeout limit and AI is not chasing player, then switch to next patrol point
            {
                SwitchNextPoint(); // Switch to next patrol point
            }
        }
    }

    public void SwitchNextPoint()
    {
        _currentPatrolPointIndex = Random.Range(0, patrolPoints.Length); // Set new random patrol point
        navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Set new patrol point as destination
        Move(walkSpeed); // Move to next patrol point (with walking feet)
        if (!isJelly)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("playerCaught", false);
        }
        _lastMoveTime = 0; // Reset last moved timer
    }

    public void MoveToRandomPosition()
    {
        Vector3 randomPosition = patrolPoints[Random.Range(0, patrolPoints.Length - 1)].position;
        gameObject.transform.position = randomPosition;
        _isChasing = false;
        //Debug.Log("Jelly teleported to " + randomPosition);
    }

    public void StopMovement()
    {
        Stop();
    }

    public void ResumeMovement()
    {
        if (_isChasing)
        {
            Move(sprintSpeed);
            if (!isJelly)
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("playerCaught", false);
            }
        }
        else
        {
            Move(walkSpeed);
            if (!isJelly)
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("playerCaught", false);
            }
        }
    }

    private void Move(float moveSpeed)
    {
        navMeshAgent.isStopped = false; // Make it so that the AI is no longer stopped
        navMeshAgent.speed = moveSpeed * MoralitySystem.Instance.monsterSpeedModifier; // Set AI move speed to method input speed
    }

    private void Stop()
    {
        {
            navMeshAgent.isStopped = true; // Make it so that the AI is stopped
            navMeshAgent.speed = 0; // Set AI move speed to zero
            if (!isJelly)
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("playerCaught", false);
            }
        }
    }

    private void Search(Vector3 lastDetectedPlayerLocation)
    {
        navMeshAgent.SetDestination(lastDetectedPlayerLocation); // Set new AI destination to be last detected player location 
        if (Vector3.Distance(transform.position, lastDetectedPlayerLocation) <= 0.25) // If in range, wait 
        {
            if (_waitTime <= 0) // If player detection timer runs out, then resume patrolling
            {
                _isDetectingPlayer = false; // Set player as no longer detected
                Move(walkSpeed); // Set AI movement speed to walk
                if (!isJelly)
                {
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("playerCaught", false);
                }

                navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Resume AI movement to current patrol point
                _waitTime = waitTime; // Reset action wait timer
                _detectionTime = detectionTime; // Reset player detection timer
            }
            else // Stop at last detected player location and wait for action timer to elapse
            {
                Stop(); // Stop AI movement
                _waitTime -= Time.deltaTime; // Decrement action wait timer
            }
        }
    }

    private void Patrol()
    {
            _isDetectingPlayer = false; // Set player as no longer detected
            _lastDetectedLocation = Vector3.zero; // Reset location that player was last detected
            navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Resume AI movement to current patrol point
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) // Control stopping when approaching patrol point
            {
                if (_waitTime <= 0) // If action wait timer has elapsed then switch and move to next patrol point
                {
                    SwitchNextPoint(); // Switch to next patrol point and begin moving there
                    Move(walkSpeed); // Set AI move speed to walk
                if (!isJelly)
                {
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("playerCaught", false);
                }
                    _waitTime = waitTime; // Reset action wait timer
                }
                else // If action wait timer has not yet elapsed then continue decrementing it
                {
                    Stop(); // Stop AI movement
                    _waitTime -= Time.deltaTime; // Decrement action wait timer
                }
            }
    }
    
    private void Chase()
    {
        _isDetectingPlayer = false; // Player is not being detected because they're already seen by the AI
        _lastDetectedLocation = Vector3.zero; // Reset last detected location

        if (!_playerCaught) // If player isn't caught then chase them
        {
            Move(sprintSpeed); // Set AI move speed to sprint
            if (!isJelly)
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            navMeshAgent.SetDestination(_lastSeenLocation); // Move AI to the location where player was seen last
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) // Keep movement under control when in proximity to player
        {
            if (_waitTime <= 0 && !_playerCaught && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 0f)
            {
                //_isPatrolling = true; // Set AI status to patrolling
                //_isDetectingPlayer = false; // Set player not detected
                Move(walkSpeed); // Set AI move speed to walk
                if (!isJelly)
                {
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("playerCaught", true);
                }

                _detectionTime = detectionTime; // Reset player detection timer
                _waitTime = waitTime; // Reset action wait timer
                navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Resume AI movement to current patrol point
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 0f)
                {
                    Stop(); // Stop AI movement
                    _detectionTime -= Time.deltaTime; // Decrement player detection wait timer
                }
            }
        }
    }

    private void ScanEnvironment()
    {
        DrawDebugVisionArc(); // Use to show arc of AI vision

        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);  // Collider array of player colliders that are in range

        for (int i = 0; i < playerInRange.Length; i++) 
        {
            Transform playerTransform = playerInRange[i].transform; // Set player transform variable
            Vector3 playerDirection = (playerTransform.position - transform.position).normalized; // Set player direction variable
            if (Vector3.Angle(transform.forward, playerDirection) < viewAngle / 2) // If player is spotted, then begin chasing, if not then don't chase
            {
                float playerDistance = Vector3.Distance(transform.position, playerTransform.position); // Set player distance variable
                if (!Physics.Raycast(transform.position, playerDirection, playerDistance, obstacleMask)) // Check for any obstacles in the way of raycast
                {
                    Debug.DrawRay(this.transform.position, playerDirection, Color.green, playerDistance);
                    _isChasing = true; // Set AI to chase player
                    _isPatrolling = false; // Set AI to no longer patrol
                    break;
                }
                else
                {
                    _isChasing = false; // Set AI to no longer chase player
                }
            }
            if (Vector3.Distance(transform.position, playerTransform.position) > viewRadius) // If player distance is not within viewing radius then don't chase player
            {
                _isChasing = false; // Set AI to not chase player
            }
            if (_isChasing) // If player is being chased then set last seen player location
            {
                _lastSeenLocation = playerTransform.transform.position; // Set last seen player location
            }
        }
    }

    private void PlayerCaught()
    {
        _playerCaught = true; // Set player caught status to caught (true)
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

    public void MoveEnemiesToAlarmSound()
    {
        Debug.Log("MoveEnemiesToAlarmSoundCalled");

        GameObject[] mazeEnemies = GameObject.FindGameObjectsWithTag("MazeEnemy");
        foreach (GameObject enemy in mazeEnemies)
        {
            Search(PenguinBell.Instance.gameObject.transform.position);
            Debug.Log(enemy + " moving to " + PenguinBell.Instance.gameObject.transform.position);
        }
    }
    public void MoveEnemiesToUrnBreakSound()
    {
        Debug.Log("MoveEnemiesToUrnBreakSoundCalled");

        GameObject[] mazeEnemies = GameObject.FindGameObjectsWithTag("MazeEnemy");
        foreach (GameObject enemy in mazeEnemies)
        {
            Search(ItemPot.Instance.gameObject.transform.position);
            Debug.Log(enemy + " moving to " + ItemPot.Instance.gameObject.transform.position);
        }
    }

}
