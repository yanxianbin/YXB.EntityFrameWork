using MySql.Data.MySqlClient;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core.Configurations
{
    public class BreakExecuteStrategy : IDbExecutionStrategy
    {
        private Policy _policy;
        public bool RetriesOnFailure=>true;

        public BreakExecuteStrategy(Policy policy)
        {
            _policy = policy;
        }

        public void Execute(Action operation)
        {
            _policy.Execute(() =>
            {
                operation.Invoke();
            });
        }

        public TResult Execute<TResult>(Func<TResult> operation)
        {
           return _policy.Execute(()=>{
               return operation.Invoke();
           });
        }

        public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
        {
            return _policy.Execute(()=> { return operation.Invoke(); });
        }

        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
        {
            return _policy.Execute(() => { return operation.Invoke(); });
        }
    }

    public class SqlExecuteStrategy : DbExecutionStrategy
    {
        public SqlExecuteStrategy()
        {
        }

        public SqlExecuteStrategy(int maxRetryCount,TimeSpan maxDelay):base(maxRetryCount,maxDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            bool bRetry = false;

            if (exception is MySqlException objectSqlException)
            {
                if (objectSqlException.Number >= 5)
                {
                    bRetry = true;
                }
            }
            return bRetry;
        }
    }
}
