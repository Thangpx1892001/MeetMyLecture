using BAL.DTOs.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface ISubjectDAO
    {
        public List<GetSubject> GetAll();
        public GetSubject Get(int key);
        public void Create(CreateSubject create);
        public void Update(int key, UpdateSubject update);
        public void Delete(int key);
    }
}
