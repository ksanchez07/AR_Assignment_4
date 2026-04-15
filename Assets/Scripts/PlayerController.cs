using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private TMP_Text countText;
    private GameObject winTextObject;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        countText = FindTMPTextIncludingInactive("CountText");
        winTextObject = FindGameObjectIncludingInactive("WinText");

        SetCountText();

        if (winTextObject != null)
            winTextObject.SetActive(false);
    }

    private TMP_Text FindTMPTextIncludingInactive(string objectName)
    {
        TMP_Text[] texts = Resources.FindObjectsOfTypeAll<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            if (text.name == objectName && text.gameObject.scene.IsValid())
                return text;
        }

        Debug.LogWarning(objectName + " not found.");
        return null;
    }

    private GameObject FindGameObjectIncludingInactive(string objectName)
    {
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();

        foreach (Transform t in allTransforms)
        {
            if (t.name == objectName && t.gameObject.scene.IsValid())
                return t.gameObject;
        }

        Debug.LogWarning(objectName + " not found.");
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched: " + other.gameObject.name + " tag=" + other.gameObject.tag);

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        if (countText != null)
            countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            if (winTextObject != null)
            {
                winTextObject.SetActive(true);

                TMP_Text winText = winTextObject.GetComponent<TMP_Text>();
                if (winText != null)
                    winText.text = "You Win!";
            }

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null)
                Destroy(enemy);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            if (winTextObject != null)
            {
                winTextObject.SetActive(true);

                TMP_Text winText = winTextObject.GetComponent<TMP_Text>();
                if (winText != null)
                    winText.text = "Kareem, You Lose!";
            }
        }
    }
}