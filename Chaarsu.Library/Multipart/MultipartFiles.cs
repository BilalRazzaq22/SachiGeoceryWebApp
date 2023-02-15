using Chaarsu.Library.Utilities;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Net;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Configuration;
using System.Diagnostics;
using System.Security;
using System.Web.Hosting;

namespace Chaarsu.Library.Multipart
{


    public static class MultipartFiles
    {
        //string _FolderPath = "Messageimages" + @"\" + _MessageID;
        // string _FolderSite = "Messageimages" + "/" + _MessageID;

        // var imgResponse = MultipartFiles.GetMultipartImage(HttpContext.Current.Request.Files, "Image", _FolderPath, _imagethumbsize, _imagethumbsize, _imageSize, _imageSize, true, true, true, _FolderSite);
        public static ImageResponse GetMultipartImage(
                                    HttpFileCollection files, string fileKey, string folderPath,
                                    int thumbWidth, int thumbHeight,
                                    int imgWidth, int imgHeight,
                                    string folderPathSite,
                                    bool deleteOriginal = false, bool GenThumbnail = true,
                                    bool GenImage = true, bool GenBanner = false)
        {

            string _SiteRoot = WebConfigurationManager.AppSettings["RootPath"];
            string _SiteURL = WebConfigurationManager.AppSettings["WebPath"];


            var file = files.Count > 0 ? files[fileKey] : null;
            if (file == null)
            {
                 file = files.Count > 0 ? files[0] : null;
            }
            ImageResponse response = new ImageResponse();

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

                if (arr.Contains(extension))
                {
                    string _NewFileName = Guid.NewGuid() + extension;

                    string tempfilePath = _SiteRoot + @"\" + "TempFolder" + @"\" + _NewFileName;
                    Helper.CreateDirectories(_SiteRoot + @"\" + "TempFolder"  + @"\");

                    string newFilePath = _SiteRoot + @"\" + folderPath + @"\";
                    Helper.CreateDirectories(_SiteRoot + @"\" + folderPath + @"\");

                    

                    string strIamgeURLfordb = _SiteURL + "/" + folderPathSite + "/" + _NewFileName;

                    file.SaveAs(tempfilePath);
                    string thumbnailresizename = "";
                    string Imageresizename = "";
                    string ImageBannerresizename = "";
                    string imageresizenametmp = "";

                    if (GenThumbnail == true)
                    {
                        thumbnailresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "_T_" + _NewFileName, thumbWidth, thumbHeight);
                    }

                    if (GenImage == true)
                    {
                        //Scale up the image 
                        //imageresizenametmp = ResizeImage.ScaleImage(tempfilePath, tempfilePath, "_S_" + _NewFileName, 650, 650);

                        Imageresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "_A_" + _NewFileName, imgWidth, imgHeight);
                        //Imageresizename = ResizeImage.CropImage(tempfilePath , newFilePath, "_A_" + imageresizenametmp, 0, 50, 640, 360);
                    }

                    if (GenBanner == true)
                    {
                        //Banner Image (event listing)
                        ImageBannerresizename = ResizeImage.CropImage(tempfilePath, newFilePath, "_B_" + imageresizenametmp, 0, 100, 640, 270);
                    }


                    if (deleteOriginal == true)
                    {
                        if (File.Exists(tempfilePath))
                        {
                            File.Delete(tempfilePath);
                        }
                    }

                    response.ThumbnailURL = _SiteURL + folderPathSite + "/" + thumbnailresizename;
                    response.ImageURL = _SiteURL + folderPathSite + "/" + Imageresizename;
                    response.BannerImage_URL = _SiteURL + folderPathSite +"/" + ImageBannerresizename;
                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File Extension not supported";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "File Not found";

                return response;
            }



        }



       

