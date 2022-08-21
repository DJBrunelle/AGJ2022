using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DinoManager : MonoBehaviour, IManager
{

    [SerializeField] private DinoController dino;

    private readonly string[] _winObjective = {"Finish", "Tree", "Bird"};
    private readonly string[] _objectivePrompts = {"Win the game", "Vegetarian's only", "Lose the game", "High 5ing Birds Required"};

    private readonly Dictionary<int, List<int>> _promptObjectives = new Dictionary<int, List<int>>();


    public int winCondition = 0;

    private void Awake()
    {
        _promptObjectives.Add(0,new List<int>(){1,2});
        _promptObjectives.Add(1,new List<int>(){0,2});
        _promptObjectives.Add(2,new List<int>(){0});
        _promptObjectives.Add(3,new List<int>(){0,1});
        
        
    }

    private void OnEnable()
    {
        dino.OnHitObject += DinoManager_OnHitObstacle;
    }
    private void OnDisable()
    {
        dino.OnHitObject -= DinoManager_OnHitObstacle;
    }

    private void DinoManager_OnHitObstacle(string obstacleName)
    {
        foreach (int win in _promptObjectives[winCondition])
        {
            if (obstacleName == _winObjective[win])
            {
                EventManager.InvokeMiniGameFinish(true);
                return;
            }
        }
        
        EventManager.InvokeMiniGameFinish(false);

    }

    public void SetCurrentPrompt(int currentPrompt)
    {
        winCondition = currentPrompt;
        Debug.Log(_objectivePrompts[winCondition]);
    }
}
