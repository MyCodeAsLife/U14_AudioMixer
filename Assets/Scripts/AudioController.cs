using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string BackgroundVolume = "MusicVolume";
    private const float MinValue = -80;

    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _masterVolume;
    [SerializeField] private Slider _backgroundVolume;

    private float _currentMasterVolume;
    private float _currentBackgroundVolume;

    private bool _masterEnabled;
    private bool _backgroundEnabled;

    private void Start()
    {
        _mixer.audioMixer.GetFloat(MasterVolume, out _currentMasterVolume);
        _mixer.audioMixer.GetFloat(BackgroundVolume, out _currentBackgroundVolume);
        _masterEnabled = true;
        _backgroundEnabled = true;
    }

    public void ChangeMasterVolume(float volume)
    {
        if (_masterEnabled)
        {
            _currentMasterVolume = volume;
            _mixer.audioMixer.SetFloat(MasterVolume, _currentMasterVolume);
        }
    }

    public void ChangeBackgroundVolume(float volume)
    {
        if (_backgroundEnabled && _masterEnabled)
        {
            _currentBackgroundVolume = volume;
            _mixer.audioMixer.SetFloat(BackgroundVolume, _currentBackgroundVolume);
        }
    }

    public void ToogleMasterMusic(bool enabled)
    {
        _masterEnabled = enabled;
        _masterVolume.interactable = _masterEnabled;

        ChangeGroupValue(MasterVolume, _currentMasterVolume, _masterEnabled);
    }

    public void ToogleBackgroundMusic(bool enabled)
    {
        _backgroundEnabled = enabled;
        _backgroundVolume.interactable = _backgroundEnabled;

        ChangeGroupValue(BackgroundVolume, _currentBackgroundVolume, _backgroundEnabled);
    }

    private void ChangeGroupValue(string groupName, float value, bool enabled) => _mixer.audioMixer.SetFloat(groupName, enabled ? value : MinValue);
}
