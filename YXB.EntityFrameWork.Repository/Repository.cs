using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YXB.EntityFrameWork.Core;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using System.Linq.Expressions;
using System.Transactions;

namespace YXB.EntityFrameWork.Repository
{
    public class Repository<TEntity> where TEntity: BaseEntity
    {
        public static Repository<TEntity> Instance {
            get { return _lazy.Value; }
        }
        //延迟加载
        private static readonly Lazy<Repository<TEntity>> _lazy = new Lazy<Repository<TEntity>>(() =>
        {
            return new Repository<TEntity>();
        });

        private static readonly string ConnectionCached = "EFContext";
        public Repository()
        {
        }
        private static DBConnection EFDB
        {
            get
            {
                DBConnection connection = CallContext.GetData(ConnectionCached) as DBConnection;
                if (CallContext.GetData(ConnectionCached) == null)
                {
                    connection = new DBConnection();
                    CallContext.SetData(ConnectionCached, connection);
                }
                else
                {
                    connection = CallContext.GetData(ConnectionCached) as DBConnection;
                }
                return connection;
            }
        }

        /// <summary>
        /// 排除已删除的
        /// </summary>
        public virtual IEnumerable<TEntity> Source {
            get
            {
                return EFDB.Set<TEntity>().Where(x => !x.IsDel);
            }
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        public virtual IEnumerable<TEntity> SourceAll
        {
            get
            {
                return EFDB.Set<TEntity>();
            }
        }

        public virtual TEntity FindByID(long Id)
        {
            return Source.Where(x=>x.ID==Id).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> FindForPaging(int size, int index, Expression<Func<TEntity, bool>> expression, out int total)
        {
            return FindForPaging(size, index, this.Find(expression), out total);
        }

        private IEnumerable<TEntity> FindForPaging(int size, int index, IEnumerable<TEntity> source, out int total)
        {
            if (index <= 0)
                index = 1;
            var temp = source.Skip((index - 1) * size).Take(size);
            total = source.Count();
            return temp;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            var data= EFDB.Set<TEntity>().Where(expression).Where(x=>!x.IsDel);
            return data;
        }

        public virtual void Delete(TEntity entity)
        {
            EFDB.Entry<TEntity>(entity).State = EntityState.Deleted;
            EFDB.SaveChanges();
        }
        public virtual void Delete(Expression<Func<TEntity,bool>> expression)
        {
            this.DeleteBatch(Find(expression));
        }

        public virtual void DeleteBatch(IEnumerable<TEntity> list)
        {
            var autoDelect = EFDB.Configuration.AutoDetectChangesEnabled;
            EFDB.Configuration.AutoDetectChangesEnabled = false;
            EFDB.Set<TEntity>().RemoveRange(list);
            EFDB.SaveChanges();
            EFDB.Configuration.AutoDetectChangesEnabled = autoDelect;
        }

        public virtual bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("保存的实体不能为空");
            }
            if (entity.LastUpdateTime == null)
            {
                entity.LastUpdateTime = DateTime.Now;
            }
            EFDB.Entry<TEntity>(entity).State = EntityState.Modified;
            return EFDB.SaveChanges()>0;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("新增的实体不能为空");
            }
            if (entity.LastUpdateTime == default(DateTime))
            {
                entity.LastUpdateTime = DateTime.Now;
            }
            if (entity.CreateDate == default(DateTime))
            {
                entity.CreateDate = DateTime.Now;
            }
            entity.IsDel = false;
            EFDB.Entry<TEntity>(entity).State = EntityState.Added;
            EFDB.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 新增列表
        /// </summary>
        /// <param name="entities"></param>
        public virtual void InsertBatch(IEnumerable<TEntity> entities)
        {
            var autoDelect = EFDB.Configuration.AutoDetectChangesEnabled;
            EFDB.Configuration.AutoDetectChangesEnabled = false;
            EFDB.Set<TEntity>().AddRange(entities);
            EFDB.SaveChanges();
            EFDB.Configuration.AutoDetectChangesEnabled = autoDelect;
        }

        public void Transaction(Action action)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    action();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }
            };
        }
    }
}
