using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;

    private void Awake()
    {
        playAgain.onClick.AddListener(PlayAgainButtonOnClick);
        mainMenu.onClick.AddListener(MainMenuButtonOnClick);
    }

    private void MainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void PlayAgainButtonOnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnEnable()
    {
        scoreText.text = "Score: "+ GameSessionManager.instance.playerScore;
    }
}
