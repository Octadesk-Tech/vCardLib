﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace vCardLib.Models
{
    /// <summary>
    /// Supported ecnoding types
    /// </summary>
    public enum PhotoEncoding
    {
        JPEG,
        GIF
    }

    /// <summary>
    /// Support image format types
    /// </summary>
    public enum PhotoType
    {
        Image,
        URL
    }

    /// <summary>
    /// Class to hold images embedded in the vCard 
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// The image
        /// </summary>
        public Bitmap Picture { get; set; }

        /// <summary>
        /// The image type
        /// </summary>
        public PhotoType Type { get; set; }

        /// <summary>
        /// The encoding of the image
        /// </summary>
        public PhotoEncoding Encoding { get; set; }

        /// <summary>
        /// The URL for remote images
        /// </summary>
        public string PhotoURL { get; set; }

        /// <summary>
        /// Converts the embedded image to a base 64 string
        /// </summary>
        /// <returns>An empty string  if the picture is null or a base 64 representation of the image</returns>
        public string ToBase64String()
        {
            if(Picture == null)
            {
                return "";
            }
            var stream = new MemoryStream();
            Picture.Save(stream, ImageFormat.Bmp);
            var imageBytes = stream.ToArray();
            var base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
