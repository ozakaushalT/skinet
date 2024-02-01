using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        public System.Linq.Expressions.Expression<Func<T, bool>> Criteria { get; }

        public List<System.Linq.Expressions.Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            //add includes in the list;
            Includes.Add(includeExpression);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByEx)
        {
            OrderBy = orderByEx;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDscEx)
        {
            OrderByDescending = orderByDscEx;
        }
        protected void ApplyPaging(int skipPages, int takePages)
        {
            Skip = skipPages;
            Take = takePages;
            IsPagingEnabled = true;
        }
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }
    }
}