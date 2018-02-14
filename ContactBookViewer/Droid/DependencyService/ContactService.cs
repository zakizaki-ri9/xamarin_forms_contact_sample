using System;
using System.Collections.ObjectModel;
using System.IO;

using ContactBookViewer.DependencyService;
using ContactBookViewer.Model;
using ContactBookViewer.Droid.DependencyService;

using Android.Content;
using Android.Provider;

using Xamarin.Forms;

[assembly: Dependency(typeof(ContactService))]
namespace ContactBookViewer.Droid.DependencyService
{
    public class ContactService : IContact
    {
        /// <summary>
        /// アドレス帳取得
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Contact> GetContactList()
        {
            ObservableCollection<Contact> list = new ObservableCollection<Contact>();

            // アドレス帳から取得する名前を指定
            string[] projection = 
            {
                ContactsContract.Contacts.InterfaceConsts.Id,   // ID(おそらくアドレス帳一意となる値)
                ContactsContract.Contacts.InterfaceConsts.DisplayName,  // 名前
                ContactsContract.Contacts.InterfaceConsts.PhoneticName, // 読みがな
            };

            // アクティビティ取得
            //  Forms.Context から取得すると警告が発生するため、以下で取得するほうが良さそう
            var activity = Android.App.Application.Context;

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
        /// <param name="cr">Android.App.Application.Context.ContentResolver</param>
        /// <returns>'/'区切りの電話番号文字列</returns>
        private ObservableCollection<string> GetTel(string id, ContentResolver cr)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

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
                        result.Add(val);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// メールアドレス取得
        /// </summary>
        /// <param name="id">ContactId</param>
        /// <param name="cr">Android.App.Application.Context.ContentResolver</param>
        /// <returns>'/'区切りのメールアドレス文字列</returns>
        private ObservableCollection<string> GetEmail(string id, ContentResolver cr)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

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
                        result.Add(val);
                    }
                }
            }

            return result;
        }
    }
}
