using NonFraud.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NonFraud.Service.Mappers
{
    /// <summary>
    /// Mapper class for Profile entity and model
    /// </summary>
    public class ProfileMapper
    {
        /// <summary>
        /// Maps DB data to SelectListItem web component
        /// </summary>
        /// <param name="profiles">List of profiles from DB</param>
        public List<SelectListItem> Map(List<Profile> profiles)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var profile in profiles)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = profile.ProfileName,
                    Value = profile.ProfileID.ToString()
                };

                items.Add(item);
            }

            return items;
        }
    }
}