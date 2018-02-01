using System;
using System.Collections.ObjectModel;
using System.IO;

using ContactBookViewer.Interface;
using ContactBookViewer.Model;
using ContactBookViewer.Droid.DependencyService;

using Android.Widget;

using Xamarin.Forms;

[assembly: Dependency(typeof(ToastService))]
namespace ContactBookViewer.Droid.DependencyService
{
    public class ToastService : IToast
    {
        public void Show(string message)
        {
            Toast.MakeText(
                Forms.Context,
                message,
                ToastLength.Short).Show();
        }
    }
}
