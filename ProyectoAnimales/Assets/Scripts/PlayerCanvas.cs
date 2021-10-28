using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    PlayerController _target;

    public void SetTarget(PlayerController target)
    {
        if (target == null)
        {
             Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.",this);
            return;
        }
        // Cache references for efficiency
        _target = target;

    }

    void Update(){
        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
