using System;
using Plugin.Permissions;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.Permissions.Abstractions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using XamInvoice.Util;

namespace XamInvoice
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoicesPage : ContentPage
    {
        public InvoicesPage()
        {
            InitializeComponent();
        }


        async void TakePhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });
            if (file == null)
                return;

          //  DisplayAlert("File Location", file.Path, "OK");

            foto.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();

               string OcrStringResult=  OCR.MakeOCRRequest(ReadFully(stream)).Result;
                file.Dispose();
                return stream;
            });

            //      ProcessImage(photoResult);
        }

        private static IPermissions GetCurrent()
        {
            return CrossPermissions.Current;
        }
        #region TomarImagen
        async void PickImage(object sender, EventArgs e)
        {


            try
            {
                PermissionStatus status = await CrossPermissions.Current.RequestPermissionAsync<PhonePermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                    {
                        await DisplayAlert("Necestas permisos", "Necesitas permisos para la camara", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<PhonePermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    //Query permission
                }
                else if (status != PermissionStatus.Unknown)
                {

                    return;

                }
            }
            catch (Exception ex)
            {
                //Something went wrong
            }





            var initialized = await CrossMedia.Current.Initialize();

            if (!initialized || !CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            var options = new PickMediaOptions
            {
                CompressionQuality = 70
            };
            var photoResult = await CrossMedia.Current.PickPhotoAsync(options);

            //ProcessImage(photoResult);
        }


        #endregion

       
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }

}





