using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;
    public Image fadeScreen;
    private bool shouldFadeToBlack;
    private bool shouldFadeFromBlack;
    public float fadeSpeed;
    private float fps;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        fps = 1f / Time.deltaTime;
        fadeSpeed = ( fps / (fps - 1) );

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        /**
         * Setting fadeScreen color. It should be black by tradition for simple 'loading' screens.
         * By contrast white is associated with bright lights or explosions,
         *  red is associated with the player being hurt, etc... and it's not desirable for a loading screen.
         *  
         * The default RGB values for our image(fadeScreen) set to black in the editor, fadeScreen RGB values are used
         *  instead of numeral values.
         * 
         * 
         * Using Mathf.MoveTowards to set the transition speed between alpha 0-1 (or 0-255 in editor)
         * Using default alpha value(transparency) because when 'shouldFadeToBlack = true' that value should be 0.
         * Target alpha value should be 1f for max opacity. 1f translates to 255(max) in editor.
         * For speed a variable is created - fadeSpeed - which is multiplied by Time.deltaTime.
         * 
         * Time.deltaTime is the amount of time it took from the last Update to the next Update.
         * This is done because computing power of different machines varies. (i.e PC1 runs the game at 47fps, PC2 runs at 123fps, etc...)
         * It is desirable that the experience is the same no matter on what machine the game is played on so the transition time 
         *  should be the same(or very close).
         *  
         * Lastly when the fading is complete our condition (shouldFadeToBlack/shouldFadeFromBlack) shouldn't be true anymore.
         */
        if(shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r,
                                         fadeScreen.color.g, 
                                         fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 
                                                           1f,
                                                           fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if(shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r,
                                         fadeScreen.color.g,
                                         fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a,
                                                           0f, // For fading from max opacity to full transparency.
                                                           fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack() {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack() {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}
