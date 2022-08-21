using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDudeManager : MonoBehaviour, IManager
{
    private Transform holding;

    [SerializeField] private DudeController dude;
    
    
    private readonly string[] _winObjective = {"Cool", "NotCool"};
    private readonly string[] _objectivePrompts = {"No Cool Dudes Allowed, VERY ILLEGAL", "No Nerds Allowed"};
    public int winCondition = 0; 
    
    private readonly Dictionary<int, List<int>> _promptObjectives = new Dictionary<int, List<int>>();
    
    void Awake()
    {
        
    }

    private void OnEnable()
    {
        dude.OnDudeCollision += DudeCollisionEvent;
    }
    private void OnDisable()
    {
        dude.OnDudeCollision -= DudeCollisionEvent;
    }

    private void DudeCollisionEvent(string objectTag)
    {
        if (objectTag == _winObjective[winCondition])
        {
            EventManager.InvokeMiniGameFinish(true);
        }
        else
        {
            EventManager.InvokeMiniGameFinish(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();
        CheckRelease();

        if (holding != null)
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPoint.z = 0;
            holding.position = mouseWorldPoint;
        }
    }

    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPoint, Vector2.down);

            if (hit.collider != null)
            {
                string hitTag = hit.collider.gameObject.tag;

                if (hitTag is "Cool" or "NotCool")
                {
                    holding = hit.collider.transform;
                }
            }
        }
    }

    private void CheckRelease()
    {
        if (Input.GetMouseButtonUp(0))
        {
            holding = null;
        }
    }

    public void SetCurrentPrompt(int currentPrompt)
    {
        winCondition = currentPrompt;
        Debug.Log(_objectivePrompts[winCondition]);
    }
}
