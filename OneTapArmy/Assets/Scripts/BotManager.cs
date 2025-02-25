using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(MoveArmy),15f,15f);
        InvokeRepeating(nameof(MoveArmy2),20f,20f);

    }

    private void MoveArmy()
    {
        //GameManager.Instance.movementManager.MoveEnemyArmy();
        GameEventManager.Instance.OnOnMoveEnemyArmy1();
    }
    private void MoveArmy2()
    {
       // GameManager.Instance.movementManager.MoveEnemyArmy2();
        GameEventManager.Instance.OnOnMoveEnemyArmy2();

    }
}
