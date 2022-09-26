
using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {

        public void StartHostButton()
        {
            NetworkManager.Singleton.StartHost();
        }
        
        public void StartClientButton()
        {
            //if(NetworkManager.Singleton.ConnectedClientsList.Count.Equals(2))
            NetworkManager.Singleton.StartClient();
        }

    }
}