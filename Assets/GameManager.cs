using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<Minigame> Minigames;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in Minigames[0].mapObjects)
        {
            Instantiate(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
