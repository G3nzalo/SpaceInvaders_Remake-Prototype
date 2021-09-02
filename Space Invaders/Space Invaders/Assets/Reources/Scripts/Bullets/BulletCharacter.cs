using UnityEngine;
using System.Collections;

public class BulletCharacter: Bullet
{
    #region Unity Methods
    void Update() => Destroy(gameObject, 2.0f);

    void FixedUpdate() => BulletUp();

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("bullet_enemy"))
        {
            SfxManager.instance.PlaySfxDestroy(transform.position.x);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }

    #endregion

    void BulletUp() => rb.AddForce(GetVelocityImpulse() * Time.fixedDeltaTime * transform.up, ForceMode2D.Impulse);

}
