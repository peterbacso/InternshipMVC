﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipMvc.Pages
{
    public class InternshipModel : PageModel
    {
        private readonly ILogger<InternshipModel> _logger;

        public InternshipModel(ILogger<InternshipModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
