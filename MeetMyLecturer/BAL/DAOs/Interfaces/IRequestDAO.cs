using BAL.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IRequestDAO
    {
        public List<GetRequest> GetAllById(int key);
        public GetRequest Get(int key);
        public void Create(CreateRequest create);
        public void Update(int key, UpdateRequest update);
        public void Delete(int key);
    }
}
