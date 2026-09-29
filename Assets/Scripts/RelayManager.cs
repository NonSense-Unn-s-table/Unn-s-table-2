/** 
*   Code borrowed from Lab 6 of DH2310 Extended Reality in Theory and Practice
*/

using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    [SerializeField] private string joinCode;
    [SerializeField] private TMP_Text joinCodeText;
    private UnityTransport unityTransport;

    // Start is called before the first frame update
    void Start()
    {
        unityTransport = (UnityTransport) NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        SignIn();
    }

    async void SignIn()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void HostRelay()
    {
        //1. Create an allocation for 2 people 
        int maxConnections = 2;
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);

        // 2. Create a join code
        joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        //Writing to our text UI so player can see the code
        joinCodeText.text = "Join Code:\n" + joinCode;

        //3. Configure the UnityTransport and tell it about our Relay server data
        unityTransport.SetHostRelayData(
            allocation.RelayServer.IpV4, 
            (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, 
            allocation.Key, 
            allocation.ConnectionData
        );

        //StartHost()
        NetworkManager.Singleton.StartHost();
    }
}
