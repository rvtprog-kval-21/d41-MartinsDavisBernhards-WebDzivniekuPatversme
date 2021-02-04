﻿using System.Collections.Generic;
using WebDzivniekuPatversme.Models;

namespace WebDzivniekuPatversme.Repository.Interfaces
{
    public interface IShelterRepository
    {
        List<Shelters> GetAllAnimalShelters();

        void CreateNewAnimalShelter(Shelters newAnimalShelters);

        void DeleteShelters(Shelters shelters);

        void EditShelter(Shelters shelter);
    }
}