using System.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Chaarsu.Models;
using Chaarsu.Repository.ADO;
using Chaarsu.Models.InputModel;

namespace Chaarsu.Repository.SPRepository
{
    
    public class SpRepository
    {
        private readonly anytimea_GROCERYEntities db;
        private Int64 _RetVal = 0;
        private string _ErrorDescription = "";
        private double _RetVal2 = 0;


        public SpRepository()
        {
            db = new anytimea_GROCERYEntities();
        }

        public Int64 RetVal
        {
            get
            {
                return _RetVal;
            }
            set
            {
                _RetVal = value;
            }
        }

        public double RetVal2
        {
            get
            {
                return _RetVal2;
            }
            set
            {
                _RetVal2 = value;
            }
        }

        public string ErrorDescription
        {
            get
            {
                return _ErrorDescription;
            }
            set
            {
                _ErrorDescription = value;
            }
        }

        public int CreateCardField(string ColumnName,string ColumnType,string ColumnLength,string TableName,bool AllowNull)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            int result=0;

            sCmd.Parameters.Add(new SqlParameter("@ColumnName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, ColumnName));
            sCmd.Parameters.Add(new SqlParameter("@ColumnType", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, ColumnType));
            sCmd.Parameters.Add(new SqlParameter("@ColumnLength", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, ColumnLength));
            sCmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, TableName));
            sCmd.Parameters.Add(new SqlParameter("@AllowNull", SqlDbType.Bit, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, true));


            result = sqlManager.ExecuteNonQuery("SpCreateCustomColumns", SqlCommandType.StoredProcedure, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return result;
        }

        public int InsertDataDynamic(string ColumnNames,string ColumnValues, string TableName)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            int result = 0;
            
            result = sqlManager.ExecuteNonQuery("Insert into "+TableName+"("+ ColumnNames + ")values("+ ColumnValues + ")", SqlCommandType.Text, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return result;
        }

        public DataTable SpGetAllPrograms(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            DataTable dt = null;

            sCmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageIndex));
            sCmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            sCmd.Parameters.Add(new SqlParameter("@SortColumn", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortColumn));
            sCmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortOrder));
            sCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SearchText));

            dt = sqlManager.ExecuteDataTable("SpGetAllPrograms", SqlCommandType.StoredProcedure, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return dt;
        }

        public DataTable SpGetAllCompanies(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            DataTable dt = null;

            sCmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageIndex));
            sCmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            sCmd.Parameters.Add(new SqlParameter("@SortColumn", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortColumn));
            sCmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortOrder));
            sCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SearchText));

            dt = sqlManager.ExecuteDataTable("SpGetAllCompanies", SqlCommandType.StoredProcedure, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return dt;
        }

        public DataTable SpGetAllGroups(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            DataTable dt = null;

            sCmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageIndex));
            sCmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            sCmd.Parameters.Add(new SqlParameter("@SortColumn", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortColumn));
            sCmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortOrder));
            sCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SearchText));

            dt = sqlManager.ExecuteDataTable("SpGetAllGroups", SqlCommandType.StoredProcedure, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return dt;
        }

        public DataTable SpGetAllUsers(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            DataTable dt = null;

            sCmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageIndex));
            sCmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            sCmd.Parameters.Add(new SqlParameter("@SortColumn", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortColumn));
            sCmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortOrder));
            sCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SearchText));

            dt = sqlManager.ExecuteDataTable("SpGetAllUsers", SqlCommandType.StoredProcedure, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return dt;
        }

        public DataTable SpGetAllCards(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText,string UserId)
        {
            var sqlManager = new SqlManager();
            var sCmd = new SqlCommand();
            DataTable dt = null;

            sCmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageIndex));
            sCmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, PageSize));
            sCmd.Parameters.Add(new SqlParameter("@SortColumn", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortColumn));
            sCmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SortOrder));
            sCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, SearchText));
            sCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Default, UserId));

            dt = sqlManager.ExecuteDataTable("SpGetAllCards", SqlCommandType.StoredProcedure, sCmd);
            _ErrorDescription = sqlManager.ErrorDescription;


            if (_ErrorDescription != "")
            {
                throw new Exception(_ErrorDescription);
            }
            else
            {

            }

            return dt;
        }

        public List<SpGetAllBlogs_Result> SpGetAllBlogs(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                return db.SpGetAllBlogs(PageIndex,PageSize,SortColumn,SortOrder,SearchText).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public SpGetBlogDetailByBlogTitleUrl_Result GetBlogDetailByBlogTitleUrl(string blogTitleUrl)
        {
            try
            {
                return db.SpGetBlogDetailByBlogTitleUrl(blogTitleUrl).FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SpGetAllProducts_Result> SpGetAllProducts(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, string CategoryId, string SubCategoryId, string GroupId,int BranchId)
        {
            try
            {
             
                return db.SpGetAllProducts(PageIndex, PageSize, SortColumn, SortOrder, SearchText,CategoryId,SubCategoryId,GroupId, BranchId).ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SP_GetAllProductsByCategories_Result> GetAllProductsByCategoryId(int PageIndex, int PageSize,string CategoryId,int branchId)
        {
            try
            {
                return db.SP_GetAllProductsByCategories(PageIndex, PageSize, Convert.ToInt32(CategoryId), branchId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SpGetProductDetailByProductNameUrl_Result GetProductDetailByProductNameUrl(string productNameUrl,int BranchId)
        {
            try
            {
                return db.SpGetProductDetailByProductNameUrl(productNameUrl, BranchId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SpGetProductImagesByProductId_Result> GetProductImagesById(int id)
        {
            try
            {
                return db.SpGetProductImagesByProductId(id).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SpGetProductReviewsByProductId_Result> GetProductReviewsById(int PRODUCT_ID,int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                return db.SpGetProductReviewsByProductId(PRODUCT_ID, PageIndex, PageSize, SortColumn,SortOrder, SearchText).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SpGetAllProductReviews_Result> GetAllProductReviews(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                return db.SpGetAllProductReviews(PageIndex, PageSize, SortColumn, SortOrder, SearchText).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SpGetAllBanners_Result> GetAllBanners(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                return db.SpGetAllBanners(PageIndex, PageSize, SortColumn, SortOrder, SearchText).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SpGetAllOrdersByCustomerId_Result> GetAllOrders(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, int OrderStatusId,int CustomerId)
        {
            try
            {
                return db.SpGetAllOrdersByCustomerId(PageIndex, PageSize, SortColumn, SortOrder, SearchText, OrderStatusId, CustomerId).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<SpGetWishList_Result> GetWishesProduct(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, int UserId)
        {
            try
            {
                return db.SpGetWishList(PageIndex, PageSize, SortColumn, SortOrder, SearchText,UserId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SpGetAllCartItemsByUserId_Result> GetAllCartItems(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, int UserId)
        {
            try
            {
                return db.SpGetAllCartItemsByUserId(PageIndex, PageSize, SortColumn, SortOrder, SearchText, UserId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
