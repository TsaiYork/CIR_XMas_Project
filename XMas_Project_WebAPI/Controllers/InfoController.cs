using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XMas_Project_WebAPI.Models;

namespace XMas_Project_WebAPI.Controllers
{
    public class InfoController : ApiController
    {
        


        [Route("api/HandsomeYorkTsai/GetAll")]
        public IEnumerable<InfoModel.Format_Detail> GetAll()
        {
            InfoModel model = new InfoModel();
            return model.GetAll();
        }

        [Route("api/GetAllStatus")]
        public IEnumerable<InfoModel.Privacy_Detail> GetAllPrivacy()
        {
            InfoModel model = new InfoModel();
            return model.GetAllPrivacy();
        }

        [Route("api/RandomDraw")]
        public HttpResponseMessage GetRandomDraw()
        {
            InfoModel model = new InfoModel();

            if (model.RandomDraw())
            {
                var msg = Request.CreateResponse();
                msg.StatusCode = HttpStatusCode.OK;
                msg.Content = new StringContent("Already Random Receiver");
                return msg;
            }
            else {
                var msg = Request.CreateResponse();
                msg.StatusCode = HttpStatusCode.BadRequest;
                msg.Content = new StringContent("Try again");
                return msg;
            }


        }

        [Route("api/GetDetailById/{id}")]
        public HttpResponseMessage GetDetailById(int id, string password)
        {
            using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
            {

                var L2Enty = entities.Info.Find(id);

                if (!L2Enty.RandomCode.Contains(password))
                {
                    var msg = Request.CreateResponse();
                    msg.StatusCode = HttpStatusCode.BadRequest;
                    msg.Content = new StringContent("Wrong Password!!");
                    return msg;
                }
                else {
                    InfoModel.ById_Detail data = new InfoModel.ById_Detail()
                    {
                        Id = L2Enty.Id,
                        PlayerName = L2Enty.PlayerName,
                        Desire = L2Enty.Desire,
                        Tip1 = L2Enty.Tip1,
                        Tip2 = L2Enty.Tip2,
                        Tip3 = L2Enty.Tip3
                    };
                    return Request.CreateResponse(HttpStatusCode.OK,data);

                }    
            }
        }

        [Route("api/GetTips/{id}")]
        public HttpResponseMessage GetTips(int id, string password)
        {
            using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
            {
                var L2Enty = entities.Info.Find(id);

                if (!L2Enty.RandomCode.Contains(password))
                {
                    var msg = Request.CreateResponse();
                    msg.StatusCode = HttpStatusCode.BadRequest;
                    msg.Content = new StringContent("Wrong Password!!");
                    return msg;
                }
                else
                {
                    var Receiver = entities.Info.Find(L2Enty.Receiver);
                    InfoModel.Tips_Detail data = new InfoModel.Tips_Detail()
                    {
                        Tip1 = Receiver.Tip1,
                        Tip2 = Receiver.Tip2,
                        Tip3 = Receiver.Tip3
                    };
                    L2Enty.Draw = true;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, data);

                }
            }
        }

        [Route("api/PutPassword/{id}")]
        public HttpResponseMessage PutPassword(int id, [FromBody]InfoModel.Password_Detail password)
        {
            try
            {
                using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
                {
                    var L2Enty = entities.Info.Find(id);
                    if (L2Enty == null)
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Id not Found");
                        return msg;
                    }
                    else if (!L2Enty.RandomCode.Contains(password.oldPassword))
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Wrong Password!!");
                        return msg;
                    }
                    else
                    {
                        L2Enty.RandomCode = password.newPassword;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.BadRequest, L2Enty.RandomCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/PutApply/{id}")]
        public HttpResponseMessage PutTips(int id, string password,[FromBody]InfoModel.Apply_Detail apply)
        {
            try
            {
                using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
                {
                    var L2Enty = entities.Info.Find(id);
                    if (L2Enty == null)
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Id not Found");
                        return msg;
                    }
                    else if (!L2Enty.RandomCode.Contains(password))
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Wrong Password!!");
                        return msg;
                    }
                    else if (L2Enty.Apply)
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Already Apply");
                        return msg;
                    }
                    else
                    {
                        L2Enty.Desire = apply.Desire;
                        L2Enty.Tip1 = apply.Tip1;
                        L2Enty.Tip2 = apply.Tip2;
                        L2Enty.Tip3 = apply.Tip3;
                        L2Enty.Apply = true;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.BadRequest, apply);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/HandsomeYorkTsai/Status/{id}")]
        public HttpResponseMessage PutStatus(int id, [FromBody]InfoModel.Status_Detail apply)
        {
            try
            {
                using (XMas_DatabaseEntities entities = new XMas_DatabaseEntities())
                {
                    var L2Enty = entities.Info.Find(id);
                    if (L2Enty == null)
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Id not Found");
                        return msg;
                    }
                    else if (!apply.Auth.Contains("821101"))
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Wrong Password!!");
                        return msg;
                    }
                    else if (L2Enty.Apply)
                    {
                        var msg = Request.CreateResponse();
                        msg.StatusCode = HttpStatusCode.BadRequest;
                        msg.Content = new StringContent("Already Apply");
                        return msg;
                    }
                    else
                    {
                        L2Enty.Apply = apply.Apply;
                        L2Enty.Draw = apply.Draw;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.BadRequest, apply);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}

