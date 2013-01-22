using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using TrackMe.Support.Configuration;

namespace TrackMe.AWS.Interface
{
    public class AWSManager
    {
        private static AWSManager _awsManager = null;
        private static AmazonS3 _client = Amazon.AWSClientFactory.CreateAmazonS3Client(AppSettingsManager.Instance.AWSAccessKey, AppSettingsManager.Instance.AWSSecretKey);

        private AWSManager()
        {
        }

        public static AWSManager Instance
        {
            get
            {
                if (_awsManager == null)
                    _awsManager = new AWSManager();
                return _awsManager;
            }
        }


        public void UploadFile(Stream inputStream, string key)
        {
            PutObjectRequest request = new PutObjectRequest();
            request.WithBucketName(AppSettingsManager.Instance.AWSBucketName);
            request.WithKey(key);
            request.WithInputStream(inputStream);
            request.AutoCloseStream = true;
            request.CannedACL = S3CannedACL.PublicRead;
            request.Timeout = AppSettingsManager.Instance.AWSTimeout;

            using (S3Response response = _client.PutObject(request))
            {
                string result = response.ResponseXml;
            }
        }

        public void DeleteFile(string key)
        {
            DeleteObjectRequest request = new DeleteObjectRequest();
            request.WithBucketName(AppSettingsManager.Instance.AWSBucketName)
                .WithKey(key);
            using (DeleteObjectResponse response = _client.DeleteObject(request))
            {
                string result = response.ResponseXml;
            }
        }

        //public Stream GetFile(string key)
        //{
        //    GetObjectRequest request = new GetObjectRequest()
        //        .WithBucketName(AppSettingsManager.Current.AWSBucketName)
        //        .WithKey(key);

        //    using (S3Response response = _client.GetObject(request))
        //    {
        //        string title = response.Metadata["x-amz-meta-title"];
        //        string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
        //        if (!File.Exists(dest))
        //        {
        //            using (Stream s = response.ResponseStream)
        //            {
        //                using (FileStream fs = new FileStream(dest, FileMode.Create, FileAccess.Write))
        //                {
        //                    byte[] data = new byte[32768];
        //                    int bytesRead = 0;
        //                    do
        //                    {
        //                        bytesRead = s.Read(data, 0, data.Length);
        //                        fs.Write(data, 0, bytesRead);
        //                    }
        //                    while (bytesRead > 0);
        //                    fs.Flush();
        //                }
        //            }
        //        }
        //    }
        //}
            


        //public void WritingAnObject()
        //{
        //    try
        //    {
        //        // simple object put
        //        PutObjectRequest request = new PutObjectRequest();
        //        request.WithContentBody("this is a test")
        //            .WithBucketName(bucketName)
        //            .WithKey(keyName);

        //        S3Response response = client.PutObject(request);
        //        response.Dispose();

        //        // put a more complex object with some metadata and http headers.
        //        PutObjectRequest titledRequest = new PutObjectRequest();
        //        titledRequest.WithMetaData("title", "the title")
        //            .WithContentBody("this object has a title")
        //            .WithBucketName(bucketName)
        //            .WithKey(keyName);

        //        using (S3Response responseWithMetadata = client.PutObject(titledRequest))
        //        {
        //            // Do something with the response like print headers
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //        }
        //        else
        //        {
        //            Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
        //        }
        //    }
        //}

        //static void ReadingAnObject()
        //{
        //    try
        //    {
        //        GetObjectRequest request = new GetObjectRequest().WithBucketName(bucketName).WithKey(keyName);

        //        using (S3Response response = client.GetObject(request))
        //        {
        //            string title = response.Metadata["x-amz-meta-title"];
        //            Console.WriteLine("The object's title is {0}", title);
        //            string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
        //            if (!File.Exists(dest))
        //            {
        //                using (Stream s = response.ResponseStream)
        //                {
        //                    using (FileStream fs = new FileStream(dest, FileMode.Create, FileAccess.Write))
        //                    {
        //                        byte[] data = new byte[32768];
        //                        int bytesRead = 0;
        //                        do
        //                        {
        //                            bytesRead = s.Read(data, 0, data.Length);
        //                            fs.Write(data, 0, bytesRead);
        //                        }
        //                        while (bytesRead > 0);
        //                        fs.Flush();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //        }
        //        else
        //        {
        //            Console.WriteLine("An error occurred with the message '{0}' when reading an object", amazonS3Exception.Message);
        //        }
        //    }
        //}

        //static void DeletingAnObject()
        //{
        //    try
        //    {
        //        DeleteObjectRequest request = new DeleteObjectRequest();
        //        request.WithBucketName(bucketName)
        //            .WithKey(keyName);
        //        using (DeleteObjectResponse response = client.DeleteObject(request))
        //        {
        //            System.Net.WebHeaderCollection headers = response.Headers;
        //            foreach (string key in headers.Keys)
        //            {
        //                Console.WriteLine("Response Header: {0}, Value: {1}", key, headers.Get(key));
        //            }
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null &&
        //            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //        }
        //        else
        //        {
        //            Console.WriteLine("An error occurred with the message '{0}' when deleting an object", amazonS3Exception.Message);
        //        }
        //    }
        //}

        //static void ListingObjects()
        //{
        //    try
        //    {
        //        ListObjectsRequest request = new ListObjectsRequest();
        //        request.BucketName = bucketName;
        //        using (ListObjectsResponse response = client.ListObjects(request))
        //        {
        //            foreach (S3Object entry in response.S3Objects)
        //            {
        //                Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
        //            }
        //        }

        //        // list only things starting with "foo"
        //        request.WithPrefix("foo");
        //        using (ListObjectsResponse response = client.ListObjects(request))
        //        {
        //            foreach (S3Object entry in response.S3Objects)
        //            {
        //                Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
        //            }
        //        }

        //        // list only things that come after "bar" alphabetically
        //        request.WithPrefix(null)
        //            .WithMarker("bar");
        //        using (ListObjectsResponse response = client.ListObjects(request))
        //        {
        //            foreach (S3Object entry in response.S3Objects)
        //            {
        //                Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
        //            }
        //        }

        //        // only list 3 things
        //        request.WithPrefix(null)
        //            .WithMarker(null)
        //            .WithMaxKeys(3);
        //        using (ListObjectsResponse response = client.ListObjects(request))
        //        {
        //            foreach (S3Object entry in response.S3Objects)
        //            {
        //                Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
        //            }
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //        }
        //        else
        //        {
        //            Console.WriteLine("An error occurred with the message '{0}' when listing objects", amazonS3Exception.Message);
        //        }
        //    }
        //}
    }
}
