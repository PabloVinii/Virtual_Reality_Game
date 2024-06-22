using UnityEngine;
using UnityEngine.InputSystem;

public class ContinuosMovementPhysics : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float turnSpeed = 60f;
    [SerializeField] private InputActionProperty moveInputSource;
    [SerializeField] private InputActionProperty turnInputSource;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider bodyCollider;
    [SerializeField] private Transform directionSource;
    [SerializeField] private Transform turnSource;
    private Vector2 inputMoveAxis; 
    private float inputTurnAxis; 

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;
    }


    void FixedUpdate()
    {
        bool isGrounded = CheckIfGround();
        if (isGrounded)
        {
            Quaternion yaw = Quaternion.Euler(0, directionSource.eulerAngles.y, 0); 
            Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);

            Vector3 targetMovePosition = rb.position + direction * Time.fixedDeltaTime * speed;

            Vector3 axis = Vector3.up;
            float angle = turnSpeed * Time.fixedDeltaTime * inputTurnAxis;

            Quaternion q = Quaternion.AngleAxis(angle, axis);

            rb.MoveRotation(rb.rotation * q);

            Vector3 newPosition = q * (targetMovePosition - turnSource.position) + turnSource.position;
            rb.MovePosition(newPosition);
        }
    }

    public bool CheckIfGround()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLenght = bodyCollider.height / 2 - bodyCollider.radius + 0.5f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLenght, groundLayer);
        
        return hasHit;
    }
}
