using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FarmManager.Model.DatabaseContext;

public static class ModelBuilderExtensions
{
    public static void ApplyQueryFilter<TBaseEntity>(this ModelBuilder builder,
        Expression<Func<TBaseEntity, bool>> filter)
    {
        var acceptableItems = builder.Model.GetEntityTypes()
            .Where(et => typeof(TBaseEntity).IsAssignableFrom(et.ClrType))
            .ToList();

        foreach (var entityType in acceptableItems)
        {
            var entityParam = Expression.Parameter(entityType.ClrType, "e");
            var filterBody = ReplacingExpressionVisitor.Replace(filter.Parameters[0], entityParam, filter.Body);
            var filterLambda = entityType.GetQueryFilter();

            if (filterLambda != null)
            {
                filterBody = ReplacingExpressionVisitor.Replace(entityParam, filterLambda.Parameters[0], filterBody);
                filterBody = Expression.AndAlso(filterLambda.Body, filterBody);
                filterLambda = Expression.Lambda(filterBody, filterLambda.Parameters);
            }
            else
            {
                filterLambda = Expression.Lambda(filterBody, entityParam);
            }

            entityType.SetQueryFilter(filterLambda);
        }
    }
}
