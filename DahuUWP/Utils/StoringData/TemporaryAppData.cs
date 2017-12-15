using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DahuUWP.Utils.StoringData
{
    class TemporaryAppData
    {
        private Windows.Storage.StorageFolder temporaryFolder = ApplicationData.Current.TemporaryFolder;

        /// <summary>
        /// Create a file in temporary data if not exist and write the content (replace content if not null)
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="content"></param>
        public async void Write(string FileName, string content)
        {
            Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter =
            new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");

            StorageFile sampleFile = await temporaryFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, content);
        }

        /// <summary>
        /// Read a file in temporary data en return his content
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public async Task<string> Read(string FileName)
        {
            try
            {
                StorageFile sampleFile = await temporaryFolder.GetFileAsync(FileName);
                String fileValue = await FileIO.ReadTextAsync(sampleFile);
                return fileValue;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                return null;
            }
        }
    }
}
