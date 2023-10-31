using BAL.DTOs.Slots;
using BAL.DTOs.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface ISlotDAO
    {
        public List<GetSlot> GetAllById(int key);
        public GetSlot Get(int key);
        public void Create(CreateSlot create);
        public void Update(int key, UpdateSlot update);
        public void Delete(int key);
        public List<GetSlot> CheckStatus(int key);
    }
}
