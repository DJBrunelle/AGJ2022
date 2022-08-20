using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoManager : MonoBehaviour
{

    [SerializeField] DinoController dino;

    string[] WinObjective = {"Finish", "Tree"};

    public int WinCondition = 0; 

    // Start is called before the first frame update
    void Start()
    {
        dino.OnHitObject += DinoManager_OnHitObject;
    }

    void DinoManager_OnHitObject(string name)
    {
        if (name == WinObjective[WinCondition])
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
