using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelTank : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPoint;
    [SerializeField] private Bullet _bulletType;
    [SerializeField] private float _enemy;
    [SerializeField] private float _angleVelocity;


    private List<Bullet> _bullets;
    private Vector2 _aimPoint;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        Rigidbody rigidbody2D1 = this.gameObject.GetComponent<Rigidbody>();
        _rigidbody = rigidbody2D1;
        GenerateBullet();
    }

    private void GenerateBullet()
    {
        _bullets = new List<Bullet>();
        for (int i = 0; i < 10; i++)
        {
            Bullet newBullet = Instantiate(_bulletType, _bulletPoint.transform);
            newBullet.gameObject.SetActive(false);
            newBullet.transform.SetParent(this.GetComponentInParent<ParentfromBullet>().transform);
            _bullets.Add(newBullet);
        }
    }

    public void SetAimPoint(Vector2 aimPoint)
    {
        _aimPoint = aimPoint;
    }

    private void FixedUpdate()
    {
        Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(_aimPoint);
        Vector3 targetDirection = mouseWordPosition - transform.position;
        
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        
        Quaternion needRotation = Quaternion.Euler(0.0f, 0.0f, angle);

        _rigidbody.transform.rotation = Quaternion.RotateTowards(_rigidbody.transform.rotation, needRotation, _angleVelocity * Time.deltaTime);
        

    }

    public Bullet GetBullet(List<Bullet> _bullets)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].gameObject.activeInHierarchy)
            {
                _bullets[i].Enemy = _enemy;
                return _bullets[i];
            }
        }
        //Создание доп платформы, если не будет хватать, + исправляет жалобы компилятора
        Bullet newBullet = Instantiate(_bulletType, _bulletPoint.transform);
        newBullet.gameObject.SetActive(false);
        newBullet.transform.SetParent(this.GetComponentInParent<ParentfromBullet>().transform);
        _bullets.Add(newBullet);
        return newBullet;
    }

    public void Shoot()
    {
        //перемещение пули на точку появления
        Bullet newBullet = GetBullet(_bullets);
        newBullet.transform.position = _bulletPoint.gameObject.transform.position;
        newBullet.transform.rotation = _bulletPoint.gameObject.transform.rotation;

        //включение пули
        newBullet.gameObject.SetActive(true);
        
    }

}