        public static ImageResponse GetMultipartImagePosted(
                                  HttpPostedFileBase file, string fileKey, string folderPath,
                                  int thumbWidth, int thumbHeight,
                                  int imgWidth, int imgHeight,
                                  string folderPathSite,
                                  bool deleteOriginal = false, bool GenThumbnail = true,
                                  bool GenImage = true, bool GenBanner = false)
        {
           
            string _SiteRoot = HostingEnvironment.MapPath("~");
            string _SiteURL = WebConfigurationManager.AppSettings["WebPath"];


            ImageResponse response = new ImageResponse();

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

                if (arr.Contains(extension))
                {
                    string _NewFileName = Guid.NewGuid() + extension;

                    string tempfilePath = _SiteRoot + @"\" + "TempImages" + @"\" + _NewFileName;
                    Helper.CreateDirectories(_SiteRoot + @"\" + "TempImages" + @"\");

                    string newFilePath = _SiteRoot + @"\" + folderPath + @"\" + _NewFileName;
                    Helper.CreateDirectories(_SiteRoot + @"\" + folderPath + @"\");


                    string strIamgeURLfordb = _SiteURL + "/" + folderPathSite + "/" + _NewFileName;

                    file.SaveAs(newFilePath);
                    string thumbnailresizename = "";
                    string Imageresizename = "";
                    string ImageBannerresizename = "";
                    string imageresizenametmp = "";

                    if (GenThumbnail == true)
                    {
                        thumbnailresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "_T_" + _NewFileName, thumbWidth, thumbHeight);
                    }

                    if (GenImage == true)
                    {
                        //Scale up the image 
                        //imageresizenametmp = ResizeImage.ScaleImage(tempfilePath, tempfilePath, "_S_" + _NewFileName, 650, 650);

                        Imageresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "_A_" + _NewFileName, imgWidth, imgHeight);
                        //Imageresizename = ResizeImage.CropImage(tempfilePath , newFilePath, "_A_" + imageresizenametmp, 0, 50, 640, 360);
                    }

                    if (GenBanner == true)
                    {
                        //Banner Image (event listing)
                        ImageBannerresizename = ResizeImage.CropImage(tempfilePath, newFilePath, "_B_" + imageresizenametmp, 0, 100, 640, 270);
                    }


                    //if (deleteOriginal == false)
                    //{
                    //    if (File.Exists(tempfilePath))
                    //    {
                    //        File.Delete(tempfilePath);
                    //    }
                    //}


                    response.ThumbnailURL = _SiteURL + "/" + folderPathSite + "/" + thumbnailresizename;
                    response.ImageURL = _SiteURL + "/" + folderPathSite + "/" + Imageresizename;
                    response.BannerImage_URL = _SiteURL + "/" + folderPathSite + "/" + ImageBannerresizename;
                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File Extension not supported";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "File Not found";

                return response;
            }



        }

        public static ImageResponse GetMultipartImage(
                                  HttpFileCollectionBase files, string fileKey, string folderPath,
                                  int thumbWidth, int thumbHeight,
                                  int imgWidth, int imgHeight,
                                  string folderPathSite,
                                  bool deleteOriginal = false, bool GenThumbnail = true,
                                  bool GenImage = true, bool GenBanner = false)
        {

            string _SiteRoot = WebConfigurationManager.AppSettings["SiteImgPath"];
            string _SiteURL = WebConfigurationManager.AppSettings["SiteImgURL"];


            var file = files.Count > 0 ? files[fileKey] : null;
            ImageResponse response = new ImageResponse();

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

                if (arr.Contains(extension))
                {
                    string _NewFileName = Guid.NewGuid() + extension;

                    string tempfilePath = _SiteRoot + @"\" + "TempImages" + @"\" + _NewFileName;
                    Helper.CreateDirectories(_SiteRoot + @"\" + "TempImages" + @"\");

                    string newFilePath = _SiteRoot + @"\" + folderPath + @"\";
                    Helper.CreateDirectories(_SiteRoot + @"\" + folderPath + @"\");


                    string strIamgeURLfordb = _SiteURL + "/" + folderPathSite + "/" + _NewFileName;

                    file.SaveAs(tempfilePath);
                    string thumbnailresizename = "";
                    string Imageresizename = "";
                    string ImageBannerresizename = "";
                    string imageresizenametmp = "";

                    if (GenThumbnail == true)
                    {
                        thumbnailresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "_T_" + _NewFileName, thumbWidth, thumbHeight);
                    }

                    if (GenImage == true)
                    {
                        //Scale up the image 
                        //imageresizenametmp = ResizeImage.ScaleImage(tempfilePath, tempfilePath, "_S_" + _NewFileName, 650, 650);

                        Imageresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "_A_" + _NewFileName, imgWidth, imgHeight);
                        //Imageresizename = ResizeImage.CropImage(tempfilePath , newFilePath, "_A_" + imageresizenametmp, 0, 50, 640, 360);
                    }

                    if (GenBanner == true)
                    {
                        //Banner Image (event listing)
                        ImageBannerresizename = ResizeImage.CropImage(tempfilePath, newFilePath, "_B_" + imageresizenametmp, 0, 100, 640, 270);
                    }


                    if (deleteOriginal == true)
                    {
                        if (File.Exists(tempfilePath))
                        {
                            File.Delete(tempfilePath);
                        }
                    }


                    response.ThumbnailURL = _SiteURL + "/" + folderPathSite + "/" + thumbnailresizename;
                    response.ImageURL = _SiteURL + "/" + folderPathSite + "/" + Imageresizename;
                    response.BannerImage_URL = _SiteURL + "/" + folderPathSite + "/" + ImageBannerresizename;
                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File Extension not supported";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "File Not found";

                return response;
            }



        }

