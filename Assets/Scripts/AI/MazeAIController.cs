using UnityEngine;
using UnityEngine.AI;

public class MazeAIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent; // Nav Mesh Agent component of game object
    public float walkSpeed = 4f; // Walking speed of the AI
    public float sprintSpeed = 5f; // Sprinting speed of the AI
    public float waitTime = 3f; // Time to wait between actions
    public float detectionTime = 6f; // Time until the AI rotates towards the player due to proximity
    public float viewRadius = 20f; // Distance AI can see
    public float viewAngle = 90f; // AI cone of vision
    public LayerMask playerMask; // Used with raycast to detect player
    public LayerMask obstacleMask; // Used with raycast to detect obstacles
    public float raycastMeshResolution = 1.0f; // Amount of rays that are cast per degree to increase mesh filter resolution
    public int raycastEdgeIterations = 4; // Number of times raycast will iterate to increase mesh filter performance
    public float raycastEdgeDistance = 0.5f; // Maximum distance used to calculate the minimum and maximimum when ray cast hits
    public Transform[] patrolPoints; // An array containing points the AI patrols

    public float _waitTime; // Wait time delay variable for value tracking
    public float _detectionTime; // Detection rotate time variable for value tracking
    public bool _isPatrolling; // True if AI is patrolling
    public bool _isChasing; // True if player is within range of visibility and being chased
    public bool _isDetectingPlayer; // True if player is nearby and in the process of being detected
    public bool _playerCaught; // True if player has been caught
    public int _currentPatrolPointIndex; // The patrol point that the AI is currently moving to
    public Vector3 _lastSeenLocation; // The location where the player was last seen
    public Vector3 _lastDetectedLocation = Vector3.zero; // The location where the player was last detected

    private void Start()
    {
        _waitTime = waitTime; // Set initial wait time
        _detectionTime = detectionTime; // Set initial detection time
        _isPatrolling = true; // AI is currently patrolling
        _isChasing = false; // AI is not currently chasing player
        _isDetectingPlayer = false; // AI is not currently detecting player
        _playerCaught = false; // AI has not caught player

        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the Nav Mesh Agent component of the AI game object
        navMeshAgent.isStopped = false; // Make it so that the AI is no longer stopped
        navMeshAgent.speed = walkSpeed; // Set AI move speed to walk speed
        _currentPatrolPointIndex = Random.Range(0, patrolPoints.Length - 1); // Set random first patrol point
        navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Walk to next patrol point
    }

    private void Update()
    {
        ScanEnvironment(); // Scan environment for player

        if (_isChasing) // If AI set to chase then chase player, else patrol
        {
            Chase(); // Chase player
        }
        else
        {
            Patrol(); // Patrol maze
        }
    }

    public void SwitchNextPoint()
    {
        _currentPatrolPointIndex = Random.Range(0, patrolPoints.Length); // Set new random patrol point
        navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Set new patrol point as destination
    }

    private void Move(float moveSpeed)
    {
        navMeshAgent.isStopped = false; // Make it so that the AI is no longer stopped
        navMeshAgent.speed = moveSpeed; // Set AI move speed to method input speed
    }

    private void Stop()
    {
        {
            navMeshAgent.isStopped = true; // Make it so that the AI is stopped
            navMeshAgent.speed = 0; // Set AI move speed to zero
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
        if (_isDetectingPlayer) // If player is detected then move to player's last detected location
        {
            if (_detectionTime <= 0) // Continue search if player detection timer runs out
            {
                Move(walkSpeed); // Set AI move speed to walk
                Search(_lastDetectedLocation); // Search at player's last detected location
            }
            else // If player detection timer has not yet elapsed then continue decrementing it
            {
                Stop(); // Stop AI movement
                _detectionTime -= Time.deltaTime; // Decrement player detection timer
            }
        }
        else // If player is not detected then continue patrolling
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
                    _waitTime = waitTime; // Reset action wait timer
                }
                else // If action wait timer has not yet elapsed then continue decrementing it
                {
                    Stop(); // Stop AI movement
                    _waitTime -= Time.deltaTime; // Decrement action wait timer
                }
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
            navMeshAgent.SetDestination(_lastSeenLocation); // Move AI to the location where player was seen last
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) // Keep movement under control when in proximity to player
        {
            if (_waitTime <= 0 && !_playerCaught && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 5f)
            {
                _isPatrolling = true; // Set AI status to patrolling
                _isDetectingPlayer = false; // Set player not detected
                Move(walkSpeed); // Set AI move speed to walk
                _detectionTime = detectionTime; // Reset player detection timer
                _waitTime = waitTime; // Reset action wait timer
                navMeshAgent.SetDestination(patrolPoints[_currentPatrolPointIndex].position); // Resume AI movement to current patrol point
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2f)
                {
                    Stop(); // Stop AI movement
                    _detectionTime -= Time.deltaTime; // Decrement player detection wait timer
                }
            }
        }
    }

    private void ScanEnvironment() 
    {
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
}