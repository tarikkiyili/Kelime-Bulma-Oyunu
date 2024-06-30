using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private Image soundImage;
    [SerializeField] private Image hapticsImage;

    [Header(" Settings ")]
    private bool soundsState;
    private bool hapticsState;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadStates();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SoundsButtonCallback(){

        soundsState = !soundsState;
        UpdateSoundState();
        SaveStates();
    }

    private void UpdateSoundState(){

        if(soundsState)
        EnableSounds();
        else
        DisableSounds();
    }

    private void EnableSounds(){
        
        SoundsManager.instance.EnableSounds();
        soundImage.color =  Color.white;
    }

    private void DisableSounds(){

        SoundsManager.instance.DisableSounds();
        soundImage.color =  Color.black;
    }

        public void HapticsButtonCallback(){

        hapticsState = !hapticsState;
        UpdateHapticState();
        SaveStates();
    }

    private void UpdateHapticState(){

        if(hapticsState)
        EnableHaptics();
        else
        DisableHaptics();
    }

    private void EnableHaptics(){
        
        hapticsImage.color =  Color.white;
    }

    private void DisableHaptics(){

        hapticsImage.color =  Color.black;
    }

    private void LoadStates(){
        soundsState = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1;

        UpdateSoundState();
        UpdateHapticState();
    }

    private void SaveStates(){
        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);
    }
}
