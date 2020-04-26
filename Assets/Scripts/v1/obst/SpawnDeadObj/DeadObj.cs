using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnDeadObj
{
    public class DeadObj : MonoBehaviour
    {
        public bool destroyOnCollision = false;
        public GameObject moveTo;
        public float speed = 5;
        private bool needMove = false;

        private void Start()
        {
            if (moveTo != null)
            {
                transform.GetComponent<Rigidbody>().useGravity = false;
                needMove = true;
            }
        }
        void Update()
        {
            if (!needMove) return;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveTo.transform.position, step);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                DamageController.GetDamage(999);
                //collision.gameObject.GetComponent<Player>().PlayerIsDead();
            }
            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.layer == Constants.DestroyObj)
            {
                Debug.Log("OnTriggerEnter Destroy");
                Destroy(gameObject);
            }
        }
    }
}
