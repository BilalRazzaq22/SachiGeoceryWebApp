using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chaarsu.Repository.Interface;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Chaarsu.Models;

namespace Chaarsu.Repository.UnitofWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private anytimea_GROCERYEntities _db;
        private bool _IsTransaction = false;


        public bool IsTransaction
        {
            get { return _IsTransaction; }
        }

        public UnitOfWork()
        {
            _db = new anytimea_GROCERYEntities();
        }

        public void Dispose()
        {
          
        }

        public void StartTransaction()
        {
            _db.Database.BeginTransaction();
            _IsTransaction = true;
        }

        public void Commit()
        {
            try
            {
                _db.SaveChanges();
                _db.Database.CurrentTransaction.Commit();
                _IsTransaction = false;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(e.Message, e);
            }
        }

        public void RollBack()
        {
            if (IsTransaction == true)
            {
               // _db.SaveChanges();
                _IsTransaction = false;
                _db.Database.CurrentTransaction.Rollback();
            }
        }

        public anytimea_GROCERYEntities Db
        {
            get { return _db; }
        }

    }
}
