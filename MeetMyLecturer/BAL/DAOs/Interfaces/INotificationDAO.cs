﻿using BAL.DTOs.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface INotificationDAO
    {
        public List<GetNotification> GetAll();
        public GetNotification Get(int key);
        public void Create(CreateNotification create);
        public void Update(int key, UpdateNotification update);
    }
}
