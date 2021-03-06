using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CheckItApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;

using Xamarin.Essentials;
using System.IO;
using CheckItApp.DTO;

namespace CheckItApp.Services
{
    class CheckItApi
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:52537/api"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:44379/api"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "https://localhost:44379/api"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private static CheckItApi proxy = null;
        internal static CheckItApi CreateProxy()
        {
            string baseUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
            }

            if (proxy == null)
                proxy = new CheckItApi(baseUri);
            return proxy;
        }

        public async Task<bool> SignUpAccount(Account u)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                string json = JsonSerializer.Serialize<Account>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/SignUpAccount", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public async Task<List<Form>> GetForms(int clientId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetForms?clientId={clientId}");
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Form> forms = JsonSerializer.Deserialize<List<Form>>(jsonContent, options);
                    return forms;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<bool> AlertAsync(int formId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Alert?formId={formId}");
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Class>> GetClass(int clientId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetGroupsOfStudent?clientId={clientId}");
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Class> Class = JsonSerializer.Deserialize<List<Class>>(jsonContent, options);
                    return Class;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<StudentSignDTO>> GetSignedStudentsInForm(int formId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetSignedStudentsInForm?formId={formId}");
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    Newtonsoft.Json.JsonSerializerSettings options1 = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All
                    };

                    List<StudentSignDTO> signedStudents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentSignDTO>>(jsonContent, options1);
                    return signedStudents;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<Student>> GetStudentsInGroup(int groupId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetStudentsInGroup?groupId={groupId}");
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Student> students = JsonSerializer.Deserialize<List<Student>>(jsonContent, options);
                    return students;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<Class>> GetClass2(int StaffId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                string url = $"{this.baseUri}/GetGroupsOfStaff?staffMemberId={StaffId}";
                HttpResponseMessage response = await this.client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Class> Class = JsonSerializer.Deserialize<List<Class>>(jsonContent, options);
                    return Class;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public async Task<List<Form>> GetFormsStaffMember(int clientId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetFormsStaffMember?clientId={clientId}");
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Form> forms = JsonSerializer.Deserialize<List<Form>>(jsonContent, options);
                    return forms;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<Account> LoginAsync(string email, string pass)
        {
            try
            {
                string uri = $"{this.baseUri}/Login?email={email}&pass={pass}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Account u = JsonSerializer.Deserialize<Account>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<StaffMember> Login(string email, string pass)
        {
            try
            {
                string uri = $"{this.baseUri}/Login2?email={email}&pass={pass}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    StaffMember u = JsonSerializer.Deserialize<StaffMember>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool?> EmailExists(string email)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/email-exists?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    bool? b = JsonSerializer.Deserialize<bool?>(content, options);
                    return b;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
            public async Task<StaffMember> ResetPassStaffMemberAsync(string Pass, String email)
        {
            try
            {
                string uri = $"{this.baseUri}/ResetPassStaffMember?Pass={Pass}&email={email}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    StaffMember u = JsonSerializer.Deserialize<StaffMember>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<Account> ResetPassAsync(string Pass, String email)
        {
            try
            {
                string uri = $"{this.baseUri}/ResetPass?Pass={Pass}&email={email}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Account u = JsonSerializer.Deserialize<Account>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<bool> ForgotPassword(string Email)
        {
            try
            {
                string uri = $"{this.baseUri}/ForgotPassword?Email={Email}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(content, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> CheckIfSigned(int formid, string Email)
        {
            try
            {
                string uri = $"{this.baseUri}/GetSignPeople?formid={formid}&Email={Email}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(content, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> IsSinged(int formid, int clientid)
        {
            try
            {
                string uri = $"{this.baseUri}/CheckIfSigned?formid={formid}&clientid={clientid}";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(content, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UploadFile(string fullPath, string targetFileName)
        {
            try
            {
                var multipartFormDataContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(fullPath));
                multipartFormDataContent.Add(fileContent, "file", targetFileName);

                HttpResponseMessage response = await client.PostAsync($"{this.baseUri}/UploadExcel", multipartFormDataContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


       

        public async Task<int> GetSigns(int formId)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Signs?formId={formId}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    int b = JsonSerializer.Deserialize<int>(content, options);
                    return b;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }


        public async Task<List<Class>> GetClassesAsync()
        {
            try
            {
                string uri = $"{this.baseUri}/GetClasses";
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Class> classes = JsonSerializer.Deserialize<List<Class>>(content, options);
                    return classes;
                }
                else
                {
                    return new List<Class>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Class>();
            }
        }

        public async Task<bool> PostForms(Form form, List<int> classes)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                string formSerialized = JsonSerializer.Serialize(form, options);
                string classesSerialized = JsonSerializer.Serialize(classes, options);

                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/PostForms?formJson={formSerialized}&classesJson={classesSerialized}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public async Task<bool> LogOut()
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/LogOut");
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                   bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<List<Class>> GetFormGroups(int formId)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetFormGroups?formId={formId}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    List<Class> b = JsonSerializer.Deserialize<List<Class>>(jsonContent, options);
                    return b;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        private CheckItApi(string baseUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
        }


    }
}

