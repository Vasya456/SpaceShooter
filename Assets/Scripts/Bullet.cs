using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    void Start()
    {
        
    }

  
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8f) 
        {
            if (transform.parent != null) 
            {
                Destroy(transform.parent.gameObject);
            }
            
            Destroy(this.gameObject,5f);
        }

    }
}
