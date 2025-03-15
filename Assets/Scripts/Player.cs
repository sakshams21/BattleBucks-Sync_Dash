using System.Diagnostics;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody Player_RigidBody;
    [SerializeField] private GameObject PickUp_Particle;
    [SerializeField]
    private CinemachineImpulseSource Shake_Cinemachine;

    [SerializeField] private int jumpForce = 5;
    [SerializeField] private int BackForce = 25;

    Vector3 pushBackDirection = new Vector3(0, 1, -1);

    private bool shouldMove = false;
    private bool _isGrounded;

    private float _totalDistance = 0f;

    private float _lastPos = 0;


    [EasyButtons.Button]
    public void StartMoving()
    {
        Player_RigidBody.useGravity = true;
        shouldMove = true;

    }

    [EasyButtons.Button]
    public void Jump()
    {
        if (_isGrounded && !GameManager.Instance.isGameOver)
        {
            Player_RigidBody.velocity = new Vector3(Player_RigidBody.velocity.x, jumpForce, Player_RigidBody.velocity.z);
            _isGrounded = false;
        }
    }
    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (SyncPlayer.Instance != null)
        {
            SyncPlayer.Instance.ReceivePlayerData(transform.position);
        }

        //distance calculation
        float distanceTraveled = Mathf.Abs(transform.position.z - _lastPos);

        _totalDistance += distanceTraveled;
        if (_totalDistance >= GameManager.Instance.Ref_LevelData.DistanceThresholdForPoint)
        {
            GameManager.Instance.ScoreUpdateDistance();
            _totalDistance = 0;
        }
        _lastPos = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver) return;

        if (shouldMove && _isGrounded)
        {
            Vector3 velocity = Player_RigidBody.velocity;
            velocity.z = GameManager.Instance.Ref_LevelData.InitialSpeed;
            Player_RigidBody.velocity = velocity;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //stop or take damage or take live
            GameManager.Instance.ReduceLives();

            collision.collider.enabled = false;
            shouldMove = false;

            //dissolving obstacle
            collision.gameObject.GetComponent<MeshRenderer>().materials[0].DOFloat(1, "_Dissolve", 0.5f).OnComplete(() =>
            {
                collision.gameObject.SetActive(false);
                shouldMove = true;
            });

            Shake_Cinemachine.GenerateImpulseWithForce(1f);
            Player_RigidBody.velocity = Vector3.zero;
            // Player_RigidBody.DOJump
            shouldMove = false;

            //push back player
            Player_RigidBody.AddForce(pushBackDirection * BackForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BackToPool"))
        {
            GameManager.Instance.BackToPool();
        }
        if (other.CompareTag("LoadNewSection"))
        {
            GameManager.Instance.SpawnNextSection();
            //check for origin distance
            GameManager.Instance.PlayerDistanceCheck(transform.position.z);
        }
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            PickUp_Particle.SetActive(false);
            PickUp_Particle.SetActive(true);
            GameManager.Instance.ScoreAdd_Pickup();
        }

    }




    public void OriginReset(float z)
    {
        _lastPos = 0;
        Vector3 newPos = transform.position;
        newPos.z = transform.position.z - z;
        transform.position = newPos;

    }

    public void PlayerDead()
    {
        Player_RigidBody.velocity = Vector3.zero;
    }



}