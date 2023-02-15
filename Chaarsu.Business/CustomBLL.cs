using Chaarsu.Models;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Business
{
    public class CustomBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomBLL()
        {
            _unitOfWork = new UnitOfWork();
        }

        private GenericRepository<GROUP> _group;
        private GenericRepository<CATEGORy> _categories;
        private GenericRepository<PRODUCT> _products;
        private SpRepository _sp;

        
    }
}
