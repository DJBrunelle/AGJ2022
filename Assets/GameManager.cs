using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using Random = System.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<Minigame> miniGames;

    private int _currentMiniGame;

    private List<List<int>> _combinations = new List<List<int>>();
    private IOrderedEnumerable<List<int>> _combinationsRandom;
    

    private void Awake()
    {
        _currentMiniGame = 0;

        for (int ii = 0; ii < miniGames.Count; ii++)
        {
            for (int ij = 0; ij < miniGames[ii].numWinConditions; ij++)
            {
                _combinations.Add(new List<int>(){ii,ij});
            }
        }
        var rnd = new Random();
        _combinationsRandom = _combinations.OrderBy(item => rnd.Next());
        
        
        foreach (var item in _combinationsRandom)
        {
            _combinations.Remove(item);
            _combinations.Add(item);
        }
    }

    private void OnEnable()
    {
        EventManager.MiniGameFinished += EventManagerOnMiniGameFinished;
        
        LoadMiniGame();
    }

    private void OnDisable()
    {
        EventManager.MiniGameFinished -= EventManagerOnMiniGameFinished;
    }

    private void LoadMiniGame()
    {
        GameObject miniGameObject = GameObject.FindWithTag("MiniGame");
        if (miniGameObject)
        {
            Destroy(miniGameObject);
        }

        var curMiniGame = miniGames[_combinations[_currentMiniGame][0]];
        miniGameObject = Instantiate(curMiniGame.MiniGameObject);
        miniGameObject.GetComponent<IManager>().SetCurrentPrompt(_combinations[_currentMiniGame][1]);
    }

    private void EventManagerOnMiniGameFinished(bool win)
    {
        Debug.Log(win);
        GameObject miniGameObject = GameObject.FindWithTag("MiniGame");
        if (miniGameObject)
        {
            Destroy(miniGameObject);
        }
        if (_currentMiniGame < _combinations.Count - 1)
        {
            _currentMiniGame++;
        }
        else return;
        
        LoadMiniGame();
    }
}
