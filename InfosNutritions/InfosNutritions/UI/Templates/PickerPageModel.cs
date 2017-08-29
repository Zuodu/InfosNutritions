using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using InfosNutritions.Navigation;
using Xamarin.Forms;

namespace InfosNutritions.UI.Templates
{
    public abstract class PickerPageModel<TResult> : FreshBasePageModel where TResult : class
    {
        #region Protected

        protected INavigationServices NavigationServices;

        #endregion

        #region Constructor

        protected PickerPageModel(INavigationServices navigationServices)
        {
            NavigationServices = navigationServices;
        }

        #endregion

        #region Properties

        public TResult Result { get; set; }

        #endregion

        #region Commands

        public ICommand CancelCommand => new Command(Cancel);
        public ICommand PickCommand => new Command(Pick);

        #endregion

        #region Public Methods

        protected virtual void Cancel()
        {
            this.Result = null;
            NavigationServices.GoBack(this, null);
        }

        protected virtual void Pick()
        {
            if (Result != null) NavigationServices.GoBack(this, Result);
        }

        #endregion
    }
}
