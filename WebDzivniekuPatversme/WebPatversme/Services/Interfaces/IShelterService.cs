﻿using WebDzivniekuPatversme.Models;
using System.Collections.Generic;

namespace WebDzivniekuPatversme.Services.Interfaces
{
    public interface IShelterService
    {
        List<Shelters> GetAllShelterList();

        void AddNewShelter(Shelters shelter);

        Shelters GetShelterById(string Id);

        void DeleteShelter(Shelters shelter);

        void EditShelter(Shelters shelter);
    }
}