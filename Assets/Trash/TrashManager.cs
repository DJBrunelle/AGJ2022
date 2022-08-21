using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour, IManager
{
    private Transform holding;

    [SerializeField] private BinController recycleBin;
    [SerializeField] private BinController trashBin;
    
    private readonly string[] _winObjective = {"Trash", "Recycle"};
    private readonly string[] _objectivePrompts = { "Recycle", "Don't Recycle" };
    public int winCondition = 0;


    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        recycleBin.OnThrowOut += ThrowOutEvent;
        trashBin.OnThrowOut += ThrowOutEvent;
    }

    private void OnDisable()
    {
        recycleBin.OnThrowOut -= ThrowOutEvent;
        trashBin.OnThrowOut -= ThrowOutEvent;
    }

    private void ThrowOutEvent(string binTag)
    {
        EventManager.InvokeMiniGameFinish(_winObjective[winCondition] == binTag);
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

                if (hitTag is "Trash" or "Recycle")
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
