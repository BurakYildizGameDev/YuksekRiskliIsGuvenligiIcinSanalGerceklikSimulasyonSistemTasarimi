using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firelevelUı : MonoBehaviour
{
    public void Mainlevel()
    {
        SceneManager.LoadScene(0);
    }
    public void RestatButton()
    {
        SceneManager.LoadScene(3);
    }
}
