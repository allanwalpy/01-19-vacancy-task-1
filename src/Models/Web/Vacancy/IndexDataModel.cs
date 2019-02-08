using System.Collections.Generic;

using App.Server.Models.Responses;

namespace App.Server.Models.Web.Vacancy
{
    public class IndexDataModel
    {
        public ICollection<VacancyResponse> Data { get; set; }

        public bool HasStatus => Status != null;
        public IndexPageStatus Status { get; set; }
    }
}