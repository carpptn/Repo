using System.ServiceModel.Channels;

namespace PUConnector.Notifications.Service
{
    /// <summary>
    /// Implementuje dla wiązania typu WebHttpBinding funkcjonalność mapowania typu mime treści żądania na typ WebContentFormat
    /// </summary>
    public class WebContentTypeMapperExt : WebContentTypeMapper
    {
        // public methods

        /// <summary>
        /// Mapuje typ mime treści żądania na typ WebContentFormat
        /// </summary>
        /// <param name="contentType">Typ mime</param>
        /// <returns>WebContentFormat.Raw</returns>
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            return WebContentFormat.Raw;
        }
    }
}