        //[HttpPost]
        //[Route("api/v1/Assets/AddAssetVideo")]
        //public async Task<HttpResponseMessage> AddAssetVideo()
        //{
        //    try
        //    {

        //        var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];

        //        if (httpContext.Request.Form["UserID"] == "")
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, JsonResponse.GetResponse(ResponseCode.Info, "Please provide UserID"));
        //        }


        //        _AssetsRepo = new GenericRepository<Assets>(_unitOfWork);
        //        _UsersRepo = new GenericRepository<Users>(_unitOfWork);
        //        _PostsRepo = new GenericRepository<Posts>(_unitOfWork);

        //        int userId = Convert.ToInt16(httpContext.Request.Form["UserID"]);


        //        var entity = _UsersRepo.Repository.Get(p => p.UserID == userId);


        //        //Check whether user exists
        //        if (entity == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                JsonResponse.GetResponse(ResponseCode.Info, Response.UserNotFound));
        //        }


        //        //Check whether the image file is provided
        //        if (HttpContext.Current.Request.Files.Count == 0)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                JsonResponse.GetResponse(ResponseCode.Info, "Video file is not provided", "UserData"));
        //        }

        //        var imgResponse = MultipartFiles.GetVideo(HttpContext.Current.Request.Files, "Video", userId.ToString());

        //        // Check whether the image file is created successfully
        //        if (!imgResponse.IsSuccess)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                JsonResponse.GetResponse(ResponseCode.Info, imgResponse.ResponseMessage));
        //        }

        //        //_unitOfWork.StartTransaction();

        //        Assets _Assets = new Assets();

        //        _Assets.AssetType = EPostType.Video.ToString();
        //        _Assets.ThumbnailURL = imgResponse.ThumbnailURL;
        //        _Assets.AssetURL = imgResponse.ImageURL;
        //        _Assets.CreatedDate = DateTime.Now;
        //        _Assets.RecordStatus = RecordStatus.Active.ToString();
        //        _Assets.UserID = userId;
        //        _Assets.AssetSort = 0;

        //        _AssetsRepo.Repository.Add(_Assets);

        //        //Adding posts
        //        var _NextSort = _PostsRepo.Repository.GetAll(p => p.UserID == userId).OrderByDescending(p => p.PostSort).FirstOrDefault().PostSort.ToString();

        //        Posts _Posts = new Posts();
        //        _Posts.AssetsID = _Assets.AssetsID;
        //        _Posts.UserID = userId;
        //        _Posts.PostType = EPostType.Image.ToString();
        //        _Posts.RecordStatus = RecordStatus.Active.ToString();
        //        _Posts.CreatedDate = DateTime.Now;
        //        _Posts.PostSort = Convert.ToInt16(_NextSort) + 1;

        //        _PostsRepo.Repository.Add(_Posts);

        //        //  _unitOfWork.Commit();


        //        Dictionary<string, string> _dic = new Dictionary<string, string>();
        //        _dic.Add("AssetID", _Assets.AssetsID.ToString());
        //        _dic.Add("ThumbnailURL", _SiteImgURL + _Assets.ThumbnailURL);

        //        return Request.CreateResponse(HttpStatusCode.OK, JsonResponse.GetResponse(ResponseCode.Success, _dic, "Assets"));

