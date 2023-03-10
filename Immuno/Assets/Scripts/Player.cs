using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    public float _thrustSpeed = 1.0f;
    public float _turnSpeed = 1.0f;
    private AudioSource _shootSound;

    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody2D>();
        _shootSound= GetComponent<AudioSource>();
    }
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection= 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
    }
    private void FixedUpdate()
    {
        if(_thrusting )
        {
            _rigidbody.AddForce(transform.up * _thrustSpeed);
        }
        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection*_turnSpeed);
        }
    }
    private void Shoot()
    {
        _shootSound.Play();
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }
    private void OnEnable()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            //stopping movement
            _rigidbody.angularVelocity = 0.0f;
            //stopping rotation
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
