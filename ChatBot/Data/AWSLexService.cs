using Amazon.CognitoIdentity;
using Amazon.Lex;
using Amazon.Lex.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Data
{
    public class AWSLexService : IAWSLexService
    {
        private readonly AWSOptions _awsOptions;
        private Dictionary<string, string> _lexSessionAttribs;
        private CognitoAWSCredentials awsCredentials;
        private AmazonLexClient awsLexClient;
        private bool disposedValue = false; // To detect redundant calls

        public AWSLexService(IOptions<AWSOptions> awsOptions)
        {
            _awsOptions = awsOptions.Value;

            InitLexService();
        }

        protected void InitLexService()
        {
            Amazon.RegionEndpoint svcRegionEndpoint;

            //Grab region for Lex Bot services
            svcRegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_awsOptions.BotRegion);

            //Get credentials from Cognito
            awsCredentials = new CognitoAWSCredentials(
                                _awsOptions.CognitoPoolID, // Identity pool ID
                                svcRegionEndpoint); // Region

            //Instantiate Lex Client with Region
            awsLexClient = new AmazonLexClient(awsCredentials, svcRegionEndpoint);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    awsLexClient.Dispose();
                    awsCredentials.ClearCredentials();
                }

                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }

        public string PostContentToLex(string messageToSend)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> SendAudioMsgToLex(Stream audioToSend)
        {
            throw new NotImplementedException();
        }

        public async Task<PostTextResponse> SendTextMsgToLex(string messageToSend, Dictionary<string, string> lexSessionAttributes, string sessionId)
        {
            _lexSessionAttribs = lexSessionAttributes;
            return await SendTextMsgToLex(messageToSend, sessionId);
        }

        public async Task<PostTextResponse> SendTextMsgToLex(string messageToSend, string sessionId)
        {
            PostTextResponse lexTextResponse;
            PostTextRequest lexTextRequest = new PostTextRequest()
            {
                BotAlias = _awsOptions.LexBotAlias,
                BotName = _awsOptions.LexBotName,
                UserId = sessionId,
                InputText = messageToSend,
                SessionAttributes = _lexSessionAttribs
            };

            try
            {
                lexTextResponse = await awsLexClient.PostTextAsync(lexTextRequest);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex);
            }

            return lexTextResponse;
        }
    }
}
