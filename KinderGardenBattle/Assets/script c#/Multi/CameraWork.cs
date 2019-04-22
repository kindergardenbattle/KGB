using UnityEngine;

namespace multi
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields

        public float vitesse_cam = 0.125f;
        public Vector3 offset = new Vector3(20, 20, 20);
        public int cas = 0;

        [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;

        // cached transform of the target
        Transform cameraTransform;

        // maintain a flag internally to reconnect if target is lost or camera is switched
        bool isFollowing;


        #endregion

        #region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
        {
            // Start following the target if wanted.
            if (followOnStart)
            {
                OnStartFollowing();
            }

        }

        private void Update()
        {
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    set_cas_plus();
                }

                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    set_cas_less();
                }

                if (cas < 0)
                    cas += 4;

                if (cas == 0)
                {
                    if (offset.x < 0)
                        offset.x = offset.x * (-1);
                    if (offset.z < 0)
                        offset.z = offset.z * (-1);

                }

                if (cas == 1)
                {
                    if (offset.x < 0)
                        offset.x = offset.x * (-1);
                    if (offset.z > 0)
                    {
                        offset.z = offset.z * (-1);
                    }

                }

                if (cas == 2)
                {
                    if (offset.x > 0)
                        offset.x = offset.x * (-1);
                    if (offset.z > 0)
                        offset.z = offset.z * (-1);

                }

                if (cas == 3)
                {
                    if (offset.x > 0)
                        offset.x = offset.x * (-1);
                    if (offset.z < 0)
                        offset.z = offset.z * (-1);

                }

                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    zoom_plus();
                }

                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    zomm_less();
                }
            }
        }

        /// <summary>
        /// MonoBehaviour method called after all Update functions have been called. This is useful to order script execution. For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
        /// </summary>
        void LateUpdate()
        {
            // The transform target may not destroy on level load, 
            // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }

            // only follow is explicitly declared
            if (isFollowing)
            {
                Apply();
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the start following event. 
        /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
        /// </summary>
        public void OnStartFollowing()
        {
            
            cameraTransform = Camera.main.transform;
            isFollowing = true;
            // we don't smooth anything, we go straight to the right camera shot
            Apply();
        }

        public void set_cas_plus()
        {
            cas = (cas + 1) % 4;
        }

        public void set_cas_less()
        {
            cas = cas == 0 ? 3 : --cas;
        }

        public void zoom_plus()
        {
            if (offset.y > 15)
            {
                offset.y -= 5;
                offset.x += offset.x < 0 ? 5 : -5;
                offset.z += offset.z < 0 ? 5 : -5;
            }
        }

        public void zomm_less()
        {
            if (offset.y < 45)
            {
                offset.y += 5;
                offset.x += offset.x < 0 ? -5 : 5;
                offset.z += offset.z < 0 ? -5 : 5;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Follow the target smoothly
        /// </summary>
        void Apply()
        {
            Vector3 Position_ = transform.position + offset;
            Vector3 Nposition = Vector3.Lerp(cameraTransform.transform.position, Position_, vitesse_cam);
            cameraTransform.transform.position = Nposition;
            cameraTransform.transform.LookAt(transform);
        }
        #endregion
    }
}