using System;
using System.Collections.ObjectModel;
using System.IO;

using ContactBookViewer.Interface;
using ContactBookViewer.Model;
using ContactBookViewer.Droid.DependencyService;

using Android.Content;
using Android.Provider;

using Xamarin.Forms;

[assembly: Dependency(typeof(ContactOperation))]
namespace ContactBookViewer.Droid.DependencyService
{
    public class ContactOperation : IContactOperation
    {
        public ObservableCollection<Contact> GetContactList()
        {
            ObservableCollection<Contact> list = new ObservableCollection<Contact>();

            string[] projection = 
            {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName,
                ContactsContract.Contacts.InterfaceConsts.PhoneticName,
            };

            // アクティビティ取得
            var activity = Forms.Context;

            // アクティビティが取得できた場合にクエリ発行
            if(activity != null)
            {
                var cursor = activity.ContentResolver.Query(
                    ContactsContract.Contacts.ContentUri,
                    //ContactsContract.Data.ContentUri,
                    projection,
                    string.Empty,
                    null,
                    ContactsContract.Contacts.InterfaceConsts.DisplayName
                );

                if (0 < cursor.Count)
                {
                    while (cursor.MoveToNext())
                    {
                        Contact model = new Contact();
                        string id = cursor.GetString(cursor.GetColumnIndex(projection[0]));

                        model.Name = cursor.GetString(cursor.GetColumnIndex(projection[1]));
                        model.Kana = cursor.GetString(cursor.GetColumnIndex(projection[2]));
                        model.Tel = GetTel(id, activity.ContentResolver);
                        model.Email = GetEmail(id, activity.ContentResolver);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 電話番号取得
        /// </summary>
        /// <param name="id">ContactId</param>
        /// <param name="cr">Forms.Context.ContentResolver</param>
        /// <returns>'/'区切りの電話番号文字列</returns>
        private string GetTel(string id, ContentResolver cr)
        {
            string result = string.Empty;

            var cursor = cr.Query(
                ContactsContract.CommonDataKinds.Phone.ContentUri,
                null,
                ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId + "=" + id,
                null,
                null
            );

            if(0 < cursor.Count)
            {
                while(cursor.MoveToNext())
                {
                    string val = string.Empty;
                    val = cursor.GetString(cursor.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));
                    if(!string.IsNullOrEmpty(val))
                    {
                        result += (string.IsNullOrEmpty(result) ? val : "/" + val);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// メールアドレス取得
        /// </summary>
        /// <param name="id">ContactId</param>
        /// <param name="cr">Forms.Context.ContentResolver</param>
        /// <returns>'/'区切りのメールアドレス文字列</returns>
        private string GetEmail(string id, ContentResolver cr)
        {
            string result = string.Empty;

            var cursor = cr.Query(
                ContactsContract.CommonDataKinds.Email.ContentUri,
                null,
                ContactsContract.CommonDataKinds.Email.InterfaceConsts.ContactId + "=" + id,
                null,
                null
            );

            if (0 < cursor.Count)
            {
                while (cursor.MoveToNext())
                {
                    string val = string.Empty;
                    val = cursor.GetString(cursor.GetColumnIndex(ContactsContract.CommonDataKinds.Email.Address));
                    if (!string.IsNullOrEmpty(val))
                    {
                        result += (string.IsNullOrEmpty(result) ? val : "/" + val);
                    }
                }
            }

            return result;
        }
    }
}
