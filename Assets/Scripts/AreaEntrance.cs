using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string spawnPointName;

    // Start is called before the first frame update
    void Start()
    {
        /**
         *  The player is set an areaTransitionName when they exit an area. (i.e 'area_X')
         * areaTransitionName should be equal to the spawn point on the next area they are exiting towards. (i.e 'area_Y')
         *  This way the player spawn location is controlled when they leave 'area_X' and enter 'area_Y'.
         *  In a 2D environment it is expected that when a player travels from 'area_X' to 'area_Y' they spawn in 'area_Y'
         * right next to the exit of 'area_Y', connected to 'area_X'. 
         *  
         *  Thus if (the player's areaTransitionName, set when exiting 'area_X', is equals to the spawnPointName of 'area_Y')
         *       then {the player's position will be set to the spawn point position in 'area_Y'}
         */
        if(spawnPointName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = this.transform.position;
        }

        UIFade.instance.FadeFromBlack();
        GameManager.instance.fadinBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
