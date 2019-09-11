using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * If there's no player present in the scene then a new one will be instantiated.
 */ 
public class PlayerLoader : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.instance == null)
        {
            Instantiate(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
