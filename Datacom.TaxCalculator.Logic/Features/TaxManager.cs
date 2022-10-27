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
            IEnumerable<TaxTableEntry> taxTable, ILogger<TaxManager> logger)
        {
            _taxCalculator = taxCalculator;
            _context = context;
            _taxTable = taxTable;
            _logger = logger;
        }
        public async Task<BatchProcessResult> BatchProcessAsync(string csvSourceFile, string destinationFile = null)
        {
            try
            {
                if(!File.Exists(csvSourceFile)) throw new FileNotFoundException($"The csv file {csvSourceFile} does not exist.");

                _logger.LogInformation("Fetching tax data from csv file {file}", csvSourceFile);

                var csvEntryData = await _context.Read(csvSourceFile);

                _logger.LogInformation("Successfully fetched tax data with entry size {}", csvEntryData.UserTaxes.Count());

                _logger.LogInformation("Started performing tax calculations");

                foreach (var userTax in csvEntryData.UserTaxes)
                {
                    _taxCalculator.Calculate(_taxTable, userTax);
                }

                _logger.LogInformation("Finished performing tax calculations");

                var directoryPath = Path.GetDirectoryName(csvSourceFile);

                var inputPutFileName = Path.GetFileName(csvSourceFile).Split(".")[0];

                var outputFileName = string.Empty;

                if (!string.IsNullOrEmpty(destinationFile) && !string.IsNullOrWhiteSpace(destinationFile))
                {
                    var outputFile= Path.GetFileName(destinationFile);

                    if (string.IsNullOrEmpty(Path.GetExtension(outputFile)) && string.IsNullOrWhiteSpace(Path.GetExtension(outputFile)))
                    {
                        outputFileName = Path.Combine(destinationFile, $"{inputPutFileName}Output.csv");
                    }
                    else
                    {
                        outputFileName = destinationFile;
                    }
                }
                else
                {
                    outputFileName = Path.Combine(directoryPath, $"{inputPutFileName}Output.csv");
                }

                _logger.LogInformation("Started writing tax output data to file: {file}", outputFileName);

                
                await _context.Write(csvEntryData.UserTaxes, outputFileName);

                _logger.LogInformation("Completed writing tax output data to file: {file}", outputFileName);

                var numofsuccessReads = csvEntryData.UserTaxes.Count;
                var numoffailedReads = csvEntryData.ErrorMessages.Count;
                var totalReads = numofsuccessReads + numoffailedReads;

                var result = new BatchProcessResult { 
                    Success = totalReads > 0 && numofsuccessReads == totalReads,
                    NumberOfFailedReads = numoffailedReads,
                    NumberOfSuccessfulReads = numofsuccessReads,
                    ErrorMessage = string.Join("\n", csvEntryData.ErrorMessages)
                };

                if(result.NumberOfSuccessfulReads > 0)
                {
                    result.OutputFileName = outputFileName;
                }

                return result;
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
