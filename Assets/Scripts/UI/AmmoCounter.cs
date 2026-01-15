using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMPro.TextMeshProUGUI counterText;
    public float counter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "Ammo: " + counter.ToString(); 
    }

    public void AmmoCount()
    {
        counter++;
    }
}
