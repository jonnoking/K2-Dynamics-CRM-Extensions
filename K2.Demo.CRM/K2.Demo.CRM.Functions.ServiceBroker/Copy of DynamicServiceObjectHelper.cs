using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.Services;
using System.Net;
using K2.PSUK.ServiceObjectSchema;
using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using RestSharp;

namespace K2.Demo.CRM.REST.ServiceBroker
{
    public class DynamicServiceObjectHelper
    {
        public ServiceInstance SvcI;
        public ServiceAssemblyBase SvcBase;

        public K2CRMConfig config;

        public DynamicServiceObjectHelper(ServiceAssemblyBase svcBase)
        {
            //when an instance of this class is created set the internal variables to 
            //the base class
            SvcBase = svcBase;
            SvcI = svcBase.Service;

        }

        public void DescribeSchema()
        {
            string schemaPath = Environment.CurrentDirectory + @"\K2.Demo.CRM.REST.ServiceBroker.xml";
            SchemaObject schemaObject = SchemaManager.LoadSchemaXMLFile(schemaPath);

            //Populate the details of the current instance of the service base and create an instance of the service object

            SvcI.Name = schemaObject.ServiceInstanceName;
            SvcI.MetaData.DisplayName = schemaObject.ServiceInstanceDisplayName;
            SvcI.MetaData.Description = schemaObject.ServiceInstanceDescription;

            //Create the service object instance
            ServiceObject SO = new ServiceObject(schemaObject.ServiceInstanceName);
            SO.MetaData.DisplayName = schemaObject.ServiceInstanceDisplayName;
            SO.Active = true;

            //Create the properties for the serviceObject
            //Create a property for the serviceObject
            Property prop = null;

            //Dynamic Properties;
            foreach (SchemaObject.SchemaProperty sProp in schemaObject.SchemaProperties)
            {
                prop = SchemaManager.CreateProperty(sProp.Name,sProp.DisplayName, sProp.Description, sProp.TrueType,sProp.K2Type);
                //Add this property to the service object properties collection
                SO.Properties.Add(prop);
            }

            Method meth = null;
            foreach (SchemaObject.SchemaMethod sMeth in schemaObject.SchemaMethods)
            {
                meth = SchemaManager.CreateMethod(sMeth.Name,sMeth.DisplayName,sMeth.Description,sMeth.K2Type,sMeth.InputProperties,sMeth.RequiredProperties,sMeth.ReturnProperties);
                SO.Methods.Add(meth);
            }

            SvcI.ServiceObjects.Add(SO);
        }

        public void Execute()
        {
            SvcI = SvcBase.Service;

            config = GetK2CRMConfig(SvcI.ServiceConfiguration.ServiceAuthentication.UserName, SvcI.ServiceConfiguration.ServiceAuthentication.Password, GetConfigPropertyValue("RESTServiceURL"));

            ServiceObject so = SvcI.ServiceObjects[0];
            string methodName = so.Methods[0].Name;
            switch (methodName.ToLower())
            {
                case "changeowner":
                    ChangeOwner(ref so);
                    break;
                case "setstatestatus":
                    SetStateStatus(ref so);
                    break;
                case "getentities":
                    GetEntities(ref so);
                    break;
                case "bulkactiontasks":
                    BulkActionTasks(ref so);
                    break;
                case "createtask":
                    CreateTask(ref so);
                    break;
                case "getcrmuser":
                    GetCRMUser(ref so);
                    break;
            }
        }

