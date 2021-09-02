using UnityEngine;

class Character : Ship
{
    #region Unity Ref
    [Header("Ref platey and values")]
    [SerializeField] PlayerControls playerControls = null;
    [SerializeField] ShipTemplate valuesShip = null;
    [SerializeField] byte speedCharacter;

    [Space(2.0f)]

    [Header("Bullet ref")]
    [SerializeField] Transform bulletspawnPos = null;
    [SerializeField] GameObject bulletPrefab;
    [Space(2.0f)]


    [SerializeField] GameObject particlesExplosivePrefab;

    #endregion

    #region Vars
    Rigidbody2D rb;
    float movHorizontal;
    float InitPosX { get; set; }
    float InitPosY { get; set; }
    #endregion

    #region Unity Methods

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Start()
    {
        InitValues();
    }

    public override void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("bullet_enemy"))
        {
            BulletEnemy bullet_enemy = _other.GetComponent<BulletEnemy>();
            Destroy(_other.gameObject);
            TakeDamage(bullet_enemy.GetAtackPower());
            Destroy(bullet_enemy);

        }
    }

    private void Update() => ReadAxisValuesToMove();

    private void FixedUpdate() => Move(movHorizontal);

    private void LateUpdate() => ReadFireAxisValues();
    #endregion

    #region Public Methods
    public override void TakeDamage(byte _otherAttackPower)
    {
        base.TakeDamage(_otherAttackPower);
        UiManager.instance.CurrentLife(Life);

        if (Life > 0)
        {
            VfxManager.instance.SetParticlesDestroyShip(transform.position);
            SfxManager.instance.PlaySfxDestroy(transform.position.x);
            transform.position = new Vector2(InitPosX, InitPosY);
        }

        if (Life <= 0)
        {
            UiManager.instance.InCredits();
        }
        
    }

    public void ReadFireAxisValues()
    {
        if (Input.GetButtonDown(playerControls.fire) && movHorizontal == 0) OnPlayerFire();
        else
            return;
    }
    #endregion

    #region Private Methods

    private void InitValues()
    {
        Life = valuesShip.life;
        InitPosX = transform.position.x;
        InitPosY = transform.position.y;
    }

    private void ReadAxisValuesToMove() => movHorizontal = (sbyte)Input.GetAxis(playerControls.movHorizontal);

    private void Move(float axisValue) => rb.velocity = axisValue * speedCharacter * Time.fixedDeltaTime * transform.right;

    private void OnPlayerFire()
    {
        GameObject bulletShoot = Instantiate(bulletPrefab);
        bulletShoot.transform.position = bulletspawnPos.position;
        SfxManager.instance.PlaySfxShoot(transform.position.x);
    }

    #endregion
}
