using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FarmManager.Model.DatabaseContext;

public static class ModelBuilderExtensions
{
    public static void ApplyQueryFilter<TBaseEntity>(this ModelBuilder builder,
        Expression<Func<TBaseEntity, bool>> filter)
    {
        var entities = builder.Model.GetEntityTypes()
            .Where(et => typeof(TBaseEntity).IsAssignableFrom(et.ClrType))
            .ToList();

        foreach (var entity in entities)
        {
            var parameter = Expression.Parameter(entity.ClrType, "e");
            var body = ReplacingExpressionVisitor.Replace(filter.Parameters[0], parameter, filter.Body);
            var lambda = entity.GetQueryFilter();

            if (lambda != null)
            {
                body = ReplacingExpressionVisitor.Replace(parameter, lambda.Parameters[0], body);
                body = Expression.AndAlso(lambda.Body, body);
                lambda = Expression.Lambda(body, lambda.Parameters);
            }
            else
            {
                lambda = Expression.Lambda(body, parameter);
            }

            entity.SetQueryFilter(lambda);
        }
    }
}
