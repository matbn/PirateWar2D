using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverlayUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionTime;
    [SerializeField] private TMP_Text playerScore;
    public static OverlayUIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating(nameof(UpdateSessionTime), 0.1f, 1f);
    }
    public void UpdateScore(int value)
    {
        playerScore.text = value.ToString();
    }

    private void UpdateSessionTime()
    {
        double mainGameTimerd = GameSessionManager.instance.sessionTime;
        TimeSpan time = TimeSpan.FromSeconds(mainGameTimerd);
        string displayTime = time.ToString(@"m\:ss");
        sessionTime.text = displayTime;
    }
}
