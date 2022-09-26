using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Position.Value = new Vector3(-16.45f, 0, 0);
                //Move();
            }
            else
            {
                Position.Value = new Vector3(16.45f, 0, 0);
            }

        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Debug.Log("serveur position changed");
                //Position.Value = new Vector3(-16.45f, 0, 0);
            }
            else
            {
                Debug.Log("client position changed");
                //SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    }
}