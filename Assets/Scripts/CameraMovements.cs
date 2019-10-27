using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GadeTask4
{
    public class CameraMovements : MonoBehaviour
    {
        // Start is called before the first frame update

        public float speed = 1;
        public float target;
        public float smoothSpeed = 2.0f;
        public float min = 1.0f;
        public float max = 20.0f;

        // Start is called before the first frame update
        void Start()
        {
            target = Camera.main.orthographicSize;
        }

        // Update is called once per frame
        void Update()
        {
            Movements();
            Zoom();
        }

        public void Movements() //Move the camera left, right, down and up
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
        }

        public void Zoom()  //Smooth zoom in and out
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                target -= scroll * speed;
                target = Mathf.Clamp(target, min, max);
            }

            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, target, smoothSpeed * Time.deltaTime);
        }

    }
}
