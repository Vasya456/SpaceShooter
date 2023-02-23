using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float RandomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(RandomX, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            //other.transform.GetComponent<Player>().Damage();
            if (player != null) 
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
       
        if (other.tag == "Bullet") 
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    
    
    }



}