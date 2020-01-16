using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChanceViewer : MonoBehaviour
{
    public static ChanceViewer instance;
    void Awake()
    {
        instance = this;
        window = transform.GetChild(0).gameObject;
    }

    [SerializeField] private Image image;
    private GameObject window;
    private bool doesPlayerPressButton = false;

    public IEnumerator display(Sprite chanceImage)
    {
        image.gameObject.GetComponent<Image>().sprite = chanceImage;      
        
        window.SetActive(true);

        while (!doesPlayerPressButton)
        {
            yield return null;
        }

        window.SetActive(false);
        doesPlayerPressButton = false;
    }

    public void onUserPressOKButton()
    {
        doesPlayerPressButton = true;
    }
}