        private void ChangeOwner(ref ServiceObject so)
        {
            Method meth = so.Methods[0];
            BingMapsHelper bmHelper = new BingMapsHelper();

            BingResult SearchResult = new BingResult();

            try
            {
                SearchResult = bmHelper.SearchByLocation(so.Properties["SearchLocation"].Value.ToString(), NotNull(so.Properties["ConfidenceFilter"].Value), GetConfigPropertyValue("BingMapsKey"));

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetResultListValue(prop, SearchResult);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void SetStateStatus(ref ServiceObject so)
        {
            Method meth = so.Methods[0];
            BingMapsHelper bmHelper = new BingMapsHelper();

            BingResult SearchResult = new BingResult();

            try
            {
                SearchResult = bmHelper.SearchByCoordinates(so.Properties["SearchLatitude"].Value.ToString(), so.Properties["SearchLongitude"].Value.ToString(), NotNull(so.Properties["ConfidenceFilter"].Value), GetConfigPropertyValue("BingMapsKey"));

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetResultListValue(prop, SearchResult);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetEntities(ref ServiceObject so)
        {
            Method meth = so.Methods[0];
            BingMapsHelper bmHelper = new BingMapsHelper();

            BingResult SearchResult = new BingResult();

            try
            {

                if (meth.Name.ToLower().Equals("generatemapfromcoordinates"))
                {
                    SearchResult = bmHelper.GenerateCoordinatesImage(so.Properties["SearchLatitude"].Value.ToString(), so.Properties["SearchLongitude"].Value.ToString(), int.Parse(NotNull(so.Properties["ImageZoom"].Value)), NotNull(so.Properties["MapStyle"].Value), int.Parse(NotNull(so.Properties["MapWidth"].Value)), int.Parse(NotNull(so.Properties["MapHeight"].Value)), NotNull(so.Properties["ImageFormat"].Value), NotNull(so.Properties["FileSystemLocation"].Value), GetConfigPropertyValue("BingMapsKey"));
                }
                else
                {
                    SearchResult = bmHelper.GenerateLocationImage(so.Properties["SearchLocation"].Value.ToString(), int.Parse(NotNull(so.Properties["ImageZoom"].Value)), NotNull(so.Properties["MapStyle"].Value), int.Parse(NotNull(so.Properties["MapWidth"].Value)), int.Parse(NotNull(so.Properties["MapHeight"].Value)), NotNull(so.Properties["ImageFormat"].Value), NotNull(so.Properties["FileSystemLocation"].Value), GetConfigPropertyValue("BingMapsKey"));
                }
                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetResultListValue(prop, SearchResult);
                }

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BulkActionTasks(ref ServiceObject so)
        {
            Method meth = so.Methods[0];
            BingMapsHelper bmHelper = new BingMapsHelper();

            List<BingResult> SearchResults = new List<BingResult>();

            try
            {
                SearchResults = bmHelper.SearchLocation(so.Properties["SearchLocation"].Value.ToString(), so.Properties["SearchQuery"].Value.ToString(), NotNull(so.Properties["SearchCategory"].Value), NotNull(so.Properties["SortBy"].Value), int.Parse(so.Properties["ResultCount"].Value.ToString()), GetConfigPropertyValue("BingMapsKey"));

                so.Properties.InitResultTable();

                foreach (BingResult br in SearchResults)
                {
                    for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                    {
                        Property prop = so.Properties[meth.ReturnProperties[c]];
                        prop = SetResultListValue(prop, br);
                    }

                    so.Properties.BindPropertiesToResultTable();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CreateTask(ref ServiceObject so)
        {
            SourceCode.SmartObjects.Services.ServiceSDK.Objects.Method meth = so.Methods[0];
            K2CRMHelper helper = new K2CRMHelper();

            CRMTask task = new CRMTask();

            task.Category = NotNull(so.Properties["Category"].Value);
            task.Description = NotNull(so.Properties["Description"].Value);
            task.DueDate = DateTime.Parse(NotNull(so.Properties["DueDate"].Value));
            task.Duration = int.Parse(NotNull(so.Properties["Duration"].Value));
            task.OwnerFQN = NotNull(so.Properties["OwnerFQN"].Value);
            task.OwnerId = NotNull(so.Properties["OwnerId"].Value);
            task.Owner = NotNull(so.Properties["Owner"].Value);
            task.Priority = int.Parse(NotNull(so.Properties["Priority"].Value));
            task.Regarding = NotNull(so.Properties["Regarding"].Value);
            task.RegardingId = NotNull(so.Properties["RegardingId"].Value);
            task.State = int.Parse(NotNull(so.Properties["State"].Value));
            task.Status = int.Parse(NotNull(so.Properties["Status"].Value));
            task.Subcategory = NotNull(so.Properties["Subcategory"].Value);
            task.Subject = NotNull(so.Properties["Subject"].Value);

            try
            {
                RestResponse<CRMTask> response = helper.CreateTask(task, config);

                so.Properties.InitResultTable();

                for (int c = 0; c < meth.ReturnProperties.Count; c += 1)
                {
                    Property prop = so.Properties[meth.ReturnProperties[c]];
                    prop = SetTaskProperties(prop, response);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetCRMUser(ref ServiceObject so)
        {
            Method meth = so.Methods[0];
            BingMapsHelper bmHelper = new BingMapsHelper();

            RouteDirections SearchResults = new RouteDirections();

            try
            {
                if (meth.Name.ToLower().Equals("getdirectionsbycoordinates"))
                {
                    SearchResults = bmHelper.GetDirectionsByCoordinates(so.Properties["RouteCoordinates"].Value.ToString(), NotNull(so.Properties["RouteTravelMode"].Value), NotNull(so.Properties["RouteTrafficUsage"].Value), NotNull(so.Properties["RouteOptimisation"].Value), GetConfigPropertyValue("BingMapsKey"));
                }
                else
                {
                    SearchResults = bmHelper.GetDirectionsByLocations(so.Properties["SearchLocation"].Value.ToString(), NotNull(so.Properties["RouteTravelMode"].Value), NotNull(so.Properties["RouteTrafficUsage"].Value), NotNull(so.Properties["RouteOptimisation"].Value), GetConfigPropertyValue("BingMapsKey"));
                }

                so.Properties.InitResultTable();
                so.Properties["RouteDirections"].Value = SearchResults.Directions;
                so.Properties["RouteTotalDistance"].Value = SearchResults.TotalDistance;

                so.Properties.BindPropertiesToResultTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Property SetTaskProperties(Property prop, RestResponse<CRMTask> task)
        {
            switch (prop.Name.ToLower())
            {
                case "category":
                    prop.Value = task.Data.Category;
                    break;
                case "description":
                    prop.Value = task.Data.Description;
                    break;
                case "duedate":
                    prop.Value = task.Data.DueDate;
                    break;
                case "duration":
                    prop.Value = task.Data.Duration;
                    break;
                case "ownerfqn":
                    prop.Value = task.Data.OwnerFQN;
                    break;
                case "owner":
                    prop.Value = task.Data.Owner;
                    break;
                case "ownerid":
                    prop.Value = task.Data.OwnerId;
                    break;
                case "priority":
                    prop.Value = task.Data.Priority;
                    break;
                case "regarding":
                    prop.Value = task.Data.Regarding;
                    break;
                case "regardingid":
                    prop.Value = task.Data.RegardingId;
                    break;
                case "state":
                    prop.Value = task.Data.State;
                    break;
                case "status":
                    prop.Value = task.Data.Status;
                    break;
                case "subcategory":
                    prop.Value = task.Data.Subcategory;
                    break;
                case "subject":
                    prop.Value = task.Data.Subject;
                    break;
                case "zcontent":
                    prop.Value = task.Content;
                    break;
                case "zerrorexception":
                    prop.Value = task.ErrorException.StackTrace;
                    break;
                case "zerrormessage":
                    prop.Value = task.ErrorMessage;
                    break;
                case "zstatuscode":
                    prop.Value = task.StatusCode.ToString();
                    break;
                case "zstatusdescription":
                    prop.Value = task.StatusDescription;
                    break;
            }
            return prop;
        }

        private Property SetResultListValue(Property prop, BingResult SearchResult)
        {
            switch (prop.Name.ToLower())
            {
                case "locationname":
                    prop.Value = SearchResult.LocationName;
                    break;
                case "phonenumber":
                    prop.Value = SearchResult.PhoneNumber;
                    break;
                case "distance":
                    prop.Value = SearchResult.Distance;
                    break;
                case "addressline":
                    prop.Value = SearchResult.AddressLine; 
                    break;
                case "postcode":
                    prop.Value = SearchResult.Postcode;
                    break;
                case "formatteaddress":
                    prop.Value = SearchResult.FormattedAddress;
                    break;
                case "latitude":
                    prop.Value = SearchResult.Latitude;
                    break;
                case "longitude":
                    prop.Value = SearchResult.Longitude;
                    break;
                case "imageuri":
                    prop.Value = SearchResult.ImageURI;
                    break;
                case "imagepath":
                    prop.Value = SearchResult.ImagePath;
                    break;
                case "imagebase64":
                    prop.Value = SearchResult.ImageBase64;
                    break;
                case "admindistrict":
                    prop.Value = SearchResult.AdminDistrict;
                    break;
                case "countryregion":
                    prop.Value = SearchResult.CountryRegion;
                    break;
                case "locality":
                    prop.Value = SearchResult.Locality;
                    break;
                case "postaltown":
                    prop.Value = SearchResult.PostalTown;
                    break;
                case "bestviewnelatitude":
                    prop.Value = SearchResult.BestViewNELatitude;
                    break;
                case "bestviewnelongitude":
                    prop.Value = SearchResult.BestViewNELongitude;
                    break;
                case "bestviewswlatitude":
                    prop.Value = SearchResult.BestViewSWLatitude;
                    break;
                case "bestviewswlongitude":
                    prop.Value = SearchResult.BestViewSWLongitude;
                    break;
                case "confidence":
                    prop.Value = SearchResult.Confidence;
                    break;
                case "website":
                    prop.Value = SearchResult.WebSite;
                    break;
                case "category":
                    prop.Value = SearchResult.Category;
                    break;
            }
            return prop;
        }

        private Property SetLegListValue(Property prop, RouteResultLeg SearchResult)
        {
            switch (prop.Name.ToLower())
            {
                case "legdistance":
                    prop.Value = SearchResult.Distance;
                    break;
                case "legtimeinseconds":
                    prop.Value = SearchResult.TimeInSeconds;
                    break;
                case "legstartlatitude":
                    prop.Value = SearchResult.StartLatitude;
                    break;
                case "legstartlongitude":
                    prop.Value = SearchResult.StartLongitude;
                    break;
                case "legendlatitude":
                    prop.Value = SearchResult.EndLatitude;
                    break;
                case "legendlongitude":
                    prop.Value = SearchResult.EndLongitude;
                    break;
                case "legnelatitude":
                    prop.Value = SearchResult.NELatitude;
                    break;
                case "legnelongitude":
                    prop.Value = SearchResult.NELongitude;
                    break;
                case "legswlatitude":
                    prop.Value = SearchResult.SWLatitude;
                    break;
                case "legswlongitude":
                    prop.Value = SearchResult.SWLongitude;
                    break;
                case "legdescription":
                    prop.Value = SearchResult.Description;
                    break;
            }
            return prop;
        }

        private Property SetItineraryListValue(Property prop, RouteResultItinerary SearchResult)
        {
            switch (prop.Name.ToLower())
            {
                case "itinerarydistance":
                    prop.Value = SearchResult.Distance;
                    break;
                case "itinerarytimeinseconds":
                    prop.Value = SearchResult.TimeInSeconds;
                    break;
                case "itinerarylatitude":
                    prop.Value = SearchResult.Latitude;
                    break;
                case "itinerarylongitude":
                    prop.Value = SearchResult.Longitude;
                    break;
                case "itinerarymaneuver":
                    prop.Value = SearchResult.ManeuverType;
                    break;
                case "itinerarynelatitude":
                    prop.Value = SearchResult.NELatitude;
                    break;
                case "itinerarynelongitude":
                    prop.Value = SearchResult.NELongitude;
                    break;
                case "itineraryswlatitude":
                    prop.Value = SearchResult.SWLatitude;
                    break;
                case "itineraryswlongitude":
                    prop.Value = SearchResult.SWLongitude;
                    break;
                case "itinerarydescription":
                    prop.Value = SearchResult.Description;
                    break;
            }
            return prop;
        }

        private K2CRMConfig GetK2CRMConfig(string User, string Password, string RESTUrl)
        {
            string[] domainuser = User.Split('\\');

            K2CRMConfig config = new K2CRMConfig();
            config.RESTUrl = RESTUrl;
            config.User = domainuser[1];
            config.Domain = domainuser[0];
            config.Password = Password;
            config.Credentials = new NetworkCredential(domainuser[1], Password, domainuser[0]);
            config.CredentialCache = new CredentialCache();
            config.CredentialCache.Add(new Uri(RESTUrl), "NTLM", config.Credentials);

            return config;
        }

        
        private string GetConfigPropertyValue(string propertyName)
        {
            string configValue = "";

            if (SvcBase.Service.ServiceConfiguration.Contains(propertyName) == true)
            {
                if (SvcBase.Service.ServiceConfiguration[propertyName] != null)
                {
                    configValue = SvcBase.Service.ServiceConfiguration[propertyName].ToString();
                }
            }
            return configValue;
        }

        public string NotNull(object x)
        {
            if (x != null)
            {
                return x.ToString();
            }
            else
            {
                return "";
            }
        }

    }
}
