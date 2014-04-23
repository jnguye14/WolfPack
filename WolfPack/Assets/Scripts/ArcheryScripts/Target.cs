using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    void SetColor(Color c)
    {
        this.renderer.material.color = c;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Arrow") // hit target
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 100); // earn money
            Destroy(col.gameObject);
            Invoke("ReturnToMap", 0.5f); // return to the world map in a second
        }
    }

    void ReturnToMap()
    {
        Destroy(this.gameObject);
        Application.LoadLevel("World Map");
    }
}
