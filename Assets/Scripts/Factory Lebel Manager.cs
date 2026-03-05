using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FactoryLebelManager : MonoBehaviour
{
    [Header("Tutorial Noktalarý (Transform olarak)")]
    public Transform[] tutorialPoints;

    [Header("Oklar (her noktayla ayný sýra)")]
    public GameObject[] tutorialArrows;

    [Header("Yönlendirme Yazýsý")]
    public TextMeshProUGUI instructionText;

    [Header("Bitiþ Paneli")]
    public GameObject finishPanel;

    [Header("Baþlangýç Butonlarý")]
    public Button alarmButton;
    public Button suitButton;

    [Header("Ana Menü Butonu")]
    public GameObject mainbutton;

    [Header("Oyuncu Nesnesi")]
    public Transform player; // Dýþarýdan atanabilir hale getirildi

    private int currentPointIndex = 0;

    private bool allPointsReached = false;
    private bool isAlarmActivated = false;
    private bool isSuitWorn = false;
    private bool tutorialStarted = false;

    void Start()
    {
        // Eðer elle atanmadýysa, Camera.main fallback olarak kullan
        if (player == null)
            player = Camera.main.transform;

        finishPanel.SetActive(false);

        // Tüm oklarý kapat
        foreach (var arrow in tutorialArrows)
            arrow.SetActive(false);

        instructionText.text = "Lütfen önce alarmý baþlat ve suit giy.";

        // Butonlara týklama iþlevlerini baðla
        alarmButton.onClick.AddListener(ActivateAlarm);
        suitButton.onClick.AddListener(WearSuit);
    }

    void Update()
    {
        if (tutorialStarted && !allPointsReached && currentPointIndex < tutorialPoints.Length)
        {
            float distance = Vector3.Distance(player.position, tutorialPoints[currentPointIndex].position);
            if (distance < 1.5f)
            {
                // Þu anki oku kapat
                if (currentPointIndex < tutorialArrows.Length)
                    tutorialArrows[currentPointIndex].SetActive(false);

                currentPointIndex++;

                // Sonraki oku aç veya bitir
                if (currentPointIndex < tutorialArrows.Length)
                {
                    tutorialArrows[currentPointIndex].SetActive(true);
                    instructionText.text = $"Go to Area {currentPointIndex + 1}";
                }
                else
                {
                    allPointsReached = true;
                    instructionText.text = "Görev Tamamlandý!";
                    finishPanel.SetActive(true);
                }
            }
        }
    }

    void ActivateAlarm()
    {
        isAlarmActivated = true;
        CheckTutorialStart();
    }

    void WearSuit()
    {
        isSuitWorn = true;
        CheckTutorialStart();
    }

    void CheckTutorialStart()
    {
        if (isAlarmActivated && isSuitWorn && !tutorialStarted)
        {
            tutorialStarted = true;

            // Ýlk yönlendirme
            instructionText.text = $"Go to Area 1";

            // Ýlk oku göster
            if (tutorialArrows.Length > 0)
                tutorialArrows[0].SetActive(true);
        }
    }

    public void RelodScence()
    {
        SceneManager.LoadScene(1);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
