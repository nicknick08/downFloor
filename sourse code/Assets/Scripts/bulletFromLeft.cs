using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletFromLeft : MonoBehaviour
{
    [SerializeField] float moveSpeed=2f;
    AudioSource gotHit ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gotHit = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed*Time.deltaTime,0,0);
        if(transform.position.x> 6f){
            Destroy(gameObject);
            transform.parent.GetComponent<bulletManager>().SpawnBullet();
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            
            
            Destroy(gameObject);
            
            transform.parent.GetComponent<bulletManager>().SpawnBullet();
        }
         if (other.gameObject.tag == "shield"){
            Destroy(gameObject);
            transform.parent.GetComponent<bulletManager>().SpawnBullet();
        }

    }
}
