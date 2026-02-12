using DVLD_DataAccess;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsCountry
    {

        public int ID { set; get; }
        public string CountryName { set; get; }
   
        public clsCountry()

        {
            this.ID = -1;
            this.CountryName = "";

        }

        private clsCountry(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref CountryName))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }

        public static clsCountry Find(string CountryName)
        {

            int ID = -1;
           
            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID ))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }

        public static async Task<DataTable> GetAllCountries()
        {
            return await clsCountryData.GetAllCountries();

        }

    }
}
