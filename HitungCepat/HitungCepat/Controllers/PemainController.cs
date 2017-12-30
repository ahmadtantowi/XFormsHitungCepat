using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace HitungCepat.Controllers
{
    public class PemainController
    {
        private IMobileServiceTable<Models.Pemain> _pemainTable;

        public PemainController()
        {
            var client = new MobileServiceClient(Common.Constants.ApplicationURL);
            _pemainTable = client.GetTable<Models.Pemain>();
        }

        public async Task<ObservableCollection<Models.Pemain>> GetPemainAsync()
        {
            try
            {
                IEnumerable<Models.Pemain> pemains = await _pemainTable.ToEnumerableAsync();
                return new ObservableCollection<Models.Pemain>(pemains);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SavePemainAsync(Models.Pemain pemain)
        {
            if (pemain.Id == null)
            {
                await _pemainTable.InsertAsync(pemain);
            }
            else
            {
                await _pemainTable.UpdateAsync(pemain);
            }
        }
    }
}
