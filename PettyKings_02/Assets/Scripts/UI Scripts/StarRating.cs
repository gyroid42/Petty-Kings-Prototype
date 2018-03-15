using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour {

    private WorldManager worldController;

    public int noStarRequirement;
    public int halfStarRequirement;
    public int oneStarRequirement;
    public int oneHalfStarRequirement;
    public int twoStarRequirement;
    public int twoHalfStarRequirement;
    public int threeStarRequirement;
    public int threeHalfStarRequirement;
    public int fourStarRequirement;
    public int fourHalfStarRequirement;
    public int fiveStarRequirement;
   
    private Image image;

    public Sprite noStarImage;
    public Sprite halfStarImage;
    public Sprite oneStarImage;
    public Sprite oneHalfStarImage;
    public Sprite twoStarImage;
    public Sprite twoHalfStarImage;
    public Sprite threeStarImage;
    public Sprite threeHalfStarImage;
    public Sprite fourStarImage;
    public Sprite fourHalfStarImage;
    public Sprite fiveStarImage;
  


    void Awake () {

        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script

        image = this.GetComponent<Image>();

        image.sprite = noStarImage;
        
    }


    public void UpdateStars(int starRating)
    {

        if (starRating <= noStarRequirement)
        {
            image.sprite = noStarImage;  
        }
        else if(starRating >= fiveStarRequirement)
        {
            image.sprite = fiveStarImage;
        }
        else if (starRating >= fourHalfStarRequirement)
        {
            image.sprite = fourHalfStarImage;
        }
        else if(starRating >= fourStarRequirement)
        {
            image.sprite = fourStarImage;
        }
        else if (starRating >= threeHalfStarRequirement)
        {
            image.sprite = threeHalfStarImage;
        }
        else if (starRating >= threeStarRequirement)
        {
            image.sprite = threeStarImage;
        }
        else if (starRating >= twoHalfStarRequirement)
        {
            image.sprite = twoHalfStarImage;
        }
        else if (starRating >= twoStarRequirement)
        {
            image.sprite = twoStarImage;
        }
        else if (starRating >= oneHalfStarRequirement)
        {
            image.sprite = oneHalfStarImage;
        }
        else if (starRating >= oneStarRequirement)
        {
            image.sprite = oneStarImage;
        }
        else if (starRating >= halfStarRequirement)
        {
            image.sprite = halfStarImage;
        }


        //add end condition?

    }
}
