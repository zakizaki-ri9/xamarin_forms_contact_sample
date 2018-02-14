using System;
using System.Collections.ObjectModel;
using System.IO;

using ContactBookViewer.DependencyService;
using ContactBookViewer.Model;
using ContactBookViewer.iOS.DependencyService;

// Xamarin.iOS系のらライブラリ
using Contacts;
using Foundation;

using Xamarin.Forms;

[assembly: Dependency(typeof(ContactService))]
namespace ContactBookViewer.iOS.DependencyService
{
    public class ContactService : IContact
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
        private ObservableCollection<string> GetTel(CNLabeledValue<CNPhoneNumber>[] list)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            if(list != null)
            {
                foreach (var tel in list)
                {
                    string telNumber = (tel != null && tel.Value != null ? tel.Value.StringValue : string.Empty);
                    if(!string.IsNullOrEmpty(telNumber))
                    {
                        result.Add(telNumber);
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
        private ObservableCollection<string> GetEmail(CNLabeledValue<NSString>[] list)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            if (list != null)
            {
                foreach (var email in list)
                {
                    if (email != null && !string.IsNullOrEmpty(email.Value))
                    {
                        result.Add(email.Value);
                    }
                }
            }

            return result;
        }
    }
}
