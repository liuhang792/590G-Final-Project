using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public int turn;

    void Start()
    {
        turn = 1;
    }

    void NextTurn()
    {
        turn += 1;
    }
}
