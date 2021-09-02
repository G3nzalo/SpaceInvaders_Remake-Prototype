using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Unity ref
    [SerializeField] byte atackPower;
    [SerializeField] byte velocityImpulse;
    #endregion

    protected Rigidbody2D rb = null;

    void Awake () => rb = GetComponent<Rigidbody2D>();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("envirovent"))
        {
            SfxManager.instance.PlaySfxDestroy(transform.position.x);
            Destroy(gameObject);
        }
    }

    public byte GetAtackPower() { return this.atackPower; }
    public byte GetVelocityImpulse() { return this.velocityImpulse; }

}
