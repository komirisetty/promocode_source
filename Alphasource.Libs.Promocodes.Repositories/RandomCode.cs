using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alphasource.Libs.Promocodes.Models;

namespace Alphasource.Libs.Promocodes.Repositories
{
    public class RandomCode
    {
        Random _random = new Random();
        PromoCodeModel pcm = new PromoCodeModel();
        public void randomMethod(PromoCodeModel pcm)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var strChars = new char[6];
           
            int n = pcm.NoOfPromoCodes;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < strChars.Length; j++)
                {
                    strChars[j] = chars[_random.Next(chars.Length)];
                }
                var finalString = new string(strChars);

                var promoCodeGenerated = pcm.Prefix + finalString;

                pcm.PromocodeGenerated += promoCodeGenerated + ",";               
            }

        }

    }
}
