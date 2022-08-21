using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigame", menuName = "ScriptableObjects/Minigame", order = 1)]
public class Minigame : ScriptableObject
{
    public int numWinConditions;
    public GameObject MiniGameObject;

}
