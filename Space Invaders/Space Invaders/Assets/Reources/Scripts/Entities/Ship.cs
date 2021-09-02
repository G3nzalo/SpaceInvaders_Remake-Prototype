using UnityEngine;

abstract class Ship : MonoBehaviour
{
    public byte Life { get; set; }

    public virtual void TakeDamage(byte _otherAttackPower)
    {
        if (Life > 0) this.Life -= _otherAttackPower;

        if(Life <= 0)
        {
            VfxManager.instance.SetParticlesDestroyShip(transform.position);
            SfxManager.instance.PlaySfxDestroy(transform.position.x);
            Destroy(gameObject);
        }
    }

    public abstract void OnTriggerEnter2D(Collider2D _other);

    ~Ship()
    {
    }
}
