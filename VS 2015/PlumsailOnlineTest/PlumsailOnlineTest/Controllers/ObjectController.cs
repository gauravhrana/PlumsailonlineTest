using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;

namespace PlumsailOnlineTest.Controllers
{
    //[EnableCors("http://patang.in", headers: "*", methods: "*")]
    [EnableCors("http://localhost:1001, http://patang.in", headers: "*", methods: "*")]
    public class ObjectController : ApiController
    {

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/Object
        public List<JObject> Get()
        {
            Log.Debug("GET Request traced");

            var xmlDocObj = new XmlDocument();

            //loading XML File in memory  
            xmlDocObj.Load(HostingEnvironment.MapPath("~/data/ObjectData.xml"));

            //getting the root node
            var rootNode = xmlDocObj.SelectSingleNode("objectRepository");

            var resultArray = new List<JObject>();

            foreach (XmlNode childNode in rootNode.ChildNodes)
            {
                var childData = new JObject();
                //dynamic childData = new ExpandoObject();
                foreach (XmlNode dataNode in childNode.ChildNodes)
                {
                    if (dataNode.ChildNodes.Count > 1)
                    {
                        var subChildData = new JObject();
                        foreach (XmlNode dataChildNode in dataNode.ChildNodes)
                        {
                            subChildData.Add(dataChildNode.Name, dataChildNode.InnerText);
                        }
                        childData.Add(dataNode.Name, subChildData);
                    }
                    else
                    {
                        childData.Add(dataNode.Name, dataNode.InnerText);
                    }
                }
                resultArray.Add(childData);
            }
            return resultArray;
        }

        // GET: api/Object/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Object
        [HttpPost]
        public HttpResponseMessage Post(JObject formData)
        {
            try
            {
                Log.Debug("IN Post Method");

                var xmlDocObj = new XmlDocument();

                //loading XML File in memory  
                xmlDocObj.Load(HostingEnvironment.MapPath("~/data/ObjectData.xml"));

                //getting the root node
                var rootNode = xmlDocObj.SelectSingleNode("objectRepository");

                // creating child element
                var objNode = rootNode.AppendChild(xmlDocObj.CreateNode(XmlNodeType.Element, "object", string.Empty));

                // get to actual data and create nodes accordingly
                foreach (KeyValuePair<string, JToken> sub_obj in (JObject)formData["formData"])
                {
                    var subNode = objNode.AppendChild(xmlDocObj.CreateNode(XmlNodeType.Element, sub_obj.Key, string.Empty));
                    var key = sub_obj.Key;

                    if (sub_obj.Value.HasValues)
                    {
                        var child = (JObject)sub_obj.Value;
                        foreach (KeyValuePair<string, JToken> child_obj in child)
                        {
                            subNode.AppendChild(xmlDocObj.CreateNode(XmlNodeType.Element, child_obj.Key, string.Empty)).InnerText = child_obj.Value.ToString();
                        }
                    }
                    else
                    {
                        var val = sub_obj.Value.ToString();
                        subNode.InnerText = val;
                    }
                }

                Log.Debug("Prepared main object node");

                //save node to the xml file
                xmlDocObj.Save(HostingEnvironment.MapPath("~/data/ObjectData.xml"));

                Log.Debug("Saved in Object Data xml file");

                //return Ok(new { success = true });
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true });
            }
            catch (Exception ex)
            {
                Log.Error("Error: " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, " Employee Not Found");
            }
        }

        // PUT: api/Object/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Object/5
        public void Delete(int id)
        {
        }
    }
}
