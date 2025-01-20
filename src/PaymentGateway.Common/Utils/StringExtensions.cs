﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Common.Utils
{
    public static class StringExtensions
    {
        public static int GetLastFourDigits(this string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 4)
            {
                throw new ArgumentException("Input must be at least 4 characters long", nameof(input));
            }


            if (!BigInteger.TryParse(input, out _))
            {
                throw new FormatException("The last 4 characters are not numeric.");
            }

            string lastFourDigits = input[^4..];

            if (!int.TryParse(lastFourDigits, out int result))
            {
                throw new FormatException("The last 4 characters are not numeric.");
            }

            return result;
        }
    }
}
