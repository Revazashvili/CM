using System;
using System.Linq.Expressions;
using Application.Common.DTOs.Rates;
using Application.Common.Wrappers;
using Domain.Entities;

namespace Application.Queries.Rates
{
    public record GetRateQuery(Expression<Func<Rate,bool>> Predicate) : IRequestWrapper<RateDto>{}
    
    
}