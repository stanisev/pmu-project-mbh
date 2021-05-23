using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject ShopCanvas;
    public GameObject[] ShopCategories;

    public GameObject EnergyCanvas;
    public GameObject SpinCanvas;

    public TMP_Text spinsAmount;
    public TMP_Text energyAmount;

    public void Update()
    {
        spinsAmount.text = DataUpdater.instance.GetSpins().ToString();
        energyAmount.text = DataUpdater.instance.GetEnergy().ToString();
    }

    public void OpenShopCanvas()
    {
        AudioPicker.instance.buttonClicked.Play();
        ShopCanvas.SetActive(true);
    }

    public void CloseShopCanvas()
    {
        AudioPicker.instance.buttonClicked.Play();
        ShopCanvas.SetActive(false);
    }

    private void CloseAllShopCategories()
    {
        foreach(GameObject category in ShopCategories)
        {
            category.SetActive(false);
        }
    }

    public void OpenBuyEnergyCategory()
    {
        AudioPicker.instance.buttonClicked.Play();
        OpenShopCanvas();
        CloseAllShopCategories();
        EnergyCanvas.SetActive(true);
    }

    public void OpenBuySpinsCategory()
    {
        AudioPicker.instance.buttonClicked.Play();
        OpenShopCanvas();
        CloseAllShopCategories();
        SpinCanvas.SetActive(true);
    }

    public void Buy5Spins()
    {
       if(!(DataUpdater.instance.GetCoins() <= 500))
        {
            AudioPicker.instance.buttonClicked.Play();
            DataUpdater.instance.SetCoins(-500);
            DataUpdater.instance.SetSpins(5);
        }
    }

    public void Buy10Spins()
    {
        if (!(DataUpdater.instance.GetCoins() <= 800))
        {
            AudioPicker.instance.buttonClicked.Play();
            DataUpdater.instance.SetCoins(-800);
            DataUpdater.instance.SetSpins(10);
        }
    }

    public void Buy5Energy()
    {
        if (!(DataUpdater.instance.GetCoins() <= 1000))
        {
            AudioPicker.instance.buttonClicked.Play();
            DataUpdater.instance.SetCoins(-1000);
            DataUpdater.instance.SetEnergy(5);
        }
    }

    public void Buy10Energy()
    {
        if (!(DataUpdater.instance.GetCoins() <= 1500))
        {
            AudioPicker.instance.buttonClicked.Play();
            DataUpdater.instance.SetCoins(-1500);
            DataUpdater.instance.SetEnergy(10);
        }
    }
}
