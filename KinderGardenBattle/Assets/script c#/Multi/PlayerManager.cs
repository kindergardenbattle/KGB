using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace multi
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {
            
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

            
            if (_cameraWork != null)
            {
                
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
