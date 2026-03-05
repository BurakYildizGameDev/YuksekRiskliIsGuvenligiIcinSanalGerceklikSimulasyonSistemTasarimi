using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Eðer TextMeshPro kullanýyorsan

public class MainMenu : MonoBehaviour
{
    public GameObject LevelPanel;
    public GameObject ComingSoonText; // UI Text objesi (örneðin TMP_Text veya GameObject)

    public void LevelpanelClose()
    {
        LevelPanel.SetActive(false);
    }

    public void LevelpanelOpen()
    {
        LevelPanel.SetActive(true);
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }

    public void Level2()
    {
        SceneManager.LoadScene(3);
    }

    public void Level3()
    {
        StartCoroutine(ShowComingSoon());
    }

    private IEnumerator ShowComingSoon()
    {
        ComingSoonText.SetActive(true);
        yield return new WaitForSeconds(2f);
        ComingSoonText.SetActive(false);
    }
}
