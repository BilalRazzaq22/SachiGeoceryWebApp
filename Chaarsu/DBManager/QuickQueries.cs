using Chaarsu.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Chaarsu.DBManager
{
    public class QuickQueries
    {

        public List<ModelHomeProduct> GetHomeProducts(int CategoryId,int BranchId)
        {
            SqlConnection con = null;
            List<ModelHomeProduct> list = null;
            try
            {
                string efConnectionString = ConfigurationManager.ConnectionStrings["anytimea_GROCERYEntities"].ToString();
                var efConnectionStringBuilder = new EntityConnectionStringBuilder(efConnectionString);
                string sqlConnectionString = efConnectionStringBuilder.ProviderConnectionString;

                con = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand("SpGetHomeProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                SqlDataReader r;
                // Execute the command.
                con.Open();
                r = cmd.ExecuteReader();
                list = new List<ModelHomeProduct>();
                while (r.Read())
                {
                    list.Add(new ModelHomeProduct()
                    {
                        DESCRIPTION = r["DESCRIPTION"] as string,
                        BRAND = r["BRAND"] as string,
                        PRODUCT_NAME_URL = r["PRODUCT_NAME_URL"] as string,
                        CategoryName = r["CategoryName"] as string,
                        COLOR= r["COLOR"] as string,
                        FLAVOR = r["FLAVOR"] as string,
                        IMAGE_THUMBNAIL_PATH = r["IMAGE_THUMBNAIL_PATH"] as string,
                        NAME = r["NAME"] as string,
                        PACKING = r["PACKING"] as string,
                        PRICE1 =Convert.ToInt32(r["PRICE1"]),
                        PRICE = Convert.ToInt32(r["PRICE"]),
                        PRICE2 = Convert.ToInt32(r["PRICE2"]),
                        TAG = r["TAG"] as string,
                        PRODUCT_ID= Convert.ToInt32(r["PRODUCT_ID"])
                    });

                }
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public List<ModelHomeBlogs> GetHomeBlogs()
        {
            SqlConnection con = null;
            List<ModelHomeBlogs> list = null;
            try
            {
                string efConnectionString = ConfigurationManager.ConnectionStrings["anytimea_GROCERYEntities"].ToString();
                var efConnectionStringBuilder = new EntityConnectionStringBuilder(efConnectionString);
                string sqlConnectionString = efConnectionStringBuilder.ProviderConnectionString;

                con = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand("SpGetHomeBlogs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageIndex", 1);
                SqlDataReader r;
                // Execute the command.
                con.Open();
                r = cmd.ExecuteReader();
                list = new List<ModelHomeBlogs>();
                while (r.Read())
                {
                    list.Add(new ModelHomeBlogs()
                    {
                        CreatedDateString = r["CreatedDateString"] as string,
                        BlogHtml =r["BlogHtml"] as string,
                        BlogId =Convert.ToInt32(r["BlogId"]),
                        BlogImageUrl =r["BlogImageUrl"] as string,
                        BlogTitle = r["BlogTitle"] as string,
                        BlogTitleUrl = r["BlogTitleUrl"] as string
                    });

                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public List<ModelProductReviewHome> GetHomeProductReviews()
        {
            SqlConnection con = null;
            List<ModelProductReviewHome> list = null;
            try
            {
                string efConnectionString = ConfigurationManager.ConnectionStrings["anytimea_GROCERYEntities"].ToString();
                var efConnectionStringBuilder = new EntityConnectionStringBuilder(efConnectionString);
                string sqlConnectionString = efConnectionStringBuilder.ProviderConnectionString;

                con = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand("SpGetAllProductReviewsHome", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageIndex", 1);
                SqlDataReader r;
                // Execute the command.
                con.Open();
                r = cmd.ExecuteReader();
                list = new List<ModelProductReviewHome>();
                while (r.Read())
                {
                    list.Add(new ModelProductReviewHome()
                    {
                        DateString = r["DateString"] as string,
                        USERNAME = r["USERNAME"] as string,
                        COMMENT = r["COMMENT"] as string,
                        IMAGE_PATH = r["IMAGE_PATH"] as string,
                        PRODUCT_REVIEW_ID = Convert.ToInt32(r["PRODUCT_REVIEW_ID"]),
                        REVIEW = Convert.ToInt32(r["REVIEW"])
                    });

                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public List<ModelHomeBanner> GetAllBannersHome()
        {
            SqlConnection con = null;
            List<ModelHomeBanner> list = null;
            try
            {
                string efConnectionString = ConfigurationManager.ConnectionStrings["anytimea_GROCERYEntities"].ToString();
                var efConnectionStringBuilder = new EntityConnectionStringBuilder(efConnectionString);
                string sqlConnectionString = efConnectionStringBuilder.ProviderConnectionString;

                con = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand("SpGetAllBannersHome", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageIndex", 1);
                SqlDataReader r;
                // Execute the command.
                con.Open();
                r = cmd.ExecuteReader();
                list = new List<ModelHomeBanner>();
                while (r.Read())
                {
                    list.Add(new ModelHomeBanner()
                    {
                        AdminImagePath = r["AdminImagePath"] as string,
                        Description = r["Description"] as string,
                        BannerTitle = r["BannerTitle"] as string,
                        ImageUrl =r["ImageUrl"] as string
                    });

                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

    }
}