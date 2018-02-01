using System;
using System.Collections.Generic;

using ContactBookViewer.Interface;

using Xamarin.Forms;

namespace ContactBookViewer.View
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void contactButton_Clicked(object sender, EventArgs e)
        {
            var contactOperation = DependencyService.Get<IContactOperation>();
            if(contactOperation != null)
            {
                var list = contactOperation.GetContactList();
                if(list != null && list.Count > 0)
                {
                    Navigation.PushAsync(new ContactListPage(list), true);
                }
            }
        }
    }
}
