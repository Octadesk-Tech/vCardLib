﻿using System;
using System.Collections.Generic;
using System.Linq;
using vCardLib.Enums;
using vCardLib.Models;

namespace vCardLib.Deserializers
{
    // ReSharper disable once InconsistentNaming
    public sealed class v2Deserializer : Deserializer
    {
        protected override List<Address> ParseAddresses(string[] contactDetails)
        {
             var addressCollection = new List<Address>();
            var addressStrings = contactDetails.Where(s => s.StartsWith("ADR"));
            foreach (var addressStr in addressStrings)
            {
                var addressString = addressStr.Replace("ADR;", "").Replace("ADR:", "");
                if (addressString.StartsWith("HOME:"))
                {
                    addressString = addressString.Replace("HOME:", "");
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.Home
                    };
                    addressCollection.Add(address);
                }
                else if (addressString.StartsWith("WORK:"))
                {
                    addressString = addressString.Replace("WORK:", "");
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.Work
                    };
                    addressCollection.Add(address);
                }
                else if (addressString.StartsWith("DOM:") || addressString.StartsWith("dom:"))
                {
                    addressString = addressString.Replace("DOM:", "").Replace("dom:", "");
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.Domestic
                    };
                    addressCollection.Add(address);
                }
                else if (addressString.StartsWith("INTL:") || addressString.StartsWith("intl:"))
                {
                    addressString = addressString.Replace("INTL:", "").Replace("intl:", "");
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.International
                    };
                    addressCollection.Add(address);
                }
                else if (addressString.StartsWith("PARCEL:") || addressString.StartsWith("parcel:"))
                {
                    addressString = addressString.Replace("PARCEL:", "").Replace("parcel:", "");
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.Parcel
                    };
                    addressCollection.Add(address);
                }
                else if (addressString.StartsWith("POSTAL:") || addressString.StartsWith("postal:"))
                {
                    addressString = addressString.Replace("POSTAL:", "").Replace("postal:", "");
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.Postal
                    };
                    addressCollection.Add(address);
                }
                else
                {
                    var address = new Address
                    {
                        Location = addressString.Replace(";", " "),
                        Type = AddressType.None
                    };
                    addressCollection.Add(address);
                }
            }

            return addressCollection;
        }

        protected override List<PhoneNumber> ParsePhoneNumbers(string[] contactDetails)
        {
            var phoneNumberCollection = new List<PhoneNumber>();

            var telStrings = contactDetails.Where(s => s.StartsWith("TEL"));
            foreach (var telString in telStrings)
            {
                var phoneString = telString.Replace("TEL;", "").Replace("TEL:", "");
                //Remove multiple typing
                if (phoneString.Contains(";"))
                {
                    var index = phoneString.LastIndexOf(";", StringComparison.Ordinal);
                    phoneString = phoneString.Remove(0, index + 1);
                }

                //Logic
                if (phoneString.StartsWith("CELL"))
                {
                    phoneString = phoneString.Replace(";VOICE", "");
                    phoneString = phoneString.Replace("CELL:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Cell
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("HOME"))
                {
                    phoneString = phoneString.Replace(";VOICE", "");
                    phoneString = phoneString.Replace("HOME:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Home
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("WORK"))
                {
                    phoneString = phoneString.Replace(";VOICE", "");
                    phoneString = phoneString.Replace("WORK:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Work
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("VOICE:"))
                {
                    phoneString = phoneString.Replace("VOICE:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Voice
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("FAX"))
                {
                    phoneString = phoneString.Replace("FAX:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Fax
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("TEXTPHONE"))
                {
                    phoneString = phoneString.Replace("TEXTPHONE:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Fax
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("TEXT"))
                {
                    phoneString = phoneString.Replace("TEXT:", "");
                    var phoneNumber = new PhoneNumber();
                    phoneNumber.Number = phoneString;
                    phoneNumber.Type = PhoneNumberType.Text;
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("VIDEO"))
                {
                    phoneString = phoneString.Replace("VIDEO:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Video
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("PAGER"))
                {
                    phoneString = phoneString.Replace("PAGER:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Pager
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("MAIN-NUMBER"))
                {
                    phoneString = phoneString.Replace("MAIN-NUMBER:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Fax
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("BBS"))
                {
                    phoneString = phoneString.Replace("BBS:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Pager
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("CAR"))
                {
                    phoneString = phoneString.Replace("CAR:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Pager
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("MODEM"))
                {
                    phoneString = phoneString.Replace("MODEM:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Pager
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else if (phoneString.StartsWith("ISDN"))
                {
                    phoneString = phoneString.Replace("ISDN:", "");
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.Pager
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
                else
                {
                    var phoneNumber = new PhoneNumber
                    {
                        Number = phoneString,
                        Type = PhoneNumberType.None
                    };
                    phoneNumberCollection.Add(phoneNumber);
                }
            }

            return phoneNumberCollection;
        }

        protected override List<EmailAddress> ParseEmailAddresses(string[] contactDetails)
        {
            var emailAddresses = new List<EmailAddress>();

            var emailStrings = contactDetails.Where(s => s.StartsWith("EMAIL"));
            foreach (var email in emailStrings)
            {
                try
                {
                    var emailString = email.Replace("EMAIL;", "").Replace("EMAIL:", "");
                    //Remove multiple typing
                    if (emailString.Contains(";"))
                    {
                        emailString = emailString.Replace(";", "");
                    }

                    //Logic
                    if (emailString.StartsWith("INTERNET:"))
                    {
                        emailString = emailString.Replace("INTERNET:", "");
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.Internet
                        };
                        emailAddresses.Add(emailAddress);
                    }
                    else if (emailString.StartsWith("HOME:"))
                    {
                        emailString = emailString.Replace("HOME:", "");
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.Home
                        };
                        emailAddresses.Add(emailAddress);
                    }
                    else if (emailString.StartsWith("WORK:"))
                    {
                        emailString = emailString.Replace("WORK:", "");
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.Work
                        };
                        emailAddresses.Add(emailAddress);
                    }
                    else if (emailString.StartsWith("AOL:") || emailString.StartsWith("aol:"))
                    {
                        emailString = emailString.Replace("AOL:", "").Replace("aol:", "");
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.AOL
                        };
                        emailAddresses.Add(emailAddress);
                    }
                    else if (emailString.StartsWith("APPLELINK:") || emailString.StartsWith("applelink:"))
                    {
                        emailString = emailString.Replace("APPLELINK:", "").Replace("applelink:", "");
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.Applelink
                        };
                        emailAddresses.Add(emailAddress);
                    }
                    else if (emailString.StartsWith("IBMMAIL:") || emailString.StartsWith("ibmmail:"))
                    {
                        emailString = emailString.Replace("IBMMAIL:", "").Replace("ibmmail:", "");
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.Work
                        };
                        emailAddresses.Add(emailAddress);
                    }
                    else
                    {
                        var emailAddress = new EmailAddress
                        {
                            Email = emailString,
                            Type = EmailType.None
                        };
                        emailAddresses.Add(emailAddress);
                    }
                }
                catch (FormatException)
                {
                }
            }

            return emailAddresses;
        }

        protected override List<Hobby> ParseHobbies(string[] contactDetails)
        {
            var hobbyCollection = new List<Hobby>();
            var hobbyStrings = contactDetails.Where(s => s.StartsWith("HOBBY;"));
            foreach (var hobbyStr in hobbyStrings)
            {
                var hobbyString = hobbyStr.Replace("HOBBY;", "");
                var hobby = new Hobby();
                if (hobbyString.StartsWith("HIGH") || hobbyString.StartsWith("high"))
                {
                    hobby.Level = Level.High;
                    hobby.Activity = hobbyString.Replace("HIGH:", "").Replace("high:", "").Trim();
                    hobbyCollection.Add(hobby);
                }
                else if (hobbyString.StartsWith("MEDIUM") || hobbyString.StartsWith("medium"))
                {
                    hobby.Level = Level.Medium;
                    hobby.Activity = hobbyString.Replace("MEDIUM:", "").Replace("medium:", "").Trim();
                    hobbyCollection.Add(hobby);
                }
                else if (hobbyString.StartsWith("LOW") || hobbyString.StartsWith("low"))
                {
                    hobby.Level = Level.Low;
                    hobby.Activity = hobbyString.Replace("LOW:", "").Replace("low:", "").Trim();
                    hobbyCollection.Add(hobby);
                }
            }

            return hobbyCollection;
        }

        protected override List<Expertise> ParseExpertises(string[] contactDetails)
        {
            var expertiseCollection = new List<Expertise>();
            var expertiseStrings = contactDetails.Where(s => s.StartsWith("EXPERTISE;"));
            foreach (var expertiseStr in expertiseStrings)
            {
                var expertiseString = expertiseStr.Replace("EXPERTISE;", "");
                expertiseString = expertiseString.Replace("LEVEL=", "");
                var expertise = new Expertise();
                if (expertiseString.StartsWith("HIGH") || expertiseString.StartsWith("high"))
                {
                    expertise.Level = Level.High;
                    expertise.Area = expertiseString.Replace("HIGH:", "").Replace("high:", "").Trim();
                    expertiseCollection.Add(expertise);
                }
                else if (expertiseString.StartsWith("MEDIUM") || expertiseString.StartsWith("medium"))
                {
                    expertise.Level = Level.Medium;
                    expertise.Area = expertiseString.Replace("MEDIUM:", "").Replace("medium:", "").Trim();
                    expertiseCollection.Add(expertise);
                }
                else if (expertiseString.StartsWith("LOW") || expertiseString.StartsWith("low"))
                {
                    expertise.Level = Level.Low;
                    expertise.Area = expertiseString.Replace("LOW:", "").Replace("low:", "").Trim();
                    expertiseCollection.Add(expertise);
                }
            }

            return expertiseCollection;
        }

        protected override List<Interest> ParseInterests(string[] contactDetails)
        {
            var interestCollection = new List<Interest>();
            var interestStrings = contactDetails.Where(s => s.StartsWith("INTEREST;"));
            foreach (var interestStr in interestStrings)
            {
                var interestString = interestStr.Replace("INTEREST;", "");
                interestString = interestString.Replace("LEVEL=", "");
                var interest = new Interest();
                if (interestString.StartsWith("HIGH") || interestString.StartsWith("high"))
                {
                    interest.Level = Level.High;
                    interest.Activity = interestString.Replace("HIGH:", "").Replace("high:", "").Trim();
                    interestCollection.Add(interest);
                }
                else if (interestString.StartsWith("MEDIUM") || interestString.StartsWith("medium"))
                {
                    interest.Level = Level.Medium;
                    interest.Activity = interestString.Replace("MEDIUM:", "").Replace("medium:", "").Trim();
                    interestCollection.Add(interest);
                }
                else if (interestString.StartsWith("LOW") || interestString.StartsWith("low"))
                {
                    interest.Level = Level.Low;
                    interest.Activity = interestString.Replace("LOW:", "").Replace("low:", "").Trim();
                    interestCollection.Add(interest);
                }
            }

            return interestCollection;
        }

        protected override List<Photo> ParsePhotos(string[] contactDetails)
        {
            var photoCollection = new List<Photo>();
            var photoStrings = contactDetails.Where(s => s.StartsWith("PHOTO;"));
            foreach (var photoStr in photoStrings)
            {
                var photo = new Photo();
                if (photoStr.Replace("PHOTO;", "").StartsWith("JPEG:"))
                {
                    photo.PhotoURL = photoStr.Replace("PHOTO;JPEG:", "").Trim();
                    photo.Encoding = PhotoEncoding.JPEG;
                    photo.Type = PhotoType.URL;
                    photoCollection.Add(photo);
                }
                else if (photoStr.Contains("JPEG") && photoStr.Contains("ENCODING=BASE64"))
                {
                    var photoString = photoStr.Trim();
                    photoString = photoString.Replace("PHOTO;", "");
                    photoString = photoString.Replace("JPEG", "");
                    photoString = photoString.Replace("ENCODING=BASE64", "");
                    photoString = photoString.Trim(';', ':');

                    photo.Encoding = PhotoEncoding.JPEG;
                    photo.Picture = Convert.FromBase64String(photoString);
                    photo.Type = PhotoType.Image;
                    photoCollection.Add(photo);
                }

                else if (photoStr.Replace("PHOTO;", "").StartsWith("GIF:"))
                {
                    photo.PhotoURL = photoStr.Replace("PHOTO;GIF:", "").Trim();
                    photo.Encoding = PhotoEncoding.GIF;
                    photo.Type = PhotoType.URL;
                    photoCollection.Add(photo);
                }
            }

            return photoCollection;
        }
    }
}
