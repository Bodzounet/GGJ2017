using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class CurrenciesManager : MonoBehaviour
{
    public enum e_Currencies
    {
        Gold,
        Life,
        Income
    }

    public GameObject goUITextGold;
    private Text _goldText;

    public GameObject goUITextLife;
    private Text _lifeText;

    public GameObject goUITextIncome;
    private Text _incomeText;

    public Text timeIncomeText;

    public JoystickManager jm;

    [HideInInspector]
    public Dictionary<e_Currencies, CurrencyHelper> currencies;

    public int initialGold;
    public int initialIncome;
    public int life;
    public float incomeTime;
    private int timeCount;

    void Awake()
    {
        currencies = new Dictionary<e_Currencies, CurrencyHelper>();
        currencies[e_Currencies.Gold] = new CurrencyHelper();
        currencies[e_Currencies.Life] = new CurrencyHelper();
        currencies[e_Currencies.Income] = new CurrencyHelper();
    }

    void Start()
    {
        _goldText = goUITextGold.GetComponent<Text>();
        _lifeText = goUITextLife.GetComponent<Text>();
        _incomeText = goUITextIncome.GetComponent<Text>();

        currencies[CurrenciesManager.e_Currencies.Gold].OnCurrencyAmountModification += UpdateUIGold;
        currencies[CurrenciesManager.e_Currencies.Life].OnCurrencyAmountModification += UpdateUILife;
        currencies[CurrenciesManager.e_Currencies.Income].OnCurrencyAmountModification += UpdateUIIncome;

        currencies[e_Currencies.Gold].AddCurrency(initialGold);
        currencies[e_Currencies.Life].AddCurrency(life);
        currencies[e_Currencies.Income].AddCurrency(initialIncome);

        timeCount = Mathf.FloorToInt(incomeTime);
        StartCoroutine("AddIncomeToGold", incomeTime);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currencies[e_Currencies.Gold].AddCurrency(1000);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            currencies[e_Currencies.Life].AddCurrency(1);
        if (Input.GetKeyDown(KeyCode.Backspace))
            currencies[e_Currencies.Life].UseCurrency(1);
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            currencies[e_Currencies.Income].AddCurrency(1);


    }

    void UpdateUIGold(int before, int after)
    {
        _goldText.text = after.ToString();
    }

    void UpdateUILife(int before, int after)
    {
        _lifeText.text = after.ToString();
    }

    void UpdateUIIncome(int before, int after)
    {
        _incomeText.text = after.ToString();
    }

    private bool _incomeCheck = true;

    IEnumerator AddIncomeToGold(float time)
    {
        while (_incomeCheck)
        {
            currencies[e_Currencies.Gold].AddCurrency(currencies[e_Currencies.Income].Amount);
            timeCount--;
            timeIncomeText.text = timeCount.ToString();
            yield return new WaitForSeconds(time);
            timeCount = Mathf.FloorToInt(time);
        }
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
