using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    [SerializeField] private Slider sessionTimeSlider;
    [SerializeField] private Slider enemySpawnSlider;
    [SerializeField] private TMP_Text sessionTimeText;
    [SerializeField] private TMP_Text enemySpawnText;
    [SerializeField] private Button saveButton;
    [SerializeField] private GameObject initialScreenRef;

    private void Awake()
    {
        saveButton.onClick.AddListener(SaveButtonOnClick);
        sessionTimeSlider.onValueChanged.AddListener(SessionTimeSliderOnValueChange);
        enemySpawnSlider.onValueChanged.AddListener(EnemySpawnSliderOnValueChange);
    }

    private void SessionTimeSliderOnValueChange(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        string displayTime = time.ToString(@"m\:ss");
        sessionTimeText.text = displayTime;
    }

    private void EnemySpawnSliderOnValueChange(float value)
    {
        enemySpawnText.text = value + " segundos";
    }
    private void SaveButtonOnClick()
    {
        PlayerPrefs.SetFloat("GameSessionTime", sessionTimeSlider.value);
        PlayerPrefs.SetFloat("EnemySpawnTime", enemySpawnSlider.value);
        initialScreenRef.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        sessionTimeSlider.value = PlayerPrefs.GetFloat("GameSessionTime", sessionTimeSlider.minValue);
        enemySpawnSlider.value = PlayerPrefs.GetFloat("EnemySpawnTime", enemySpawnSlider.minValue);
    }
}
