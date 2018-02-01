using System;
using System.Collections.ObjectModel;
using System.IO;

using ContactBookViewer.Interface;
using ContactBookViewer.Model;
using ContactBookViewer.iOS.DependencyService;

// Xamarin.iOS系のらライブラリ
using Contacts;
using Foundation;

using Xamarin.Forms;

[assembly: Dependency(typeof(ContactOperation))]
namespace ContactBookViewer.iOS.DependencyService
{
    public class ContactOperation : IContactOperation
    {
        public ObservableCollection<Contact> GetContactList()
        {
            ObservableCollection<Contact> list = new ObservableCollection<Contact>();

            NSError error;
            var containerId = new CNContactStore().DefaultContainerIdentifier;
            var predicate = CNContact.GetPredicateForContactsInContainer(containerId);

            // 取得したいフィールドを設定
            var fetchKeys = new NSString[]
            {
                CNContactKey.GivenName,
                CNContactKey.FamilyName,
                CNContactKey.PhoneticGivenName,
                CNContactKey.PhoneticFamilyName,
                CNContactKey.EmailAddresses,
                CNContactKey.PhoneNumbers
            };

            //全ての連絡先を取得する
            var store = new CNContactStore();
            var contacts = store.GetUnifiedContacts(predicate, fetchKeys, out error);

            // 名前、かな、電話番号、メールアドレスを取得
            if(contacts != null && contacts.Length > 0)
            {
                foreach(var contact in contacts)
                {
                    list.Add(new Contact()
                    {
                        Name = contact.FamilyName + " " + contact.GivenName,
                        Kana = contact.PhoneticFamilyName + " " + contact.PhoneticGivenName,
                        Tel = GetTel(contact.PhoneNumbers),
                        Email = GetEmail(contact.EmailAddresses)
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// CNContact.PhoneNumbersから電話番号を取得します
        /// </summary>
        /// <returns>'/'区切りの電話番号文字列</returns>
        /// <param name="list">CNContact.PhoneNumbers</param>
        private string GetTel(CNLabeledValue<CNPhoneNumber>[] list)
        {
            string result = string.Empty;

            if(list != null)
            {
                foreach (var tel in list)
                {
                    string telNumber = (tel != null && tel.Value != null ? tel.Value.StringValue : string.Empty);
                    if(!string.IsNullOrEmpty(telNumber))
                    {
                        result += (string.IsNullOrEmpty(result) ? telNumber : "/" + telNumber);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// CNContact.EmailAddressesからメールアドレスを取得します
        /// </summary>
        /// <returns>'/'区切りのメールアドレス</returns>
        /// <param name="list">CNContact.EmailAddresses</param>
        private string GetEmail(CNLabeledValue<NSString>[] list)
        {
            string result = string.Empty;

            if (list != null)
            {
                foreach (var email in list)
                {
                    if (email != null && !string.IsNullOrEmpty(email.Value))
                    {
                        result += (string.IsNullOrEmpty(result) ? email.Value : "/" + email.Value);
                    }
                }
            }

            return result;
        }
    }
}
