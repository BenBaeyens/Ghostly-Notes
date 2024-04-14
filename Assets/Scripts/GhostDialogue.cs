using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GhostDialogue : MonoBehaviour
{
    public Dictionary<GhostType, GhostComments> ghostComments = new Dictionary<GhostType, GhostComments>();

    public Sprite[] ghostSprites;
    public GameObject ghostImageObject; // Reference to the pre-defined UI Image object


    public GameObject currentGhost;

    public TMPro.TextMeshProUGUI ghostText;
    

    [System.Serializable]
    public class GhostComments
    {
        public string[] positiveComments;
        public string[] negativeComments;
    }

    public void ShowCommentAndSpawnGhost(bool isCorrect)
    {
        ghostImageObject.SetActive(true);
        
        // Randomly select a GhostType
        GhostType ghostType = (GhostType)Random.Range(0, ghostSprites.Length);

        // Set the sprite of the pre-defined ghost image
        Image ghostImage = ghostImageObject.GetComponent<Image>();
        ghostImage.sprite = ghostSprites[(int)ghostType];

        // Set the text of the spawned ghost
        if (ghostText != null)
        {
            GhostComments comments = ghostComments[ghostType];
            ghostText.text = GetRandomComment(isCorrect ? comments.positiveComments : comments.negativeComments);
        }
    }
    
    public void RemoveGhostAndText()
    {
        // Set the ghost image to inactive
        ghostImageObject.SetActive(false);

        // Clear the text
        if (ghostText != null)
        {
            ghostText.text = "";
        }
    }
    private string GetRandomComment(string[] comments)
    {
        int randomIndex = Random.Range(0, comments.Length);
        return comments[randomIndex];
    }

    private void Awake()
    {
        RemoveGhostAndText();
        // ghostComments.Add(GhostType.Disco, new GhostComments
        // {
        //     positiveComments = new string[]
        //     {
        //         "Groovy! That's the vibe we're after! Let's keep the beats pumping and the dance floor rocking!",
        //         "Oh, yeah! That's the funky stuff! Thanks for summoning me to the groove, buddy! With tunes like these, we're gonna light up the dance floor!"
        //     },
        //     negativeComments = new string[]
        //     {
        //         "Oh, come on now! That melody's more offbeat than a malfunctioning disco ball! Let's try to find our rhythm, okay?",
        //         "Whoa, buddy! My dance moves just can't groove to that tune! Try again, and let's boogie properly!"
        //     }
        // });

        // ghostComments.Add(GhostType.Opera, new GhostComments
        // {
        //     positiveComments = new string[]
        //     {
        //         "Magnificent! Your melody sings with the passion of a thousand arias! Together, we'll create music that moves souls.",
        //         "Ah, my dear summoner, your melody fills me with joy! Thank you for bringing me forth to share in this musical journey. Together, we shall weave melodies that echo through eternity!"
        //     },
        //     negativeComments = new string[]
        //     {
        //         "Oh, the agony! Your performance feels like a tragic aria sung off-key. Please, let's strive for something more harmonious, shall we?",
        //         "My dear player, your rendition has stirred not my soul, but my dismay. Pray, try a melody more melodious!"
        //     }
        // });

        ghostComments.Add(GhostType.Rockstar, new GhostComments
        {
            positiveComments = new string[]
            {
                "Whoa, that's what I'm talking about! Thanks for summoning me to rock out with you! With your talent and my spirit, we'll make musical history together!",
                "Yeah! That's the spirit! Your music is like a thundering storm of power and passion! Let's keep the rock and roll alive!"
            },
            negativeComments = new string[]
            {
                "Whoa, buddy! That tune's more out of tune than a broken guitar string! Let's dial it back and aim for some real rock 'n' roll magic, yeah?",
                "Dude, seriously? My ears are bleeding! How about we stick to the classics? I'm all about the air guitar, but that was just noise!"
            }
        });

        ghostComments.Add(GhostType.Chef, new GhostComments
        {
            positiveComments = new string[]
            {
                "Ooh la la! That melody is as delicious as a freshly baked croissant! With your musical skills and my culinary flair, we shall create a masterpiece!",
                "Magnifique! Your summoning has brought me great joy! Let's continue to cook up some delicious tunes together. Merci beaucoup for including me in your musical feast!"
            },
            negativeComments = new string[]
            {
                "Mon dieu! That melody's more burnt than a pizza left in the oven too long! Let's try a different recipe, shall we?",
                "Sacrebleu! Your musical concoction tastes as sour as an expired soufflé! Let's spice things up with some proper notes, shall we?"
            }
        });

        ghostComments.Add(GhostType.Nerd, new GhostComments
        {
            positiveComments = new string[]
            {
                "Excellent! Your melody is as precise as a well-crafted algorithm!",
                "Incredible! Your summoning has activated my circuits! Your skills are as impressive as a well-optimized code!"
            },
            negativeComments = new string[]
            {
                "Error! Error! Your melody is as buggy as a poorly written script! Let's debug this musical code, shall we?",
                "Hmm, interesting choice... if you're aiming for the worst performance in the history of music! Let's analyze and optimize, shall we?"
            }
        });

        ghostComments.Add(GhostType.Cowboy, new GhostComments
        {
            positiveComments = new string[]
            {
                "Yeehaw! Smooth as a ride on the open range! Let's keep the music flowing like a cool breeze.",
                "Well, howdy there, partner! Your music is as lively as a hoedown on a Saturday night! Let's keep the tunes coming and the good times rolling!"
            },
            negativeComments = new string[]
            {
                "Well, shucks! That tune's wilder than a bucking bronco at a rodeo! How about we rein it in and hit the right notes, partner?",
                "Well, slap my chaps and call me disappointed! That tune's as out of tune as a cowpoke at a square dance! How about we lasso some melody, partner?"
            }
        });

        ghostComments.Add(GhostType.Surfer, new GhostComments
        {
            positiveComments = new string[]
            {
                "Cowabunga! That melody's as rad as a perfect wave! Let's keep the good vibes rolling!",
                "Dude, that's totally tubular! Your music is as chill as a day at the beach!"
            },
            negativeComments = new string[]
            {
                "Whoa, bro! That melody wiped out harder than a gnarly wipeout on a killer wave! Let's catch a better rhythm, dude!",
                "Bummer, dude! That tune's as off-key as a surfer without a board! How about we catch a better wave, dude?"
            }
        });

        ghostComments.Add(GhostType.Superhero, new GhostComments
        {
            positiveComments = new string[]
            {
                "Great job! Your melody is as heroic as a caped crusader saving the day!",
                "Incredible! Your music is as powerful as a superhero's punch! Let's keep the heroic tunes coming!"
            },
            negativeComments = new string[]
            {
                "Fear not, citizen! Though your melody may be villainous, I, the Melodic Avenger, shall assist you in your quest for harmony! Onward, to a better tune!",
                "Hold up, citizen! That melody's more villainous than a supervillain's evil laugh! Let's channel our inner hero and find the harmony, shall we?"
            }
        });
    }
}





