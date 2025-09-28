using AutoMapper;

namespace rapid.erp.Common
{
    public static class SearchModelConvert<TSource, TDestination> where TSource : class where TDestination : class
    {
        public static SearchModel<TDestination> Convert(SearchModel<TSource> source, IMapper mapper)
        {
            var data = mapper.Map<TSource, TDestination>(source.Value);
            SearchModel<TDestination> dest = new SearchModel<TDestination>(null);
            dest.Value = data;
            dest.SerchKeyAndValue = source.SerchKeyAndValue;
            dest.SearchText = source.SearchText;
            return dest;
        }

    }
}
