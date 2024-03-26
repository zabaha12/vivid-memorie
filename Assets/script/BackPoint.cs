using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPoint : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneController.instance.PreviousLevel();
        }
    }
}