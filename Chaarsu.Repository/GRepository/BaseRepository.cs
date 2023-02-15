
using Chaarsu.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Chaarsu.Repository.GRepository
{
    public class BaseRepository<T> where T : class
    {
        // protected PhyschologyEntities Db = null;
        private readonly IUnitOfWork _unitOfWork;


        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
        internal DbContext Database { get { return _unitOfWork.Db; } }

        //private bool _disposed = false;
        //private DbContextTransaction _transaction = null;
        private DbSet<T> _objectset;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            _objectset = _unitOfWork.Db.Set<T>();
        }


        protected DbSet<T> ObjectSet
        {
            get
            {
                return _objectset ?? (_objectset = _unitOfWork.Db.Set<T>());
            }
        }

        protected IQueryable<T> Collection
        {
            get
            {
                return AsQueryable();
            }
        }

        public virtual void Add(T entity)
        {
            try
            {
                ObjectSet.Add(entity);
                SaveChanges();
                // _unitOfWork.Db.Database.BeginTransaction().Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                //_unitOfWork.Db.Database.BeginTransaction().Rollback();
            }
        }

        //public virtual void Save()
        //{
        //    try
        //    {
        //        _unitOfWork.Db.SaveChanges();
        //        _transaction.Commit();
        //    }
        //    catch
        //    {
        //        _transaction.Rollback();
        //        throw;
        //    }
        //    finally
        //    {
        //        _transaction = _unitOfWork.Db.Database.BeginTransaction();
        //    }

        //}

        public virtual void Add(List<T> entities)
        {
            foreach (var entity in entities)
            {
                ObjectSet.Add(entity);
            }
            SaveChanges();
        }

        public virtual void SaveUpdate(T entity, int Id)
        {
            if (Id == 0)
            {
                Add(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public virtual void Update(T entity)
        {
            ObjectSet.Attach(entity);
            var entry = _unitOfWork.Db.Entry(entity);
            entry.State = EntityState.Modified;
            if (entity.GetType().GetProperty("CreatedDate") != null)
            {
                entry.Property("CreatedDate").IsModified = false;
            }
            SaveChanges();
        }

        public virtual void Update(List<T> entities)
        {
            foreach (var entity in entities)
            {
                ObjectSet.Attach(entity);
                var entry = _unitOfWork.Db.Entry(entity);
                entry.State = EntityState.Modified;
                if (entity.GetType().GetProperty("CreatedDate") != null)
                {
                    entry.Property("CreatedDate").IsModified = false;
                }
            }
            SaveChanges();
        }

        public virtual T GetById(Int64 id)
        {
            return ObjectSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(Int64 id)
        {
            return await ObjectSet.FindAsync(id);
        }

        public virtual void DeletePermanent(Int64 id)
        {
            var entity = ObjectSet.Find(id);
            if (entity == null) return;
            ObjectSet.Remove(entity);
            SaveChanges();
        }

        public virtual async Task DeletePermanentAsync(Int64 id)
        {
            var entity = await ObjectSet.FindAsync(id);
            if (entity == null) return;
            ObjectSet.Remove(entity);
            SaveChanges();
        }
        public void DeleteAll(List<int> IDs)
        {
            foreach (var id in IDs)
            {
                var entity = ObjectSet.Find(id);
                if (entity == null) return;
                ObjectSet.Remove(entity);
            }
            SaveChanges();
        }
        public virtual void DeletePermanent(List<Int64> IDs)
        {
            foreach (var id in IDs)
            {
                var entity = ObjectSet.Find(id);
                if (entity == null) return;
                ObjectSet.Remove(entity);
            }
            SaveChanges();
        }

        public virtual async Task DeletePermanentAsync(List<Int64> IDs)
        {
            foreach (var id in IDs)
            {
                var entity = await ObjectSet.FindAsync(id);
                if (entity == null) return;
                ObjectSet.Remove(entity);
            }
            SaveChanges();
        }

        public virtual void Delete(Int64 id)
        {
            var entity = ObjectSet.Find(id);
            if (entity == null) return;
            ((dynamic)entity).RecordStatus = "Deleted";
            SaveChanges();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await ObjectSet.FindAsync(id);
            if (entity == null) return;
            ((dynamic)entity).RecordStatus = "Deleted";
            SaveChanges();
        }

        public virtual void Delete(List<Int64> IDs)
        {
            foreach (var id in IDs)
            {
                var entity = ObjectSet.Find(id);
                if (entity == null) return;
                ((dynamic)entity).RecordStatus = "Deleted";
                SaveChanges();
            }

        }

        public virtual async Task DeleteAsync(List<Int64> IDs)
        {
            foreach (var id in IDs)
            {
                var entity = await ObjectSet.FindAsync(id);
                if (entity == null) return;
                ((dynamic)entity).RecordStatus = "Deleted";
                SaveChanges();
            }

        }

        public virtual T FirstOrDefault()
        {
            return Collection.FirstOrDefault();
        }

        public virtual async Task<T> FirstOrDefaultAsync()
        {
            return await Collection.FirstOrDefaultAsync();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Collection.FirstOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.FirstOrDefaultAsync(predicate);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return ObjectSet.FirstOrDefault(predicate);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await ObjectSet.FirstOrDefaultAsync(predicate);
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            // return Collection.Where(predicate).ToList();

            if (typeof(T).GetProperty("RecordStatus") != null)
            {
                //Func<T, bool> predicateExp;
                //predicateExp = item => ((dynamic)item).RecordStatus != "Deleted";
                var @params = predicate.Parameters;
                var checkNotDeleted = Expression.NotEqual(Expression.PropertyOrField(@params[0], "RecordStatus"), Expression.Constant("Deleted"));

                var originalBody = predicate.Body;

                var fullExpr = Expression.And(originalBody, checkNotDeleted);

                var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, @params);


                return ObjectSet.Where(lambda).ToList();
            }
            else
            {
                return ObjectSet.Where(predicate).ToList();
            }
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            // return await Collection.Where(predicate).ToListAsync();


            if (typeof(T).GetProperty("RecordStatus") != null)
            {
                //Func<T, bool> predicateExp;
                //predicateExp = item => ((dynamic)item).RecordStatus != "Deleted";
                var @params = predicate.Parameters;
                var checkNotDeleted = Expression.NotEqual(Expression.PropertyOrField(@params[0], "RecordStatus"), Expression.Constant("Deleted"));

                var originalBody = predicate.Body;

                var fullExpr = Expression.And(originalBody, checkNotDeleted);

                var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, @params);


                return await ObjectSet.Where(lambda).ToListAsync();
            }
            else
            {
                return await ObjectSet.Where(predicate).ToListAsync();
            }
        }

        public virtual List<T> GetAll()
        {
            return Collection.ToList();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Collection.ToListAsync();
        }

        public virtual IQueryable<T> AsQueryable()
        {
            if (typeof(T).GetProperty("RecordStatus") != null)
            {
                Func<T, bool> predicate;
                predicate = item => ((dynamic)item).RecordStatus != "Deleted";
                return ObjectSet.Where(predicate).AsQueryable();
            }
            else
            {
                return ObjectSet.AsQueryable();
            }
        }

        protected virtual void SaveChanges()
        {
            try
            {
                _unitOfWork.Db.SaveChanges();
            }
         
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        _unitOfWork.RollBack();
                        throw new Exception(ve.PropertyName + " - " + ve.ErrorMessage);

                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        protected virtual async Task SaveChangesAsync()
        {
            try
            {
                await _unitOfWork.Db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public int Count()
        {
            return ObjectSet.Count();
        }

        public async Task<int> CountAsync()
        {
            return await ObjectSet.CountAsync();
        }

    }
}
