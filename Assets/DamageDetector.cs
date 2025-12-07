using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    public bool EbashilovoAcces { get; set; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!EbashilovoAcces)
            return;

        if (collision.gameObject.layer == 6)
        {
            var force = collision.relativeVelocity.magnitude;
            force = Mathf.Clamp(force, 0, 150);

            var body = collision.gameObject.GetComponent<Rigidbody2D>();

            if (force > 17 && body)
            {
                var mineX = transform.root.GetChild(0).position.x;
                var enemyX = collision.transform.root.GetChild(0).position.x;
                int dirX = mineX > enemyX ? -1 : 1;

                var dir = collision.relativeVelocity;
                dir.x = Mathf.Abs(dir.x);
                dir = dir.normalized;
                //print(dir.x);
                dir.x = Mathf.Clamp(dir.x, 0.3f, 1f);
                dir.x *= dirX;
                dir.y *= Random.Range(1, 7);

                body.AddForceAtPosition(dir * force * 50, collision.contacts[0].point, ForceMode2D.Impulse);

                //body.AddForce(dir * force * 50, ForceMode2D.Impulse);
                //body.AddTorque(force * 3.6f, ForceMode2D.Impulse);

                body.transform.root.GetComponent<AIStickman>().Damage();
            }
        }
    }
}
