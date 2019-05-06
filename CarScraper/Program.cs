using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace CarScraper
{
    internal static class Program
    {
        private const string InputFile = "Content/dataset.dat";
        private const string OutputFile = "Content/dataset_updated.dat";
        
        public static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            
            string line;
            
            System.IO.StreamReader file =   
                new System.IO.StreamReader(InputFile);  
            while((line = file.ReadLine()) != null)
            {
                var data = line.Split(',');
                
                cars.Add(new Car(data[0], Convert.ToInt32(data[1])));
            }

            for (var i = 0; i < cars.Count; i++)
            {
                if (i % 100 == 0)
                    Console.WriteLine(i + " / " + cars.Count);

                RetrieveAndWriteCarInfoToFile(cars[i]);
            }
        }

        private static void RetrieveAndWriteCarInfoToFile(Car car)
        {
            if (RetrieveCarInfo(car) && RetrieveCarPrice(car))
            {
                using (System.IO.StreamWriter file = 
                    new System.IO.StreamWriter(OutputFile, true))
                {
                    file.WriteLine(car.GetCarData());
                }
            }
            else
            {
                Console.WriteLine("Could not retrieve: " + car.GetLicensePlate());
            }
        }

        private static bool RetrieveCarInfo(Car car)
        {
            var url = "https://www.anwb.nl/auto/autodashboard/auto/models/" + car.GetLicensePlate() + "?applicatie=autodashboard";

            try
            {
                var json = new WebClient().DownloadString(url);
                dynamic array = JsonConvert.DeserializeObject(json);
                dynamic modelInfo = array.resultList[0];
                
                car.SetModelInfo((string)modelInfo["uitvoeringID"], (int)modelInfo["jaarKenteken"], (int)modelInfo["maandKenteken"], (int)modelInfo["laatstBekendeNieuwprijs"]);
            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }

        private static bool RetrieveCarPrice(Car car)
        {
            var url = "https://www.anwb.nl/auto/autodashboard/auto/ratelist"
                      + "?modelId=" + car.GetModelID()
                      + "&plate=" + car.GetLicensePlate()
                      + "&licensePlate=" + car.GetLicensePlate()
                      + "&plateY=" + car.GetPlateY()
                      + "&buildYear=" + car.GetPlateY()
                      + "&plateM=" + car.GetPlateM()
                      + "&buildMonth="+ car.GetPlateM()
                      + "&kilometerstand=" + car.GetKilometerstand()
                      + "&currentkm=" + car.GetKilometerstand()
                      + "&newPrice=" + car.GetNewPrice()
                      + "&applicatie=autodashboard";

            try
            {
                var json = new WebClient().DownloadString(url);
                
                dynamic array = JsonConvert.DeserializeObject(json);
                
                car.SetPriceInfo(
                    (int)array.dagwaarde["value"],
                    (int)array.meeneemPrijs["value"],
                    (int)array.veilingPrijs["value"],
                    (int)array.situaties[0]["value"],
                    (int)array.situaties[1]["value"],
                    (int)array.situaties[2]["value"],
                    (int)array.situaties[3]["value"]
                );
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}