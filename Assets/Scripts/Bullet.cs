using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float enemy;
    [SerializeField] private ParticleSystem explosion;

    private Rigidbody2D _rigidbody;
    private int _consSpeed;

    public float Enemy { get => enemy; set => enemy = value; }
    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        Rigidbody2D rigidbody2D1 = this.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody = rigidbody2D1;
        //_consSpeed = Speed;
    }

    private void OnEnable()
    {
        explosion.gameObject.SetActive(false);
        _rigidbody.AddForce(_rigidbody.transform.up * speed);
        //Speed = _consSpeed;
    }
    private void Start()
    {
        
    }

    /*private void Move()
    {
        Vector2 velocity = transform.up * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position +  velocity);
    }

    private void FixedUpdate()
    {
        Move();
    }*/

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(explosion.duration);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ParentfromBullet>() != null && collision.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer != this.gameObject.GetComponentInParent<ParentfromBullet>().gameObject.layer)
        {
            Speed = 0;
            explosion.gameObject.SetActive(true);
            //his.delay(explosion.duration);
            StartCoroutine(ExampleCoroutine());
            
        }
    }
}
