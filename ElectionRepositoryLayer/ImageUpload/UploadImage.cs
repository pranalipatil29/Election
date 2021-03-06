﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionRepositoryLayer.ImageUpload
{
   public class UploadImage
    {
        /// <summary>
        /// creating variables to store value of API key, API secret key and Cloud name
        /// </summary>
        private string apiKey, apiSecretKey, cloudName;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadImage"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecretKey">The API secret key.</param>
        /// <param name="cloudName">Name of the cloud.</param>
        public UploadImage(string apiKey, string apiSecretKey, string cloudName)
        {
            this.apiKey = apiKey;
            this.apiSecretKey = apiSecretKey;
            this.cloudName = cloudName;
        }

        /// <summary>
        /// Uploads the specified form file.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the url of image</returns>
        /// <exception cref="Exception"> exception message</exception>
        public string Upload(IFormFile formFile)
        {
            try
            {
                // assigning cloud name, API key and API secret key
                Account account = new Account()
                {
                    Cloud = this.cloudName,
                    ApiKey = this.apiKey,
                    ApiSecret = this.apiSecretKey
                };

                // assigning account info to cloudinary class object
                Cloudinary cloudinary = new Cloudinary(account);

                // assigning file name
                var file = formFile.FileName;

                // reading the uploaded file and store the info in stream variable
                var stream = formFile.OpenReadStream();

                // geting the result after uploading the image
                ImageUploadResult result = cloudinary.Upload(new ImageUploadParams
                {
                    File = new FileDescription(file, stream)
                });

                // returning the image url
                return result.Uri.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
