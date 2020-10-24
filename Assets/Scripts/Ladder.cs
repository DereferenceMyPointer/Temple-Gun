using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (Collider2D))]
public class Ladder : MonoBehaviour
{
    public string nextLevel;
    public float moveTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadNextLevel(moveTime));
        }
    }

    public IEnumerator LoadNextLevel(float t)
    {
        PController.Instance.SetDisabled();
        UIManager.Instance.FadeToBlack(moveTime);
        yield return new WaitForSeconds(moveTime);
        SceneManager.LoadScene(nextLevel);
    }

}
