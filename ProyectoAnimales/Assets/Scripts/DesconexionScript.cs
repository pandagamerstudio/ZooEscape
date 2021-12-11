using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DesconexionScript : MonoBehaviourPun
{
    public GameObject canvasMenu;
    bool isMobile;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern bool IsMobile();
#endif
    void CheckIfMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif
    }
    public void Start()
    {
        if (!photonView.IsMine) return;

        CheckIfMobile();

        if (isMobile)
        {
            canvasMenu.transform.localScale = new Vector3(0.8f, 0.8f, canvasMenu.transform.localScale.z);
        }
    }
    public void OnBackToMainMenu(){
        PhotonNetwork.LeaveRoom();
    }
}
