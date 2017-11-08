using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NonFraud.Data.Repositories;
using NonFraud.Data.Entities;
using NonFraud.Service.Mappers;
using System.Web.Mvc;

namespace NonFraud.Service.Controllers
{
    /// <summary>
    /// Profile controller of application
    /// </summary>
    public class ProfileController : ApiController
    {
        BaseRepo<Profile> _baseRepo;  
        ProfileMapper _profileMapper;

        public ProfileController()
        {
            _baseRepo = new BaseRepo<Profile>();
            _profileMapper = new ProfileMapper();
        }

        /// <summary>
        /// Returns the profile list
        /// </summary>
        public List<SelectListItem> Get()
        {
            return _profileMapper.Map(_baseRepo.GetAll().ToList());
        }
    }
}
