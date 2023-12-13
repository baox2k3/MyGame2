using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{
    public static UI ínstance;
    
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private TextMeshProUGUI ammoText;

    private int scoreValue;
    [Header("Reload")] [SerializeField] private BoxCollider2D reloadwindow;
   
    [SerializeField] private GameObject tryAgainButton;

    [SerializeField]private GunController gunController;
    [SerializeField]private int readSteps;

    [SerializeField]private UI_Reloadbutton[] reloadbutton;
    
    // Start is called before the first frame update

    private void Awake()
    {
        ínstance = this;
    }

    void Start()
    {
        reloadbutton = GetComponentInChildren<UI_Reloadbutton[]>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 1)
            timeText.text = Time.time.ToString("#,#");
        if(Input.GetKeyDown(KeyCode.R))
            OpenReloadUI();

    }

    public void OpenReloadUI()
    {
        foreach (UI_Reloadbutton button in reloadbutton)
        {
            button.gameObject.SetActive(true);

            float randomX = Random.Range(reloadwindow.bounds.min.x, reloadwindow.bounds.max.x);
            float randomY = Random.Range(reloadwindow.bounds.min.y, reloadwindow.bounds.max.y);
            button.transform.position = new Vector2(randomX, randomY);
        }

        Time.timeScale = .6f;
        readSteps = reloadbutton.Length;
    }

    public void AttempToReload()
    {
        if (readSteps > 0)
            readSteps--;
            readSteps--;
        if (readSteps <= 0)
        {
            gunController.ReloadGun();
        } 
    }

    public void AddScore()
    {
        scoreValue++;
        scoreText.text = scoreValue.ToString("#,#");
    }

    public void UpdateAmmoInfo(int currentBullets, int maxBullets)
    {
        ammoText.text = currentBullets + "/" + maxBullets;
    }

    public void OpenEndScreen()
    {
        Time.timeScale = 0;
        tryAgainButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
