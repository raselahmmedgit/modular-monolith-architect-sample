using Microsoft.AspNetCore.Mvc;

namespace rapid.erp.Common
{
    public class DefaultPageViewModel<TCreate, TEdit, TDetail, TSearchOption, TSearchResult> where TCreate : class where TEdit : class where TSearchOption : class where TSearchResult : class
    {
        public DefaultPageViewModel()
        {
        }
        public TCreate CreateViewModel { get; set; }
        public TEdit EditViewModel { get; set; }
        public TDetail DetailViewModel { get; set; }
        public TSearchOption SearchOptionViewModel { get; set; }
        public TSearchResult SearchResultViewModel { get; set; }

        public DefaultPageViewModel(TCreate createViewModel, TEdit editViewModel, TDetail detailViewModel, TSearchOption searchOptionViewModel, TSearchResult searchResultViewModel)
        {
            CreateViewModel = createViewModel;
            EditViewModel = editViewModel;
            DetailViewModel = detailViewModel;
            SearchOptionViewModel = searchOptionViewModel;
            SearchResultViewModel = searchResultViewModel;
        }
        public ControllerBase ControllerBase { get; set; }
    }
    public class DefaultPageViewModel<TCreate, TEdit, TSearchOption, TSearchResult> where TCreate : class where TEdit : class where TSearchOption : class where TSearchResult : class
    {
        public DefaultPageViewModel()
        {
        }
        public TCreate CreateViewModel { get; set; }
        public TEdit EditViewModel { get; set; }
        public TSearchOption SearchOptionViewModel { get; set; }
        public TSearchResult SearchResultViewModel { get; set; }

        public DefaultPageViewModel(TCreate createViewModel, TEdit editViewModel, TSearchOption searchOptionViewModel, TSearchResult searchResultViewModel)
        {
            CreateViewModel = createViewModel;
            EditViewModel = editViewModel;
            SearchOptionViewModel = searchOptionViewModel;
            SearchResultViewModel = searchResultViewModel;
        }
        public ControllerBase ControllerBase { get; set; }
    }
    public class DefaultPageViewModel<TCreateOrEdit, TSearchOption, TSearchResult> : DefaultPageViewModelBase where TCreateOrEdit : class where TSearchOption : class where TSearchResult : class
    {

        public TCreateOrEdit CreateorEditViewModel { get; set; }
        public TSearchOption SearchOptionViewModel { get; set; }
        public TSearchResult SearchResultViewModel { get; set; }
        

        public DefaultPageViewModel()
        {
        }        

        public DefaultPageViewModel(TCreateOrEdit createorEditViewModel, TSearchOption searchOptionViewModel, TSearchResult searchResultViewModel)
        {
            CreateorEditViewModel = createorEditViewModel;
            SearchOptionViewModel = searchOptionViewModel;
            SearchResultViewModel = searchResultViewModel;
        }
        public ControllerBase ControllerBase { get; set; }
    }    
    public class DefaultPageViewModel<TSearchOption, TSearchResult> : DefaultPageViewModelBase where TSearchOption : class where TSearchResult : class
    {
        public DefaultPageViewModel()
        {
            //
        }        

        public TSearchOption SearchOptionViewModel { get; set; }
        public TSearchResult SearchResultViewModel { get; set; }
        
        public DefaultPageViewModel(TSearchOption searchOptionViewModel, TSearchResult searchResultViewModel)
        {
            SearchOptionViewModel = searchOptionViewModel;
            SearchResultViewModel = searchResultViewModel;
        }
        public ControllerBase ControllerBase { get; set; }

    }
    public class DefaultPageViewModelEx<TModelOne, TModelTwo, TModelThree, TModelFour> : DefaultPageViewModelBase where TModelOne : class where TModelTwo : class where TModelThree : class where TModelFour : class
    {
        public DefaultPageViewModelEx()
        {
            //
        }       
        public TModelOne ViewModelOne { get; set; }
        public TModelTwo ViewModelTwo { get; set; }
        public TModelThree ViewModelThree { get; set; }
        public TModelFour ViewModelFour { get; set; }
        
        public DefaultPageViewModelEx(TModelOne tModelOne, TModelTwo tModelTwo, TModelThree tModelThree, TModelFour tModelFour)
        {
            ViewModelOne = tModelOne;
            ViewModelTwo = tModelTwo;
            ViewModelThree = tModelThree;
            ViewModelFour = tModelFour;
        }
        public ControllerBase ControllerBase { get; set; }

    }
    public class DefaultPageViewModelEx<TModelOne, TModelTwo, TModelThree, TModelFour, TModelFive> : DefaultPageViewModelBase where TModelOne : class where TModelTwo : class where TModelThree : class where TModelFour : class where TModelFive : class
    {
        public DefaultPageViewModelEx()
        {
            //
        }       
        public TModelOne ViewModelOne { get; set; }
        public TModelTwo ViewModelTwo { get; set; }
        public TModelThree ViewModelThree { get; set; }
        public TModelFour ViewModelFour { get; set; }
        public TModelFive ViewModelFive { get; set; }
        
        public DefaultPageViewModelEx(TModelOne tModelOne, TModelTwo tModelTwo, TModelThree tModelThree, TModelFour tModelFour, TModelFive tModelFive)
        {
            ViewModelOne = tModelOne;
            ViewModelTwo = tModelTwo;
            ViewModelThree = tModelThree;
            ViewModelFour = tModelFour;
            ViewModelFive = tModelFive;
        }
        public ControllerBase ControllerBase { get; set; }

    }
    public class DefaultPageViewModelBase
    {
        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public int FileType { get; set; }
        public string RequestedUrl { get; set; }
        public string RequestedUrlQueryKey { get; set; }
        public int? LoginUserId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
    }
}
