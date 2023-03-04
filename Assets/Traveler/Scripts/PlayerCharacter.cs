using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Space]
    [SerializeField] Text _costBread;
    [SerializeField] Text _priseUbgradeBread;
    [SerializeField] Text _costMeat;
    [SerializeField] Text _priseUbgradeMeat;
    [SerializeField] Text _costTextile;
    [SerializeField] Text _priseUbgradeTextile;
    public Text _gold;
    public Image Bread;
    public Image Meat;
    public Image Textile;
    public static int PlayerGold;
    public int PriceBread;
    public int BasePriceUpgreadBread;
    public int PriceMeat;
    public int BasePriceUpgreadMeat;
    public int PriceTextile;
    public int BasePriceUpgreadTextile;
    int ProductionClick;
    public int _lvlBread = 1;
    public int _lvlMeat = 1;
    public int _lvlTextile = 1;

    private const string _saveKey = "Player";

    private void Awake()
    {
        Load();
    }
    public void ClickButtonUpgrade(int no)
    {
        switch (no)
        {
            case 1:
                if (PlayerGold>= BasePriceUpgreadBread * _lvlBread)
                {
                    PlayerGold -= (BasePriceUpgreadBread * _lvlBread);
                    PriceBread += (PriceBread / _lvlBread);
                    _lvlBread++;  ProductionClick = 1 + (_lvlBread / 5);
                    _priseUbgradeBread.text = $"{BasePriceUpgreadBread * _lvlBread}$";
                    _costBread.text = $"LVL {_lvlBread}      {PriceBread}$->{PriceBread + (PriceBread /_lvlBread)}$          {ProductionClick}->{ProductionClick + 1} (На {(ProductionClick) * 5} LVL)";
                }
                break;

            case 2:
                if (PlayerGold >= BasePriceUpgreadMeat * _lvlMeat)
                {
                    PlayerGold -= (BasePriceUpgreadMeat * _lvlMeat);
                    PriceMeat += (PriceMeat / _lvlMeat);
                    _lvlMeat++; ProductionClick = 1 + (_lvlMeat / 5);
                    _priseUbgradeMeat.text = $"{BasePriceUpgreadMeat * _lvlMeat}$";
                    _costMeat.text = $"LVL {_lvlMeat}      {PriceMeat}$->{PriceMeat + (PriceMeat / _lvlMeat)}$          {ProductionClick}->{ProductionClick + 1} (На {(ProductionClick) * 5} LVL)";
                }
                break;

            case 3:
                if (PlayerGold >= BasePriceUpgreadTextile * _lvlTextile)
                {
                    PlayerGold -= (BasePriceUpgreadTextile * _lvlTextile);
                    PriceTextile += (PriceTextile / _lvlTextile);
                    _lvlTextile++; ProductionClick = 1 + (_lvlTextile / 5);
                    _priseUbgradeTextile.text = $"{BasePriceUpgreadTextile * _lvlTextile}$";
                    _costTextile.text = $"LVL {_lvlTextile}      {PriceTextile}$->{PriceTextile + (PriceTextile /_lvlTextile)}$          {ProductionClick}->{ProductionClick + 1} (На {(ProductionClick) * 5} LVL)";
                }
                break;
        }
        Save();
        _gold.text = $"{PlayerGold}$";
    }                            // события для кнопок в инвентаре персонажа.

    public void RestartGame()
    {
        PlayerGold = 0 ;
        PlayerPrefs.DeleteAll();
        {
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Load()
    {
        
        if (PlayerPrefs.HasKey("Player"))
        {  
            var data = SaveGame.Load<SaveDate.PlayerProfile>(_saveKey);
            PlayerGold = data.Gold;
          _lvlBread = data.LvlBread;
          _lvlMeat = data.LvlMeat;
          _lvlTextile = data.LvlTextile;
           LoadPrintResource();
        }
        
    }

    public void Save()
    {
        SaveGame.Save(_saveKey, GetSavePlayerCharacters());

    }

    private SaveDate.PlayerProfile GetSavePlayerCharacters()
    {
        var data = new SaveDate.PlayerProfile()
        {
            Gold = PlayerGold,
            LvlBread = _lvlBread,
            LvlMeat = _lvlMeat,
            LvlTextile = _lvlTextile,
        };
        return data;
    }         // сохраняемые параметры игрока.

    private void LoadPrintResource()
    {
        PriceBread *= _lvlBread;
        PriceMeat *= _lvlMeat;
        PriceTextile *= _lvlTextile;
        _gold.text = $"{PlayerGold}$";
        ProductionClick = 1 + (_lvlBread / 5);
        _priseUbgradeBread.text = $"{BasePriceUpgreadBread * _lvlBread}$";
        _costBread.text = $"LVL {_lvlBread}      {PriceBread}$->{PriceBread + (PriceBread / _lvlBread)}$          {ProductionClick}->{ProductionClick + 1} (На {(ProductionClick) * 5} LVL)";
        ProductionClick = 1 + (_lvlMeat / 5);
        _priseUbgradeMeat.text = $"{BasePriceUpgreadMeat * _lvlMeat}$";
        _costMeat.text = $"LVL {_lvlMeat}      {PriceMeat}$->{PriceMeat + (PriceMeat / _lvlMeat)}$          {ProductionClick}->{ProductionClick + 1} (На {(ProductionClick) * 5} LVL)";
        ProductionClick = 1 + (_lvlTextile / 5);
        _priseUbgradeTextile.text = $"{BasePriceUpgreadTextile * _lvlTextile}$";
        _costTextile.text = $"LVL {_lvlTextile}      {PriceTextile}$->{PriceTextile + (PriceTextile / _lvlTextile)}$          {ProductionClick}->{ProductionClick + 1} (На {(ProductionClick) * 5} LVL)";
    }                                 // Вывод значений на поля текста.
}
