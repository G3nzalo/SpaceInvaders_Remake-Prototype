using UnityEngine;

public class BulletEnemy : Bullet
{
    #region Unity Methods
    void Update() => Destroy(gameObject, 1.2f);

    void FixedUpdate() => BulletDowm();

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("bullet"))
        {
            SfxManager.instance.PlaySfxDestroy(transform.position.x);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
    #endregion

    void BulletDowm() => rb.AddForce(GetVelocityImpulse() * Time.fixedDeltaTime * -transform.up, ForceMode2D.Impulse);

}
