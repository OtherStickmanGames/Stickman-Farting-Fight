using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamager : MonoBehaviour
{
    public bool EbashitMojno { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EbashitMojno && collision.transform.root.CompareTag("Player"))
        {
            var body = collision.gameObject.GetComponent<Rigidbody2D>();
            var dd = body.GetComponent<DamageDetector>();
            var notBlocked = dd == null || (dd != null && !dd.EbashilovoAcces);

            if (!notBlocked)
            {
                return;
            }

            var force = collision.relativeVelocity.magnitude;
            force = Mathf.Clamp(force, 0, 39);

            if (force > 7 && body)
            {
                var dir = collision.relativeVelocity.normalized;

                var player = collision.transform.root;
                var mineX = transform.root.GetChild(0).position.x;
                var playerX = player.GetChild(0).position.x;
                int dirX = mineX > playerX ? -1 : 1;

                dir.x = Mathf.Abs(dir.x);

                dir.x *= dirX;
                //dir.y = Mathf.Clamp(dir.y, 0, 0.5f);
                var mass = body.mass;
                var power = dir * force * mass;

                body.AddForce(power * 0.3f, ForceMode2D.Impulse);
                player.GetComponent<Stickman>().Damage(1);
            }
        }
    }
}
