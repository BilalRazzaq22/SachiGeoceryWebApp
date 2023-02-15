
using Chaarsu.Library.Utilities;
using Chaarsu.Models;
using Chaarsu.Models.ViewModel;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chaarsu.Business
{
    public interface IHomeBLL
    {
        List<GROUP> GetHeaderCategories();
        AjaxResponse GetGroupCategories();
        List<GroupViewModel> LoadCategoreis();
    }
    public class HomeBLL: IHomeBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeBLL()
        {
            _unitOfWork =new UnitOfWork();
        }
        private GenericRepository<GROUP> _group;
       // private GenericRepository<TUTORIAL> _TUTORIAL;
        private GenericRepository<CATEGORy> _categories;
        private GenericRepository<PRODUCT> _products;
        private SpRepository _sp;

        public List<GROUP> GetHeaderCategories()
        {
            
                _group = new GenericRepository<GROUP>(_unitOfWork);
                return  _group.Repository.GetAll();
               
        }
        public AjaxResponse GetGroupCategories()
        {
            try
            {
                Dictionary<string, object> response = new Dictionary<string, object>();
                _group = new GenericRepository<GROUP>(_unitOfWork);
                _categories = new GenericRepository<CATEGORy>(_unitOfWork);
                var listObj = _group.Repository.GetAll();
                response.Add("GroupList", listObj);
                var listObjCat = _categories.Repository.GetAll();
                List<GroupViewModel> listGroup = new List<GroupViewModel>();
                if (listObj != null)
                {

                    foreach(var obj in listObj)
                    {

                        listGroup.Add(new GroupViewModel()
                        {
                            GroupId=obj.GROUP_ID,
                            Name=obj.NAME,
                            listCategories= listObjCat.Where(x => x.GROUP_ID==obj.GROUP_ID).ToList()
                        });
                    }
                }
                response.Add("GroupAndCatList", listGroup);

                return new AjaxResponse(true, "", true, response);

            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                return new AjaxResponse(false, ex.Message);
            }
        }
        public List<GroupViewModel> LoadCategoreis()
        {
            try
            {
                Dictionary<string, object> response = new Dictionary<string, object>();
                _group = new GenericRepository<GROUP>(_unitOfWork);
                _categories = new GenericRepository<CATEGORy>(_unitOfWork);
                var listObj = _group.Repository.GetAll();
                response.Add("GroupList", listObj);
                var listObjCat = _categories.Repository.GetAll();
                List<GroupViewModel> listGroup = new List<GroupViewModel>();
                if (listObj != null)
                {

                    foreach (var obj in listObj)
                    {

                        listGroup.Add(new GroupViewModel()
                        {
                            GroupId = obj.GROUP_ID,
                            Name = obj.NAME,
                            listCategories = listObjCat.Where(x => x.GROUP_ID == obj.GROUP_ID).ToList()
                        });
                    }
                }
               

                return listGroup;

            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                return null;
            }
        }


    }
}
