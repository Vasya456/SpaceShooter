using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
   [SerializeField] private float _speed = 3f;

    [SerializeField] private int buffID;
    void Start()
    {
        
    }


    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f) 
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            {
                if (buffID == 0)
                {
                    player.TripleShotActive();
                }
                else if (buffID == 1) 
                {
                    Debug.Log("Speed");
                }
                else if (buffID == 2)
                {
                    Debug.Log("Shield");
                }

                switch (buffID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBuffActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                }
            }
            
            Destroy(this.gameObject);
        }
    }

}
