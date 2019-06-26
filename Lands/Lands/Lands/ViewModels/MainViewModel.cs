using System;
using System.Collections.Generic;
using System.Text;
using Lands.Models;

namespace Lands.ViewModels
{
    class MainViewModel
    {
        #region Properties
        public TokenResponse Token
        {
            get; set;
        }

        #endregion

        #region ViewModels
        public LandsViewModel Login
        {
            get; set;
        }
        public LandsViewModel Lands
        {
            get; set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LandsViewModel();

        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();

            }
            return instance;
        }

        #endregion
    }
}
