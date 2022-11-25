using System;
using PlacowkaOswiatowa.ViewModels.Abstract;

namespace PlacowkaOswiatowa.ViewModels.Helpers
{
    public class ExceptionManager : BaseViewModel
    {
        #region Instancja
        private static ExceptionManager _instance;

        public static ExceptionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExceptionManager();
                }

                return _instance;
            }
            set { _instance = value; }
        }
        #endregion

        #region Publish
        public virtual void Publish(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
        #endregion
    }
}
