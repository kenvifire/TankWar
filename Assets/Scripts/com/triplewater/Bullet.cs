using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.triplewater
{

    public class Bullet : MonoBehaviour
    {
        public float moveSpeed = 10;
        internal Role role;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Tank":
                    Tank tank = collision.GetComponent<Tank>();
                    if (tank.role != role) 
                    {
                        collision.SendMessage("Die");
                    }

                    break;
                case "Heart":
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                    break;
                case "Wall":
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    break;
                case "Barriar":
                    Destroy(gameObject);
                    break;
                case "Bullet":
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    break;
            }


        }

    }
}