        //    }
        //    catch (Exception ex)
        //    {
        //        //if (_ApiLogger==true) LogHelper.CreateLog(ex);
        //        return Request.CreateResponse(HttpStatusCode.OK, JsonResponse.GetResponse(ResponseCode.Failure, ex.Message));

        //    }
        //}

        public static ImageResponse GetMultipartImage2(
                                  HttpFileCollection files, string fileKey, string folderPath,
                                  int thumbWidth, int thumbHeight,
                                  int imgWidth, int imgHeight,
                                  string folderPathSite,string _Filename,
                                  bool deleteOriginal = false, bool GenThumbnail = false,
                                  bool GenImage = true, bool GenBanner = false)
        {

            string _SiteRoot = WebConfigurationManager.AppSettings["SiteImgPath"];
            string _SiteURL = WebConfigurationManager.AppSettings["SiteImgURL"];


            var file = files.Count > 0 ? files[fileKey] : null;
            ImageResponse response = new ImageResponse();

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

                if (arr.Contains(extension))
                {
                    string _NewFileName = Guid.NewGuid() + extension;

                    string tempfilePath = _SiteRoot + @"\" + "TempImages" + @"\" + _Filename;
                    Helper.CreateDirectories(_SiteRoot + @"\" + "TempImages" + @"\");

                    string newFilePath = _SiteRoot + @"\" + folderPath + @"\";
                    Helper.CreateDirectories(_SiteRoot + @"\" + folderPath + @"\");


                    string strIamgeURLfordb = _SiteURL + "/" + folderPathSite + "/" + _Filename;

                    file.SaveAs(tempfilePath);
                    string thumbnailresizename = "";
                    string Imageresizename = "";
                    string ImageBannerresizename = "";
                    string imageresizenametmp = "";

                    if (GenThumbnail == true)
                    {
                        thumbnailresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, "T_" + _Filename, thumbWidth, thumbHeight);
                    }

                    if (GenImage == true)
                    {
                        //Scale up the image 
                        //imageresizenametmp = ResizeImage.ScaleImage(tempfilePath, tempfilePath, "_S_" + _NewFileName, 650, 650);

                        Imageresizename = ResizeImage.Resize_Image_Thumb(tempfilePath, newFilePath, _Filename, imgWidth, imgHeight);
                        //Imageresizename = ResizeImage.CropImage(tempfilePath , newFilePath, "_A_" + imageresizenametmp, 0, 50, 640, 360);
                    }

                    if (GenBanner == true)
                    {
                        //Banner Image (event listing)
                        ImageBannerresizename = ResizeImage.CropImage(tempfilePath, newFilePath, "_B_" + imageresizenametmp, 0, 100, 640, 270);
                    }


                    if (deleteOriginal == true)
                    {
                        if (File.Exists(tempfilePath))
                        {
                            File.Delete(tempfilePath);
                        }
                    }


                    response.ThumbnailURL =  folderPathSite + "/" + thumbnailresizename;
                    response.ImageURL =  folderPathSite + "/" + Imageresizename;
                    response.BannerImage_URL =  folderPathSite + "/" + ImageBannerresizename;
                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = "File Extension not supported";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "File Not found";

                return response;
            }



        }


        public static ImageResponse GetVideo(HttpFileCollection files, string fileKey, string folderPath)
        {
            ImageResponse response = new ImageResponse();

            var file = files.Count > 0 ? files[fileKey] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName).ToLower();
                string videoName = Guid.NewGuid() + extension;

                string currentYear = DateTime.Now.Year.ToString();
                string currentMonth = DateTime.Now.Month.ToString();

                string VideoPath = ConfigurationManager.AppSettings["SiteImgPath"].ToString() + @"\" + folderPath + "\\Videos\\";
                string VideoUrl = ConfigurationManager.AppSettings["SiteImgURL"].ToString() + "/" + folderPath + "/Videos/" + videoName;

                if (!Directory.Exists(VideoPath))
                {
                    Directory.CreateDirectory(VideoPath);
                }
                file.SaveAs(VideoPath + videoName);



                response.ThumbnailURL = "/" + folderPath + "/Videos/" + videoName;
                response.ImageURL = "/" + folderPath + "/Videos/" + videoName;
                response.IsSuccess = true;
                response.ResponseMessage = "";

                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "";
                return response;
            }
        }




        public static ResumeResponse SaveResume(HttpFileCollectionBase files, string fileKey, string folderPath)
        {
            var response = new ResumeResponse();

            var file = files.Count > 0 ? files[fileKey] : null;
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName).ToLower();
                string videoName = Guid.NewGuid() + extension;

                string currentYear = DateTime.Now.Year.ToString();
                string currentMonth = DateTime.Now.Month.ToString();

                string ResumePath = ConfigurationManager.AppSettings["SiteImgPath"].ToString() + @"\" + folderPath + "\\Resumes\\";
                string ResumeUrl = ConfigurationManager.AppSettings["SiteImgURL"].ToString() + "/" + folderPath + "/Resumes/" + videoName;

                if (!Directory.Exists(ResumePath))
                {
                    Directory.CreateDirectory(ResumePath);
                }
                file.SaveAs(ResumePath + videoName);
                
                response.Resume = fileName;
                response.ResumeUrl = ResumeUrl;
                response.IsSuccess = true;
                response.ResponseMessage = "";

                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage = "";
                return response;
            }
        }

        public static ImageResponse UploadVideo(HttpFileCollection files, string fileKey, string folderPath)
        {
            var file = files.Count > 0 ? files[fileKey] : null;
            ImageResponse response = new ImageResponse();
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName).ToLower();
                string[] arr = new string[] { ".avi", ".flv", ".wmv", ".mov", ".mp4", ".asf", ".webm" };

                if (arr.Contains(extension))
                {
                    string photoName = Guid.NewGuid() + extension;
                    string thumb = Guid.NewGuid() + extension;
                    string resolution = "300*300";

                    string FilePath = ConfigurationManager.AppSettings["RootPath"] + folderPath + photoName;
                    string newFilePath = ConfigurationManager.AppSettings["RootPath"] + folderPath + "\\";
                    string newFilePathNew = ConfigurationManager.AppSettings["RootPath"] + folderPath;
                    if (!Directory.Exists(newFilePath))
                    {
                        Directory.CreateDirectory(newFilePath);
                    }
                    file.SaveAs(FilePath);

                    //Create thumbnail      
                    string imageName = "Thumbnail" + Guid.NewGuid() + ".jpg";
                    try
                    {
                        // New Code
                      
                            //Create thumbnail                                     
                            string thumbnailMaker = ConfigurationManager.AppSettings["RootPath"].ToString() + @"ThumbNailExe\ffmpeg.exe";
                            Process p = new Process();
                            p.StartInfo.FileName = thumbnailMaker;
                            p.StartInfo.Arguments = "-i " + FilePath + " -ss 00:00:02.435 -s " + resolution + " -vframes 1 -f image2 -vcodec mjpeg " + newFilePathNew + imageName;
                            p.Start();
                            //End thumbnail  

                        
                    }
                    catch (Exception ex)
                    {
                        LogHelper.CreateLog(ex);
                    }

                    //End thumbnail                

                    response.ImageURL = ConfigurationManager.AppSettings["WebPath"].ToString() + "content/" + folderPath.Replace("\\", "/") + photoName;
                    response.ThumbnailURL = ConfigurationManager.AppSettings["WebPath"].ToString() + "content/" + folderPath.Replace("\\", "/") + imageName;

                    response.ImageURL = response.ImageURL.Replace("\\", "/");
                    response.ImageURL = response.ImageURL.Replace(@"\", "/");

                    response.ThumbnailURL = response.ThumbnailURL.Replace("\\", "/");
                    response.ThumbnailURL = response.ThumbnailURL.Replace(@"\", "/");

                    response.IsSuccess = true;
                    response.ResponseMessage = "";

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMessage ="File extension not supported.";

                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMessage ="File not found.";

                return response;
            }
        }
    }




    public class ImageResponse
    {
        public string ThumbnailURL { get; set; }
        public string ImageURL { get; set; }

        public string BannerImage_URL { get; set; }

        public bool IsSuccess { get; set; }

        public string ResponseMessage { get; set; }
    }

    public class ResumeResponse
    {
        public string Resume { get; set; }
        public string ResumeUrl { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }

}