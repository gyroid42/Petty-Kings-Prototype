using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour {

    private WorldManager worldController;

    public int oneStarRequirement;
    public int twoStarRequirement;
    public int threeStarRequirement;
    public int fourStarRequirement;
    public int fiveStarRequirement;

    private Image image;

    public Sprite oneStarImage;
    public Sprite twoStarImage;
    public Sprite threeStarImage;
    public Sprite fourStarImage;
    public Sprite fiveStarImage;


    void Awake () {

        worldController = Terrain.activeTerrain.GetComponent<WorldManager>(); //get script

        image = this.GetComponent<Image>();

        image.sprite = oneStarImage;
        
    }


    public void UpdateStars(int starRating)
    {

        if(starRating >= fiveStarRequirement)
        {
            image.sprite = fiveStarImage;
        }
        else if(starRating >= fourStarRequirement)
        {
            image.sprite = fourStarImage;
        }
        else if (starRating >= threeStarRequirement)
        {
            image.sprite = threeStarImage;
        }
        else if (starRating >= twoStarRequirement)
        {
            image.sprite = twoStarImage;
        }
        else if (starRating >= oneStarRequirement)
        {
            image.sprite = oneStarImage;
        }
        //add end condition?

    }
}
