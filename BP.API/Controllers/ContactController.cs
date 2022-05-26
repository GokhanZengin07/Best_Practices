using BP.Api.Service;
using BP.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IContactService contactService;

        public ContactController(IConfiguration Configuration,IContactService ContactService)
        {
            configuration = Configuration;
            contactService = ContactService;
        }
        [HttpGet]
        public string Get()
        {
            return configuration["ReadMe"].ToString();
        }
        [ResponseCache(Duration = 10)]
        [HttpGet("{id}")]
        public ContactDVO GetContactById(int id)
        {

            return contactService.GetContactById(id);
        }
        [HttpPost]
        public ContactDVO CreateContact(ContactDVO Contact)
        {

            return Contact;
        }
        
    }
}
