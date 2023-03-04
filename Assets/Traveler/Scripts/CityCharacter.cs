using UnityEngine;
using UnityEngine.UI;


    public class CityCharacter : MonoBehaviour
    {
        [Space] 
        [SerializeField] PlayerCharacter Player;
        public Image _currentProduct;
        public Image BarProduct;
        public Text CurrentDemand;
        int _lvlProductBread = 1;
        int _lvlProductMeat = 1;
        int _lvlProductTextile = 1;
        int _currentDemand;
        int _click;
        public string NameCity;
        public int BaseDemand;

        public string _saveKey = "NameCity";                    // уникальный ID локации
        private void Awake()
        {
        Load();
        if (!_currentProduct.sprite) { _currentProduct.sprite = NextProductImage(Random.Range(1, 4)); }
        }

        public void OnClickButton(Image product)
        {
            if (product.sprite == Player.Bread.sprite)
            {
               _click = 1 + (Player._lvlBread / 5);
               if (_currentDemand < _click)
               {
                  PlayerCharacter.PlayerGold += Player.PriceBread * _currentDemand;
                  _currentDemand = 0; BarProduct.fillAmount = 1;
               }
               else
               {
                  PlayerCharacter.PlayerGold += Player.PriceBread * _click;
                  _currentDemand-= _click;
                  BarProduct.fillAmount += 1 / (float)(BaseDemand * _lvlProductBread);
               }
               CurrentDemand.text = _currentDemand.ToString();
            }                  // ’леб

            else if (product.sprite == Player.Meat.sprite)
            {
               _click = 1 + (Player._lvlMeat / 5);
               if (_currentDemand < _click)
               {
                  PlayerCharacter.PlayerGold += Player.PriceMeat * _currentDemand;
                  _currentDemand = 0; BarProduct.fillAmount = 1;
               }
               else
               {
                PlayerCharacter.PlayerGold += Player.PriceMeat * _click;
                _currentDemand -= _click;
                BarProduct.fillAmount += 1 / (float)(BaseDemand * _lvlProductMeat);
               }
               CurrentDemand.text = _currentDemand.ToString();
            }             // ћ€со

            else if (product.sprite == Player.Textile.sprite)
            {
                _click = 1 + (Player._lvlTextile / 5);
                if (_currentDemand < _click)
                {
                   PlayerCharacter.PlayerGold += Player.PriceTextile * _currentDemand;
                   _currentDemand = 0; BarProduct.fillAmount = 1;
                }
                else
                {
                   PlayerCharacter.PlayerGold += Player.PriceTextile * _click;
                   _currentDemand -= _click;
                   BarProduct.fillAmount += 1 / (float)(BaseDemand * _lvlProductTextile);
                }
                CurrentDemand.text = _currentDemand.ToString();
            }          // “кань
            Player._gold.text = $"{PlayerCharacter.PlayerGold}$";
            if (_currentDemand == 0) { UpgradeLvLProductCity(product); }

        Save();
        }             // событие дл€ клика по локации

        void UpgradeLvLProductCity(Image product)
        {
            if (product.sprite == Player.Bread.sprite)
            {
                _lvlProductBread++;
            }

            else if (product.sprite == Player.Meat.sprite)
            {
                _lvlProductMeat++;
            }

            else if (product.sprite == Player.Textile.sprite)
            {
                _lvlProductTextile++;
            }
            BarProduct.fillAmount = 0;
            _currentProduct.sprite = NextProductImage(Random.Range(1, 4));
        }            // повышение уровн€ товаров

        Sprite NextProductImage(int no)
        {
            Image nextImage = _currentProduct;

            switch (no)
            {
                case 1:
                    nextImage = Player.Bread;
                    _currentDemand = _lvlProductBread * BaseDemand;
                    CurrentDemand.text = _currentDemand.ToString();
                    break;

                case 2:
                    nextImage = Player.Meat;
                    _currentDemand = _lvlProductMeat * BaseDemand;
                    CurrentDemand.text = _currentDemand.ToString();
                    break;

                case 3:
                    nextImage = Player.Textile;
                    _currentDemand = _lvlProductTextile * BaseDemand;
                    CurrentDemand.text = _currentDemand.ToString();
                    break;
            }

            return nextImage.sprite;
        }                      // работа со спрайтами


        public void Load()
        {
           if (PlayerPrefs.HasKey($"{_saveKey}"))
            {
            var data = SaveGame.Load<SaveDate.CityProfile>(_saveKey);
            _lvlProductBread = data.lvlProductBread;
            _lvlProductMeat = data.lvlProductMeat;
            _lvlProductTextile = data.lvlProductTextile;
            _currentProduct.sprite = data.CurrentProduct;
            _currentDemand = data.CurrentDemand;
            CurrentDemand.text = _currentDemand.ToString() ;
            BarProduct.fillAmount = data.Bar;
           }
        }

        public void Save()
        {
            SaveGame.Save(_saveKey, GetSaveCityCharacters());

        }

        private SaveDate.CityProfile GetSaveCityCharacters()
        {
        var data = new SaveDate.CityProfile()
        {
            lvlProductBread = _lvlProductBread,
            lvlProductMeat = _lvlProductMeat,
            lvlProductTextile = _lvlProductTextile,
            CurrentProduct = _currentProduct.sprite,
            CurrentDemand = _currentDemand,
            Bar = BarProduct.fillAmount,
        };               
            return data;
        }                   // сохран€емые параметры локации
    }
