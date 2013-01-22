using System;
using System.Configuration;

namespace TrackMe.Support.Configuration
{
    /// <summary>
    /// The class encapsulates the access to the application settings stored in the app.config file.
    /// </summary>
    public sealed class AppSettingsManager
    {
        private static readonly AppSettingsManager instance = new AppSettingsManager();
       

        private AppSettingsManager()
        {
        }

        /// <summary>
        /// AppSettingsManager instance.
        /// </summary>
        public static AppSettingsManager Instance
        {
            get
            {
                return instance;
            }
        }

        public string DefaultIoCContainer
        {
            get { return ConfigurationManager.AppSettings["DefaultIoCContainer"]; }
        }

        public double ServiceWaitTime
        {
            get { return SafeCast.SafeCast.ToDouble(ConfigurationManager.AppSettings["ServiceWaitTime"]); }
        }

        public string TrackFilePath
        {
            get { return ConfigurationManager.AppSettings["TrackFilePath"]; }
        }

        public string AWSAccessKey
        {
            get { return ConfigurationManager.AppSettings["AWSAccessKey"]; }
        }

        public string AWSSecretKey
        {
            get { return ConfigurationManager.AppSettings["AWSSecretKey"]; }
        }

        public string AWSBucketName
        {
            get { return ConfigurationManager.AppSettings["AWSBucketName"]; }
        }

        public int AWSTimeout
        {
            get { return SafeCast.SafeCast.ToInteger(ConfigurationManager.AppSettings["AWSTimeout"]); }
        }

        public string AWSItemUrlFormat
        {
            get { return ConfigurationManager.AppSettings["AWSItemUrlFormat"]; }
        }

        public int PhotoMaxSize
        {
            get { return SafeCast.SafeCast.ToInteger(ConfigurationManager.AppSettings["PhotoMaxSize"]); }
        }

        public string EmailFrom
        {
            get { return ConfigurationManager.AppSettings["EmailFrom"]; }
        }

        public string ContactEmail
        {
            get { return ConfigurationManager.AppSettings["ContactEmail"]; }
        }
    }
}
