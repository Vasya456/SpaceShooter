using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedBuff = 2;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _tripleBulletPrefab;

    [SerializeField] private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField] private int _lives = 3;

    private SpawnManager _spawnManager;

    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBuffActive = false;
    [SerializeField] private bool _isShieldsActive = false;
    
   [SerializeField] private GameObject _shieldVisualizer;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("spawn null");
        }

    }


    void Update()
    {
        Movement();
        Bullet();

    }

    void Movement()
    {
        float horizontaInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontaInput, verticalInput, 0);

        if (_isSpeedBuffActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _speedBuff) * Time.deltaTime);
        }



        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }


    void Bullet()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.deltaTime + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleBulletPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_bulletPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            }

        }
    }


    public void Damage()
    {

        if (_isShieldsActive == true) 
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.PlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;

    }
    public void SpeedBuffActive()
    {
        _isSpeedBuffActive = true;
        StartCoroutine(SpeedBuffRoutine());
    }

    IEnumerator SpeedBuffRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBuffActive = false;

    }

    public void ShieldsActive() 
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }
}
