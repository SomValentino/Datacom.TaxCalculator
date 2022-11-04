using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Data
{
    public class CsvDataContext : IDataContext
    {
        private readonly IValidatorManager _validatorManager;
        private readonly ILogger<CsvDataContext> _logger;

        public CsvDataContext(IValidatorManager validatorManager, ILogger<CsvDataContext> logger)
        {
            _validatorManager = validatorManager;
            _logger = logger;
        }
        public async Task<CsvEntryData> Read(string source)
        {
            try
            {
                using var reader = new StreamReader(source);

                var numline = 1;

                var csvEntryData = new CsvEntryData();  

                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                        {
                            throw new Exception($"Csv entry cannot be empty or whitespace at line: {numline}");
                        }
                        var values = line.Split(',');

                        UserTax userTax = _validatorManager.Validate(values, numline);

                        csvEntryData.UserTaxes.Add(userTax);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        csvEntryData.ErrorMessages.Add(ex.Message);
                        
                    }
                    numline++;
                }

                return csvEntryData;

            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public async Task Write(IEnumerable<UserTax> data, string destination)
        {
            try
            {
                using var writer = new StreamWriter(destination);

                foreach (var userTax in data)
                {
                    var dataString = new List<string>();

                    dataString.Add($"{userTax.FirstName} {userTax.LastName}");
                    dataString.Add(userTax.PayPeriod);
                    dataString.Add(userTax.GrossIncome.ToString());
                    dataString.Add(userTax.IncomeTax.ToString());
                    dataString.Add(userTax.NetIncome.ToString());
                    dataString.Add(userTax.SuperAmount.ToString());

                    await writer.WriteLineAsync(string.Join(",", dataString));
                    await writer.FlushAsync();
                }
            }
            catch(Exception)
            {
                throw;
            }
            
            
        }
    }
}
