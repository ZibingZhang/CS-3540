using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIndicator : MonoBehaviour
{
    public enum Element
    {
        Air,
        Earth,
        Fire,
        Water
    }
    public Image fireImage;
    public Image waterImage;
    public Image airImage;
    public Image earthImage;

    Element currentElement;
    Image[] elementImages;
    Image previousImage;
    Image highlightedImage;
    Color pastColor;
    Color currColor;

    // Start is called before the first frame update
    void Start()
    {
        currentElement = Element.Air;
        elementImages = new Image[4] { airImage, earthImage, fireImage, waterImage};
        //currentElement = ThrowProjectile.currentElement;
        previousImage = airImage;
        SetElementTransparency(fireImage);
        SetElementTransparency(waterImage);
        SetElementTransparency(earthImage);
        SetElementTransparency(airImage);

        SetInactiveElements();
    }
    public void SetInactiveElements()
    {
        int index = PlayerPrefs.GetInt("CurrentProgress");
        for (int i = 0; i < 4; i++)
        {
            if (i >= index)
            {
                Image tempImage = elementImages[i];
                tempImage.color = Color.clear;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentElement = (Element)ThrowProjectile.currentElement;
        switch (currentElement)
        {
            case Element.Water:
                highlightedImage = waterImage;
                break;
            case Element.Air:
                highlightedImage = airImage;
                break;
            case Element.Fire:
                highlightedImage = fireImage;
                break;
            case Element.Earth:
                highlightedImage = earthImage;
                break;
        }
        pastColor = previousImage.color;
        pastColor.a = .2f;
        previousImage.color = pastColor;

        currColor = highlightedImage.color;
        currColor.a = 1f;
        highlightedImage.color = currColor;

        previousImage = highlightedImage;

    }
    private void SetElementTransparency(Image image)
    {
        Color tempColor = image.color;
        tempColor.a = .2f;
        image.color = tempColor;
    }
}
