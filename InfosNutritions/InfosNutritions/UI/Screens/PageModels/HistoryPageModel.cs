using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using InfosNutritions.Entities;
using InfosNutritions.Navigation;
using InfosNutritions.Services;
using Xamarin.Forms;

namespace InfosNutritions.UI.Screens.PageModels
{
    class HistoryPageModel : FreshBasePageModel
    {
        #region Fields

        private readonly ISettingsServices _settingsServices;
        private readonly INavigationServices _navigationServices;

        #endregion

        #region Constructor

        public HistoryPageModel(ISettingsServices settingsServices, INavigationServices navigationServices)
        {
            _settingsServices = settingsServices;
            _navigationServices = navigationServices;
        }

        #endregion

        #region Properties

        public List<FoodProduct> RecentList => _settingsServices.GetRecentProductsList();

        private FoodProduct _chosenItem;

        public FoodProduct ChosenItem
        {
            get { return _chosenItem; }
            set
            {
                _chosenItem = value;
                if(_chosenItem != null) SelectItemCommandExecute();
            }
        }

        #endregion

        #region Commands

        public ICommand SelectItemCommand => new Command(SelectItemCommandExecute);

        private void SelectItemCommandExecute()
        {
            CoreMethods.DisplayAlert("Selection", $"Item selected : {ChosenItem.Barcode}", "ok");
            _navigationServices.PopToRoot(this);
        }

        #endregion

        #region Override

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            RaisePropertyChanged("RecentList");
        }

        #endregion
    }
}
