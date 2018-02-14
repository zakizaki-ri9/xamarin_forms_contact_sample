using System;
using System.Collections.Generic;

using ContactBookViewer.DependencyService;

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
            var contactOperation = Xamarin.Forms.DependencyService.Get<IContact>();
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
