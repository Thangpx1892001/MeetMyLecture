using BAL.DTOs.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IFeedbackDAO
    {
        public List<GetFeedback> GetAllById(int key);
        public GetFeedback Get(int key);
        public void Create(CreateFeedback create);
    }
}
