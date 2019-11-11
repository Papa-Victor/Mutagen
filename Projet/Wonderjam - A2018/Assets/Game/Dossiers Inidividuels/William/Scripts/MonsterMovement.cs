using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    private GameObject playerGameObject;

    private Rigidbody2D rigidbody2;

    [SerializeField]
    float fovAngle = 100;
    [SerializeField]
    float fovLength = 10;

    [SerializeField]
    float speed = 10;

    [SerializeField]
    private float rotationSpeed = 50;

    [SerializeField]
    private float wallEvadeRotation = 5;

    [SerializeField]
    private float meleeRange = 10;

    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 upv2 = transform.up;

        float leftAngle = upv2.ToAngle() + fovAngle / 2;
        float rightAngle = upv2.ToAngle() - fovAngle / 2;

        Vector2 leftAngleVector = leftAngle.ToVector();
        Vector2 rightAngleVector = rightAngle.ToVector();

        Debug.DrawRay(transform.position, leftAngleVector * fovLength, Color.green);
        Debug.DrawRay(transform.position, rightAngleVector * fovLength, Color.green);

        Vector3 vectorToPlayer = playerGameObject.transform.position - transform.position;
        float distanceToPlayer = vectorToPlayer.magnitude;
        Vector3 directionToPlayer = vectorToPlayer.normalized;

        float angleBetween = Vector2.SignedAngle(transform.up, directionToPlayer);

        float wallEvadeRotation = CheckForWalls(leftAngleVector, rightAngleVector);

        float rotationVectorSpeed = 0;

        if (angleBetween.Abs() < rotationSpeed)
        {
            //en face
            rotationVectorSpeed = angleBetween * Time.fixedDeltaTime;
        }
        else if (angleBetween > rotationSpeed)
        {
            rotationVectorSpeed = rotationSpeed * Time.fixedDeltaTime;
        }
        else if (angleBetween < -rotationSpeed)
        {
            rotationVectorSpeed = -rotationSpeed * Time.fixedDeltaTime;
        }

        Debug.Log(rotationVectorSpeed * wallEvadeRotation);

        transform.Rotate(Vector3.forward, rotationVectorSpeed);
        transform.Rotate(Vector3.forward, wallEvadeRotation);

        rigidbody2.velocity = transform.up * speed;

        //if (!isInRangeOfPlayer(distanceToPlayer))
        //{
        //    Vector3 wallEvadeVector = CheckForWalls(leftAngleVector, rightAngleVector);
        //    float dotProduct = Vector3.Dot(transform.up , directionToPlayer);

        //    float angleBetween = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        //    Debug.Log(Mathf.Acos(dotProduct)*Mathf.Rad2Deg);
        //    Vector3 LerpedDirectionToPlayer = Vector3.Lerp(transform.up, directionToPlayer, (1 / angleBetween).Capped(1));
        //    transform.up = LerpedDirectionToPlayer.normalized;
        //    rigidbody2.velocity = transform.up * speed;
        //}
        //else
        //    rigidbody2.velocity = Vector2.zero;
        

        //Monstre direction
        Debug.DrawRay(transform.position, directionToPlayer, Color.red);
        //Monster FOV boundaries

    }

    private float CheckForWalls(Vector2 leftVec, Vector2 rightVec)
    {
        int layerMask = ~(1 << LayerMask.NameToLayer("Monsters"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, leftVec, fovLength, layerMask);
        if (hit)
        {
            //Turn right a bit
            Debug.Log(hit.distance);
            return -wallEvadeRotation / hit.distance;
        }
        hit = Physics2D.Raycast(transform.position, rightVec, fovLength, layerMask);
        if (hit)
        {
            //turn left a bit
            Debug.Log(hit.distance);
            return wallEvadeRotation / hit.distance;
        }

        return 0;
    }

    private bool isInRangeOfPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= meleeRange)
            return true;
        return false;
    }
}
