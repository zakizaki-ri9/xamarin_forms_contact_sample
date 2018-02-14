using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using ContactBookViewer.Model;
using ContactBookViewer.DependencyService;

using PCLStorage;
using Newtonsoft.Json;

namespace ContactBookViewer.View
{
    public partial class ContactListPage : ContentPage
    {
        public ContactListPage(ObservableCollection<Contact> list)
        {
            InitializeComponent();
            listView.ItemsSource = list;
        }

        void listView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ContactDetailPage(e.SelectedItem as Contact), true);
        }

        async void exportButton_Clicked(object sender, System.EventArgs e)
        {
            // ContactのJson文字列化
            string jsonString = JsonConvert.SerializeObject(listView.ItemsSource);

            // ファイル出力
            Random c = new Random();
            var file = await FileSystem.Current.LocalStorage.CreateFileAsync(
                $"{DateTime.Now:yyyyMMddHHmmssfff}_{c.Next(1000):0000}.json",
                CreationCollisionOption.ReplaceExisting
            );
            await file.WriteAllTextAsync(jsonString);

            // トーストで通知
            IToast toastService = Xamarin.Forms.DependencyService.Get<IToast>();
            if(toastService != null)
            {
                toastService.Show($"{file.Path} の出力が完了しました。");
            }
        }
    }
}
