using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    GameObject player;
    //[SerializeField] private Camera cam;

    //missile behavior variables
    [SerializeField] float launchTime;
    float launchTimer;
    bool lockingOn;
    bool targetAcquired;
    float angleMover;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        launchTimer += Time.deltaTime;
        if(launchTimer >= launchTime) {
            //stop moving up
            rb.velocity = Vector3.zero;
            if(!lockingOn && !targetAcquired) {
                lockingOn = true;
            }

            //orientate towards player

            //launch at player
        }

        if(lockingOn) {
            //point towards player
            Vector3 targetPosition = GameObject.FindWithTag("Player").transform.position;
            Vector3 dir = (targetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            if(angle < 0) {
                angle += 360;
            }
            print(Mathf.Round(angle));

            if(targetPosition.x >= transform.position.x && rotationSpeed > 0) {
                rotationSpeed *= -1;
            } else if(targetPosition.x < transform.position.x && rotationSpeed < 0) {
                rotationSpeed *= -1;
            }
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
            print(Mathf.Round(transform.localRotation.eulerAngles.z));

            if(Mathf.Round(transform.localRotation.eulerAngles.z) == Mathf.Round(angle)) {
                print("locked on");
                targetAcquired = true;
                lockingOn = false;
            }
        }

        if(targetAcquired) {
            //launch at player
        }
    }
}
