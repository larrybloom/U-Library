using Library.Core.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Utilities;

public static class Paginator
{
    public static async Task<PaginatorResponseDto<IEnumerable<TDestination>>> PaginationAsync<TSource, TDestination>(this IQueryable<TSource> queryable, int pageSize, int pageNumber, IMapper mapper)
        where TSource : class
        where TDestination : class
    {
        var count = await queryable.CountAsync();
        var pageResult = new PaginatorResponseDto<IEnumerable<TDestination>>
        {
            PageSize = (pageSize > 10 || pageNumber < 1) ? 10 : pageSize,
            CurrentPage = pageNumber > 1 ? pageNumber : 1,
            PreviousPage = pageNumber > 0 ? pageNumber - 1 : 0,
        };

        pageResult.NumberOfPages = count % pageResult.PageSize != 0
            ? count / pageResult.PageSize + 1
            : count / pageResult.PageSize;

        var sourceList = await queryable.Skip((pageResult.CurrentPage - 1) * pageResult.PageSize)
            .Take(pageResult.PageSize).ToListAsync();
        var destinationList = mapper.Map<IEnumerable<TDestination>>(sourceList);
        pageResult.PageItems = destinationList;
        return pageResult;
    }
    
    public static async Task<PaginatorResponseDto<IEnumerable<TSource>>> PaginationAsync<TSource>(this IQueryable<TSource> queryable, int pageSize, int pageNumber)
        where TSource : class
    {
        var count = await queryable.CountAsync();
        var pageResult = new PaginatorResponseDto<IEnumerable<TSource>>
        {
            PageSize = (pageSize > 10 || pageNumber < 1) ? 10 : pageSize,
            CurrentPage = pageNumber > 1 ? pageNumber : 1,
            PreviousPage = pageNumber > 0 ? pageNumber - 1 : 0,
        };

        pageResult.NumberOfPages = count % pageResult.PageSize != 0
            ? count / pageResult.PageSize + 1
            : count / pageResult.PageSize;

        pageResult.PageItems = await queryable.Skip((pageResult.CurrentPage - 1) * pageResult.PageSize)
            .Take(pageResult.PageSize).ToListAsync();
        
        return pageResult;
    }
}