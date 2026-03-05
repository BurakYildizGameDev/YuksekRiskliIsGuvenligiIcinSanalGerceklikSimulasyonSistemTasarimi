using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator; // Animator bileþenini buraya baðlayýn
    public Image image1 , image2;
    public Sprite sprite1, sprite2;
    public bool isexplosion;
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Animator atanmadýysa otomatik olarak bul
        }
        image1.sprite = sprite1;
        image2.sprite = sprite2;
        isexplosion = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            // Animasyonu baþlatan Coroutine'i çaðýr
            StartCoroutine(PlayAnimationForSeconds(3f)); // 3 saniye boyunca oynat
            image1.sprite = sprite2;
            image2.sprite = sprite1;
            isexplosion=true;
        }

        
    }

    IEnumerator PlayAnimationForSeconds(float duration)
    {
        // Animasyonu baþlat
        animator.SetBool("IsExploding", true); // "IsExploding" parametresini Animator Controller'a ekleyin

        // Belirtilen süre kadar bekle
        yield return new WaitForSeconds(duration);

        // Animasyonu durdur
        animator.SetBool("IsExploding", false);
    }
}
