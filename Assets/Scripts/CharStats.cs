using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public short playerLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 1001;
    public short baseEXP = 50;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int strength;
    public int defence;
    public int weaponPwr;
    public int armorPwr;

    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;


    // Start is called before the first frame update
    void Start()
    {
        SetEXPForNextLevel();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Keypad1))
        {
            AddExp(100);
        }
    }


    private void SetEXPForNextLevel()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;
        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            if (i < 11)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.2f);
            }
            else if (i > 10 && i < 31)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.1f);
            }
            else if (i > 30 && i < 101)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
            }
            else if (i > 100 && i < 1001)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.01f);
            }
        }
    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;
        if (playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;

                //determine if we add to str or def based on odds or evens
                if (playerLevel % 2 == 0) //if even
                {
                    if (playerLevel < 11)
                    {
                        strength++;
                    }
                    else
                    {
                        strength = strength + Mathf.FloorToInt((playerLevel / 10) * 1.2f);
                    }

                }
                else //if odd
                {
                    if (playerLevel < 11)
                    {
                        defence++;
                    }
                    else
                    {
                        defence = defence + Mathf.FloorToInt((playerLevel / 10) * 1.2f);
                    }

                }

                maxHP = Mathf.FloorToInt((maxHP * 1.005f) + 100);
                currentHP = maxHP;
                maxMP = Mathf.FloorToInt((maxMP * 1.005f) + 70);
                currentMP = maxMP;
            }
        }

        if(playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
