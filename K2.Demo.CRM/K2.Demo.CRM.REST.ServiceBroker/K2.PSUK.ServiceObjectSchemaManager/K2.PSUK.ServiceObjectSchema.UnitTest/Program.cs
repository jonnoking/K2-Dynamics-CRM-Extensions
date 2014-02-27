using System;
using System.Collections.Generic;
using System.Text;

using SourceCode.SmartObjects.Services.ServiceSDK.Types;
namespace K2.PSUK.ServiceObjectSchema.UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTests();
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void RunTests()
        {
            CreateSchema();
        }

        private static void CreateSchema()
        {
            SchemaObject so = new SchemaObject();

            so.ServiceInstanceName = "DynamicADSO";
            so.ServiceInstanceDisplayName = "DynamicADSO";
            so.ServiceInstanceDescription = "DynamicADSO";

            //Standard Inputs
            List<string> inputProp = new List<string>();
            List<string> reqProp = new List<string>();
            List<string> retProp = new List<string>();

            so.AddProperty("sAMAccountName", "sAMAccountName", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("displayName", "Display Name", "AD Display Name Property", "DirectoryString", SoType.Text);
            so.AddProperty("cn", "Common Name", "AD Common Name Property", "DirectoryString", SoType.Text);
            so.AddProperty("userPrincipleName", "userPrincipleName", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("givenName", "givenName", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("initials", "initials", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("sn", "sn", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("userPassword", "userPassword", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("mail", "mail", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("WhenCreated", "WhenCreated", "AD Schema Property", "DirectoryString", SoType.Text);
            so.AddProperty("WhenUpdated", "WhenUpdated", "AD Schema Property", "DirectoryString", SoType.Text);

            reqProp = new List<string>();

            //Hard Coded Properties

            so.AddProperty("SubStringSearchInput", "SubString Search Input", "User/Group Common Name SubString Search Input","DirectoryString", SoType.Text);
            so.AddProperty("Domain", "Domain", "Specify Domain to use for action [Coded Property]", "DirectoryString", SoType.Text);
            so.AddProperty("MaxSearchResultSize", "MaxSearchResultSize", "Specify the maximun number of records to return - default is 2000 - *=Unlimited [Coded Property]", "DirectoryString", SoType.Text);
            so.AddProperty("OrganisationalUnit", "OrganisationalUnit", "Specify OU, for multilevel OU pass delimited string ie America|HR [Coded Property]", "DirectoryString", SoType.Text);
            so.AddProperty("UAC_AccountDisabled", "UAC_AccountDisabled", "User Account Disabled [Coded Property]", "bool", SoType.YesNo);
            so.AddProperty("UAC_PasswordCannotChange", "UAC_PasswordCannotChange", "User Cannot Change password [Coded Property]", "bool", SoType.YesNo);
            so.AddProperty("UAC_PasswordNeverExpired", "UAC_PasswordNeverExpired", "User password never expires [Coded Property]", "bool", SoType.YesNo);
            so.AddProperty("UAC_PasswordExpired", "UAC_PasswordExpired", "User password expired [Coded Property]", "bool", SoType.YesNo);
            so.AddProperty("transactionStatus", "Transaction Status", "Status of last operation","DirectoryString", SoType.Text);
            so.AddProperty("transactionMessage", "Transaction Message", "Error message from last operation","DirectoryString", SoType.Text);

            //CreateUser
            reqProp.Add("sAMAccountName");

            inputProp.Add("sAMAccountName");
            inputProp.Add("givenName");
            inputProp.Add("initials");
            inputProp.Add("userPassword");
            inputProp.Add("sn");
            inputProp.Add("mail");

            inputProp.Add("OrganisationalUnit");
            inputProp.Add("UAC_AccountDisabled");
            inputProp.Add("UAC_PasswordCannotChange");
            inputProp.Add("UAC_PasswordNeverExpired");
            inputProp.Add("UAC_PasswordExpired");

            retProp.Add("WhenCreated");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");

            so.AddMethod("CreateUser", "CreateUser", "Create a User in Active Directory", MethodType.Create, inputProp, reqProp, retProp);

            //UpdateUser
            inputProp = new List<string>();
            reqProp = new List<string>();
            retProp = new List<string>();

            reqProp.Add("sAMAccountName");

            inputProp.Add("sAMAccountName");
            inputProp.Add("givenName");
            inputProp.Add("initials");
            inputProp.Add("userPassword");
            inputProp.Add("sn");
            inputProp.Add("mail");

            inputProp.Add("OrganisationalUnit");
            inputProp.Add("UAC_AccountDisabled");
            inputProp.Add("UAC_PasswordCannotChange");
            inputProp.Add("UAC_PasswordNeverExpired");
            inputProp.Add("UAC_PasswordExpired");

            retProp.Add("WhenUpdated");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");

            so.AddMethod("UpdateUser", "UpdateUser", "Update a User in Active Directory", MethodType.Update, inputProp, reqProp, retProp);


            //Read User
            inputProp = new List<string>();
            reqProp = new List<string>();
            retProp = new List<string>();

            reqProp.Add("sAMAccountName");

            inputProp.Add("sAMAccountName");

            retProp.Add("sAMAccountName");
            retProp.Add("givenName");
            retProp.Add("initials");
            retProp.Add("sn");
            retProp.Add("mail");
            retProp.Add("OrganisationalUnit");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");

            so.AddMethod("ReadUser", "ReadUser", "Read a Users details from Active Directory", MethodType.Read, inputProp, reqProp, retProp);


            //GetUsers
            inputProp = new List<string>();
            reqProp = new List<string>();
            retProp = new List<string>();

            inputProp.Add("givenName");
            inputProp.Add("initials");
            inputProp.Add("sn");
            inputProp.Add("mail");
            inputProp.Add("MaxSearchResultSize");
            inputProp.Add("OrganisationalUnit");

            retProp.Add("sAMAccountName");
            retProp.Add("givenName");
            retProp.Add("initials");
            retProp.Add("sn");
            retProp.Add("mail");
            retProp.Add("MaxSearchResultSize");
            retProp.Add("OrganisationalUnit");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");
            
            so.AddMethod("GetUsers", "GetUsers", "Get a list of users details from Active Directory", MethodType.List, inputProp, reqProp, retProp);

            //MoveUserOU
            inputProp = new List<string>();
            reqProp = new List<string>();
            retProp = new List<string>();

            reqProp.Add("sAMAccountName");
            reqProp.Add("OrganisationalUnit");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");
            inputProp.Add("OrganisationalUnit");
            inputProp.Add("sAMAccountName");
            so.AddMethod("MoveUserOU", "Move User OU", "Move a user to a different OU in Active Directory", MethodType.Execute, inputProp, reqProp, retProp);

            //AddUserToGRoups / RemoveUserFRomGroups
            inputProp = new List<string>();
            reqProp = new List<string>();
            retProp = new List<string>();

            reqProp.Add("sAMAccountName");
            reqProp.Add("memberOf");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");
            inputProp.Add("sAMAccountName");
            inputProp.Add("memberOf");
            so.AddMethod("AddUserToGroups", "Add User To Groups", "Add User To Active Directory Groups", MethodType.Execute, inputProp, reqProp, retProp);
            so.AddMethod("RemoveUserFromGroups", "Remove User From Groups", "Remove a user from Active Directory Groups", MethodType.Execute, inputProp, reqProp, retProp);

            //Search Users By SubString / Search Groups By SubString
            inputProp = new List<string>();
            reqProp = new List<string>();
            retProp = new List<string>();

            retProp.Add("sAMAccountName");
            retProp.Add("SubStringSearchInput");
            retProp.Add("displayName");
            retProp.Add("cn");
            retProp.Add("transactionStatus");
            retProp.Add("transactionMessage");

            reqProp.Add("SubStringSearchInput");

            inputProp.Add("sAMAccountName");
            inputProp.Add("SubStringSearchInput");
            inputProp.Add("displayName");
            inputProp.Add("cn");

            so.AddMethod("SearchUsersBySubString", "Search Users By SubString", "Search for users by Selected Properties", MethodType.List, inputProp, reqProp, retProp);
            so.AddMethod("SearchGroupsBySubString", "Search Groups By SubString", "Search for groups by Selected Properties", MethodType.List, inputProp, reqProp, retProp);

            SchemaManager.SaveSchemaXMLFile(so, string.Empty);
            
        }
    }
}
