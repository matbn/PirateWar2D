using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsScreenRef;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { SceneManager.LoadScene("SampleScene"); });
        optionsButton.onClick.AddListener(() => { optionsScreenRef.gameObject.SetActive(true); this.gameObject.SetActive(false); });
    }
}
