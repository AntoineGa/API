﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linq2Azure
{
    public class Deployment
    {
        public CloudService Parent { get; private set; }

        protected Subscription Subscription { get { return Parent.Subscription; } }

        public Deployment()
        {
            Configuration = new ServiceConfiguration();
        }

        internal Deployment(XElement element, CloudService parent)
        {
            Contract.Requires(element != null);
            Contract.Requires(parent != null);

            Parent = parent;
            DeploymentName = (string)element.Element(XmlNamespaces.Base + "Name");
            Url = (string)element.Element(XmlNamespaces.Base + "Url");
            DeploymentSlot = (string)element.Element(XmlNamespaces.Base + "DeploymentSlot");
            PrivateID = (string)element.Element(XmlNamespaces.Base + "PrivateID");
            Label = ((string)element.Element(XmlNamespaces.Base + "Label")).FromBase64String();
            Configuration = new ServiceConfiguration(element.Element(XmlNamespaces.Base + "Configuration"));
        }

        public string DeploymentName { get; set; }
        public string Url { get; private set; }
        public string DeploymentSlot { get; set; }
        public string PrivateID { get; private set; }
        public string Label { get; set; }
        public ServiceConfiguration Configuration { get; set; }

        //        public string UpdateDeploymentConfiguration()
        //        {
        //            Uri requestUri = null;

        //            const string changeConfig = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //                <ChangeConfiguration xmlns=""http://schemas.microsoft.com/windowsazure"">
        //                    <Configuration>{0}</Configuration>
        //                </ChangeConfiguration>";

        //            string configData = Convert.ToBase64String(Encoding.ASCII.GetBytes(ConfigurationXml));

        //            string requestBody = string.Format(changeConfig, configData);

        //            // Create the request.
        //            requestUri = new Uri("https://management.core.windows.net/"
        //                                 + Subscription.SubscriptionId
        //                                 + "/services/"
        //                                 + "hostedservices/"
        //                                 + Parent.ServiceName + "/"
        //                                 + "deploymentslots/"
        //                                 + DeploymentSlot.ToLower() + "/"
        //                                 + "?comp=config");

        //            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(requestUri);
        //            httpWebRequest.Method = "POST";
        //            httpWebRequest.ContentType = "application/xml";
        //            httpWebRequest.ContentLength = Encoding.UTF8.GetBytes(requestBody).Length;

        //            // Add the certificate to the request.
        //            httpWebRequest.ClientCertificates.Add(Subscription.ManagementCertificate);

        //            // Specify the version information in the header.
        //            httpWebRequest.Headers.Add("x-ms-version", "2012-03-01");

        //            using (var sw = new StreamWriter(httpWebRequest.GetRequestStream()))
        //            {
        //                sw.Write(requestBody);
        //                sw.Close();
        //            }

        //            // Make the call using the web request.
        //            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        //            // TODO: handle other status codes?
        //            // Display the web response status code.
        //            //Console.WriteLine("Response status code: " + httpWebResponse.StatusCode);

        //            string xmlResponse;

        //            // Parse the web response.
        //            using (var responseStream = httpWebResponse.GetResponseStream())
        //            {
        //                using (var reader = new StreamReader(responseStream))
        //                {
        //                    xmlResponse = reader.ReadToEnd();
        //                    reader.Close();
        //                }
        //                // Close the resources no longer needed.
        //                httpWebResponse.Close();
        //            }

        //            return xmlResponse;

        //            //var xdocument = XDocument.Parse(xmlResponse);
        //            //return xdocument.Descendants(@Constants.AzureXmlNamespace + "Deployment").Select(x => Deployment.LoadAttached(x, _subscription, this)).ToArray();
        //        }
    }
}