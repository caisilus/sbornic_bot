using System;
using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public class VkBotApi: IBotApi
    {
        private VkApi _vkApi;
        private static string server; //for vk api (is used for every query)
        private static string key; //for vk api (is used for every query)
        private string ts; //for vk api (is used for every query), changes for every query 
        private int wait;
        public VkBotApi(string token, ulong groupId)
        {
            _vkApi = new VkApi();
            _vkApi.Authorize(new ApiAuthParams{AccessToken = token});
            var response = _vkApi.Groups.GetLongPollServer(groupId);
            server = response.Server;
            key = response.Key;
            ts = response.Ts;
            wait = 25;
        }
        public IEnumerable<Message> NewMessages()
        {
            var events_response = _vkApi.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams()
            {
                Key = key, Server = server, Ts = ts, Wait = wait
            });
            ts = events_response.Ts;
            IEnumerable<Message> messages = events_response.Updates
                .Where(u => u.Type == GroupUpdateType.MessageNew)
                .Select(u => u.MessageNew.Message);
            return messages;
        }

        public void SendMessage(Message message)
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                PeerId = message.PeerId,
                Message = message.Text,
                RandomId = new DateTime().Millisecond
            });
        }
    }
}