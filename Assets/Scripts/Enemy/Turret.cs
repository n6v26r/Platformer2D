using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : PrototypeEnemyAI
{
    public GameObject Bullet;
    public float TurnSpeed = 110f;
    public float ShootCooldown = 0.5f;
    public float ShootPower = 100f;
    [System.NonSerialized] public float TimeToNextShot;

    private void Update()
    {
        TimeToNextShot -= Time.deltaTime;
        if(TimeToNextShot<=0){ // shoot bullet
            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right*ShootPower);
            TimeToNextShot = ShootCooldown;
        }
        SetTarget(Victim.GetComponent<Transform>().position);
        MoveTarget();
    }

    protected override void MoveTarget()
    {
        float slope = (Target.y-transform.position.y)/(Target.x-transform.position.x); // the slope of the vector between the turret nad the target
        float alpha = Mathf.Atan(slope) * Mathf.Rad2Deg; // Alpha is the target angle
        if(Target.x < transform.position.x)
            alpha -= 180;

        float updateAngle = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, alpha, TurnSpeed*Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, updateAngle));
    } 
}
