using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { CambiarVolumenMaster(musicSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { CambiarVolumenSFX(sfxSlider.value); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarVolumenMaster(float volume)
    {

        AudioMixer.SetFloat("volMaster", volume);
    }

    public void CambiarVolumenSFX(float volume)
    {
        AudioMixer.SetFloat("volSFX", volume);
        audioSource.Play();
    }

    private void Awake()
    {
        
    }

}
