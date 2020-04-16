using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnDeadObj
{
    public class Spawner : MonoBehaviour
    {
        public float startDeley = 0;
        public float spawnTimer = 0;
        public float destroyAfter = 30;
        public GameObject prefab;
        Transform instPosition;
        // Start is called before the first frame update
        void Start()
        {
            instPosition = transform;
            InvokeRepeating("CreateBullet", startDeley, spawnTimer);
        }

        void CreateBullet()
        {
            var bullet = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
            bullet.SetActive(true);
            Destroy(bullet, destroyAfter);
        }
    }
}
