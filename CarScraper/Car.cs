namespace CarScraper
{
    public class Car
    {
        //Input variables
        private string _licensePlate;
        private int _kilometerstand;

        //Data from first query
        private string _modelId;
        private int _plateY;
        private int _plateM;
        private int _newprice;

        //Data from second query
        private int price_dagwaarde;
        private int price_meeneem;
        private int price_veiling;
        private int price_bovag;
        private int price_merkmealer_garantie;
        private int price_inruil;
        private int price_verkoop_particulieren;

        public Car(string licensePlate, int kilometerstand)
        {
            _licensePlate = licensePlate;
            _kilometerstand = kilometerstand;
        }

        #region Setters
        
        public void SetModelInfo(string modelId, int plateY, int plateM, int newPrice)
        {
            _modelId = modelId;
            _plateY = plateY;
            _plateM = plateM;
            _newprice = newPrice;
        }

        public void SetPriceInfo(int dagwaarde, int meeneem, int veiling, int bovag, int merkdealer, int inruil, int particulieren)
        {
            price_dagwaarde = dagwaarde;
            price_meeneem = meeneem;
            price_veiling = veiling;
            price_bovag = bovag;
            price_merkmealer_garantie = merkdealer;
            price_inruil = inruil;
            price_verkoop_particulieren = particulieren;
        }

        #endregion
        
        #region Getters

        public string GetLicensePlate()
        {
            return _licensePlate;
        }

        public int GetKilometerstand()
        {
            return _kilometerstand;
        }
        
        public string GetModelID()
        {
            return _modelId;
        }

        public int GetPlateY()
        {
            return _plateY;
        }

        public int GetPlateM()
        {
            return _plateM;
        }

        public int GetNewPrice()
        {
            return _newprice;
        }

        public string GetCarData()
        {
            return ""
                   + _licensePlate + ";" 
                   + _kilometerstand + ";" 
                   + _newprice + ";" 
                   + price_dagwaarde + ";" 
                   + price_meeneem + ";" 
                   + price_veiling + ";" 
                   + price_bovag + ";" 
                   + price_merkmealer_garantie + ";" 
                   + price_inruil + ";" 
                   + price_verkoop_particulieren;
        }

        #endregion
    }
}