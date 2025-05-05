using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        SFX,
        Ambience
    }

    [SerializeField] VolumeType volumeType;

    private Slider volumeSlider;


    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        switch(volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
            case VolumeType.Ambience:
                volumeSlider.value = AudioManager.instance.ambienceVolume;
                break;
            default:
                Debug.LogWarning("VolumeType not supported " + volumeType);
                break;
        }
    }

    public void  OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                break;
            case VolumeType.Ambience:
                AudioManager.instance.ambienceVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VolumeType not supported " + volumeType);
                break;
        }
    }
}
