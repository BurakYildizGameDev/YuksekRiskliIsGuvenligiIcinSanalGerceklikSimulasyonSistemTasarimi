using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Noktalarý (Transform olarak)")]
    public Transform[] tutorialPoints;

    [Header("Oklar (her noktayla ayný sýra)")]
    public GameObject[] tutorialArrows;

    [Header("UI Görev Öðeleri")]
    public GameObject uiCanvas;
    public Button uiButton;
    public Toggle uiToggle;
    public Slider uiSlider;

    [Header("Yönlendirme Yazýsý")]
    public TextMeshProUGUI instructionText;

    [Header("Bitiþ Paneli")]
    public GameObject finishPanel;

    [Header("Oyuncu Nesnesi")]
    public Transform player; // Dýþarýdan atanabilir!

    private int currentPointIndex = 0;

    private bool buttonUsed = false;
    private bool toggleUsed = false;
    private bool sliderUsed = false;

    private bool allPointsReached = false;
    public GameObject mainbutton;

    void Start()
    {
        // Eðer elle atanmamýþsa Camera.main kullan
        if (player == null)
            player = Camera.main.transform;

        // UI görevleri dinleyicileri
        uiButton.onClick.AddListener(OnUIButtonClicked);
        uiToggle.onValueChanged.AddListener(OnUIToggleChanged);
        uiSlider.onValueChanged.AddListener(OnUISliderChanged);

        uiCanvas.SetActive(false);
        finishPanel.SetActive(false);

        // Tüm oklarý kapat, sadece ilk oku aç
        for (int i = 0; i < tutorialArrows.Length; i++)
            tutorialArrows[i].SetActive(i == 0);

        // Ýlk yönlendirme yazýsý
        instructionText.text = $"Go to Area 1";
    }

    void Update()
    {
        if (!allPointsReached && currentPointIndex < tutorialPoints.Length)
        {
            float distance = Vector3.Distance(player.position, tutorialPoints[currentPointIndex].position);
            // Debug için:
            Debug.Log($"Distance to point {currentPointIndex}: {distance}");

            if (distance < 1.5f)
            {
                if (currentPointIndex < tutorialArrows.Length)
                    tutorialArrows[currentPointIndex].SetActive(false);

                currentPointIndex++;

                if (currentPointIndex < tutorialArrows.Length)
                {
                    tutorialArrows[currentPointIndex].SetActive(true);
                    instructionText.text = $"Go to Area {currentPointIndex + 1}";
                }
                else
                {
                    allPointsReached = true;
                    uiCanvas.SetActive(true);
                    instructionText.text = "Use the Button";
                }
            }
        }

        if (allPointsReached)
        {
            if (!buttonUsed)
            {
                instructionText.text = "Use the Button";
            }
            else if (!toggleUsed)
            {
                instructionText.text = "Use the Toggle";
            }
            else if (!sliderUsed)
            {
                instructionText.text = "Use the Slider";
            }

            if (buttonUsed && toggleUsed && sliderUsed)
            {
                if (!finishPanel.activeSelf)
                {
                    instructionText.text = "Tutorial Complete!";
                    finishPanel.SetActive(true);
                    mainbutton.SetActive(false);
                }
            }
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

    void OnUIButtonClicked() => buttonUsed = true;
    void OnUIToggleChanged(bool value) => toggleUsed = true;
    void OnUISliderChanged(float value) => sliderUsed = true;
}
