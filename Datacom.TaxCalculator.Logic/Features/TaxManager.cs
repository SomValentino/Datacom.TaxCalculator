using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Interfaces;
using Datacom.TaxCalculator.Logic.Contracts;
using Datacom.TaxCalculator.Logic.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Logic.Features
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxCalculator _taxCalculator;
        private readonly IDataContext _context;
        private readonly IEnumerable<TaxTableEntry> _taxTable;
        private readonly ILogger<TaxManager> _logger;

        public TaxManager(ITaxCalculator taxCalculator, IDataContext context,
            IOptions<IEnumerable<TaxTableEntry>> taxTable, ILogger<TaxManager> logger)
        {
            _taxCalculator = taxCalculator;
            _context = context;
            _taxTable = taxTable.Value;
            _logger = logger;
        }
        public async Task<BatchProcessResult> BatchProcessAsync(string csvSourceFile)
        {
            try
            {
                if(!File.Exists(csvSourceFile)) throw new FileNotFoundException("The csv file {file} does not exist.", csvSourceFile);

                _logger.LogInformation("Fetching tax data from csv file {file}", csvSourceFile);

                var csvEntryData = await _context.Read(csvSourceFile);

                _logger.LogInformation("Successfully fetched tax data with entry size {}", csvEntryData.UserTaxes.Count());

                _logger.LogInformation("Started performing tax calculations");

                foreach (var userTax in csvEntryData.UserTaxes)
                {
                    _taxCalculator.Calculate(_taxTable, userTax);
                }

                _logger.LogInformation("Finished performing tax calculations");

                var outputFileName = $"{csvSourceFile}Output.csv";

                _logger.LogInformation("Started writing tax output data to file: {file}", outputFileName);

                await _context.Write(csvEntryData.UserTaxes, outputFileName);

                _logger.LogInformation("Completed writing tax output data to file: {file}", outputFileName);

                var numofsuccessReads = csvEntryData.UserTaxes.Count;
                var numoffailedReads = csvEntryData.ErrorMessages.Count;
                var totalReads = numofsuccessReads + numoffailedReads;

                return new BatchProcessResult { 
                    Success = totalReads > 0 && numofsuccessReads == totalReads,
                    NumberOfFailedReads = numoffailedReads,
                    NumberOfSuccessfulReads = numofsuccessReads,
                    ErrorMessage = string.Join("\n", csvEntryData.ErrorMessages)
                };
            }
            catch(FileNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);

                return new BatchProcessResult { ErrorMessage = ex.Message};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new BatchProcessResult { ErrorMessage = ex.Message };
            }
        }
    }
}
