using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bulletManager : MonoBehaviour
{
    [SerializeField] GameObject[] bulletPrefabs;
    public void SpawnBullet()
    {
        int r = Random.Range(0, bulletPrefabs.Length);
        int startPoint = Random.Range(0, 1);
        float spawnPosition ;
        if(r<=1){ //左右に生成できるように
            spawnPosition=5f;
        }else{
            spawnPosition = -6f;
        }
        GameObject bullet = Instantiate(bulletPrefabs[r], transform);
        bullet.transform.position = new Vector3(spawnPosition, Random.Range(-3.8f, 2.8f), 0f);
    }
}
