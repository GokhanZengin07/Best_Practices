using BP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP.API.Data.Models;
using AutoMapper;
using System.Net.Http;

namespace BP.Api.Service
{
    public class ContactService : IContactService

    {
        private readonly IMapper mapper;
        private readonly IHttpClientFactory httpClientFactory;

        public ContactService(IMapper Mapper, IHttpClientFactory HttpClientFactory)
        {
            mapper = Mapper;
            httpClientFactory = HttpClientFactory;
        }

        public async Task<ContactDVO> GetContactById(int Id)
        {
            // Veritabanı kaydın getirilmesi.
            Contact dbContact = getDummyContact();
            var client = httpClientFactory.CreateClient("http://garantiapi.com");
            /* HttpClient client = new HttpClient();
             client.BaseAddress = new Uri("garanti.com");
             client.DefaultRequestHeaders.Add("Authorzation", "Bearer 123");
             string get= await client.GetStringAsync("/api/getpayment");
             client.Dispose();*/
            ContactDVO resultContact = new ContactDVO();
            mapper.Map(dbContact, resultContact); // aşağıdaki işleme gerek yok mapliyoruz direk.

            return resultContact;
            /*
            return new ContactDVO()
            {
                Id = dbContact.Id,
                FullName = $"{dbContact.FirstName} {dbContact.LastName}"
            };*/

        }

        ContactDVO IContactService.GetContactById(int Id)
        {
            throw new NotImplementedException();
        }

        private Contact getDummyContact()
        {
            return new Contact()
            {
                Id = 1,
                FirstName = "GOkhan",
                LastName = "Zengin",
            };
        }
    }
}

