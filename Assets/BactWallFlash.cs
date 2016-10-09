using UnityEngine;
using System.Collections;

public class BactWallFlash : MonoBehaviour {

    public Material flashMaterial;
    private float flashStart = 0;
    private float flashDuration = 0;
    public Color flashColor;

    void Start()
    {
        FlashDisable();
      
    }

    void Update()
    {
        if (Time.time - flashStart >= flashDuration)
        {
            FlashDisable();
        }
    }
    public void Flash(float duration)
    {
        flashDuration = duration;
        flashStart = Time.time;
        flashMaterial.SetColor("_FlashColor", flashColor);

    }
    public void FlashDisable()
    {
       
        flashMaterial.SetColor("_FlashColor",Color.black);

    }
    
}
