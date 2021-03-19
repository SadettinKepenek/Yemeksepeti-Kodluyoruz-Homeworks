using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Homework_4.Clean_Code
{
    class Program
    {
        public static void Main(string[] args)
        {
            #region Boolean Karşılaştırmalar

            bool isAvailable = true;

            if (isAvailable == true)
            {
            }

            if (isAvailable)
            {
            }

            #endregion

            #region Boolean Değer Atamaları

            var totalWeeklySpend = 600;
            bool hasQualifyCampaign;

            if (totalWeeklySpend > 500)
            {
                hasQualifyCampaign = true;
            }
            else
            {
                hasQualifyCampaign = false;
            }

            hasQualifyCampaign = totalWeeklySpend > 500;

            #endregion

            #region Pozitif Ol

            var isUserNotRegistered = true;


            if (!isUserNotRegistered)
            {
                //kullanıcı kayıt olmuşsa if'e gir
            }

            var isUserRegistered = true;
            if (isUserRegistered)
            {
                //kullanıcı kayıt olmuşsa if'e gir
            }

            #endregion

            #region Ternary If

            int dailyMessageLimit = 100;

            bool isFreeMember = true;

            if (isFreeMember)
            {
                dailyMessageLimit = 10;
            }
            else
            {
                dailyMessageLimit = 100;
            }

            dailyMessageLimit = isFreeMember ? 10 : 100;

            #endregion

            #region Strongly Type

            string orderStatus = "Delivered";
            if (orderStatus.Equals("Delivered"))
            {
            }

            if (orderStatus.Equals(OrderStatus.Delivered))
            {
            }

            #endregion

            #region Başıboş ifadeler

            int age = 10;

            //bazı kodlar 


            if (age < 16)
            {
            }

            //bunun yerine 
            int maxAge = 16;
            if (age < maxAge)
            {
            }

            #endregion

            #region Karmaşık Koşullar

            var subscriptionEndDate = DateTime.Now.AddDays(30);
            var subscriptionStartDate = DateTime.Now;
            if (DateTime.Now <= subscriptionEndDate && DateTime.Now >= subscriptionStartDate)
            {
                //kullanıcının aboneliği devam ediyor
            }

            var isSubscriptionAvailable = DateTime.Now <= subscriptionEndDate && DateTime.Now >= subscriptionStartDate;
            if (isSubscriptionAvailable)
            {
                //kullanıcının aboneliği devam ediyor
            }

            #endregion

            #region Doğru aracı kullanmak

            List<string> relatedContracts = new List<string>();
            List<string> contracts = new List<string>();

            foreach (var contract in contracts)
            {
                if (!string.IsNullOrEmpty(contract) && contract.Contains("koşul"))
                {
                    relatedContracts.Add(contract);
                }
            }

            //bunun yerine 

            relatedContracts = contracts.Where(c => !string.IsNullOrEmpty(c) && c.Contains("koşul")).ToList();

            #endregion

            #region RuleOf7

            int minAge = 15;
            
            //some code 
            
            //some code again
            if (10 < minAge)
            {
                //do something
            }
            //değişkeni kullacağımız yerin üstüne yazmalıyız
            int minAge2 = 15;
            if (10 < minAge2)
            {
                //do something
            }

            #endregion

            #region Parametreler

            var methodResult = SomeMethod("Sadettin123","123","Sadettin","Kepenek",DateTime.Now);
            
            //bunun yerine metodumuzun parametrelerini bir modele aktarmamız lazım
            
            #endregion

            #region Tekrarlamayı Azaltmak

            bool if1 = true;
            bool if2 = true;
            bool if3 = true;
            if (if1)
            {
                if (if2)
                {
                    if (if3)
                    {
                        
                    }
                }
            }
            
            //böyle bir kod yazmaktan uzak durmalıyız

            #endregion

            #region Fail Fast

            bool isLogined = false;
            bool isAuthorized = true;

            if (isLogined)
            {
                if (isAuthorized)
                {
                    //kullanıcı yetkilendirilmiştir ve istediği aksiyonu gerçekleştirebilir
                }
            }
            
            //bunun yerine

            if (!isLogined)
            {
                //return
            }

            if (!isAuthorized)
            {
                //return
            }
            //işleme devam et
            #endregion
            
            Console.ReadKey();
        }

        public class OrderStatus
        {
            public const string Delivered = "Delivered";
            public const string Canceled = "Canceled";
        }

        public static string SomeMethod(string userName,string password,string firstname,string lastname,DateTime birthDate)
        {
            return "";
        }
    }
}