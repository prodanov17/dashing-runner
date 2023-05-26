using UnityEngine;
using TMPro;

public class CrystalCollection : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int crystalCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crystals"))
        {
            AudioManager.instance.Play("pickup");
            crystalCount += 1;
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        text.text = crystalCount.ToString();
    }
}
