using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TravelExperts.Repository.Domain;

namespace WebApp.Models
{
    public class IndexModel : PageModel
    {
        private readonly TravelExperts.Repository.Domain.TravelExpertsContext _context;

        public IndexModel(TravelExperts.Repository.Domain.TravelExpertsContext context)
        {
            _context = context;
        }

        public IList<SupsOld> SupsOld { get;set; }

        public async Task OnGetAsync()
        {
            SupsOld = await _context.SupsOld.ToListAsync();
        }
    }
}
