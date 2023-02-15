
using Chaarsu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        void Commit();

         /// <summary>
        /// Call this to Rollback the unit of work
        /// </summary>
        void  RollBack();


        bool IsTransaction { get; }

        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        anytimea_GROCERYEntities Db { get; }

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();
       

    }
}
