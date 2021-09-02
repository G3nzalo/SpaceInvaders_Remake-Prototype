using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGrid : MonoBehaviour
{
    public static EnemiesGrid instance = null;

    #region Unity ref
    [Space(2.0f)]
    [Header("Grid Size")]
    [SerializeField] static int  column = 6;   
    [SerializeField] static int row = 11;    

    [Space(2.0f)]
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] GameObject[,] enemiesGrid = new GameObject[row, column];

    [Space(2.0f)]
    [SerializeField] Transform enemyPosIterator;
    [SerializeField] Transform enemyPosBase;

    [SerializeField] GameObject enemiesController;

    [Space(2.0f)]
    [Header("Grid offset Positions Invaders")]
    [SerializeField] float offsetX = 0.1f;
    [SerializeField] float offsetY = 0.1f;
    #endregion

    List<GameObject> enemiesListToRestart = new List<GameObject>();

    #region MonoBehaviour
    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        createEnemies();
    }

    private void LateUpdate()
    {
        CheckResetGame();
    }

    #endregion

    #region CreateGridAndEnemyes
    void createEnemies()
    {
        for (int i = 0; i < row; i++) //x
        {
            for (int j = 0; j < column; j++) // y 
            {
                if (j == column-1)
                {
                    CreateEnemyShipInGame(i, j, true, true);
                }
                else
                {
                     CreateEnemyShipInGame(i, j, true, false);
                }


                NextPosEnemyY();
            }
            NextPosEnemyX();
        }
        ResetEnemiesPosInit();  // For new armies
    }

    private void ResetEnemiesPosInit()=> enemyPosIterator.position = enemyPosBase.position;

    private void NextPosEnemyY()=> enemyPosIterator.position = new Vector2(enemyPosIterator.position.x, enemyPosIterator.position.y - offsetY);

    private void NextPosEnemyX()=> enemyPosIterator.position = new Vector2(enemyPosIterator.position.x + offsetX, enemyPosBase.position.y);

    private void CreateEnemyShipInGame(int _posInRow, int _posInColumn, bool _isAlive, bool _isTheLastShipInMyColumn)
    {

        enemiesGrid[_posInRow, _posInColumn] = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)]);
        enemiesGrid[_posInRow, _posInColumn].transform.parent = enemiesController.transform;  // Generate parent for controller movement army
        enemiesGrid[_posInRow, _posInColumn].transform.position = enemyPosIterator.transform.position;

        SetDataEnemye(enemiesGrid[_posInRow, _posInColumn], _posInRow, _posInColumn, _isTheLastShipInMyColumn);
    }

    private void SetDataEnemye(GameObject _enemy, int _posInRow, int _posInColumn, bool _isTheLastShipInMyColumn)
    {
        Enemy enemyComponent = _enemy.GetComponent<Enemy>();
        enemyComponent.posInRow = _posInRow;
        enemyComponent.posInColumn = _posInColumn;
        enemyComponent.IsTheLastShipInMyColumn = _isTheLastShipInMyColumn;
    }
    #endregion


    #region GridColorKillLogic

    #region Kill Right and Left
    public void _testRightKill(int _posInRow, int _posInColumn, string _colorShip)
    {
        KillCurrentEnemy(_posInRow, _posInColumn);

        if (_posInRow < row - 1)
        {
            if (enemiesGrid[_posInRow + 1, _posInColumn].gameObject != null)

            {
                int iterator = _posInRow + 1;

                while (enemiesGrid[iterator, _posInColumn].tag == _colorShip && enemiesGrid[iterator, _posInColumn].gameObject != null)
                {
                    if (iterator >= 0)
                    {
                        CheckUpAndDownAfterKilling(_posInColumn, iterator);
                    }
                    iterator++;
                }
            }
            else
                return;
        }
        else
            return;
    }

    public void _testLeftKill(int _posInRow, int _posInColumn, string _colorShip)
    {
        KillCurrentEnemy(_posInRow, _posInColumn);


        if (_posInRow >= 1)

        {
            if (enemiesGrid[_posInRow - 1, _posInColumn].gameObject != null)
            {
                int iterator = _posInRow - 1;

                while (enemiesGrid[iterator, _posInColumn].tag == _colorShip && enemiesGrid[iterator, _posInColumn].gameObject != null)
                {
                    if (iterator >= 0)
                    {
                        CheckUpAndDownAfterKilling(_posInColumn, iterator);
                    }
                    iterator--;

                }
            }
            else
                return;

        }
        else
            return;
    }

    private void CheckUpAndDownAfterKilling(int _posInColumn, int iterator)
    {
        if (enemiesGrid[iterator, _posInColumn] != null)
        {
            Destroy(enemiesGrid[iterator, _posInColumn]);
        }

        int currentColumAbajo = _posInColumn;
        if (currentColumAbajo + 1 < column && enemiesGrid[iterator, currentColumAbajo + 1].gameObject != null)
        {
            CheckKillDown(iterator, currentColumAbajo, enemiesGrid[iterator, currentColumAbajo].tag);
        }

        int currentColumArriba = _posInColumn;
        if (currentColumArriba - 1 > 0 && enemiesGrid[iterator, currentColumAbajo - 1].gameObject != null)
        {
            CheckKillUp(iterator, currentColumArriba, enemiesGrid[iterator, currentColumArriba].tag);
        }
    }
    #endregion

    #region Kill Down and Up
    public void CheckKillUp(int _posInRow, int _posInColumn, string _colorShip)
    {
        KillCurrentEnemy(_posInRow, _posInColumn);

        if (_posInColumn >= 1)
        {
            if (enemiesGrid[_posInRow, _posInColumn - 1].gameObject != null)
            {
                int iterator = _posInColumn - 1;

                while (enemiesGrid[_posInRow, iterator].tag == _colorShip && enemiesGrid[_posInRow, iterator].gameObject != null)
                {

                    if (iterator >= 0)
                    {
                        CheckRightAndLeftAfterKilling(_posInRow, _posInColumn, iterator);
                    }
                    iterator--;

                }
            }
            ChekCurrentEnemyFire(_posInRow, _posInColumn);
        }
        else
           return;
       
    }

   


    public void CheckKillDown(int _posInRow, int _posInColumn, string _colorShip)
    {
        KillCurrentEnemy(_posInRow, _posInColumn);

        if (_posInColumn < column - 1)
        {
            if (enemiesGrid[_posInRow, _posInColumn + 1] != null)
            {
                int iterator = _posInColumn + 1;

                while (enemiesGrid[_posInRow, iterator].tag == _colorShip && enemiesGrid[_posInRow, iterator].gameObject != null)
                {

                    if (iterator >= 0)
                    {
                        CheckRightAndLeftAfterKilling(_posInRow, _posInColumn, iterator);
                    }
                    iterator++;

                }
            }
            else
                return;
        }
        else
            return;

    }

    private void CheckRightAndLeftAfterKilling(int _posInRow, int _posInColumn, int iterator)
    {
        if (enemiesGrid[iterator, _posInColumn].gameObject == null)
        {
            Destroy(enemiesGrid[iterator, _posInColumn]);
        }

        int currentFilaDerecha = _posInRow;
        if (currentFilaDerecha + 1 < row && enemiesGrid[currentFilaDerecha + 1, iterator].gameObject != null)
        {
            _testRightKill(currentFilaDerecha, iterator, enemiesGrid[currentFilaDerecha, iterator].tag);
        }

        int currentFilaIzquierda = _posInRow;
        if (currentFilaIzquierda - 1 > 0 && enemiesGrid[currentFilaIzquierda - 1, iterator].gameObject != null)
        {
            _testLeftKill(currentFilaIzquierda, iterator, enemiesGrid[currentFilaIzquierda, iterator].tag);
        }
    }
    #endregion

    #region Current Enemy Fire
    private void ChekCurrentEnemyFire(int _posInRow, int _posInColumn)
    {
        int iterator2 = column - 1;
        while (iterator2 >= _posInColumn && iterator2 > 0)
        {
            if (enemiesGrid[_posInRow, iterator2 - 1].gameObject != null)
            {
                StartCoroutine(Fire(_posInRow, iterator2 - 1));
            }
            iterator2--;
        }
    }

    IEnumerator Fire(int _posInRow, int _posInColumn)
    {
        yield return new WaitForSeconds(1.0f);
        Enemy currentEnemy = enemiesGrid[_posInRow, _posInColumn].GetComponent<Enemy>();
        currentEnemy.IsTheLastShipInMyColumn = true;
        currentEnemy.Shoot(currentEnemy.IsTheLastShipInMyColumn);
    }
    #endregion

    private void KillCurrentEnemy(int _posInRow, int _posInColumn)
    {
        if (enemiesGrid[_posInRow, _posInColumn] != null)
        {
            Destroy(enemiesGrid[_posInRow, _posInColumn].gameObject);
        }
    }

    #endregion

    #region Reset Game
    void CheckResetGame()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if(enemiesGrid[i,j].gameObject != null)
                {
                    enemiesListToRestart.Add(enemiesGrid[i, j]);
                }
            }
        }

        if(enemiesListToRestart.Count == 0)
        {
            enemiesListToRestart.Clear();
            ResetGame();
        }

        else
        {
            enemiesListToRestart.Clear();
        }

    }

    private void ResetGame()
    {
        createEnemies();
    }
    #endregion

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
