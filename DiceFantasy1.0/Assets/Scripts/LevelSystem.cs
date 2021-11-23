using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXp;
    public float requiredXp;

    private float lerpTimer;
    private float delayTimer;

    [SerializeField]
    TactictsMove tacticsNPC;

    [Header("UI")]
    public Image xpAmmount;// barra cheia 
    public Image borderXp;// borda da barra vazia
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI xpTxt;

    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;

    [Range(2f, 4f)]
    public float powerMultiplier = 2;

    [Range(7f, 14f)]
    public float divisionMultiplier = 2;


    // Start is called before the first frame update
    void Start()
    {
        xpAmmount.fillAmount = currentXp / requiredXp;
        borderXp.fillAmount = currentXp / requiredXp;
        requiredXp = CalculateRequiredXp();
        levelTxt.text = "Level" + level;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
        //if (Input.GetKeyDown(KeyCode.Equals))// funciona so que ganha xp infinitamente arrumar
        //    GainExperienceFlatRate(20);
        if (currentXp > requiredXp)
            LevelUp();
    }

    public void KilledEnemy()
    {
        //     UpdateXpUI();
        if (tacticsNPC.npcDead == true)// funciona so que ganha xp infinitamente arrumar
            GainExperienceFlatRate(40);
        //   if (currentXp > requiredXp)
        //       LevelUp();
    }


    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = xpAmmount.fillAmount;
        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            borderXp.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                xpAmmount.fillAmount = Mathf.Lerp(FXP, borderXp.fillAmount, percentComplete);
            }
        }
        xpTxt.text = currentXp + "/" + requiredXp;

    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
        lerpTimer = 0f;
    }

    public void GainedExperienceScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXp += xpGained * multiplier;
        }
        else
        {
            currentXp += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        xpAmmount.fillAmount = 0f;
        borderXp.fillAmount = 0f;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        //GetComponent<HealthBar>().IncreaseHealth(level);
        requiredXp = CalculateRequiredXp();
        levelTxt.text = "Level" + level;
    }

    private int CalculateRequiredXp()
    {
        int solvedForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solvedForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solvedForRequiredXp / 4;
    }
}

