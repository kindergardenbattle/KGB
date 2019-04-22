using UnityEngine;
using System.Collections;
using Photon.Pun;


namespace multi

{
    public class PlayerAnimatorManager : MonoBehaviourPunCallbacks
    {
        public float speed;
        #region MonoBehaviour Callbacks


        // Use this for initialization
        private void Start()
        {
        }


        // Update is called once per frame
        private void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * Time.deltaTime;
            transform.Translate(new Vector3(horizontal * speed, 0f, vertical * speed)); // mouvement
        }


        #endregion
    }
}
