using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using InfosNutritions.Entities;
using InfosNutritions.Helpers;
using InfosNutritions.Navigation;
using InfosNutritions.Services;
using InfosNutritions.UI.Components.PageModels;
using InfosNutritions.UI.Globalization;
using PropertyChanged;
using Xamarin.Forms;

namespace InfosNutritions.UI.Screens.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomePageModel : FreshBasePageModel
    {
        #region Fields

        private readonly INavigationServices _navigationServices;
        private readonly IDataAccessServices _dataAccessServices;
        private readonly ISettingsServices _settingsServices;

        #endregion

        #region Constructor

        public HomePageModel(INavigationServices navigationServices, IDataAccessServices dataAccessServices, ISettingsServices settingsServices)
        {
            _navigationServices = navigationServices;
            _dataAccessServices = dataAccessServices;
            _settingsServices = settingsServices;
        }

        #endregion

        #region Properties

        public string ResultText { get; set; }
        [DependsOn("WebWorking")]
        public double Opacity => WebWorking ? 0.3 : 1;

        public FoodProduct ResultProduct { get; set; }
        public bool WebWorking { get; set; }

        #endregion

        #region Commands

        public ICommand ScanCommand => new Command(ScanCommandExecute);
        private async void ScanCommandExecute()
        {
            await CoreMethods.PushPageModel<ScannerPageModel>();
        }

        public ICommand RetrieveDataCommand => new Command(RetrieveDataCommandExecute);
        private async void RetrieveDataCommandExecute()
        {
            if (!WebWorking) await RetrieveData(ResultText);
        }

        #endregion

        #region Override

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData != null)
            {
                ResultText = ((ZXing.Result)returnedData).Text;
                Task.Run(async () => await RetrieveData(ResultText));
            }
        }

        #endregion

        #region Methods

        private async Task RetrieveData(string productId)
        {
            WebWorking = true;
            try
            {
                var data = await _dataAccessServices.GetProductInfo(productId);
                if (data != null)
                {
                    ResultProduct = DataParser.ToFoodProduct(data);
                    ResultProduct.Barcode = productId;
                    _settingsServices.AddProductToRecentList(ResultProduct);
                }
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(
                    async () => await CoreMethods.DisplayAlert(Lang.Error_Title, e.Message,Lang.Ok));
            }
            WebWorking = false;

        }

        #endregion
    }
}
