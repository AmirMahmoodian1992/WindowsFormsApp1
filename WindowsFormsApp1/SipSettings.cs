using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utils;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace SIPWindowsAgent
{

    public class SipSettings
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string AuthenticationId { get; set; }
        public string RegisterPassword { get; set; }
        public string DomainHost { get; set; }
        public string DomainPort { get; set; }
        //public string BarcaPass { get; set; }
        public string BarsaUserName { get; set; }
        internal bool IsTransferEnabled { get; set; }
        internal string CouplePhone { get; set; }
        internal string BarsaAddress { get; set; }
        public string BarcaPass { get; set; }


        //public string userName;
        //public string displayName;
        //public string authenticationId;
        //public string registerPassword;
        //public string domainHost;
        //public string domainPort;
        //public string BarcaPass;
        //public string BarsaUSerName;
        //internal bool IstransferEnabled;
        //internal string CouplePhone;
        //internal string BarsaAddress;


    }

    public class AppConfig
    {
        public Dictionary<string, SipSettings> SipSettings { get; set; } = new Dictionary<string, SipSettings>();
    }

    public class SettingsManager
    {
        private readonly string configFile = "config.json";

        public AppConfig LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(configFile);
                AppConfig config = JsonConvert.DeserializeObject<AppConfig>(json);
                if (config == null)
                {
                    return new AppConfig(); 
                }
                foreach (var userSettings in config?.SipSettings?.Values)
                {
                    userSettings.BarcaPass = DecryptString(userSettings.BarcaPass);
                    userSettings.RegisterPassword = DecryptString(userSettings.RegisterPassword);
                }
                return config;
            }
            catch (FileNotFoundException)
            {
                // Handle file not found, or return a default config
                return new AppConfig();
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                Console.WriteLine($"Error loading settings: {ex.Message}");
                return new AppConfig();
            }
        }

        public void SaveSettings(AppConfig config)
        {
            try
            {
                //string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                
                foreach (var userSettings in config.SipSettings.Values)
                {
                    userSettings.BarcaPass = EncryptString(userSettings.BarcaPass);
                    userSettings.RegisterPassword = EncryptString(userSettings.RegisterPassword);
                }
                string json = JsonConvert.SerializeObject(config);
                File.WriteAllText(configFile, json);

            }
            catch (Exception ex)
            {
                // Handle exceptions when saving settings
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }

        }
        public static string EncryptString(string plainText, string entropy = null)
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] entropyBytes = string.IsNullOrEmpty(entropy) ? null : Encoding.UTF8.GetBytes(entropy);

            byte[] encryptedData = ProtectedData.Protect(plaintextBytes, entropyBytes, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        // Decryption
        public static string DecryptString(string encryptedText, string entropy = null)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] entropyBytes = string.IsNullOrEmpty(entropy) ? null : Encoding.UTF8.GetBytes(entropy);

            byte[] decryptedData = ProtectedData.Unprotect(encryptedBytes, entropyBytes, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedData);
        }

    }

}
