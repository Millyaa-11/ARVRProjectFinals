using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    public Button bombButton;
    public CharacterControl characterControl; // Reference to CharacterControl script


    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BombTag"))
        {
            StartCoroutine(Respawn(other.gameObject));
        }
    }

    IEnumerator Respawn(GameObject bomb)
    {
        bomb.SetActive(false);

        yield return new WaitForSeconds(5f);

        // Respawn the bomb at the location of the character
        bomb.transform.position = characterControl.transform.position;

        if (characterControl.bombButtonPressed)
        {
            bomb.SetActive(true);
        }
    }
}
