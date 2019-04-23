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

        public AudioSource AudioS;

        void init()
        {
            
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
    }
}