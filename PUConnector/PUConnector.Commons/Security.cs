using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PUConnector.Commons
{
    /// <summary>
    /// Funkcje kryptograficzne, kontroli bezpieczeństwa transmisji i zabezpieczeń
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Weryfikuje poprawność nagłówka Authorization zawierającego uwierzytelnienie notyfikacji
        /// Nagłówek jest przesyłany przez PayU zabezpieczonym kanałem HTTPS (SSL)
        /// </summary>
        /// <param name="authHeader">Treść nagłówka Authorization</param>
        /// <param name="merchantPosId">Identyfikator punktu płatności</param>
        /// <param name="secondSecurityKey">Klucz szyfrujący do komunikacji z PayU</param>
        /// <returns>Znacznik poprawności weryfikacji nagłówka Authorization</returns>
        public static bool VerifyAuthorization(
            string authHeader,
            string merchantPosId,
            string secondSecurityKey
            )
        {
            if (authHeader == null) authHeader = string.Empty;
            authHeader = authHeader.Replace("Basic ", "");
            authHeader = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader));

            string[] tokens = authHeader.Split(':');
            if (tokens.Length != 2) tokens = new string[2];

            string _merchantPosId = tokens[0];
            string _secondSecurityKey = tokens[1];

            if (!merchantPosId.Equals(_merchantPosId, StringComparison.OrdinalIgnoreCase) ||
                !secondSecurityKey.Equals(_secondSecurityKey, StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }
        
        
        /// <summary>
        /// Weryfikuje poprawność nagłówka OpenPayu-Signature zawierającego podpis notyfikacji
        /// Nagłówek jest przesyłany przez PayU niezabezpieczonym kanałem HTTP
        /// </summary>
        /// <param name="signatureHeader">Treść nagłówka OpenPayu-Signature</param>
        /// <param name="requestContent">Treść odebranej notyfikacji</param>
        /// <param name="secondSecurityKey">Klucz szyfrujący do komunikacji z PayU</param>
        /// <returns>Znacznik poprawności weryfikacji nagłówka OpenPayu-Signature</returns>
        public static bool VerifySignature(
            string signatureHeader, 
            string requestContent,
            string secondSecurityKey
            )
        {
            if (string.IsNullOrEmpty(signatureHeader))
                return false;

            string[] items = signatureHeader.Split(';');
            Dictionary<string, string> dictItems = new Dictionary<string, string>();

            foreach (string item in items)
            {
                string[] kvpair = item.Split('=');
                if (kvpair.Length == 2)
                    dictItems.Add(kvpair[0], kvpair[1]);
            }

            string signature = dictItems["signature"];
            string algorithm = dictItems["algorithm"];

            HashAlgorithm hashAlgorithm = null;

            switch (algorithm.ToLower())
            {
                case "md5":
                    hashAlgorithm = MD5.Create();
                    break;
                case "sha-1":
                    hashAlgorithm = SHA1.Create();
                    break;
                case "sha-256":
                    hashAlgorithm = SHA256.Create();
                    break;
                case "sha-384":
                    hashAlgorithm = SHA384.Create();
                    break;
                case "sha-512":
                    hashAlgorithm = SHA512.Create();
                    break;
                default:
                    throw new NotSupportedException(
                        "Hash algorithm not supported: " + algorithm
                        );
            }

            string hashSource = requestContent + secondSecurityKey;
            string hash = Security.CalculateHash(hashSource, hashAlgorithm);

            return signature.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Tworzy treść nagłówka uwierzytelniającego Authorization przesyłanego do PayU wraz z każdym żądaniem
        /// </summary>
        /// <param name="merchantPosId">Identyfikator punktu płatności</param>
        /// <param name="secondSecurityKey">Klucz szyfrujący do komunikacji z PayU</param>
        /// <returns>Treść nagłówka Authorization</returns>
        public static string BasicAuthHdrValueCreate(
            string merchantPosId,
            string secondSecurityKey
            )
        {
            string authValue =
                merchantPosId + ":" + secondSecurityKey;

            byte[] authValueBytes =
                Encoding.UTF8.GetBytes(authValue);

            return "Basic " + System.Convert.ToBase64String(authValueBytes);
        }


        /// <summary>
        /// Oblicza wartość skrótu skryptograficznego z łańcucha tekstowego
        /// </summary>
        /// <param name="input">Łańcuch tekstowy</param>
        /// <param name="hashAlgorithm">Algorytm funkcji skrótu kryptograficznego</param>
        /// <returns>Wartość skrótu kryptograficznego</returns>
        public static string CalculateHash(string input, HashAlgorithm hashAlgorithm)
        {
            using (hashAlgorithm)
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = hashAlgorithm.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
