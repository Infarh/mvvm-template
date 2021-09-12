namespace MVVM.ViewModels.Base
{
    public abstract class TittledViewModel : ViewModel
    {
        #region Tittle : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Tittle;

        /// <summary>Заголовок</summary>
        public string Tittle { get => _Tittle; set => Set(ref _Tittle, value); }

        #endregion
    }
}
