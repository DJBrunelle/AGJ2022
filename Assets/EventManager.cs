using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void MiniGameFinish(bool win);
    public static event MiniGameFinish MiniGameFinished;
    public static void InvokeMiniGameFinish(bool win) => MiniGameFinished?.Invoke(win);
}
