using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    #region Unity Ref
    [Header("Limits Movements Army")]
    [SerializeField] float limitMovementRight = -0.30f;
    [SerializeField] float limitMovementLeft = -0.6f;    

    [Space(2.0f)]
    [Header("Speed Movements Army")]
    [SerializeField] float speedMovementX = 0.005f;
    [SerializeField] float speedMovementY = 0.01f;
    #endregion

    #region Private vars And Component
    Transform initPos = null;
    bool invert = false;
    #endregion

    #region Unity Methods

    private void Start()
    {
        SetValuesPosInitForResetGame();
    }

    void Update()
    {
        MovementContoller();
    }
    #endregion

    #region Private methods
    private void SetValuesPosInitForResetGame()
    {
        initPos = GetComponent<Transform>();
        initPos.position = transform.position;
    }

    private void MovementContoller()
    {
        if (transform.position.x != limitMovementRight && !invert) MovementRight();

        CheckInvertMovementRigthToLeft();

        if (invert)
        {
            MovementLeft();

            CheckInvertMovementLeftToRight();

        }
    }

    private void CheckInvertMovementLeftToRight()
    {
        if (transform.position.x <= limitMovementLeft)
        {
            DownStepY();
            invert = false;
        }
        else
            return;
    }

    private void CheckInvertMovementRigthToLeft()
    {
        if (transform.position.x >= limitMovementRight && !invert)
        {
            DownStepY();
            invert = true;
        }
        else
            return;
    }

    private void MovementLeft() =>
        transform.position = new Vector2(transform.position.x - speedMovementX * Time.deltaTime, transform.position.y);

    private void MovementRight() =>
        transform.position = new Vector2(transform.position.x + speedMovementX * Time.deltaTime, transform.position.y);

    private void DownStepY() =>
        transform.position = new Vector2(transform.position.x, transform.position.y - speedMovementY);
    #endregion
}
