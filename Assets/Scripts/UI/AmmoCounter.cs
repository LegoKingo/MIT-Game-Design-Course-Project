using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMPro.TextMeshProUGUI counterText;
    public float ammoMax = 25;
    public float counter;

    void Start()
    {
        InitializeAmmo();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void AmmoCount()
    {
        counter--;
        counterText.text = "Ammo: " + counter.ToString();
    }
    public void InitializeAmmo()
    {
        counter = ammoMax;
        counterText.text = "Ammo: " + counter.ToString();
    }
    public void OutOfAmmo()
    {
        counterText.text = "Reload!";
    }
}
