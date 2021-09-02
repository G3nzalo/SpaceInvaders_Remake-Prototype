using System.Collections;
using UnityEngine;

class Enemy : Ship
{
    #region Unity ref
    [SerializeField] ShipTemplate valuesShip = null;
    [SerializeField] Transform bulletspawnPos = null;
    [SerializeField] GameObject bulletPrefab;
    #endregion

    #region Public Vars
    public int posInRow = 0;
    public int posInColumn = 0;
    public bool  IsAlive { get; set; } 
    public bool IsTheLastShipInMyColumn { get; set; } 
    bool fireStarted;
    #endregion

    #region Unity Methods
    private void Start()
    {
        Life = valuesShip.life;
    }

    private void LateUpdate()
    {
        Shoot(IsTheLastShipInMyColumn);
    }

    public override void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other.CompareTag("bullet")) // bullet character destroy
        {
            BulletCharacter bulletCharacter = _other.GetComponent<BulletCharacter>();
            Destroy(_other.gameObject);
            TakeDamage(bulletCharacter.GetAtackPower());
            Destroy(bulletCharacter);
        }
    }

    #endregion

    #region Public Methods
    public override void TakeDamage(byte _otherAttackPower)
    {
        base.TakeDamage(_otherAttackPower);
        EnemiesGrid.instance._testRightKill(posInRow, posInColumn, gameObject.tag);
        EnemiesGrid.instance._testLeftKill(posInRow, posInColumn, gameObject.tag);
        EnemiesGrid.instance.CheckKillUp(posInRow, posInColumn, gameObject.tag);

    }
    public void Shoot(bool _isTheLastShipInMyColumn)
    {
        if (_isTheLastShipInMyColumn && !fireStarted) StartCoroutine(Fire(Random.Range(1.0f , 3.0f)));  // Me parecio poco cada 3 segundos
    }
    #endregion

    IEnumerator Fire(float _seconds)
    {
        fireStarted = true;
        GameObject bulletShoot = Instantiate(bulletPrefab);
        bulletShoot.transform.position = bulletspawnPos.position;
        SfxManager.instance.PlaySfxShoot(transform.position.x);
        yield return new WaitForSeconds(_seconds);
        fireStarted = false;
    }

}
