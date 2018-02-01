using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using ContactBookViewer.Model;

namespace ContactBookViewer.View
{
    public partial class ContactDetailPage : ContentPage
    {
        private class DetailModel
        {
            public string Text { get; set; }
            public string Detail { get; set; }
        }

        public ContactDetailPage(Contact contact)
        {
            InitializeComponent();

            ObservableCollection<DetailModel> list = new ObservableCollection<DetailModel>();
            list.Add(new DetailModel() { Text = contact.Name, Detail = contact.Kana });
            list.Add(new DetailModel() { Text = contact.Tel, Detail = string.Empty });
            list.Add(new DetailModel() { Text = contact.Email, Detail = string.Empty });

            DataTemplate cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Text");
            cell.SetBinding(TextCell.DetailProperty, "Detail");

            listView.ItemsSource = list;
            listView.ItemTemplate = cell;
        }
    }
}
