using BAL.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IBookingDAO
    {
        public List<GetBooking> GetAllById(int key);
        public GetBooking Get(int key);
        public void Create(CreateBooking create);
        public void Create(string code, int id, int subjectId);
        public void Update(int key, UpdateBooking update);
        public void Delete(int key);
    }
}
