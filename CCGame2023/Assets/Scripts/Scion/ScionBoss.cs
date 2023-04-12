using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScionBoss : MonoBehaviour
{
    [SerializeField] GameObject missile;
    [SerializeField] float loadCooldown;
    float loadTimer;
    [SerializeField] float missileCooldown;
    float missileTimer;
    float missileAngle;
    [SerializeField] float missileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        missileTimer += Time.deltaTime;
        if(missileTimer >= missileCooldown) {
            //fire missile
            missileAngle = Random.Range(-22f, 31f); //sets random angle for missiel to spawn at, so they don't all take same path up
            GameObject newMissile = Instantiate(missile, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.Euler(0, 0, missileAngle));
            newMissile.GetComponent<Rigidbody2D>().velocity = (newMissile.transform.GetChild(0).transform.position - newMissile.transform.GetChild(1).transform.position).normalized * missileSpeed * (Time.deltaTime + 1);

            missileTimer = 0;
        }
    }
}
