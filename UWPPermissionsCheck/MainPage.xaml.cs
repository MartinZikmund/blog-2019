using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPPermissionsCheck
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var library = await TryAccessLibraryAsync(KnownLibraryId.Pictures);
            MessageDialog dialog = new MessageDialog(library!= null ? "Accessed library successfully" : "User didn't grant us library permission");
            await dialog.ShowAsync();
        }

private async Task<StorageLibrary> TryAccessLibraryAsync(KnownLibraryId library)
{
    try
    {
        return await StorageLibrary.GetLibraryAsync(library);
    }
    catch (UnauthorizedAccessException)
    {
        //explain the issue
        MessageDialog requestPermissionDialog =
            new MessageDialog($"The app needs to access the {library}. " +
                              "Press OK to open system settings and give this app permission. " +
                              "If the app closes, reopen it afterwards. " +
                              "If you Cancel, the app will have limited functionality only.");
        var okCommand = new UICommand("OK");
        requestPermissionDialog.Commands.Add(okCommand);
        var cancelCommand = new UICommand("Cancel");
        requestPermissionDialog.Commands.Add(cancelCommand);
        requestPermissionDialog.DefaultCommandIndex = 0;
        requestPermissionDialog.CancelCommandIndex = 1;

        var requestPermissionResult = await requestPermissionDialog.ShowAsync();
        if (requestPermissionResult == cancelCommand)
        {
            //user chose to Cancel, app will not have permission
            return null;
        }

        //open app settings to allow users to give us permission
        await Launcher.LaunchUriAsync(new Uri("ms-settings:appsfeatures-app"));

        //confirmation dialog to retry
var confirmationDialog = new MessageDialog($"Please give this app the {library} permission " +
                                           "in the Settings app which has now opened.");
                confirmationDialog.Commands.Add(okCommand);
        await confirmationDialog.ShowAsync();                
    }
    return null;
}
    }
}
