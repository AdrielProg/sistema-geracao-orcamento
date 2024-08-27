
using BudgetGenerator.Models;
using BudgetGenerator.Services;
using Microsoft.AspNetCore.Mvc;

using Rotativa.AspNetCore;

namespace BudgetGenerator.Controllers
{
    [Route("[controller]")]
    public class BudgetGeneratorController : Controller
    {
        private readonly MatrixService _matrixService;
        private readonly ILogger<BudgetGeneratorController> _logger;

        public BudgetGeneratorController(ILogger<BudgetGeneratorController> logger)
        {
            _matrixService = new MatrixService();
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ShowReportPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateReport(ReportInputModel input)
        {
            if (input.Features == null || input.Features.Count == 0)
            {
                ModelState.AddModelError("", "Nenhuma funcionalidade foi adicionada.");
                return View("Index", input);
            }

            // Gera o PDF com os dados fornecidos pelo usuário
            var reportModel = new ReportModel
            {
                ReportTitle = input.ReportTitle,
                Features = input.Features
            };

            return new ViewAsPdf("Report", reportModel)
            {
                FileName = "RelatorioBudgetGenerator.pdf"
            };
        }

    }
}