using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MakeMoney : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private Button upgradeDamage;
    [SerializeField] private TextMeshProUGUI priceDamageText;
    [SerializeField] private TextMeshProUGUI levelDamageText;
    [Header("Speed")]
    [SerializeField] private Button upgradeSpeed;
    [SerializeField] private TextMeshProUGUI priceSpeedText;
    [SerializeField] private TextMeshProUGUI levelSpeedText;
    [Header("Money")]
    [SerializeField] private TextMeshProUGUI moneyText;
    private int basePrice = 100;


    private bool isBonusActive = false;
    private void upgradeShootingSpeed(bool isReward)
    {
        if (isReward)
        {
            YandexGame.savesData.shootingSpeed++;

            int randomisedInt = Random.Range(0, 100);
            upgradeSpeed.onClick.RemoveAllListeners();
            if (randomisedInt <= 10)
            {
                isBonusActive = true;

                upgradeSpeed.onClick.AddListener(() => YandexGame.RewVideoShow(1));
                DisableObjects(levelSpeedText, priceSpeedText);
                ChangeButtonColor(upgradeSpeed, Color.magenta);
            }
            else{
                upgradeSpeed.onClick.AddListener(() => upgradeShootingSpeed(false));
                ChangeButtonColor(upgradeSpeed, Color.white);
                levelSpeedText.gameObject.SetActive(true);
                priceSpeedText.gameObject.SetActive(true);
            }

        }
        else if (YandexGame.savesData.money >= (ulong)(basePrice * YandexGame.savesData.shootingSpeed))
        {
            YandexGame.savesData.money -= (ulong)(basePrice * (int)Mathf.Pow(2, YandexGame.savesData.ShootingDamage - 1));

            YandexGame.savesData.shootingSpeed++;
            ChangeButtonColor(upgradeSpeed,Color.white);
        }
        SetTextSpeed();
        SetMoneyText();
        YandexGame.SaveProgress();
    }

    private void SetMoneyText() => moneyText.text = $"Денег: {YandexGame.savesData.money}";

    private void upgradeShootingDamage(bool isReward)
    {
        if (isReward)
        {
            YandexGame.savesData.ShootingDamage++;

            int randomisedInt = Random.Range(0, 100);
            upgradeDamage.onClick.RemoveAllListeners();
            if (randomisedInt <= 10)
            {
                isBonusActive = true;

                upgradeDamage.onClick.AddListener(() => YandexGame.RewVideoShow(1));
                DisableObjects(levelDamageText, priceDamageText);
                ChangeButtonColor(upgradeDamage, Color.magenta);
            }
            else
            {
                upgradeSpeed.onClick.AddListener(() => upgradeShootingSpeed(false));
                ChangeButtonColor(upgradeDamage, Color.white);
                levelDamageText.gameObject.SetActive(true);
                priceDamageText.gameObject.SetActive(true);
            }
        }
        else if (YandexGame.savesData.money >= (ulong)(basePrice * YandexGame.savesData.ShootingDamage))
        {
            YandexGame.savesData.money -= (ulong)(basePrice * (int)Mathf.Pow(2, YandexGame.savesData.ShootingDamage-1));
            YandexGame.savesData.ShootingDamage++;
            ChangeButtonColor(upgradeDamage, Color.white);
        }
        SetTextDamage();
        SetMoneyText();
        YandexGame.SaveProgress();
    }

    private void SetTextDamage()
    {
        priceDamageText.text = $"Цена: {(basePrice * (int)Mathf.Pow(2, YandexGame.savesData.ShootingDamage - 1))}";
        levelDamageText.text = $"Уровень: {YandexGame.savesData.ShootingDamage }";
    }

    private void SetTextSpeed()
    {
        priceSpeedText.text = $"Цена: {basePrice * (int)Mathf.Pow(2, YandexGame.savesData.shootingSpeed - 1)}";
        levelSpeedText.text = $"Уровень: {YandexGame.savesData.shootingSpeed}";
    }


    private void RewardAdd(int id)
    {
        if(id == 0)
        {
            upgradeShootingDamage(true);
        }
        else if(id == 1)
        {
            upgradeShootingSpeed(true);
        }
    }

    private void DisableObjects(TextMeshProUGUI text1, TextMeshProUGUI text2)
    {
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
    }

    private void ChangeButtonColor(Button button, Color color) => button.transform.GetComponent<Image>().color = color;

    private void OnEnable()
    {
        int randomisedInt = Random.Range(0, 100);
        Debug.Log(randomisedInt);
        if (randomisedInt <= 5)
        {
            isBonusActive = true;
            YandexGame.CloseVideoEvent += RewardAdd;

            upgradeDamage.onClick.AddListener(() => YandexGame.RewVideoShow(0));
            upgradeSpeed.onClick.AddListener(() => upgradeShootingSpeed(false));
            DisableObjects(levelDamageText, priceDamageText);
            ChangeButtonColor(upgradeDamage, Color.magenta);
        }
        else if (randomisedInt <= 10)
        {
            isBonusActive = true;
            YandexGame.CloseVideoEvent += RewardAdd;

            upgradeSpeed.onClick.AddListener(() => YandexGame.RewVideoShow(1));
            upgradeDamage.onClick.AddListener(() => upgradeShootingDamage(false));
            DisableObjects(levelSpeedText, priceSpeedText);
            ChangeButtonColor(upgradeSpeed, Color.magenta);
        }
        else
        {
            upgradeDamage.onClick.AddListener(() => upgradeShootingDamage(false));
            upgradeSpeed.onClick.AddListener(() => upgradeShootingSpeed(false));
        }

        if (YandexGame.savesData.shootingSpeed >= 32)
        {
            upgradeSpeed.onClick.RemoveAllListeners();
            DisableObjects(levelSpeedText, priceSpeedText);
            ChangeButtonColor(upgradeSpeed, Color.green);
        }
        if (YandexGame.savesData.ShootingDamage >= 32)
        {
            upgradeDamage.onClick.RemoveAllListeners();
            DisableObjects(levelDamageText, priceDamageText);
            ChangeButtonColor(upgradeDamage, Color.green);
        }

        SetMoneyText();
        SetTextSpeed();
        SetTextDamage();
    }

    private void OnDisable()
    {
        if(isBonusActive)
            YandexGame.CloseVideoEvent -= RewardAdd;

        upgradeDamage.onClick.RemoveAllListeners();
        upgradeSpeed.onClick.RemoveAllListeners();
    }
}
