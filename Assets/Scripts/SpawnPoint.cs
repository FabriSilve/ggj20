using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellConnections
{
    left,
    right,
    top,
    down
}

public enum State
{
    empty,
    occupied,
    broken
}

public class SpawnPoint : MonoBehaviour
{
    public State state = State.empty;
    public int weight = 1;

}
