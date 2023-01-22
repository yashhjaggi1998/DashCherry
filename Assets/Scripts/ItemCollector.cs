using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int cherriesCount = 0;
    [SerializeField] private TMP_Text cherriesCountText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherriesCount++;
            cherriesCountText.text = "Cherries: " + cherriesCount;
        }
    }
}
