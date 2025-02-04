using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Persistance.Extentions
{
	public static class ModelExtentions
	{


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="BaseType"></typeparam>
		/// <param name="modelBuilder"></param>
		/// <param name="assemblies"></param>
		public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
		{
			var filter = (Expression<Func<IBaseEntity, bool>>)(e => e.Activated && !e.Deleted);

			IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
				.Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c) && c != typeof(BaseEntity) );
			foreach (Type type in types)
			{


				/// <summary>
				/// فعال/غیرفعال
				/// </summary>                 
				modelBuilder.Entity(type).Property<bool>("Activated");

				///// <summary>
				///// شناسه ایجاد‌کننده
				///// </summary>
				//modelBuilder.Entity(type).Property<Guid>("CreateById").ValueGeneratedOnAddOrUpdate();

				/// <summary>
				/// تاریخ و زمان ایجاد
				/// </summary>
				modelBuilder.Entity(type).Property<DateTime>("CreateDate").HasColumnType(DataTypes.Datetime.ToString());

				/// <summary>
				/// حذف منطقی
				/// </summary>
				modelBuilder.Entity(type).Property<bool>("Deleted").HasDefaultValue(false);

				///// <summary>
				///// شناسه ویرایش کننده
				///// </summary>
				//modelBuilder.Entity(type).Property<Guid?>("ModifiedById").IsRequired(false);

				/// <summary>
				/// تاریخ و زمان ویرایش 
				/// </summary>
				modelBuilder.Entity(type).Property<DateTime?>("ModifiedDate").IsRequired(false).HasColumnType(DataTypes.Datetime.ToString());

				/// <summary>
				/// فیلتر Deleted و Activate 
				/// </summary>
				var filters = new List<LambdaExpression>();
				if (typeof(IBaseEntity).IsAssignableFrom(type))
					filters.Add(filter);
				var queryFilter = CombineQueryFilters(type, filter, filters);
				modelBuilder.Entity(type).HasQueryFilter(queryFilter);

			}
		}
		
		public static void IgnorePropertyOfEntity(this ModelBuilder modelBuilder, string propertyName, Type propertyType)
		{
			foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
			{
				IMutableProperty property = entityType.GetProperties().SingleOrDefault(p => p.Name.Equals(propertyName));
				if (property != null && property.ClrType == propertyType)
					modelBuilder.Entity(property.Name).Ignore(property.Name);
			}
		}
		private static LambdaExpression CombineQueryFilters(Type entityType, LambdaExpression baseFilter, IEnumerable<LambdaExpression> andAlsoExpressions)
		{
			var newParam = Expression.Parameter(entityType);

			var andAlsoExprBase = (Expression<Func<IBaseEntity, bool>>)(_ => true);
			var andAlsoExpr = ReplacingExpressionVisitor.Replace(andAlsoExprBase.Parameters.Single(), newParam, andAlsoExprBase.Body);
			foreach (var expressionBase in andAlsoExpressions)
			{
				var expression = ReplacingExpressionVisitor.Replace(expressionBase.Parameters.Single(), newParam, expressionBase.Body);
				andAlsoExpr = Expression.AndAlso(andAlsoExpr, expression);
			}
			var baseExp = ReplacingExpressionVisitor.Replace(baseFilter.Parameters.Single(), newParam, baseFilter.Body);
			var exp = Expression.OrElse(baseExp, andAlsoExpr);

			return Expression.Lambda(exp, newParam);
		}
	}
}
