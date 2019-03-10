// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountOfPlayersProperty.cs" company="Exit Games GmbH">
//   Part of: Pun Cockpit
// </copyright>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine.UI;

using Photon.Realtime;

namespace Photon.Pun.Demo.Cockpit
{
    /// <summary>
    /// PhotonNetwork.CountOfPlayers UI property.
    /// </summary>
    public class CountOfPlayersProperty : PropertyListenerBase
    {
        public Text Text;
        private int _cache = -1;

        private void Update()
        {
            if (PhotonNetwork.NetworkingClient.Server == ServerConnection.MasterServer)
            {
                if (PhotonNetwork.CountOfPlayers != _cache)
                {
                    _cache = PhotonNetwork.CountOfPlayers;
                    Text.text = _cache.ToString();
                    this.OnValueChanged();
                }
            }
            else
            {
                if (_cache != -1)
                {
                    _cache = -1;
                    Text.text = "n/a";
                }
            }
        }
    }
}