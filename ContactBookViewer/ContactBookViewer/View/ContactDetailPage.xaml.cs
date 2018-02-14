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

            Func<ObservableCollection<string>, string> getFirstItem = (target) =>
            {
                return (target != null && target.Count > 0 ? target[0] : string.Empty);
            };

            ObservableCollection<DetailModel> list = new ObservableCollection<DetailModel>();
            list.Add(new DetailModel() { Text = contact.Name, Detail = contact.Kana });
            list.Add(new DetailModel() { Text = getFirstItem(contact.Tel), Detail = string.Empty });
            list.Add(new DetailModel() { Text = getFirstItem(contact.Email), Detail = string.Empty });

            DataTemplate cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Text");
            cell.SetBinding(TextCell.DetailProperty, "Detail");

            listView.ItemsSource = list;
            listView.ItemTemplate = cell;
        }
    }
}
