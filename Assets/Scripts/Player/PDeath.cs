using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PDeath : Death
{
    public float fadeOutTime;

    public AudioSource deathSound;

    public override void Kill()
    {
        GetComponent<PController>().animator.SetTrigger("Death");
        GetComponent<PController>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        deathSound.Play();
        StartCoroutine(Death());
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public IEnumerator Death()
    {
        UIManager.Instance.FadeToBlack(fadeOutTime);
        yield return new WaitForSeconds(fadeOutTime);
        ReturnToMenu();
    }

}
