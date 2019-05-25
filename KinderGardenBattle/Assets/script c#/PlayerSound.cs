using UnityEngine;

namespace a
{
    public class PlayerSound : MonoBehaviour
    {
        public AudioClip course;
        public AudioClip ouille;
        public AudioClip magie;
        public AudioClip piou;
        public AudioClip mort;
        public AudioClip lancepierre;

        public AudioSource AudioS;

        void init()
        {
            
        }
        private void Update()
        {
            if(AudioS == null)
            {
                GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                AudioS = camera.GetComponent<AudioSource>();
            }
        }

        void start()
        {
        }

        void Course()
        {
            AudioS.PlayOneShot(course);
        }

        void Ouille()
        {
            AudioS.PlayOneShot(ouille);
        }

        void Magie()
        {
            AudioS.PlayOneShot(magie);
        }

        void Piou()
        {
            AudioS.PlayOneShot(piou);
        }

        void Mort()
        {
            AudioS.PlayOneShot(mort);
        }

        void LancePierre()
        {
            AudioS.PlayOneShot(lancepierre);
        }
    }
}