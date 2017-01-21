using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CurrenciesManager : MonoBehaviour
{
    public enum e_Currencies
    {
        Gold,
        Life
    }

    public GameObject goUITextGold;
    private Text _goldText;

    public GameObject goUITextLife;
    private Text _lifeText;

    [HideInInspector]
    public Dictionary<e_Currencies, CurrencyHelper> currencies;

    public int initialGold;
    public int life;

    void Awake()
    {
        currencies = new Dictionary<e_Currencies, CurrencyHelper>();
        currencies[e_Currencies.Gold] = new CurrencyHelper();
        currencies[e_Currencies.Life] = new CurrencyHelper();
    }

    void Start()
    {
        _goldText = goUITextGold.GetComponent<Text>();
        _lifeText = goUITextLife.GetComponent<Text>();
        currencies[CurrenciesManager.e_Currencies.Gold].OnCurrencyAmountModification += UpdateUIGold;
        currencies[CurrenciesManager.e_Currencies.Life].OnCurrencyAmountModification += UpdateUILife;
        currencies[e_Currencies.Gold].AddCurrency(initialGold);
        currencies[e_Currencies.Life].AddCurrency(life);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currencies[e_Currencies.Gold].AddCurrency(1000);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            currencies[e_Currencies.Life].AddCurrency(1);
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            currencies[e_Currencies.Life].UseCurrency(1);
            GameObject.Find("JoystickController").GetComponent<JoystickManager>().LaunchVib(0.2f);
        }
    }

    void UpdateUIGold(int before, int after)
    {
        _goldText.text = after.ToString();
    }

    void UpdateUILife(int before, int after)
    {
        _lifeText.text = after.ToString();
    }

    public class CurrencyHelper
    {
        public delegate void CurrencyAmountModification(int before, int after);
        public event CurrencyAmountModification OnCurrencyAmountModification;

        private int _amount;
        public int Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                int before = _amount;
                _amount = value;
                if (OnCurrencyAmountModification != null)
                    OnCurrencyAmountModification(before, value);
            }
        }

        public bool HasEnoughCurrency(int amount)
        {
            return Amount >= amount;
        }

        public bool UseCurrency(int amount)
        {
            if (!HasEnoughCurrency(amount))
                return false;

            Amount -= amount;
            return true;
        }

        public void AddCurrency(int amount)
        {
            Amount += amount;
        }
    }

}
