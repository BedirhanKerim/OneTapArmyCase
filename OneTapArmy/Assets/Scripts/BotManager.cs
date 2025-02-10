using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(MoveArmy),6f,6f);
    }

    private void MoveArmy()
    {
        GameManager.Instance.movementManager.MoveEnemyArmy();
    }

}
