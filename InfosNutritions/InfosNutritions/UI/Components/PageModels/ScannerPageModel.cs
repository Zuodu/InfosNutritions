using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using InfosNutritions.Navigation;
using InfosNutritions.UI.Templates;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace InfosNutritions.UI.Components.PageModels
{
    class ScannerPageModel : PickerPageModel<ZXing.Result>
    {
        #region Fields

        //Expression régulière pour vérifier lr formattage EAN 13
        private static readonly Regex BarcodeFormat = new Regex(@"^\d{13}$");

        #endregion
        #region Constructor
        public ScannerPageModel(INavigationServices navigationServices) : base(navigationServices)
        {
        }
        #endregion

        #region Properties
        public ZXing.Mobile.MobileBarcodeScanningOptions ScannerOption { get; private set; }
        public bool ViewIsScanning { get; set; }
        public bool ViewIsAnalysing { get; set; }
        public string EntryText { get; set; }

        public ICommand ReturnCommand => new Command(Pick);

        public ICommand ForcePickCommand => new Command(ForcePickCommandExecute);

        private void ForcePickCommandExecute()
        {
            if (BarcodeFormat.IsMatch(EntryText))
            {
                NavigationServices.GoBack(this, new Result(EntryText, new byte[6], new ResultPoint[6], ZXing.BarcodeFormat.EAN_13));
            }
            else
            {
                CoreMethods.DisplayAlert("Format incorrect", "le format du code-barre ne correspond pas à EAN 13.", "ok");
            }
        }
        #endregion

        #region Override
        public override void Init(object initData)
        {
            base.Init(initData);
            ScannerOption = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                TryHarder = true
            };
            ViewIsScanning = true;
            ViewIsAnalysing = true;
        }

        #endregion
    }
}
