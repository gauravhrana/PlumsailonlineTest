using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;

namespace PlumsailOnlineTest.Controllers
{
    [EnableCors("http://localhost:1001", headers: "*", methods: "*")]
    public class ObjectController : ApiController
    {
        // GET: api/Object
        public List<JObject> Get()
        {

            var xmlDocObj = new XmlDocument();

            //loading XML File in memory  
            xmlDocObj.Load(HostingEnvironment.MapPath("~/ObjectData.xml"));

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
        public IHttpActionResult Post(JObject formData)
        {


            var xmlDocObj = new XmlDocument();

            //loading XML File in memory  
            xmlDocObj.Load(HostingEnvironment.MapPath("~/ObjectData.xml"));

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

            //save node to the xml file
            xmlDocObj.Save(HostingEnvironment.MapPath("~/ObjectData.xml"));

            return Ok(new { success = true });

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
