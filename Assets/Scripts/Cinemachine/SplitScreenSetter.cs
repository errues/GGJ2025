using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenSetter : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineCamera _cinemachineCamera;

    private void Start()
    {
        OutputChannels playerChannel = (OutputChannels)Mathf.Pow(2, 15 - _playerInput.playerIndex);
        _cinemachineBrain.ChannelMask = playerChannel;
        _cinemachineCamera.OutputChannel = playerChannel;
    }
}
